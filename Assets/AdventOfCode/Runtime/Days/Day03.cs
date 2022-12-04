using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JTuresson.AdventOfCode;
using UnityEngine;

namespace AdventOfCode.Days
{
    public class Day03
    {
        private Dictionary<string, int> _textByPriority = new Dictionary<string, int>()
        {
            {"a", 1},
            {"b", 2},
            {"c", 3},
            {"d", 4},
            {"e", 5},
            {"f", 6},
            {"g", 7},
            {"h", 8},
            {"i", 9},
            {"j", 10},
            {"k", 11},
            {"l", 12},
            {"m", 13},
            {"n", 14},
            {"o", 15},
            {"p", 16},
            {"q", 17},
            {"r", 18},
            {"s", 19},
            {"t", 20},
            {"u", 21},
            {"v", 22},
            {"w", 23},
            {"x", 24},
            {"y", 25},
            {"z", 26},
            {"A", 27},
            {"B", 28},
            {"C", 29},
            {"D", 30},
            {"E", 31},
            {"F", 32},
            {"G", 33},
            {"H", 34},
            {"I", 35},
            {"J", 36},
            {"K", 37},
            {"L", 38},
            {"M", 39},
            {"N", 40},
            {"O", 41},
            {"P", 42},
            {"Q", 43},
            {"R", 44},
            {"S", 45},
            {"T", 46},
            {"U", 47},
            {"V", 48},
            {"W", 49},
            {"X", 50},
            {"Y", 51},
            {"Z", 52},
        };

        public string PuzzleA(string input)
        {
            var rows = ParseInput.ParseAsArray(input);
            var pri = new List<string>();
            foreach (var row in rows)
            {
                string leftSack = string.Join("", row.Take(row.Length / 2));
                string rightSack = string.Join("", row.Skip(row.Length / 2));
                pri.AddRange(leftSack.Intersect(rightSack).Select(a => a.ToString()));
                //    var a = leftSack.Except(rightSack).Union(rightSack.Except(leftSack));
                //  pri.AddRange(a.Select(b => b.ToString()));
            }

            return pri.Aggregate(0, (agg, curr) =>
            {
                if (_textByPriority.ContainsKey(curr))
                {
                    return agg + _textByPriority[curr];
                }

                Debug.LogError(curr + " not found");
                return agg;
            }).ToString();
        }

        public string PuzzleB(string input)
        {
            var rows = ParseInput.ParseAsArray(input.Trim());
            var pri = new List<char>();
            for (int i = 0; i < rows.Length; i += 3)
            {
                var h = new HashSet<char>();
                var x = rows[i].ToCharArray();
                var y = rows[i + 1].ToCharArray();
                var z = rows[i + 2].ToCharArray();
                foreach (var c in x.Where(curr => y.Contains(curr) && z.Contains(curr)))
                {
                    h.Add(c);
                }

                pri.AddRange(h);
            }

            return pri.Aggregate(0, (agg, curr) =>
            {
                if (_textByPriority.ContainsKey(curr.ToString()))
                {
                    return agg + _textByPriority[curr.ToString()];
                }

                Debug.LogError(curr + " not found");
                return agg;
            }).ToString();
        }
    }
}