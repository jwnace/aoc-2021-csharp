using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc_2021_csharp
{
    public class Day25
    {
        private static readonly string INPUT_FILE = "input/day25.txt";
        private static readonly string[] input = System.IO.File.ReadAllLines(INPUT_FILE);

        public void Part1()
        {
            var grid = new Dictionary<(int, int), char>();
            var maxRow = input.Length - 1;
            var maxCol = input[0].Length - 1;

            for (int r = 0; r < input.Length; r++)
            {
                for (int c = 0; c < input[r].Length; c++)
                {
                    grid[(r, c)] = input[r][c];
                }
            }

            var step = 0;

            while (true)
            {
                step++;
                var moved = false;
                var newGrid = new Dictionary<(int, int), char>(grid);

                foreach (var cell in grid.Where(x => x.Value == '>'))
                {
                    var (r, c) = cell.Key;
                    c = c < maxCol ? c + 1 : 0;

                    if (grid[(r, c)] == '.')
                    {
                        newGrid[(r, c)] = cell.Value;
                        newGrid[cell.Key] = '.';
                        moved = true;
                    }
                }

                grid = newGrid;
                newGrid = new Dictionary<(int, int), char>(grid);

                foreach (var cell in grid.Where(x => x.Value == 'v'))
                {
                    var (r, c) = cell.Key;
                    r = r < maxRow ? r + 1 : 0;

                    if (grid[(r, c)] == '.')
                    {
                        newGrid[(r, c)] = cell.Value;
                        newGrid[cell.Key] = '.';
                        moved = true;
                    }
                }

                grid = newGrid;

                if (moved == false)
                {
                    break;
                }
            }

            Console.WriteLine($"Day 25, Part 1: {step}");
        }

        public void Part2()
        {
            Console.WriteLine($"Day 25, Part 2: ");
        }
    }
}
