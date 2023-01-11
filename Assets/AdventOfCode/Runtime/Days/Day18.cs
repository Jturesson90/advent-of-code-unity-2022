using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Vector3 = System.Numerics.Vector3;

namespace AdventOfCode.Days
{
    public class Day18
    {
        public string PuzzleA(string input)
        {
            var rows = input.Trim()
                .Split(OnRow)
                .Select(ToVector3Int)
                .ToArray();
            int result = 0;

            foreach (var row in rows)
            {
                int a = 6;

                foreach (var innerRow in rows)
                {
                    var isX = Math.Abs(row.x - innerRow.x) == 1 && row.y == innerRow.y && row.z == innerRow.z;
                    var isY = Math.Abs(row.y - innerRow.y) == 1 && row.x == innerRow.x && row.z == innerRow.z;
                    var isZ = Math.Abs(row.z - innerRow.z) == 1 && row.x == innerRow.x && row.y == innerRow.y;
                    if (isX || isY || isZ)
                    {
                        a--;
                    }

                    if (a <= 0) break;
                }

                result += a;
            }

            return result.ToString();
        }

        private static string OnRow => "\n";

        private static UnityEngine.Vector3Int ToVector3Int(string s)
        {
            var r = s.Split(",");
            return new Vector3Int(int.Parse(r[0]), int.Parse(r[1]), int.Parse(r[2]));
        }

        public class Day18Data
        {
            public Vector3Int[] Cubes;
        }

        public Day18Data Data;

        private Vector3Int[] GetImmediateNeighbors(Vector3Int pos)
        {
            return new[]
            {
                new Vector3Int(pos.x - 1, pos.y, pos.z),
                new Vector3Int(pos.x + 1, pos.y, pos.z),
                new Vector3Int(pos.x, pos.y - 1, pos.z),
                new Vector3Int(pos.x, pos.y + 1, pos.z),
                new Vector3Int(pos.x, pos.y, pos.z - 1),
                new Vector3Int(pos.x, pos.y, pos.z + 1),
            };
        }

        public string PuzzleB(string input)
        {
            var rows = input.Trim()
                .Split(OnRow)
                .Select(ToVector3Int)
                .OrderBy(x => x.x)
                .ThenBy(y => y.y)
                .ThenBy(z => z.z)
                .ToArray();
            var s = new Dictionary<int, int>();

            Data = new Day18Data() { Cubes = rows };
            var minX = rows.Min(a => a.x) - 1;
            var maxX = rows.Max(a => a.x) + 1;
            var minY = rows.Min(a => a.y) - 1;
            var maxY = rows.Max(a => a.y) + 1;
            var minZ = rows.Min(a => a.z) - 1;
            var maxZ = rows.Max(a => a.z) + 1;

            HashSet<Vector3Int>
                airGap = new() { new Vector3Int(minX, minY, minZ) }; //Flood fill the outside of the lavablob with water

            while (true)
            {
                HashSet<Vector3Int> newWater = new();
                foreach (var p in airGap)
                {
                    foreach (var n in GetImmediateNeighbors(p))
                    {
                        if (rows.Contains(n)) continue;
                        int x = n.x;
                        int y = n.y;
                        int z = n.z;
                        if (minX <= x && x <= maxX && minY <= y && y <= maxY && minZ <= z &&
                            z <= maxZ) //-2 and 25 are outside the bounds of my input so filling the cuboid around the lava with water 
                        {
                            newWater.Add(n);
                        }
                    }
                }

                if (newWater.IsSubsetOf(airGap))
                    break; //No new water was found. which means we've filled the entire cuboid
                airGap.UnionWith(newWater); //Add new water to existing water
            }

            int exposed = 0;

            foreach (var blob in rows)
            {
                foreach (var n in GetImmediateNeighbors(blob))
                {
                    if (airGap.Contains(n)) exposed++;
                }
            }

            return exposed.ToString();
            return input.ToString();
        }
    }
}