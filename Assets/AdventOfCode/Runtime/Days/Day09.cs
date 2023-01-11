using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JTuresson.AdventOfCode;
using NUnit.Framework;
using UnityEngine;

namespace AdventOfCode.Days
{
    public class Day09
    {
        public struct Day09Command
        {
            public Vector2Int Direction;
            public int Steps;
        }


        public class Day09Actor
        {
            private readonly HashSet<Vector2Int> _visitedPositions;
            public Vector2Int CurrentPosition;
            public Day09Actor Child;

            public Day09Actor(Vector2Int startPos)
            {
                CurrentPosition = startPos;
                _visitedPositions = new HashSet<Vector2Int>() {startPos};
            }

            public void Move(Vector2Int direction)
            {
                var newPos = CurrentPosition + direction;
                CurrentPosition = newPos;
                _visitedPositions.Add(CurrentPosition);
                Child?.UpdateChild(CurrentPosition);
            }

/*
 *
.....    .....    .....
.....    ..H..    ..H..
..H.. -> ..... -> ..T..
.T...    .T...    .....
.....    .....    .....

.....    .....    .....
.....    .....    .....
..H.. -> ...H. -> ..TH.
.T...    .T...    .....
.....    .....    .....
 * 
 */
            protected void UpdateChild(Vector2Int parentPos)
            {
                var tempPos = CurrentPosition;
                var yDiff = parentPos.y - CurrentPosition.y;
                var xDiff = parentPos.x - CurrentPosition.x;
                if (Math.Abs(yDiff) <= 1 && Math.Abs(xDiff) <= 1) return;
                if (xDiff == 0)
                {
                    if (yDiff > 1)
                    {
                        tempPos.y += yDiff - 1;
                    }
                    else
                        if (yDiff < -1)
                        {
                            tempPos.y += yDiff + 1;
                        }
                }
                else
                    if (yDiff == 0)
                    {
                        if (xDiff > 1)
                        {
                            tempPos.x += xDiff - 1;
                        }
                        else
                            if (xDiff < -1)
                            {
                                tempPos.x += xDiff + 1;
                            }
                    }
                    else
                        if (Math.Abs(yDiff) == 2 && Math.Abs(xDiff) == 1)
                        {
                            if (yDiff > 0)
                            {
                                tempPos.y += yDiff - 1;
                            }
                            else
                            {
                                tempPos.y += yDiff + 1;
                            }

                            tempPos.x = parentPos.x;
                        }
                        else
                            if (Math.Abs(xDiff) == 2 && Math.Abs(yDiff) == 1)
                            {
                                if (xDiff > 0)
                                {
                                    tempPos.x += xDiff - 1;
                                }
                                else
                                {
                                    tempPos.x += xDiff + 1;
                                }

                                tempPos.y = parentPos.y;
                            }
                            else
                                if (Math.Abs(xDiff) == 2 && Math.Abs(yDiff) == 2)
                                {
                                    if (xDiff > 0)
                                    {
                                        tempPos.x += xDiff - 1;
                                    }
                                    else
                                    {
                                        tempPos.x += xDiff + 1;
                                    }

                                    if (yDiff > 0)
                                    {
                                        tempPos.y += yDiff - 1;
                                    }
                                    else
                                    {
                                        tempPos.y += yDiff + 1;
                                    }
                                }
                                else
                                {
                                    throw new Exception("What?");
                                }

                CurrentPosition = tempPos;
                _visitedPositions.Add(CurrentPosition);

                Child?.UpdateChild(CurrentPosition);
            }

            public List<Vector2Int> GetVisitedPositions()
            {
                return _visitedPositions.ToList();
            }
        }

        private static List<Day09Command> ParseCommands(IEnumerable<string> parsedInput)
        {
            return parsedInput.Select(inp => inp.Split(" "))
                .Select(s => new Day09Command
                {
                    Direction = s[0] switch
                    {
                        "R" => new Vector2Int(1, 0),
                        "U" => new Vector2Int(0, 1),
                        "L" => new Vector2Int(-1, 0),
                        "D" => new Vector2Int(0, -1),
                        _ => throw new Exception("Failed to parse direction")
                    },
                    Steps = int.Parse(s[1])
                })
                .ToList();
        }

        public string PuzzleA(string input)
        {
            var parsedInput = ParseInput.ParseAsArray(input.Trim());
            List<Day09Command> commands = ParseCommands(parsedInput);
            var head = new Day09Actor(Vector2Int.zero);
            var tail = new Day09Actor(Vector2Int.zero);
            head.Child = tail;

            foreach (var command in commands)
            {
                for (int i = 0; i < command.Steps; i++)
                {
                    head.Move(command.Direction);
                }
            }

            return tail.GetVisitedPositions().Count.ToString();
        }

        public string PuzzleB(string input)
        {
            var parsedInput = ParseInput.ParseAsArray(input.Trim());
            List<Day09Command> commands = ParseCommands(parsedInput);
            var head = new Day09Actor(Vector2Int.zero);
            var current = head;
            for (int i = 0; i < 9; i++)
            {
                var t = new Day09Actor(Vector2Int.zero);
                current.Child = t;
                current = t;
            }

            foreach (var command in commands)
            {
                for (int i = 0; i < command.Steps; i++)
                {
                    head.Move(command.Direction);
                }
            }

            return current.GetVisitedPositions().Count.ToString();
        }
    }
}