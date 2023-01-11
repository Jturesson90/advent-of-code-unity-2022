using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JTuresson.AdventOfCode;
using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEngine;

namespace AdventOfCode.Days
{
    public class Day15
    {
        public class Day15Data
        {
            public Dictionary<Vector2Int, char> PosByChar;

            public Day15Data(Dictionary<Vector2Int, char> posByChar)
            {
                PosByChar = posByChar;
            }
        }

        public Day15Data Data;
        private int _top, _bottom, _left, _right;

        public string PuzzleB(string input, int ycheck = 2000000)
        {
            var a = GetPositionsOrderedByY(input.Trim());
            var sensorsA = a.Where(v => v.Value.C == 'S');
            var atop = a.First().Key.y;
            var aBottom = a.Last().Key.y;
            var aLeft = sensorsA.Min(v => v.Key.x);
            var aRight = sensorsA.Max(v => v.Key.x);

            var check = new Vector2Int(0, 0);
            var outsideEdges = new HashSet<Vector2Int>();
            // Check Horizontal
            foreach (var aa in sensorsA)
            {
                var top = aa.Key.y - aa.Value.ManhattanDistance + 1;
                var right = aa.Key.x + aa.Value.ManhattanDistance + 1;
                var bottom = aa.Key.y + aa.Value.ManhattanDistance + 1;
                var left = aa.Key.x - aa.Value.ManhattanDistance + 1;
                int y = aa.Key.y;
                for (int x = left; x <= aa.Key.x; x++, y--)
                {
                    if (x > 4000000) break;
                    if (y < 0) break;
                    if (x >= 0 && y >= 0 && x <= 4000000 && y <= 4000000)
                    {
                        if (x <= aRight && x >= aLeft && y >= atop && y <= aBottom)
                            outsideEdges.Add(new Vector2Int(x, y));
                    }
                }

                y = top;
                for (int x = aa.Key.x; x <= right; x++, y++)
                {
                    if (x > 4000000) break;
                    if (y > 4000000) break;
                    if (x >= 0 && y >= 0 && x <= 4000000 && y <= 4000000)
                    {
                        if (x <= aRight && x >= aLeft && y >= atop && y <= aBottom)
                            outsideEdges.Add(new Vector2Int(x, y));
                    }
                }

                y = bottom;
                for (int x = aa.Key.x; x <= right; x++, y--)
                {
                    if (x > 4000000) break;
                    if (y < 0) break;
                    if (x >= 0 && y >= 0 && x <= 4000000 && y <= 4000000)
                    {
                        if (x <= aRight && x >= aLeft && y >= atop && y <= aBottom)
                            outsideEdges.Add(new Vector2Int(x, y));
                    }
                }

                y = left;
                for (int x = left; x <= right; x++, y++)
                {
                    if (x > 4000000) break;
                    if (y > 4000000) break;
                    if (x >= 0 && y >= 0 && x <= 4000000 && y <= 4000000)
                    {
                        if (x <= aRight && x >= aLeft && y >= atop && y <= aBottom)
                            outsideEdges.Add(new Vector2Int(x, y));
                    }
                }
            }

            Vector2Int r = new Vector2Int();
            foreach (var outsideEdge in outsideEdges)
            {
                bool b = false;
                foreach (var s in sensorsA)
                {
                    if (IsInsideDiamond(s.Key, outsideEdge, s.Value.ManhattanDistance))
                    {
                        b = true;
                        break;
                    }
                }

                if (!b)
                {
                    r = outsideEdge;
                    Debug.Log("Found index at  " + r);
                    break;
                }
            }

            long l = r.x;
            long yy = r.y;
            var rr = l * 4000000 + yy;
            return rr.ToString();
        }

        public bool IsInsideDiamond(Vector2Int pos, Vector2Int pos2, int width)
        {
            return Algorithms.ManhattanDistance(pos.x, pos.y, pos2.x, pos2.y) <= width;
        }

        public string PuzzleA(string input, int yCheck = 2000000)
        {
            var a = GetPositionsOrderedByY(input.Trim());

            _top = a.First().Key.y;
            _bottom = a.Last().Key.y;
            var sensorsA = a.Where(v => v.Value.C == 'S');
            _left = sensorsA.Min(v => v.Key.x - v.Value.ManhattanDistance);
            _right = sensorsA.Max(v => v.Key.x + v.Value.ManhattanDistance);
            int result = 0;
            for (int x = _left; x <= _right; x++)
            {
                foreach (var s in sensorsA)
                {
                    if (Algorithms.ManhattanDistance(x, yCheck, s.Key.x, s.Key.y) <= s.Value.ManhattanDistance)
                    {
                        result++;
                        break;
                    }
                }
            }

            return (result - a.Count(b => b.Key.y == yCheck)).ToString();
        }


        public Dictionary<Vector2Int, (char C, Vector2Int BeaconDelta, int ManhattanDistance)> GetPositionsOrderedByY(
            string input)
        {
            //Sensor at x=2, y=18: closest beacon is at x=-2, y=15
            var result = new Dictionary<Vector2Int, (char C, Vector2Int BeaconDelta, int ManhattanDistance)>();
            var rows = input.Split("\n");

            foreach (var row in rows)
            {
                var data = row.Split(": ");
                var sensorPos = data[0].Replace("Sensor at ", "");
                var beaconPos = data[1].Replace("closest beacon is at ", "");
                var sensors = sensorPos.Split(", ");
                var beacons = beaconPos.Split(", ");
                var sx = int.Parse(sensors[0].Split("=")[1]);
                var sy = int.Parse(sensors[1].Split("=")[1]);
                var bx = int.Parse(beacons[0].Split("=")[1]);
                var by = int.Parse(beacons[1].Split("=")[1]);
                var beaconVector = new Vector2Int(bx, by);
                result.Add(new Vector2Int(sx, sy),
                    ('S', new Vector2Int(bx, by), Algorithms.ManhattanDistance(sx, sy, bx, by)));

                if (result.ContainsKey(beaconVector))
                {
                    Debug.Log("Beacon already exists at " + beaconVector);
                }
                else
                {
                    result.Add(beaconVector, ('B', Vector2Int.zero, 0));
                }
            }

            return result
                .OrderBy(a => a.Key.y)
                .ThenBy(a => a.Key.x)
                .ToDictionary(a => a.Key, a => a.Value);
        }
    }
}