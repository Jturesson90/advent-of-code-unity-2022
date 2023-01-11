using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using JTuresson.AdventOfCode;
using UnityEngine;

namespace AdventOfCode.Days
{
    public class Day11
    {
        public struct Operation
        {
            public bool UseOld1;
            public bool UseOld2;
            public long Value1;
            public long Value2;
            public bool UseAdd;
            public bool UseMulti;
        }

        public struct TestData
        {
            public long DivisibleBy;
            public long IfTrueThrowMonkeyId;
            public long IfFalseThrowMonkeyId;
        }

        public class Monkey
        {
            public List<long> CurrentItems { get; private set; }
            public long Id { get; private set; }
            public Operation Operation { get; private set; }
            public TestData TestData { get; private set; }
            public bool CanGetBored { get; private set; }
            public long NumberOfInspections { get; set; }

            public Monkey(long id, long[] startingItems, Operation operation, TestData testData,
                bool canGetBored = true)
            {
                Id = id;
                Operation = operation;
                CurrentItems = startingItems.ToList();
                TestData = testData;
                CanGetBored = canGetBored;
            }

            public long Test(long a)
            {
                return a % TestData.DivisibleBy == 0
                    ? TestData.IfTrueThrowMonkeyId
                    : TestData.IfFalseThrowMonkeyId;
            }

            public long Bored(long a)
            {
                return a / 3;
            }

            public void TakeTurn(Monkey[] otherMonkeys, long lcm)
            {
                while (CurrentItems.Count > 0)
                {
                    NumberOfInspections++;
                    var itemWorryLevel = CurrentItems[0];
                    CurrentItems = CurrentItems.Skip(1).ToList();
                    var a = itemWorryLevel % lcm;
                    var newWorryLevel = Operate(a);
                    if (CanGetBored)
                    {
                        newWorryLevel = Bored(newWorryLevel);
                    }


                    long throwToMonkey = Test(newWorryLevel);
                    otherMonkeys[throwToMonkey].GiveItem(newWorryLevel);
                }
            }

            public long Operate(long level)
            {
                var first = (Operation.UseOld1 ? level : Operation.Value1);
                var second = (Operation.UseOld2 ? level : Operation.Value2);
                if (Operation.UseAdd)
                {
                    return first + second;
                }

                if (Operation.UseMulti)
                {
                    return first * second;
                }

                throw new Exception("Wrong input");
            }

            public void GiveItem(long x)
            {
                CurrentItems.Add(x);
            }

            public static long[] ParseStartingItems(string startingItems)
            {
                //  Starting items: 54, 53

                return startingItems
                    .Replace("Starting items:", "")
                    .Split(",")
                    .Select(v => long.Parse(v.Trim()))
                    .ToArray();
            }

            public static long ParseId(string id)
            {
                //Monkey 0:
                return (long)char.GetNumericValue(id[^2]);
            }

            public static Operation ParseOperation(string operation)
            {
                var split = operation.Trim().Split(" ");
                if (split[0] != "Operation:")
                {
                    throw new Exception("Failed to load operation!");
                }

                var op = new Operation();
                if (split[3] == "old")
                    op.UseOld1 = true;
                else
                    op.Value1 = long.Parse(split[3]);


                op.UseAdd = split[4] == "+";
                op.UseMulti = !op.UseAdd;
                if (split[5] == "old")
                    op.UseOld2 = true;
                else
                    op.Value2 = long.Parse(split[5]);

                return op;
            }

            public static TestData ParseTest(string test)
            {
                var s = test.Split("\n");

                /*
                 *   Test: divisible by 2
                 *      If true: throw to monkey 2
                 *      If false: throw to monkey 6
                 */
                return new TestData()
                {
                    DivisibleBy = long.Parse(s[0].Split(" ").Last()),
                    IfTrueThrowMonkeyId = long.Parse(s[1].Split(" ").Last()),
                    IfFalseThrowMonkeyId = long.Parse(s[2].Split(" ").Last())
                };
            }

            public static Monkey ParseMonkey(string input, bool canGetBored = true)
            {
                var rows = input.Split("\n");
                var id = ParseId(rows[0]);
                var startingItems = ParseStartingItems(rows[1]);
                var operation = ParseOperation(rows[2]);
                var tests = new List<string>
                {
                    rows[3],
                    rows[4],
                    rows[5]
                };
                var test = ParseTest(string.Join('\n', tests));
                return new Monkey(id, startingItems, operation, test, canGetBored);
            }
        }

        public static long CalculateMonkeyBusiness(IEnumerable<Monkey> monkeys)
        {
            var twoHighest = monkeys
                .OrderByDescending(m => m.NumberOfInspections)
                .Take(2).ToArray();
            return twoHighest[0].NumberOfInspections * twoHighest[1].NumberOfInspections;
        }

        public string PuzzleA(string input)
        {
            var monkeysInput = ParseInput.ParseAsArray(input.Trim(), "\n\n");
            var monkeys = new Monkey[monkeysInput.Length];
            foreach (var monkeyInput in monkeysInput)
            {
                var monkey = Monkey.ParseMonkey(monkeyInput, true);
                monkeys[monkey.Id] = monkey;
            }

            var lcm = (long)lcm_of_array_elements(monkeys.Select(a => a.TestData.DivisibleBy).ToArray());
            long rounds = 20;
            for (long i = 0; i < rounds; i++)
            {
                foreach (var monkey in monkeys)
                {
                    monkey.TakeTurn(monkeys, lcm);
                }
            }

            return CalculateMonkeyBusiness(monkeys).ToString();
        }

        public static void PrlongMonkeyInspection(Monkey monkey)
        {
            Debug.Log($"[{monkey.Id}] {monkey.CurrentItems}");
        }

        public static long lcm_of_array_elements(long[] elementArray)
        {
            long lcmOfArrayElements = 1;
            long divisor = 2;

            while (true)
            {
                long counter = 0;
                bool divisible = false;
                for (long i = 0; i < elementArray.Length; i++)
                {
                    switch (elementArray[i])
                    {
                        // lcm_of_array_elements (n1, n2, ... 0) = 0.
                        // For negative number we convert longo
                        // positive and calculate lcm_of_array_elements.
                        case 0:
                            return 0;
                        case < 0:
                            elementArray[i] *= (-1);
                            break;
                    }

                    if (elementArray[i] == 1)
                    {
                        counter++;
                    }

                    // Divide element_array by devisor if complete
                    // division i.e. without remainder then replace
                    // number with quotient; used for find next factor
                    if (elementArray[i] % divisor == 0)
                    {
                        divisible = true;
                        elementArray[i] /= divisor;
                    }
                }

                // If divisor able to completely divide any number
                // from array multiply with lcm_of_array_elements
                // and store longo lcm_of_array_elements and continue
                // to same divisor for next factor finding.
                // else increment divisor
                if (divisible)
                {
                    lcmOfArrayElements *= divisor;
                }
                else
                {
                    divisor++;
                }

                // Check if all element_array is 1 indicate
                // we found all factors and terminate while loop.
                if (counter == elementArray.Length)
                {
                    return lcmOfArrayElements;
                }
            }
        }

        public string PuzzleB(string input)
        {
            var monkeysInput = ParseInput.ParseAsArray(input.Trim(), "\n\n");
            var monkeys = new Monkey[monkeysInput.Length];
            foreach (var monkeyInput in monkeysInput)
            {
                var monkey = Monkey.ParseMonkey(monkeyInput, false);
                monkeys[monkey.Id] = monkey;
            }

            var lcm = (long)lcm_of_array_elements(monkeys.Select(a => a.TestData.DivisibleBy).ToArray());
            long rounds = 10000;
            for (long i = 0; i < rounds; i++)
            {
                foreach (var monkey in monkeys)
                {
                    monkey.TakeTurn(monkeys, lcm);
                }

                if (i + 1 == 20)
                {
                    Debug.Log("----------");
                    foreach (var monkey in monkeys)
                    {
                        PrlongMonkeyInspection(monkey);
                    }
                }
                else if (i + 1 == 1000)
                {
                    Debug.Log("----------");
                    foreach (var monkey in monkeys)
                    {
                        PrlongMonkeyInspection(monkey);
                    }
                }
            }

            return CalculateMonkeyBusiness(monkeys).ToString();
        }
    }
}