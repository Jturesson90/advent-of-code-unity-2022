using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JTuresson.AdventOfCode;
using UnityEngine;

namespace AdventOfCode.Days
{
    public class Day08
    {
        public class Day08Data
        {
            public List<Day08Tree> Trees = new();
        }

        public Day08Data Data = null;

        public class Day08Tree
        {
            public Vector2Int Position;
            public int ScenicScore;
            public bool VisibleFromSide;
            public bool IsBestScore;
            public int Height;
        }

        public void Init(string input)
        {
            var grid = ParseInput.ParseAsMultiIntArray(input.Trim(), "\n", "");
            Data = new Day08Data()
            {
                Trees = new List<Day08Tree>()
            };
            int width = grid.GetLength(1);
            int height = grid.GetLength(0);
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Data.Trees.Add(new Day08Tree()
                    {
                        Position = new Vector2Int(x, y),
                        Height = grid[y, x]
                    });
                }
            }
        }

        public string PuzzleA(string input)
        {
            var grid = ParseInput.ParseAsMultiIntArray(input.Trim(), "\n", "");
            var count = new HashSet<Vector2Int>();

            void AddToCount(int x, int y)
            {
                count.Add(new Vector2Int(x, y));
            }

            int width = grid.GetLength(1);
            int height = grid.GetLength(0);
            int startHeight = -1;
            int maxHeight = 9;
            // Horizontal
            for (int y = 0; y < height; y++)
            {
                int left = startHeight;
                for (int x = 0; x < width; x++)
                {
                    if (left >= maxHeight) break;
                    var b = grid[y, x];
                    if (b > left)
                    {
                        AddToCount(x, y);
                        left = b;
                    }
                }

                int right = startHeight;
                for (int x = width - 1; x >= 0; x--)
                {
                    if (right >= maxHeight) break;
                    var b = grid[y, x];
                    if (b > right)
                    {
                        AddToCount(x, y);
                        right = b;
                    }
                }
            }

            // From Top
            for (int x = 0; x < width; x++)
            {
                int up = startHeight;
                for (int y = 0; y < height; y++)
                {
                    if (up >= maxHeight) break;
                    var b = grid[y, x];
                    if (b > up)
                    {
                        AddToCount(x, y);
                        up = b;
                    }
                }

                int down = startHeight;
                for (int y = height - 1; y >= 0; y--)
                {
                    if (down >= maxHeight) break;
                    var b = grid[y, x];
                    if (b > down)
                    {
                        AddToCount(x, y);
                        down = b;
                    }
                }
            }

            if (Data != null)
            {
                foreach (var c in count)
                {
                    var a = Data.Trees.First(a => a.Position == c);
                    a.VisibleFromSide = true;
                }
            }

            return count.Count.ToString();
        }

        public string PuzzleB(string input)
        {
            var grid = ParseInput.ParseAsMultiIntArray(input.Trim(), "\n", "");
            int width = grid.GetLength(1);
            int height = grid.GetLength(0);
            Dictionary<Vector2Int, int> treeNameByScenicScore = new Dictionary<Vector2Int, int>();
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int up = 0;
                    int down = 0;
                    int right = 0;
                    int left = 0;
                    var currentHeight = grid[y, x];

                    for (int i = y - 1; i >= 0; i--)
                    {
                        up++;
                        if (grid[i, x] >= currentHeight) break;
                    }

                    for (int i = x + 1; i < width; i++)
                    {
                        right++;
                        if (grid[y, i] >= currentHeight) break;
                    }

                    for (int i = x - 1; i >= 0; i--)
                    {
                        left++;
                        if (grid[y, i] >= currentHeight) break;
                    }

                    for (int i = y + 1; i < height; i++)
                    {
                        down++;
                        if (grid[i, x] >= currentHeight) break;
                    }

                    treeNameByScenicScore.Add(new Vector2Int(x, y), up * down * left * right);
                }
            }

            if (Data != null)
            {
                foreach (var d in treeNameByScenicScore)
                {
                }
            }

            var best = treeNameByScenicScore.Max(a => a.Value);
            if (Data != null)
            {
                foreach (var d in treeNameByScenicScore)
                {
                    var f = Data.Trees.Find(a => a.Position == d.Key);
                    f.ScenicScore = d.Value;
                    f.IsBestScore = d.Value == best;
                }
            }

            return best.ToString();
        }
    }
}