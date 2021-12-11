using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc_2021_csharp
{
    public class Day11
    {
        private static readonly string INPUT_FILE = "input/day11.txt";
        private static readonly string[] input = System.IO.File.ReadAllLines(INPUT_FILE);
        private readonly List<List<Octopus>> grid = input.Select(x => x.Select(y => new Octopus(y.ToInt())).ToList()).ToList();

        public void Part1()
        {
            Run(1);
        }

        public void Part2()
        {
            Run(2);
        }

        private void Run(int part)
        {
            var count = 0;

            for (int step = 1; step < int.MaxValue; step++)
            {
                // increase the energy level of every octopus
                grid.SelectMany(x => x).ToList().ForEach(x => x.EnergyLevel++);

                // process all the flashes for the current step (keep going until all the energy levels are <= 9)
                while (grid.SelectMany(x => x).Any(x => x.EnergyLevel > 9))
                {
                    for (int i = 0; i < grid.Count(); i++)
                    {
                        for (int j = 0; j < grid[i].Count(); j++)
                        {
                            if (grid[i][j].EnergyLevel > 9)
                            {
                                Flash(i, j);
                                count++;
                            }
                        }
                    }
                }

                if (part == 1 && step == 100)
                {
                    Console.WriteLine($"Day 11, Part 1: {count}");
                    return;
                }
                else if (part == 2 && grid.All(r => r.All(c => c.EnergyLevel == 0)))
                {
                    Console.WriteLine($"Day 11, Part 2: {step}");
                    return;
                }
            }
        }

        private void Flash(int i, int j)
        {
            // increment all neighbors by 1
            var neighbors = new List<(int, int)>
            {
                (i - 1, j - 1), (i - 1, j), (i - 1, j + 1),
                (i    , j - 1),             (i    , j + 1),
                (i + 1, j - 1), (i + 1, j), (i + 1, j + 1)
            };

            neighbors.ForEach(x =>
            {
                var neighbor = grid.ElementAtOrDefault(x.Item1)?.ElementAtOrDefault(x.Item2);

                if (neighbor != null && neighbor.EnergyLevel != 0)
                {
                    neighbor.EnergyLevel++;
                }
            });

            grid[i][j].EnergyLevel = 0;
        }
    }

    public class Octopus
    {
        public int EnergyLevel { get; set; }

        public Octopus(int energyLevel)
        {
            EnergyLevel = energyLevel;
        }
    }
}
