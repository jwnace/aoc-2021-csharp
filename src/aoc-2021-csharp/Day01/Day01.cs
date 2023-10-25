using System.Linq;

namespace aoc_2021_csharp.Day01;

public static class Day01
{
    private static readonly int[] Input = File.ReadAllLines("Day01/day01.txt").Select(int.Parse).ToArray();

    public static int Part1()
    {
        var count = 0;

        for (var i = 1; i < Input.Length; i++)
        {
            count += Input[i] > Input[i-1] ? 1 : 0;
        }

        return count;
    }

    public static int Part2()
    {
        var count = 0;

        for (var i = 2; i < Input.Length-1; i++)
        {
            var sumA = Input[i-2] + Input[i-1] + Input[i];
            var sumB = Input[i-1] + Input[i] + Input[i+1];

            count += sumB > sumA ? 1 : 0;
        }

        return count;
    }
}
