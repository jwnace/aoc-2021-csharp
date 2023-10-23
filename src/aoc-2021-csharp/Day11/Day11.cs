using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace aoc_2021_csharp.Day11;

public static class Day11
{
    private static readonly string[] Input = File.ReadAllLines("Day11/day11.txt");
    private static readonly List<List<Octopus>> Grid = Input.Select(x => x.Select(y => new Octopus(y.ToInt())).ToList()).ToList();

    public static int Part1() => Run(1);

    public static int Part2() => Run(2);

    private static int Run(int part)
    {
        var count = 0;

        for (var step = 1; step < int.MaxValue; step++)
        {
            // increase the energy level of every octopus
            Grid.SelectMany(x => x).ToList().ForEach(x => x.EnergyLevel++);

            // process all the flashes for the current step (keep going until all the energy levels are <= 9)
            while (Grid.SelectMany(x => x).Any(x => x.EnergyLevel > 9))
            {
                for (var i = 0; i < Grid.Count(); i++)
                {
                    for (var j = 0; j < Grid[i].Count(); j++)
                    {
                        if (Grid[i][j].EnergyLevel > 9)
                        {
                            Flash(i, j);
                            count++;
                        }
                    }
                }
            }

            if (part == 1 && step == 100)
            {
                return count;
            }
            else if (part == 2 && Grid.All(r => r.All(c => c.EnergyLevel == 0)))
            {
                throw new Exception("This is returning the wrong answer!");
                return step;
            }
        }

        throw new Exception("No solution found!");
    }

    private static void Flash(int i, int j)
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
            var neighbor = Grid.ElementAtOrDefault(x.Item1)?.ElementAtOrDefault(x.Item2);

            if (neighbor != null && neighbor.EnergyLevel != 0)
            {
                neighbor.EnergyLevel++;
            }
        });

        Grid[i][j].EnergyLevel = 0;
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
