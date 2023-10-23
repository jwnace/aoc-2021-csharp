using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace aoc_2021_csharp.Day08;

public static class Day08
{
    private static readonly string[] Input = File.ReadAllLines("Day08/day08.txt");

    public static int Part1()
    {
        var sum = 0;

        foreach (var line in Input)
        {
            var values = line.Split(" | ");
            var digits = values[1].Split(' ');
            sum += digits.Count(x => new List<int> { 2, 3, 4, 7 }.Contains(x.Length));
        }

        return sum;
    }

    public static int Part2()
    {
        var sum = 0;

        foreach (var line in Input)
        {
            var values = line.Split(" | ");
            var patterns = values[0].Split(' ').Select(x => x.Sort());
            var digits = values[1].Split(' ').Select(x => x.Sort());
            var dict = new Dictionary<string, int>();

            var one = patterns.Single(x => x.Length == 2);
            dict[one] = 1;
            var seven = patterns.Single(x => x.Length == 3);
            dict[seven] = 7;
            var four = patterns.Single(x => x.Length == 4);
            dict[four] = 4;
            var eight = patterns.Single(x => x.Length == 7);
            dict[eight] = 8;
            var three = patterns.Single(x => x.Length == 5 && x.Intersect(one).Count() == 2);
            dict[three] = 3;
            var two = patterns.Single(x => x.Length == 5 && x.Intersect(four).Count() == 2);
            dict[two] = 2;
            var five = patterns.Single(x => x.Length == 5 && !dict.ContainsKey(x));
            dict[five] = 5;
            var six = patterns.Single(x => x.Length == 6 && x.Intersect(one).Count() != 2);
            dict[six] = 6;
            var nine = patterns.Single(x => x.Length == 6 && x.Intersect(four).Count() == 4);
            dict[nine] = 9;
            var zero = patterns.Single(x => !dict.ContainsKey(x));
            dict[zero] = 0;

            var outputString = "";

            foreach (var digit in digits)
            {
                outputString += dict[digit];
            }

            sum += int.Parse(outputString);
        }

        return sum;
    }
}

public static class ExtensionMethods
{
    public static string Sort(this string str)
    {
        return new string(str.OrderBy(c => c).ToArray());
    }
}
