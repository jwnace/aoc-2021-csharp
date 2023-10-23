using System.IO;

namespace aoc_2021_csharp.Day02;

public static class Day02
{
    private static readonly string[] Input = File.ReadAllLines("Day02/day02.txt");

    public static int Part1()
    {
        var x = 0;
        var y = 0;

        foreach (var line in Input)
        {
            var values = line.Split(' ');
            var direction = values[0];
            var magnitude = int.Parse(values[1]);

            switch (direction)
            {
                case "forward":
                    x += magnitude;
                    break;

                case "up":
                    y -= magnitude;
                    break;

                case "down":
                    y += magnitude;
                    break;
            }
        }

        return x * y;
    }

    public static int Part2()
    {
        var x = 0;
        var y = 0;
        var aim = 0;

        foreach (var line in Input)
        {
            var values = line.Split(' ');
            var direction = values[0];
            var magnitude = int.Parse(values[1]);

            switch (direction)
            {
                case "forward":
                    x += magnitude;
                    y += aim * magnitude;
                    break;

                case "up":
                    aim -= magnitude;
                    break;

                case "down":
                    aim += magnitude;
                    break;
            }
        }

        return x * y;
    }
}
