using System;
using JTuresson.AdventOfCode;

namespace AdventOfCode.Days
{
    public class Day04
    {
        public string PuzzleA(string input)
        {
            var pairs = ParseInput.ParseAsJaggedArray(input.Trim(), "\n", ",");
            var count = Count(pairs, Contains);
            return count.ToString();
        }

        public string PuzzleB(string input)
        {
            var pairs = ParseInput.ParseAsJaggedArray(input.Trim(), "\n", ",");
            var count = Count(pairs, Overlaps);
            return count.ToString();
        }

        private static bool Contains(int x1, int x2, int y1, int y2) => x1 >= y1 && x2 <= y2;
        private static bool Overlaps(int x1, int x2, int y1, int y2) => x1 >= y1 && x1 <= y2 || x2 >= y1 && x2 <= y2;

        private int Count(string[][] pairs, Func<int, int, int, int, bool> callback)
        {
            var count = 0;
            foreach (var pair in pairs)
            {
                if (pair.Length != 2)
                {
                    throw new ArgumentException("There is not exactly 2 in the pair");
                }

                var left = pair[0].Split('-');
                var right = pair[1].Split('-');
                var (x1, x2) = (int.Parse(left[0]), int.Parse(left[1]));
                var (y1, y2) = (int.Parse(right[0]), int.Parse(right[1]));
                if (callback(x1, x2, y1, y2) && callback(y1, y2, x1, x2))
                {
                    count++;
                }
                else
                    if (callback(x1, x2, y1, y2))
                    {
                        count++;
                    }
                    else
                        if (callback(y1, y2, x1, x2))
                        {
                            count++;
                        }
            }

            return count;
        }
    }
}