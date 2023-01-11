using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AdventOfCode.Days
{
    public class Day17
    {
        private string s = @"####

.#.
###
.#.

..#
..#
###

#
#
#
#

##
##";

        public Shape17[] Shapes = new Shape17[]
        {
            new Shape17() // 4x1
            {
                LeftCheck = new Vector2Int[] { new(0, 0) },
                RightCheck = new Vector2Int[] { new(3, 0) },
                DownCheck = new Vector2Int[] { new(0, 0), new(1, 0), new(2, 0), new(3, 0) },
                UpChecks = new Vector2Int[] { new(0, 0), new(1, 0), new(2, 0), new(3, 0) }
            },
            new Shape17() // XX
            {
                LeftCheck = new Vector2Int[] { new(1, 0), new(0, 1), new(1, 2) },
                RightCheck = new Vector2Int[] { new(1, 0), new(2, 1), new(1, 2) },
                DownCheck = new Vector2Int[] { new(0, 1), new(1, 0), new(2, 1) },
                UpChecks = new Vector2Int[] { new(0, 1), new(1, 2), new(2, 1) }
            },
            new Shape17() // _I
            {
                LeftCheck = new Vector2Int[] { new(0, 0), new(2, 1), new(2, 2) },
                RightCheck = new Vector2Int[] { new(2, 0), new(2, 1), new(2, 2) },
                DownCheck = new Vector2Int[] { new(0, 0), new(1, 0), new(2, 0) },
                UpChecks = new Vector2Int[] { new(0, 0), new(1, 0), new(2, 2) }
            },
            new() // 1x4
            {
                LeftCheck = new Vector2Int[] { new(0, 0), new(0, 1), new(0, 2), new(0, 3) },
                RightCheck = new Vector2Int[] { new(0, 0), new(0, 1), new(0, 2), new(0, 3) },
                DownCheck = new Vector2Int[] { new(0, 0) }, UpChecks = new Vector2Int[] { new(0, 3) }
            },
            new() // 2x2
            {
                LeftCheck = new Vector2Int[] { new(0, 0), new(0, 1) },
                RightCheck = new Vector2Int[] { new(1, 0), new(1, 2) },
                DownCheck = new Vector2Int[] { new(0, 0), new(1, 0) },
                UpChecks = new Vector2Int[] { new(0, 1), new(1, 1) }
            }
        };

        public class Shape17
        {
            public Vector2Int[] LeftCheck;
            public Vector2Int[] RightCheck;
            public Vector2Int[] DownCheck;
            public Vector2Int[] UpChecks;

            public Shape17()
            {
            }

            public Shape17(string parse)
            {
                var rows = parse.Split('\n').Reverse().ToArray();
                List<Vector2Int> rrr = new List<Vector2Int>();
                for (int i = 0; i < rows.Length; i++)
                {
                    var row = rows[i];
                    for (int j = 0; j < row.Length; j++)
                    {
                        if (row[j] == '#')
                        {
                            rrr.Add(new Vector2Int(j, i));
                        }
                    }
                }

                var le = new List<Vector2Int>();
            }
        }

        public enum Day17Direction
        {
            Left,
            Right
        }

        public Day17Data Data { get; private set; }

        public class Day17Data
        {
            public int DropCount;
            public int Width;
            public Day17Direction[] Direction;
        }

        public string PuzzleA(string input)
        {
            const int dropNum = 2022;
            const int width = 7;
            var dirs = input.ToArray();
            var dirLen = dirs.Length;
            var dirIndex = 0;
            var y = 0;
            var leftChecks = new HashSet<Vector2Int>();
            var rightChecks = new HashSet<Vector2Int>();
            var upChecks = new HashSet<Vector2Int>();
            for (int i = 0; i <= width; i++)
            {
                upChecks.Add(new Vector2Int(i, 0));
            }

            var ground = 0;

            for (int i = 0; i < dropNum; i++)
            {
                var shape = Shapes[i % Shapes.Length];
                var currentPos = new Vector2Int(2, upChecks.Max(a => a.y) + 3);
                var dir = dirs[dirIndex++ % dirLen];
                switch (dir)
                {
                    case '>':
                        var checkPos = currentPos;
                        checkPos.x += 1;
                        if (shape.RightCheck.Any(a => a.x + checkPos.x >= 7))
                        {
                        }
                        else if (shape.RightCheck.FirstOrDefault(a => leftChecks.Contains(a + checkPos)) != null)
                        {
                        }
                        else
                        {
                            
                        }

                        break;
                    case '<':
                        break;
                    default:
                        throw new Exception("Invalid direction");
                }
            }

            Data = new Day17Data()
            {
                DropCount = dropNum,
                Width = width,
                Direction = dirs.Select(a =>
                    a switch
                    {
                        '>' => Day17Direction.Right,
                        '<' => Day17Direction.Left,
                        _ => throw new Exception("Direction parse failed")
                    }).ToArray()
            };
            return input;
        }

        public string PuzzleB(string input)
        {
            return input;
        }

        public class Rock
        {
            public Rock(string shape)
            {
            }
        }
    }
}