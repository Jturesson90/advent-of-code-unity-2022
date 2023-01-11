using System.Collections;
using System.Collections.Generic;
using AdventOfCode.Days;
using JTuresson.AdventOfCode.AOCClient;
using UnityEngine;

public class Day14Controller : MonoBehaviour
{
    [SerializeField] private GameObject sand;
    [SerializeField] private GameObject stone;
    private WaitForSeconds _wait;

    async void Awake()
    {
        var client = new AdventOfCodeClient(AdventOfCodeSettings.Instance, AdventOfCodeSettings.Instance.GetCache());
        var input = await client.LoadDayInput(14);
        var day = new Day14();
        // day.PuzzleB("498,4 -> 498,6 -> 496,6\n503,4 -> 502,4 -> 502,9 -> 494,9");
        day.PuzzleB(input);
        // day.PuzzleA("Sabqponm\nabcryxxl\naccszExk\nacctuvwj\nabdefghi");
        //var result = day.PuzzleB(input);

        StartCoroutine(StartWithCommands(day.Before));
        _wait = new WaitForSeconds(0.01f);
    }

    private IEnumerator StartWithCommands(Day14.Day14Data data)
    {
        int aaa = 0;
        var h = data.Grid.GetLength(0);
        for (int y = 0; y < h; y++)
        {
            for (int x = 0; x < data.Grid.GetLength(1); x++)
            {
                char c = data.Grid[y, x];
                switch (c)
                {
                    case 'o':
                        var a = Instantiate(sand, transform);
                        a.transform.localPosition = new Vector3(x, h - y, 0);
                        aaa++;
                        break;
                    case '#':
                        var d = Instantiate(stone, transform);
                        d.transform.localPosition = new Vector3(x, h - y, 0);
                        break;
                    default: break;
                }
            }
        }

        Debug.Log(aaa);
        yield break;
    }
}