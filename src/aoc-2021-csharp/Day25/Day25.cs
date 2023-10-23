using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace aoc_2021_csharp.Day25;

public static class Day25
{
    private static readonly string[] Input = File.ReadAllLines("Day25/day25.txt");

    public static int Part1()
    {
        var grid = new Dictionary<(int, int), char>();
        var maxRow = Input.Length - 1;
        var maxCol = Input[0].Length - 1;

        for (var r = 0; r < Input.Length; r++)
        {
            for (var c = 0; c < Input[r].Length; c++)
            {
                grid[(r, c)] = Input[r][c];
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

    public static int Part2() => 2;
}
