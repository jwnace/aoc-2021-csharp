using System.Collections.Generic;
using System.Linq;

namespace aoc_2021_csharp.Day24;

public static class Day24
{
    private static readonly string[] Input = File.ReadAllLines("Day24/day24.txt");

    public static long Part1() => Solve(Input).Part1;

    public static long Part2() => Solve(Input).Part2;

    private static (long Part1, long Part2) Solve(IEnumerable<string> input)
    {
        var sets = input
            .Chunk(18)
            .SelectMany(c => new[] { c[4], c[5], c[15] })
            .Select(c => int.Parse(c[6..]))
            .Chunk(3)
            .Select(c => (a: c[0], b: c[1], c: c[2]))
            .ToArray();

        var nums = Enumerable.Range(1, 9).ToArray();
        var serials = Check(Array.Empty<int>(), 0, 0, sets, nums).ToArray();

        return (serials.Max(), serials.Min());
    }

    private static IEnumerable<long> Check(
        int[] digits,
        int i,
        int z,
        (int a, int b, int c)[] sets,
        int[] nums)
    {
        if (digits.Length == sets.Length)
        {
            return z == 0
                ? new[] { long.Parse(string.Join("", digits)) }
                : Array.Empty<long>();
        }

        return sets[i].b < 0
            ? Sub(nums.Where(n => z % 26 + sets[i].b == n), _ => z / 26, digits, i, sets, nums)
            : Sub(nums, w => z * 26 + w + sets[i].c, digits, i, sets, nums);
    }

    private static IEnumerable<long> Sub(
        IEnumerable<int> range,
        Func<int, int> z,
        int[] digits,
        int i,
        (int a, int b, int c)[] sets,
        int[] nums)
    {
        return range.SelectMany(w => Check(digits.Append(w).ToArray(), i + 1, z(w), sets, nums));
    }
}
