using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JTuresson.AdventOfCode;

namespace AdventOfCode.Days
{
    public class Day01
    {
        public PuzzleACommandBuffer PuzzleABuffer { get; set; }

        public struct PuzzleACommandBuffer
        {
            public List<long> Calories;
            public int CorrectIndex;
        }

        public string PuzzleA(string input)
        {
            var buffer = new PuzzleACommandBuffer();
            var a = input.Split("\n\n");
            var list = new List<long>();
            int correctIndex = 0;
            long max = 0;
            int index = 0;
            foreach (var b in a)
            {
                if (b.Trim().Length <= 0) continue;
                var c = b.Split('\n');
                long sum = 0;
                foreach (var cc in c)
                {
                    if (long.TryParse(cc, out var j))
                    {
                        sum += j;
                    }
                }

                if (sum > max)
                {
                    max = sum;
                    correctIndex = index;
                }

                list.Add(sum);
                index++;
            }

            buffer.Calories = list;
            buffer.CorrectIndex = correctIndex;
            PuzzleABuffer = buffer;

            return list[correctIndex].ToString();
        }

        public string PuzzleB(string input)
        {
            var a = input.Split("\n\n");
            var list = new List<long>();
            foreach (var b in a)
            {
                if (b.Trim().Length <= 0) continue;
                var c = b.Split('\n');
                long sum = 0;
                foreach (var cc in c)
                {
                    if (long.TryParse(cc, out var j))
                    {
                        sum += j;
                    }
                }

                list.Add(sum);
            }

            return list.OrderByDescending(n => n).Take(3).Sum().ToString();
        }
    }
}