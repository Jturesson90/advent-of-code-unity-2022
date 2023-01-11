using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JTuresson.AdventOfCode;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace AdventOfCode.Days
{
    public class Day13
    {
        public class Day13Node
        {
            public List<object> Value;

            public Day13Node()
            {
                Value = new List<object>();
            }
        }

        public string PuzzleA(string input)
        {
            var parsedInput = ParseInput.ParseAsArray(input, "\n\n");
            var lists = new List<(List<object>, List<object>)>();
            var allLists = new List<List<object>>();
            for (int i = 0; i < parsedInput.Length; i++)
            {
                var aa = parsedInput[i];
                var row = aa.Split("\n");

                var (a, b) = (ParseList(row[0]), ParseList(row[1]));
                lists.Add((a, b));
                allLists.Add(a);
                allLists.Add(b);
            }

            int sum = 0;
            for (int i = 0; i < lists.Count; i++)
            {
                int ok = CompareItems(lists[i].Item1, lists[i].Item2);
                if (ok == 1) sum += i + 1;
            }
            //   var keyA = new List<object> { new List<object> { 2 } };
            //    var keyB = new List<object> { new List<object> { 6 } };
            //    allLists.Add(keyA);
            //    allLists.Add(keyB);

            return sum.ToString();
        }

        public string PuzzleB(string input)
        {
            var parsedInput = ParseInput.ParseAsArray(input, "\n\n");
            var lists = new List<(List<object>, List<object>)>();
            var allLists = new List<List<object>>();
            for (int i = 0; i < parsedInput.Length; i++)
            {
                var aa = parsedInput[i];
                var row = aa.Split("\n");

                var (a, b) = (ParseList(row[0]), ParseList(row[1]));
                lists.Add((a, b));
                allLists.Add(a);
                allLists.Add(b);
            }

            var keyA = new List<object> { new List<object> { 2 } };
            var keyB = new List<object> { new List<object> { 6 } };
            allLists.Add(keyA);
            allLists.Add(keyB);
            allLists.Sort((a, b) => -CompareItems(a, b));
            int posA = allLists.IndexOf(keyA) + 1;
            int posB = allLists.IndexOf(keyB) + 1;
            return (posA * posB).ToString();
        }

        private static List<object> ParseList(string text)
        {
            var a = new List<object>();
            var stack = new Stack<List<object>>();
            a.Add(null);
            stack.Push(a);
            var parseNum = 0;

            for (var i = 1; i < text.Length; i++)
            {
                var c = text[i];
                switch (c)
                {
                    case >= '0' and <= '9':
                        a[^1] = parseNum = parseNum * 10 + (c - '0');
                        break;
                    case '[':
                    {
                        var b = new List<object> { null };
                        a[^1] = b;
                        stack.Push(a);
                        a = b;
                        break;
                    }
                    case ']':
                        a.Remove(null);
                        a = stack.Pop();
                        break;
                    case ',':
                        parseNum = 0;
                        a.Add(null);
                        break;
                }
            }

            return a;
        }

        private static int CompareItems(object a, object b)
        {
            while (true)
            {
                switch (a)
                {
                    case int i when b is int j:
                    {
                        var (numA, numB) = (i, j);

                        if (numA < numB) return 1;
                        if (numA > numB) return -1;
                        break;
                    }
                    case List<object> list when b is List<object> bList:
                    {
                        var index = 0;
                        var (listA, listB) = (list, bList);
                        while (index < listA.Count)
                        {
                            if (index >= listB.Count) return -1;
                            var ret = CompareItems(listA[index], listB[index]);
                            if (ret != 0) return ret;
                            index++;
                        }

                        if (listA.Count < listB.Count) return 1;
                        break;
                    }
                    default:
                    {
                        object itemA, itemB;
                        if (a is int && b is List<object> bb)
                        {
                            (itemA, itemB) = (new List<object>() { a }, bb);
                        }
                        else
                        {
                            (itemA, itemB) = (a as List<object>, new List<object>() { b });
                        }

                        a = itemA;
                        b = itemB;
                        continue;
                    }
                }

                return 0;
            }
        }
    }
}