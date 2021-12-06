using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc_2021_csharp
{
    public class Day06
    {
        private static readonly string INPUT_FILE = "input/day06.txt";
        private static readonly string[] input = System.IO.File.ReadAllLines(INPUT_FILE);

        public void Part1()
        {
            Console.WriteLine($"Day 06, Part 1: {Solve(80)}");
        }

        public void Part2()
        {
            Console.WriteLine($"Day 06, Part 2: {Solve(256)}");
        }

        private ulong Solve(int days)
        {
            var dict = new Dictionary<int, ulong>
            {
                { 0, 0 },
                { 1, 0 },
                { 2, 0 },
                { 3, 0 },
                { 4, 0 },
                { 5, 0 },
                { 6, 0 },
                { 7, 0 },
                { 8, 0 },
                { 9, 0 }
            };

            input[0].Split(',')
                .Select(x => int.Parse(x))
                .GroupBy(x => x)
                .Select(g => new
                {
                    Value = g.Key,
                    Count = g.Count()
                })
                .ToList()
                .ForEach(x => dict[x.Value] = (ulong)x.Count);

            for (int d = 0; d < days; d++)
            {
                dict[7] += dict[0];
                dict[9] += dict[0];
                dict[0] = 0;

                for (int i = 1; i <= 9; i++)
                {
                    dict[i - 1] = dict[i];
                    dict[i] = 0;
                }
            }

            var sum = 0UL;

            for (int i = 0; i <= 9; i++)
            {
                sum += dict[i];
            }

            return sum;
        }
    }
}
