using System;
using System.Linq;
using JTuresson.AdventOfCode;

namespace AdventOfCode.Days
{
    public class Day02
    {
        public string PuzzleA(string input)
        {
            var trimmedInput = input.Trim();
            var a = ParseInput.ParseAsArray(trimmedInput);
            var res = a.Aggregate(0, (sum, next) =>
            {
                {
                    return sum + next switch
                    {
                        "A X" => 1 + 3,
                        "A Y" => 2 + 6,
                        "A Z" => 3 + 0,
                        "B X" => 1 + 0,
                        "B Y" => 2 + 3,
                        "B Z" => 3 + 6,
                        "C X" => 1 + 6,
                        "C Y" => 2 + 0,
                        "C Z" => 3 + 3,
                        _ => throw new ArgumentOutOfRangeException(nameof(next), next, null)
                    };
                }
            });
            return res.ToString();
        }

        public string PuzzleB(string input)
        {
            var trimmedInput = input.Trim();
            var a = ParseInput.ParseAsArray(trimmedInput);
            var res = a.Aggregate(0, (sum, next) =>
            {
                {
                    return sum + next switch
                    {
                        "A X" => 3 + 0,
                        "A Y" => 1 + 3,
                        "A Z" => 2 + 6,
                        "B X" => 1 + 0,
                        "B Y" => 2 + 3,
                        "B Z" => 3 + 6,
                        "C X" => 2 + 0,
                        "C Y" => 3 + 3,
                        "C Z" => 1 + 6,
                        _ => throw new ArgumentOutOfRangeException(nameof(next), next, null)
                    };
                }
            });
            return res.ToString();
        }
    }
}