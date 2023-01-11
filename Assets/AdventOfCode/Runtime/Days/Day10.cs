using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using JTuresson.AdventOfCode;
using UnityEngine;

namespace AdventOfCode.Days
{
    public class Day10
    {
        public class Day10Result
        {
            public string Result;
        }

        public Day10Result Result = null;

        public string PuzzleA(string input)
        {
            var parsedInput = ParseInput.ParseAsJaggedArray(input.Trim(), "\n", " ");
            var cpu = new CPU();

            foreach (var att in parsedInput)
            {
                var type = att.Length > 0 ? att[0] : "";
                var value = att.Length > 1 ? att[1] : "";
                cpu.AddInstruction(type, value);
            }

            Debug.Log("20: " + cpu.GetTotalPower(new[] {20}));
            Debug.Log("60: " + cpu.GetTotalPower(new[] {60}));
            Debug.Log("100: " + cpu.GetTotalPower(new[] {100}));
            Debug.Log("140: " + cpu.GetTotalPower(new[] {140}));
            Debug.Log("180: " + cpu.GetTotalPower(new[] {180}));
            Debug.Log("220: " + cpu.GetTotalPower(new[] {220}));

            var power = cpu.GetTotalPower(new[] {20, 60, 100, 140, 180, 220});
            return power.ToString();
        }

        public string PuzzleB(string input)
        {
            var parsedInput = ParseInput.ParseAsJaggedArray(input.Trim(), "\n", " ");
            var cpu = new CPU();

            foreach (var att in parsedInput)
            {
                var type = att.Length > 0 ? att[0] : "";
                var value = att.Length > 1 ? att[1] : "";
                cpu.AddInstruction(type, value);
            }

            var sprite = new Sprite(1);
            string[,] result = new string[6, 40];
            //    var l = cpu.CycleByCPULife.ToArray();
            int pos = 0;


            foreach (var life in cpu.CycleByCPULife)
            {
                sprite.UpdateSprite(life.Value.X);
                var row = pos / 40;
                var col = pos % 40;
                result[row, col] = sprite.IsInsideSprite(col) ? "#" : ".";

                pos++;
            }

            StringBuilder resultString = new();
            for (int i = 0; i < result.GetLength(0); i++)
            {
                for (int x = 0; x < result.GetLength(1); x++)
                {
                    resultString.Append(result[i, x]);
                }

                if (i + 1 < result.GetLength(0))
                {
                    resultString.Append("\n");
                }
            }

            Result = new Day10Result() {Result = resultString.ToString()};
            Debug.Log(resultString.ToString());
            return resultString.ToString();
        }

        private class Sprite
        {
            private int _x;

            public Sprite(int x)
            {
                _x = x;
            }

            public void UpdateSprite(int x)
            {
                _x = x;
            }

            public bool IsInsideSprite(int x) => _x == x || _x - 1 == x || _x + 1 == x;
        }
    }

    public class CPU
    {
        public int Cycle { get; set; }
        public CPUState State;
        public Dictionary<int, CPUStateLife> CycleByCPULife { get; }

        public CPU()
        {
            State = new CPUState(1);
            CycleByCPULife = new Dictionary<int, CPUStateLife>();
        }

        public void AddInstruction(string type, object value)
        {
            switch (type)
            {
                case "noop":
                    Noop();
                    break;
                case "addx":
                    AddX(int.Parse(value.ToString()));
                    break;
                default:
                    throw new Exception("WTF is " + type + " with value " + value);
            }
        }

        public int GetTotalPower(int[] cycles)
        {
            int result = 0;
            foreach (var c in cycles)
            {
                result += CycleByCPULife[c].Power;
            }

            return result;
        }


        private void Noop()
        {
            CycleByCPULife.Add(++Cycle, new CPUStateLife(Cycle, State.X));
        }

        private void AddX(int value)
        {
            CycleByCPULife.Add(++Cycle, new CPUStateLife(Cycle, State.X));
            CycleByCPULife.Add(++Cycle, new CPUStateLife(Cycle, State.X));
            State.AddX(value);
        }
    }

    public struct CPUStateLife
    {
        public int Cycle;
        public int X;
        public int Power;

        public CPUStateLife(int cycle, int x)
        {
            Cycle = cycle;
            X = x;
            Power = Cycle * X;
        }
    }

    public class CPUState
    {
        public int X { get; private set; }

        public void AddX(int x)
        {
            X += x;
        }

        public CPUState(int startX)
        {
            X = startX;
        }
    }
}