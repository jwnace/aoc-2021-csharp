using System.Collections.Generic;
using System.Linq;

namespace aoc_2021_csharp.Day20;

public static class Day20
{
    private static readonly string[] Input = File.ReadAllLines("Day20/day20.txt");

    private static int Solve(int iterations)
    {
        var rules = Input[0];

        var grid = new List<List<char>>();

        for (var i = 2; i < Input.Length; i++)
        {
            grid.Add(new List<char>());

            for (var j = 0; j < Input[i].Length; j++)
            {
                grid.Last().Add(Input[i][j]);
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

    public static int Part1() => Solve(2);

    public static int Part2() => Solve(50);
}
