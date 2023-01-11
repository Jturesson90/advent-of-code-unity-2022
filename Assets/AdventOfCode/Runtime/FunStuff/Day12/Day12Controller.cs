using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode.Days;
using JTuresson.AdventOfCode.AOCClient;
using UnityEngine;


public class Day12Controller : MonoBehaviour
{
    [SerializeField] private float delay = 0.1f;

    [SerializeField] private float scaleY = 0.1f;
    [SerializeField] private Day12Cube dirt;
    private WaitForSeconds _wait;
    private Dictionary<Day12.Day12Node, Day12Cube> _nodeByGameObject;

    async void Awake()
    {
        _nodeByGameObject = new Dictionary<Day12.Day12Node, Day12Cube>();
        var client = new AdventOfCodeClient(AdventOfCodeSettings.Instance, AdventOfCodeSettings.Instance.GetCache());
        var input = await client.LoadDayInput(12);
        var day = new Day12();
        day.PuzzleA(input);
        // day.PuzzleA("Sabqponm\nabcryxxl\naccszExk\nacctuvwj\nabdefghi");
        //var result = day.PuzzleB(input);

        StartCoroutine(StartWithCommands(day.State));
        _wait = new WaitForSeconds(delay);
    }


    private IEnumerator StartWithCommands(Day12.PuzzleState result)
    {
        foreach (var node in result.Nodes)
        {
            var go = Instantiate(dirt, new Vector3(node.Col, 0, -node.Row), Quaternion.identity, transform);
            go.Initialized(node);
            var nodeTransform = go.transform;
            var localScale = nodeTransform.localScale;
            var scale = localScale;
            scale.y = node.Height * scaleY;
            localScale = scale;
            nodeTransform.localScale = localScale;
            var pos = nodeTransform.localPosition;
            pos.y = localScale.y * 0.5f;
            nodeTransform.localPosition = pos;
            _nodeByGameObject.Add(node, go);
        }

        var c = result.EndNode.PathNode;
        while (c != null)
        {
            yield return _wait;
            var newNode = _nodeByGameObject[c];
            newNode.HighLight();
            c = c.PathNode;
        }

        yield return _wait;
    }
}