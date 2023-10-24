using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace aoc_2021_csharp.Day11;

public static class Day11
{
    private static readonly string[] Input = File.ReadAllLines("Day11/day11.txt");

    public static int Part1() => Run(1);

    public static int Part2() => Run(2);

    private static int Run(int part)
    {
        var grid = Input.Select(x => x.Select(y => new Octopus(y.ToInt())).ToList()).ToList();
        var count = 0;

        for (var step = 1; step < int.MaxValue; step++)
        {
            // increase the energy level of every octopus
            grid.SelectMany(x => x).ToList().ForEach(x => x.EnergyLevel++);

            // process all the flashes for the current step (keep going until all the energy levels are <= 9)
            while (grid.SelectMany(x => x).Any(x => x.EnergyLevel > 9))
            {
                for (var i = 0; i < grid.Count(); i++)
                {
                    for (var j = 0; j < grid[i].Count(); j++)
                    {
                        if (grid[i][j].EnergyLevel > 9)
                        {
                            Flash(i, j, grid);
                            count++;
                        }
                    }
                }
            }

            if (part == 1 && step == 100)
            {
                return count;
            }
            else if (part == 2 && grid.All(r => r.All(c => c.EnergyLevel == 0)))
            {
                return step;
            }
        }

        throw new Exception("No solution found!");
    }

    private static void Flash(int i, int j, List<List<Octopus>> grid)
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
