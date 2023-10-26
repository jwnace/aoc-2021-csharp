using System.Collections.Generic;
using System.Linq;

namespace aoc_2021_csharp.Day23;

public static partial class Day23
{
    private static Dictionary<(int Row, int Col), char> BuildGrid(IReadOnlyList<string> input)
    {
        var grid = new Dictionary<(int Row, int Col), char>();

        for (var row = 0; row < input.Count; row++)
        {
            for (var col = 0; col < input[row].Length; col++)
            {
                grid[(row, col)] = input[row][col];
            }
        }

        return grid;
    }

    private static State BuildInitialState(Dictionary<(int Row, int Col), char> grid)
    {
        var amphipods = new List<Amphipod>();

        foreach (var pair in grid)
        {
            var (row, col) = pair.Key;
            var value = pair.Value;

            switch (value)
            {
                case 'A':
                {
                    amphipods.Add(new Amphipod(row, col, value, 1, 3));
                    break;
                }
                case 'B':
                {
                    amphipods.Add(new Amphipod(row, col, value, 10, 5));
                    break;
                }
                case 'C':
                {
                    amphipods.Add(new Amphipod(row, col, value, 100, 7));
                    break;
                }
                case 'D':
                {
                    amphipods.Add(new Amphipod(row, col, value, 1000, 9));
                    break;
                }
            }
        }

        return new State(amphipods.ToArray(), 0);
    }

    private static void DrawGrid(Dictionary<(int Row, int Col), char> grid, Amphipod[] amphipods)
    {
        var minRow = grid.Keys.Min(k => k.Row);
        var maxRow = grid.Keys.Max(k => k.Row);
        var minCol = grid.Keys.Min(k => k.Col);
        var maxCol = grid.Keys.Max(k => k.Col);

        for (var row = minRow; row <= maxRow; row++)
        {
            for (var col = minCol; col <= maxCol; col++)
            {
                var value = grid.GetValueOrDefault((row, col));

                if (value == '#')
                {
                    Console.Write('#');
                }
                else if (amphipods.Any(a => a.Row == row && a.Col == col))
                {
                    var type = amphipods.First(a => (a.Row, a.Col) == (row, col)).Type;
                    Console.Write(type);
                }
                else
                {
                    Console.Write('.');
                }
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }
}
