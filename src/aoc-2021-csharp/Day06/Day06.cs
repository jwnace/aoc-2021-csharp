using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace aoc_2021_csharp.Day06;

public static class Day06
{
    private static readonly string[] Input = File.ReadAllLines("Day06/day06.txt");

    public static ulong Part1() => Solve(80);

    public static ulong Part2() => Solve(256);

    private static ulong Solve(int days)
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

        Input[0].Split(',')
            .Select(int.Parse)
            .GroupBy(x => x)
            .Select(g => new
            {
                Value = g.Key,
                Count = g.Count()
            })
            .ToList()
            .ForEach(x => dict[x.Value] = (ulong)x.Count);

        for (var d = 0; d < days; d++)
        {
            dict[7] += dict[0];
            dict[9] += dict[0];
            dict[0] = 0;

            for (var i = 1; i <= 9; i++)
            {
                dict[i - 1] = dict[i];
                dict[i] = 0;
            }
        }

        var sum = 0UL;

        for (var i = 0; i <= 9; i++)
        {
            sum += dict[i];
        }

        return sum;
    }
}
