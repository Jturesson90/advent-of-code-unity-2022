using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JTuresson.AdventOfCode;
using UnityEngine;

namespace AdventOfCode.Days
{
    public class Day12
    {
        private const string PriorityList = "SabcdefghijklmnopqrstuvwxyzE";

        public class Day12Node
        {
            public string Character;
            public int TentativeDistance { get; set; }
            public int Weight { get; private set; }
            public readonly List<Day12Node> ConnectedNodes;
            public int Row { get; private set; }
            public int Col { get; private set; }
            public bool Visited { get; set; }
            public int Height { get; private set; }
            public Day12Node PathNode { get; set; }

            public Day12Node(string c, int row, int col)
            {
                TentativeDistance = int.MaxValue;
                Visited = false;
                PathNode = null;
                Row = row;
                Col = col;
                ConnectedNodes = new List<Day12Node>();
                Character = c;
                Height = c switch
                {
                    "S" => 0,
                    "E" => PriorityList[^1],
                    _ => PriorityList.IndexOf(c, StringComparison.Ordinal)
                };
                Weight = 1; // PriorityList.IndexOf(c, StringComparison.Ordinal);
            }

            private bool CanConnectToNode(Day12Node node)
            {
                if (node.ConnectedNodes.Contains(node)) throw new Exception("Already contains node");
                if (node == this) throw new Exception("Why do you try to connect the node to itself?");
                var x = PriorityList.IndexOf(node.Character, StringComparison.Ordinal);
                var y = PriorityList.IndexOf(Character, StringComparison.Ordinal);
                return x - y <= 1;
            }

            private static bool IsInside(int col, int row, int cols, int rows) =>
                col >= 0 && row >= 0 && col < cols && row < rows;

            private readonly Vector2Int[] _directions =
            {
                new(1, 0),
                new(-1, 0),
                new(0, 1),
                new(0, -1)
            };

            public void ConnectNodes(Day12Node[,] nodes, int row, int col)
            {
                var rows = nodes.GetLength(0);
                var cols = nodes.GetLength(1);

                foreach (var direction in _directions)
                {
                    var checkCol = direction.y + col;
                    var checkRow = direction.x + row;
                    if (!IsInside(checkCol, checkRow, cols, rows)) continue;
                    var node = nodes[checkRow, checkCol];
                    if (CanConnectToNode(node))
                    {
                        ConnectedNodes.Add(node);
                    }
                }
            }
        }

        public PuzzleState State { get; private set; }

        public class PuzzleState
        {
            public Day12Node[,] Nodes;
            public Day12Node EndNode;
            public Day12Node StartNode;
        }

        public string PuzzleA(string input)
        {
            State = new PuzzleState();

            var parsedInput = ParseInput.ParseAsMultiArray(input.Trim(), "\n", "");
            var nodes = GetNodes(parsedInput);
            State.Nodes = nodes;
            var unvisitedSet = new HashSet<Day12Node>();
            var nodesList = new List<Day12Node>();
            foreach (var node in nodes)
            {
                unvisitedSet.Add(node);
                nodesList.Add(node);
            }

            var initialNode = nodesList.First(a => a.Character == "S");
            var goalNode = nodesList.First(a => a.Character == "E");
            initialNode.TentativeDistance = 0;
            var currentNode = initialNode;
            while (unvisitedSet.Count > 0)
            {
                var connectedNodes = currentNode.ConnectedNodes.OrderBy(a => a.TentativeDistance);
                foreach (var connectedNode in connectedNodes)
                {
                    if (connectedNode.Visited) continue;

                    if (currentNode.TentativeDistance + connectedNode.Weight < connectedNode.TentativeDistance)
                    {
                        connectedNode.PathNode = currentNode;
                        connectedNode.TentativeDistance = currentNode.TentativeDistance + connectedNode.Weight;
                    }
                }

                currentNode.Visited = true;
                unvisitedSet.Remove(currentNode);
                if (unvisitedSet.Count > 0)
                {
                    unvisitedSet = unvisitedSet.OrderBy(a => a.TentativeDistance).ToHashSet();
                    var first = unvisitedSet.First();
                    if (first.TentativeDistance != int.MaxValue)
                    {
                        currentNode = first;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            State.EndNode = goalNode;
            State.StartNode = initialNode;
            return goalNode.TentativeDistance.ToString();
        }

        private Day12Node[,] GetNodes(string[,] parsedInput)
        {
            var rows = parsedInput.GetLength(0);
            var cols = parsedInput.GetLength(1);
            var nodes = new Day12Node[rows, cols];
            for (var row = 0; row < rows; row++)
            {
                for (var col = 0; col < cols; col++)
                {
                    nodes[row, col] = new Day12Node(parsedInput[row, col], row, col);
                }
            }

            rows = nodes.GetLength(0);
            cols = nodes.GetLength(1);
            for (var row = 0; row < rows; row++)
            {
                for (var col = 0; col < cols; col++)
                {
                    nodes[row, col].ConnectNodes(nodes, row, col);
                }
            }

            return nodes;
        }

        public string PuzzleB(string input)
        {
            return input;
        }
    }
}