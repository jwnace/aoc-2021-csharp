using System.Collections.Generic;
using System.Linq;

namespace aoc_2021_csharp.Day25;

public static class Day25
{
    private static readonly string[] Input = File.ReadAllLines("Day25/day25.txt");

    public static int Part1() => Solve1(Input);

    public static string Part2() => "Merry Christmas!";

    private static int Solve1(string[] input)
    {
        var grid = new Dictionary<(int, int), char>();
        var maxRow = input.Length - 1;
        var maxCol = input[0].Length - 1;

        for (var r = 0; r < input.Length; r++)
        {
            for (var c = 0; c < input[r].Length; c++)
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

        return step;
    }
}
