using System;
using System.Collections;
using System.Collections.Generic;
using AdventOfCode.Days;
using JTuresson.AdventOfCode.AOCClient;
using UnityEngine;
using UnityEngine.Serialization;

public class Day17Controller : MonoBehaviour
{
    [SerializeField] private bool useRealData;

    [SerializeField] private Day17Shape[] rocks;

    private async void Awake()
    {
        string input;
        if (useRealData)
        {
            var client =
                new AdventOfCodeClient(AdventOfCodeSettings.Instance, AdventOfCodeSettings.Instance.GetCache());
            input = await client.LoadDayInput(17);
        }
        else
        {
            input = ">>><<><>><<<>><>>><<<>>><<<><<<>><>><<>>";
        }

        var day = new Day17();
        // day.PuzzleB("498,4 -> 498,6 -> 496,6\n503,4 -> 502,4 -> 502,9 -> 494,9");
        day.PuzzleA(input);
        // day.PuzzleA("Sabqponm\nabcryxxl\naccszExk\nacctuvwj\nabdefghi");
        //var result = day.PuzzleB(input);

        StartCoroutine(StartWithCommands(day.Data));
        //_wait = new WaitForSeconds(0.01f);
    }

    private IEnumerator StartWithCommands(Day17.Day17Data dayData)
    {
        yield return null;

        var dirLen = dayData.Direction.Length;
        var dirIndex = 0;
        var rocksLen = rocks.Length;
        int groundY = 0;
        int x = 2;
        int yAdd = 3;
        bool f = true;
        Day17Shape prevRock = null;
        for (int i = 0; i < 3; i++)
        {
            var rock = Instantiate(rocks[i % rocksLen], new Vector3(x, groundY + yAdd, 0), Quaternion.identity,
                transform);
            bool falling = true;
            while (falling)
            {
                var y = transform.position.y;
                var pos = rock.transform.position;
                var xPos = pos;
                var newPos = rock.transform.position + (dayData.Direction[dirIndex++ % dirLen] ==
                                                        Day17.Day17Direction.Left
                    ? Vector3.left
                    : Vector3.right);
                newPos.x = x < 0 ? 0 : x + rock.Width > 7 ? 7 - rock.Width : newPos.x;
                rock.transform.position = newPos;
                rock.transform.position += Vector3.down;
                yield return new WaitForSeconds(0.2f);

                if (rock.transform.position.y - y < float.Epsilon)
                {
                    falling = false;
                    if (f)
                    {
                        f = false;
                        groundY = rock.Top;
                    }
                    else
                    {
                        groundY = Math.Max(prevRock.Top, rock.Top);
                    }

                    prevRock = rock;
                }
            }
            // Drop
            // var dir = dayData.Direction[i % dayData.Direction.Length];
        }
    }
}