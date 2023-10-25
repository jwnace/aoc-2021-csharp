using System.Linq;

namespace aoc_2021_csharp.Day03;

public static class Day03
{
    private static readonly string[] Input = File.ReadAllLines("Day03/day03.txt");

    public static int Part1()
    {
        var gamma = "";
        var epsilon = "";

        for (var i = 0; i < Input[0].Length; i++)
        {
            var count0 = Input.Count(x => x[i] == '0');
            var count1 = Input.Count(x => x[i] == '1');

            gamma += (count1 > count0) ? '1' : '0';
            epsilon += (count1 > count0) ? '0' : '1';
        }

        var g = Convert.ToInt32(gamma, 2);
        var e = Convert.ToInt32(epsilon, 2);

        return g * e;
    }

    public static int Part2()
    {
        var oxygen = CalculateOxygenGeneratorRating();
        var co2 = CalculateCo2ScrubberRating();

        return oxygen * co2;
    }

    private static int CalculateOxygenGeneratorRating()
    {
        var temp = Input.ToList();

        for (var i = 0; i < Input[0].Length; i++)
        {
            var count0 = temp.Count(x => x[i] == '0');
            var count1 = temp.Count(x => x[i] == '1');

            if (count1 >= count0)
            {
                temp.RemoveAll(x => x[i] == '0');
            }
            else
            {
                temp.RemoveAll(x => x[i] == '1');
            }

            if (temp.Count == 1)
            {
                break;
            }
        }

        return temp.Select(x => Convert.ToInt32(x, 2)).Single();
    }

    private static int CalculateCo2ScrubberRating()
    {
        var temp = Input.ToList();

        for (var i = 0; i < Input[0].Length; i++)
        {
            var count0 = temp.Count(x => x[i] == '0');
            var count1 = temp.Count(x => x[i] == '1');

            if (count1 >= count0)
            {
                temp.RemoveAll(x => x[i] == '1');
            }
            else
            {
                temp.RemoveAll(x => x[i] == '0');
            }

            if (temp.Count() == 1)
            {
                break;
            }
        }

        return temp.Select(x => Convert.ToInt32(x, 2)).Single();
    }
}
