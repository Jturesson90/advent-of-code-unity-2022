using System.Collections;
using AdventOfCode.Days;
using JTuresson.AdventOfCode.AOCClient;
using UnityEngine;


public class Day18Controller : MonoBehaviour
{
    [SerializeField] private GameObject obsidian;
    private WaitForSeconds _wait;
    public bool useReal = false;

    async void Awake()
    {
        var client =
            new AdventOfCodeClient(AdventOfCodeSettings.Instance, AdventOfCodeSettings.Instance.GetCache());
        string input = "";
        if (useReal)
        {
            input = await client.LoadDayInput(18);
        }
        else
            input = "2,2,2\n1,2,2\n3,2,2\n2,1,2\n2,3,2\n2,2,1\n2,2,3\n2,2,4\n2,2,6\n1,2,5\n3,2,5\n2,1,5\n2,3,5";

        var day = new Day18();
        day.PuzzleB(input);


        StartCoroutine(StartWithCommands(day.Data));
        _wait = new WaitForSeconds(0.01f);
    }

    private IEnumerator StartWithCommands(Day18.Day18Data data)
    {
        foreach (var VARIABLE in data.Cubes)
        {
            var c = Instantiate(obsidian, transform);
            c.transform.localPosition = VARIABLE;
        }

        yield break;
    }
}