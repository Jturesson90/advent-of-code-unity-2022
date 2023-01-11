using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Days;
using JTuresson.AdventOfCode.AOCClient;
using UnityEngine;
using UnityEngine.UI;

public class Day10Controller : MonoBehaviour
{
    [SerializeField] private Image yesPrefab;
    [SerializeField] private RectTransform parent;
    [SerializeField] private float delay = 0.1f;
    [SerializeField] private float size = 20f;

    [SerializeField] private Color selected;
    [SerializeField] private Color yes;
    [SerializeField] private Color no;

    async void Awake()
    {
        var client = new AdventOfCodeClient(AdventOfCodeSettings.Instance, AdventOfCodeSettings.Instance.GetCache());
        var input = await client.LoadDayInput(10);
        var day = new Day10();
        day.PuzzleA(input);
        var result = day.PuzzleB(input);

        StartCoroutine(StartWithCommands(result));
    }

    private WaitForSeconds _wait;

    private IEnumerator StartWithCommands(string result)
    {
        _wait = new WaitForSeconds(delay);
        var s = result.Split("\n").SelectMany(a => a.Split("")).ToArray();
        for (int y = 0; y < s.Length; y++)
        {
            var yy = s[y];
            for (int x = 0; x < yy.Length; x++)
            {
                Vector3 pos = new Vector3(x * size, -y * size, 0);
                var a = Instantiate(yesPrefab, parent);
                a.rectTransform.anchoredPosition = pos;
                a.color = selected;
                yield return _wait;

                var c = yy[x];
                if (c == '#')
                    a.color = yes;
                else
                    a.color = no;
            }
        }
    }
}