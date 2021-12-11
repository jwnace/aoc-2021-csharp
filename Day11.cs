using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc_2021_csharp
{
    public class Day11
    {
        private static readonly string INPUT_FILE = "input/day11.txt";
        private static readonly string[] input = System.IO.File.ReadAllLines(INPUT_FILE);

        public void Part1()
        {
            var grid = input.Select(x => x.Select(y => new Octopus(y.ToInt())).ToList()).ToList();
            var steps = 100;
            var countFlashes = 0;

            for (int step = 0; step < steps; step++)
            {
                // increase the energy level of every octopus
                grid.SelectMany(x => x).ToList().ForEach(x => x.EnergyLevel++);

                var flashes = new List<(int, int)>();

                // process all the flashes for the current step (keep going until all the energy levels are <= 9)
                while (grid.SelectMany(x => x).Any(x => x.EnergyLevel > 9))
                {
                    for (int i = 0; i < grid.Count(); i++)
                    {
                        for (int j = 0; j < grid[i].Count(); j++)
                        {
                            if (grid[i][j].EnergyLevel > 9)
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

                                    if (neighbor != null)
                                    {
                                        neighbor.EnergyLevel++;
                                    }
                                });

                                // minor hack to prevent an octopus from flashing more than once per step
                                grid[i][j].EnergyLevel = int.MinValue;

                                // keep track of each location that flashed
                                flashes.Add((i, j));
                            }
                        }
                    }
                }

                for (int i = 0; i < flashes.Count(); i++)
                {
                    // set the energy level to 0 for all locations that flashed
                    grid[flashes[i].Item1][flashes[i].Item2].EnergyLevel = 0;
                }

                countFlashes += flashes.Count();
            }

            Console.WriteLine($"Day 11, Part 1: {countFlashes}");
        }

        public void Part2()
        {
            var grid = input.Select(x => x.Select(y => new Octopus(y.ToInt())).ToList()).ToList();
            var step = 0;

            while (true)
            {
                step++;

                // increase the energy level of every octopus
                grid.SelectMany(x => x).ToList().ForEach(x => x.EnergyLevel++);

                var flashes = new List<(int, int)>();

                while (grid.SelectMany(x => x).Any(x => x.EnergyLevel > 9))
                {
                    for (int i = 0; i < grid.Count(); i++)
                    {
                        for (int j = 0; j < grid[i].Count(); j++)
                        {
                            if (grid[i][j].EnergyLevel > 9)
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

                                    if (neighbor != null)
                                    {
                                        neighbor.EnergyLevel++;
                                    }
                                });

                                // minor hack to prevent an octopus from flashing more than once per step
                                grid[i][j].EnergyLevel = int.MinValue;

                                // keep track of each location that flashed
                                flashes.Add((i, j));
                            }
                        }
                    }
                }

                for (int i = 0; i < flashes.Count(); i++)
                {
                    grid[flashes[i].Item1][flashes[i].Item2].EnergyLevel = 0;
                }

                if (grid.All(r => r.All(c => c.EnergyLevel == 0)))
                {
                    Console.WriteLine($"Day 11, Part 2: {step}");
                    return;
                }
            }
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
