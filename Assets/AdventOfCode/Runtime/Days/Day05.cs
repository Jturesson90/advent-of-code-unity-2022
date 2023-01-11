using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using JTuresson.AdventOfCode;
using NUnit.Framework;

namespace AdventOfCode.Days
{
    public class Day05
    {
        public PuzzleACommandBuffer PuzzleABuffer { get; set; }

        public struct PuzzleACommandBuffer
        {
            public List<Stack<char>> Start;
            public List<BayMoveCommands> Commands;
            public MoveEnum Movement;
            public int Width;
        }

        public enum MoveEnum
        {
            Single,
            Multi
        }

        public struct BayMoveCommands
        {
            public int Amount;
            public int From;
            public int To;
        }

        public string PuzzleA(string input)
        {
            var cordination = ParseInput.ParseAsJaggedArray(input, "\n\n", "\n");
            var left = cordination[0];
            var right = cordination[1];
            var bay = ParseBay(left);
            var commands = ParseCommands(right);

            var v = new List<Stack<char>>();
            foreach (var VARIABLE in bay)
            {
                v.Add(new Stack<char>(VARIABLE));
            }

            PuzzleABuffer = new PuzzleACommandBuffer()
            {
                Commands = commands, Movement = MoveEnum.Single, Start = v, Width = bay.Length
            };
            foreach (var command in commands)
            {
                for (int i = 0; i < command.Amount; i++)
                {
                    char c = bay[command.From - 1].Pop();
                    bay[command.To - 1].Push(c);
                }
            }

            return new string(bay.Select(a => a.Peek()).ToArray());
        }

        public string PuzzleB(string input)
        {
            var cordination = ParseInput.ParseAsJaggedArray(input, "\n\n", "\n");
            var left = cordination[0];
            var right = cordination[1];
            var bay = ParseBay(left);
            var commands = ParseCommands(right);

            foreach (var command in commands)
            {
                var cc = new List<char>();
                for (int i = 0; i < command.Amount; i++)
                {
                    cc.Add(bay[command.From - 1].Pop());
                }

                for (int i = cc.Count - 1; i >= 0; i--)
                {
                    bay[command.To - 1].Push(cc[i]);
                }
            }

            return new string(bay.Select(a => a.Peek()).ToArray());
        }

        private List<BayMoveCommands> ParseCommands(string[] input)
        {
            var a = new List<BayMoveCommands>();
            foreach (var c in input)
            {
                Regex rx = new Regex(@"[0-9]+");
                var matches = rx.Matches(c);
                if (matches.Count != 3) continue;
                a.Add(new BayMoveCommands()
                {
                    Amount = int.Parse(matches[0].Value),
                    From = int.Parse(matches[1].Value),
                    To = int.Parse(matches[2].Value)
                });
            }

            return a;
        }

        private Stack<char>[] ParseBay(string[] input)
        {
            var reversedOrder = input.Reverse();
            var enumerable = reversedOrder as string[] ?? reversedOrder.ToArray();
            var numbersRow = enumerable.First();

            Regex rx = new Regex(@"[1-9]+");
            var matches = rx.Matches(numbersRow);

            var length = int.Parse(matches[^1].Value);
            var bay = new Stack<char>[length];
            for (int i = 0; i < bay.Length; i++)
            {
                bay[i] = new Stack<char>();
            }

            foreach (var row in enumerable.Skip(1))
            {
                var fillBlanks = ParseBayRow(row);
                for (int i = 0; i < fillBlanks.Length; i++)
                {
                    string c = fillBlanks[i];
                    if (c == null) continue;

                    bay[i].Push(c[0]);
                }
            }

            return bay;
        }

        private string[] ParseBayRow(string row)
        {
            var l = new List<string>();
            for (int i = 0; i < row.Length;)
            {
                if (row[i] == '[')
                {
                    l.Add(row[i + 1].ToString());
                    i += 4;
                }
                else
                    if (row[i] == ' ')
                    {
                        l.Add(null);
                        i += 4;
                    }
            }

            return l.ToArray();
        }
    }
}