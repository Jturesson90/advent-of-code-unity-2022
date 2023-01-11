using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JTuresson.AdventOfCode;
using UnityEngine;

namespace AdventOfCode.Days
{
    public class Day14
    {
        public class Day14Data
        {
            public char[,] Grid;

            public Day14Data(char[,] grid)
            {
                Grid = grid;
            }
        }

        public Day14Data Before;
        public Day14Data After;

        public string PuzzleA(string input)
        {
            var grid = GetGrid(input);
            var start = new Vector2Int(500, 0);
            grid.SetStartPoint(start);
            while (grid.DropSand())
            {
            }

            return grid.CountSand().ToString();
        }

        public string PuzzleB(string input)
        {
            var grid = GetGrid2(input);
            Before = new Day14Data(grid.Grid);
            var start = new Vector2Int(500, 0);
            grid.SetStartPoint(start);
            while (grid.DropSand())
            {
            }

            After = new Day14Data(grid.Grid.Clone() as char[,]);
            return grid.CountSand().ToString();
        }

        public class Day14Grid
        {
            public char[,] Grid;
            public int MinX, MaxX, MinY, MaxY;
            private int _startX, _startY;
            private bool _infinite;
            private readonly int Width, Height;

            public Day14Grid(char[,] grid, bool infinite = true)
            {
                _infinite = infinite;
                Grid = grid;
                Width = grid.GetLength(1);
                Height = grid.GetLength(0);
            }

            public void SetStartPoint(Vector2Int v)
            {
                if (_infinite)
                {
                    _startX = v.x - MinX;
                    _startY = v.y - MinY;
                }
                else
                {
                    _startX = v.x;
                    _startY = v.y;
                }
            }

            private bool IsInside(int row, int col)
            {
                return row >= 0 && col >= 0 && col < Width && row < Height;
            }

            public int CountSand()
            {
                int r = 0;
                for (int i = 0; i < Height; i++)
                {
                    for (int j = 0; j < Width; j++)
                    {
                        if (Grid[i, j] == 'o') r++;
                    }
                }

                return r;
            }

            public bool DropSand()
            {
                var sandCol = _startX - (_infinite ? 0 : MinX);
                var sandRow = _startY - (_infinite ? 0 : MinY);
                var checkState = new Vector2Int(sandCol, sandRow);
                while (true)
                {
                    //   sandRow--;
                    if (IsInside(sandRow + 1, sandCol))
                    {
                        var c = Grid[sandRow + 1, sandCol];
                        if (c is '.')
                        {
                            sandRow++;
                        }
                        else if (c is '#' or 'o')
                        {
                            if (IsInside(sandRow + 1, sandCol - 1) && Grid[sandRow + 1, sandCol - 1] == '.')
                            {
                                sandRow++;
                                sandCol--;
                                continue;
                            }

                            if (IsInside(sandRow + 1, sandCol + 1) && Grid[sandRow + 1, sandCol + 1] == '.')
                            {
                                sandRow++;
                                sandCol++;
                                continue;
                            }

                            if (!IsInside(sandRow + 1, sandCol + 1) || !IsInside(sandRow + 1, sandCol - 1))
                                return false;

                            break;
                        }
                    }
                    else return false;
                }

                if (checkState == new Vector2Int(sandCol, sandRow))
                {
                    Grid[sandRow, sandCol] = 'o';
                    return false;
                }


                Grid[sandRow, sandCol] = 'o';
                return true;
            }
        }

        public Day14Grid GetGrid(string input)
        {
            var inputRows = ParseInput.ParseAsArray(input.Trim());
            var rocks = new HashSet<Vector2Int>();
            foreach (var inputRow in inputRows)
            {
                PopulatePaths(inputRow, rocks);
            }

            var minX = rocks.Min(a => a.x);
            var maxX = rocks.Max(a => a.x);
            var minY = 0;
            var maxY = rocks.Max(a => a.y);
            var grid = new char[maxY - minY + 1, maxX - minX + 1];
            var rowsLength = grid.GetLength(0);
            var colsLength = grid.GetLength(1);

            for (int row = 0; row < rowsLength; row++)
            {
                for (int col = 0; col < colsLength; col++)
                {
                    grid[row, col] = rocks.Contains(new Vector2Int(col + minX, row + minY)) ? '#' : '.';
                }
            }

            var gridc = new Day14Grid(grid, false);
            gridc.Grid = grid;
            gridc.MaxY = maxY;
            gridc.MinY = minY;
            gridc.MinX = minX;
            gridc.MaxX = maxX;
            return gridc;
        }

        public Day14Grid GetGrid2(string input)
        {
            var inputRows = ParseInput.ParseAsArray(input.Trim());
            var rocks = new HashSet<Vector2Int>();
            foreach (var inputRow in inputRows)
            {
                PopulatePaths(inputRow, rocks);
            }

            var minX = 0;
            var maxX = 1200;
            var minY = 0;
            var maxY = rocks.Max(a => a.y) + 2;
            for (int i = 0; i < maxX; i++)
            {
                rocks.Add(new Vector2Int(i, maxY));
            }

            var grid = new char[maxY + 1, maxX + 1];
            var rowsLength = grid.GetLength(0);
            var colsLength = grid.GetLength(1);

            for (int row = 0; row < rowsLength; row++)
            {
                for (int col = 0; col < colsLength; col++)
                {
                    grid[row, col] = rocks.Contains(new Vector2Int(col, row)) ? '#' : '.';
                }
            }

            var gridc = new Day14Grid(grid);
            gridc.Grid = grid;
            gridc.MaxY = maxY;
            gridc.MinY = minY;
            gridc.MinX = minX;
            gridc.MaxX = maxX;
            return gridc;
        }

        public void PopulatePaths(string row, HashSet<Vector2Int> rockPositions)
        {
            var rr = row.Split(" -> ").Select(a =>
            {
                var aa = a.Split(",");
                return new Vector2Int(int.Parse(aa[0]), int.Parse(aa[1]));
            }).ToArray();
            var len = rr.Count();
            for (var i = 1; i < len; i++)
            {
                var from = rr[i - 1];
                var to = rr[i];
                if (from.x == to.x)
                {
                    var x = from.x;
                    var fromY = Math.Min(from.y, to.y);
                    var toY = Math.Max(from.y, to.y);
                    for (var y = fromY; y <= toY; y++)
                    {
                        rockPositions.Add(new Vector2Int(x, y));
                    }
                }
                else if (from.y == to.y)
                {
                    var y = from.y;
                    var fromX = Math.Min(from.x, to.x);
                    var toX = Math.Max(from.x, to.x);
                    for (var x = fromX; x <= toX; x++)
                    {
                        rockPositions.Add(new Vector2Int(x, y));
                    }
                }
                else
                {
                    throw new Exception("GetFromTo: From and To does not share x or y pos");
                }
            }
        }
    }
}