using System.IO;
using System.Linq;

namespace aoc_2021_csharp.Day07;

public static class Day07
{
    private static readonly string[] Input = File.ReadAllLines("Day07/day07.txt");

    public static int Part1()
    {
        var positions = Input[0].Split(',').Select(int.Parse).ToList();
        var min = positions.Min();
        var max = positions.Max();
        var answer = int.MaxValue;

        for (var i = min; i <= max; i++)
        {
            var fuel = positions.Sum(x => Math.Abs(x - i));
            answer = fuel < answer ? fuel : answer;
        }

        return answer;
    }

    public static int Part2()
    {
        var positions = Input[0].Split(',').Select(int.Parse).ToList();
        var min = positions.Min();
        var max = positions.Max();
        var answer = int.MaxValue;

        for (var i = min; i <= max; i++)
        {
            var totalFuel = 0;

            foreach (var position in positions)
            {
                var steps = Math.Abs(position - i);
                var fuel = 0;

                for (var j = 1; j <= steps; j++)
                {
                    fuel += j;
                }

                totalFuel += fuel;
            }

            answer = totalFuel < answer ? totalFuel : answer;
        }

        return answer;
    }
}
