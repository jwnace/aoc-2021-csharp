using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc_2021_csharp
{
    public class Day20
    {
        private static readonly string INPUT_FILE = "input/day20.txt";
        private static readonly string[] input = System.IO.File.ReadAllLines(INPUT_FILE);

        private int Solve(int iterations)
        {
            var rules = input[0];

            var grid = new List<List<char>>();

            for (var i = 2; i < input.Length; i++)
            {
                grid.Add(new List<char>());

                for (var j = 0; j < input[i].Length; j++)
                {
                    grid.Last().Add(input[i][j]);
                }
            }

            for (var step = 0; step < iterations; step++)
            {
                var newGrid = new List<List<char>>();

                for (var r = -1; r < grid.Count() + 1; r++)
                {
                    newGrid.Add(new List<char>());

                    for (var c = -1; c < grid.First().Count() + 1; c++)
                    {
                        var binary = "";

                        for (var dr = -1; dr <= 1; dr++)
                        {
                            for (var dc = -1; dc <= 1; dc++)
                            {
                                var pad = rules[0] == '#' ? (step % 2 == 0 ? '.' : '#') : '.';
                                var temp = r + dr < 0 || r + dr >= grid.Count || c + dc < 0 || c + dc >= grid[0].Count() ? pad : grid[r + dr][c + dc];

                                binary += temp == '#' ? "1" : "0";
                            }
                        }

                        newGrid.Last().Add(rules[Convert.ToInt32(binary, 2)]);
                    }
                }

                grid = newGrid;
            }

            return grid.Sum(x => x.Count(c => c == '#'));
        }

        public void Part1()
        {
            Console.WriteLine($"Day 20, Part 1: {Solve(2)}");
        }

        public void Part2()
        {
            Console.WriteLine($"Day 20, Part 2: {Solve(50)}");
        }
    }
}