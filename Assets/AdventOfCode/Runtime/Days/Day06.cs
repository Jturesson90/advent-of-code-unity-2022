using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JTuresson.AdventOfCode;

namespace AdventOfCode.Days
{
    public class Day06
    {
        public string PuzzleA(string input)
        {
            var input2 = ParseInput.ParseAsArray(input.Trim());
            int result = 0;
            foreach (var row in input2)
            {
                result += GetFirstMarkerCharacterIndex(row);
            }

            return result.ToString();
        }

        private static int GetFirstMarkerCharacterIndex(string row, int distinct = 4)
        {
            var length = row.Length;
            var b = new HashSet<char>();
            for (var i = 0; i + distinct <= length; i++)
            {
                b.Clear();
                for (var j = 0; j < distinct; j++)
                {
                    b.Add(row[i + j]);
                }

                if (b.Count() == distinct) return i + distinct;
            }

            throw new Exception("Something wrong here");
        }

        public string PuzzleB(string input)
        {
            var input2 = ParseInput.ParseAsArray(input.Trim());
            int result = 0;
            foreach (var row in input2)
            {
                result += GetFirstMarkerCharacterIndex(row, 14);
            }

            return result.ToString();
        }
    }
}