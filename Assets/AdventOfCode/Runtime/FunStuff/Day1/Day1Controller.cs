using System;
using System.Collections;
using System.Collections.Generic;
using AdventOfCode.Days;
using JTuresson.AdventOfCode;
using JTuresson.AdventOfCode.AOCClient;
using NUnit.Framework;
using UnityEngine;

public class Day1Controller : MonoBehaviour
{
    [SerializeField] private GameObject elfPrefab;
    [SerializeField] private GameObject package;
    [SerializeField] private GameObject parent;
    [SerializeField] private float maxHeight = 1f;
    [SerializeField] private float width = 4f;

    // Start is called before the first frame update
    private Day01.PuzzleACommandBuffer _puzzleABuffer;

    public async void Start()
    {
        var day = new Day01();
        var client = new AdventOfCodeClient(AdventOfCodeSettings.Instance, AdventOfCodeSettings.Instance.GetCache());
        var input = await client.LoadDayInput(1);
        day.PuzzleA(input);
        _puzzleABuffer = day.PuzzleABuffer;

        var list = new List<(long Calories, GameObject Go)>();
        for (int i = 0; i < _puzzleABuffer.Calories.Count; i++)
        {
            var c = _puzzleABuffer.Calories[i];
            var go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            list.Add((c, go));
            go.transform.parent = parent.transform;
            //  go.transform.localScale = new Vector3(1, c / heightScale, 1);
            //     go.transform.position = new Vector3(i, go.transform.localScale.y * 0.5f, 0);
            if (i == _puzzleABuffer.CorrectIndex)
            {
                var a = go.GetComponent<Renderer>();
                a.material.color = Color.magenta;
            }
        }

        var s = maxHeight / day.PuzzleABuffer.Calories[day.PuzzleABuffer.CorrectIndex];
        var size = width / day.PuzzleABuffer.Calories.Count;
        var left = (day.PuzzleABuffer.Calories.Count * size) * -0.5f;
        int id = 0;
        foreach (var l in list)
        {
            l.Go.transform.localScale = new Vector3(size, s * l.Calories, size);
            l.Go.transform.localPosition = new Vector3(left + (size * id), l.Go.transform.localScale.y * 0.5f, 0);
            id++;
        }
    }
}