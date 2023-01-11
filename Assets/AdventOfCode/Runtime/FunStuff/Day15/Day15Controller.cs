using System;
using System.Collections;
using System.Collections.Generic;
using AdventOfCode.Days;
using JTuresson.AdventOfCode.AOCClient;
using UnityEngine;

public class Day15Controller : MonoBehaviour
{
    [SerializeField] private GameObject empty;
    [SerializeField] private GameObject sensor;

    [SerializeField] private GameObject beacon;

    private async void Awake()
    {
        var client = new AdventOfCodeClient(AdventOfCodeSettings.Instance, AdventOfCodeSettings.Instance.GetCache());
        var input = await client.LoadDayInput(15);
        var day = new Day15();
        input =
            "Sensor at x=2, y=18: closest beacon is at x=-2, y=15\nSensor at x=9, y=16: closest beacon is at x=10, y=16\nSensor at x=13, y=2: closest beacon is at x=15, y=3\nSensor at x=12, y=14: closest beacon is at x=10, y=16\nSensor at x=10, y=20: closest beacon is at x=10, y=16\nSensor at x=14, y=17: closest beacon is at x=10, y=16\nSensor at x=8, y=7: closest beacon is at x=2, y=10\nSensor at x=2, y=0: closest beacon is at x=2, y=10\nSensor at x=0, y=11: closest beacon is at x=2, y=10\nSensor at x=20, y=14: closest beacon is at x=25, y=17\nSensor at x=17, y=20: closest beacon is at x=21, y=22\nSensor at x=16, y=7: closest beacon is at x=15, y=3\nSensor at x=14, y=3: closest beacon is at x=15, y=3\nSensor at x=20, y=1: closest beacon is at x=15, y=3";
        // day.PuzzleB("498,4 -> 498,6 -> 496,6\n503,4 -> 502,4 -> 502,9 -> 494,9");
        day.PuzzleA(input, 10);
        // day.PuzzleA("Sabqponm\nabcryxxl\naccszExk\nacctuvwj\nabdefghi");
        //var result = day.PuzzleB(input);

        StartCoroutine(StartWithCommands(day.Data));
        //_wait = new WaitForSeconds(0.01f);
    }

    private IEnumerator StartWithCommands(Day15.Day15Data data)
    {
        foreach (var (pos, c) in data.PosByChar)
        {
            var prefab = c switch
            {
                '#' => empty,
                'S' => sensor,
                'B' => beacon,
                _ => throw new Exception("Wrong char " + c)
            };

            var a = Instantiate(prefab, transform);
            a.transform.position = new Vector3(pos.x, pos.y, 0);
        }

        yield break;
    }
}