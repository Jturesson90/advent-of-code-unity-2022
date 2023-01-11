using System.Collections;
using System.Collections.Generic;
using AdventOfCode.Days;
using JTuresson.AdventOfCode.AOCClient;
using UnityEngine;

public class Day08Controller : MonoBehaviour
{
    [SerializeField] private Day08TreeBehaviour treePrefab;

    [SerializeField] private float spaceBetweenTrees;

    async void Awake()
    {
        var client = new AdventOfCodeClient(AdventOfCodeSettings.Instance, AdventOfCodeSettings.Instance.GetCache());
        var input = await client.LoadDayInput(8);
        var day = new Day08();
        day.Init(input);
        day.PuzzleA(input);
        day.PuzzleB(input);

        StartWithCommands(day.Data);
    }

    private void StartWithCommands(Day08.Day08Data puzzleACommandsBuffer)
    {
        foreach (var tree in puzzleACommandsBuffer.Trees)
        {
            var treeB = Instantiate(treePrefab, transform);
            treeB.transform.localPosition = new Vector3(tree.Position.x * spaceBetweenTrees, 0, tree.Position.y * spaceBetweenTrees);
            treeB.Initialize(tree);
        }
    }
}