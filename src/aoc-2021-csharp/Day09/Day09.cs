using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace aoc_2021_csharp.Day09;

public static class Day09
{
    private static readonly string[] Input = File.ReadAllLines("Day09/day09.txt");

    public static int Part1()
    {
        var sum = 0;

        for (var i = 0; i < Input.Length; i++)
        {
            for (var j = 0; j < Input[i].Length; j++)
            {
                // h is the height at the current location, n1..n4 are the heights at the 4 neighbor locations
                var h = Input[i][j].ToInt();
                var n1 = i - 1 >= 0 ? Input[i - 1][j].ToInt() : int.MaxValue;
                var n2 = i + 1 < Input.Length ? Input[i + 1][j].ToInt() : int.MaxValue;
                var n3 = j - 1 >= 0 ? Input[i][j - 1].ToInt() : int.MaxValue;
                var n4 = j + 1 < Input[i].Length ? Input[i][j + 1].ToInt() : int.MaxValue;

                if (h < n1 && h < n2 && h < n3 && h < n4)
                {
                    sum += h + 1;
                }
            }
        }

        return sum;
    }

    public static int Part2()
    {
        var basinSizes = new List<int>();

        // find all the low points
        for (var i = 0; i < Input.Length; i++)
        {
            for (var j = 0; j < Input[i].Length; j++)
            {
                // h is the height at the current location, n1..n4 are the heights at the 4 neighbor locations
                var h = Input[i][j].ToInt();
                var n1 = i - 1 >= 0 ? Input[i - 1][j].ToInt() : int.MaxValue;
                var n2 = i + 1 < Input.Length ? Input[i + 1][j].ToInt() : int.MaxValue;
                var n3 = j - 1 >= 0 ? Input[i][j - 1].ToInt() : int.MaxValue;
                var n4 = j + 1 < Input[i].Length ? Input[i][j + 1].ToInt() : int.MaxValue;

                if (h < n1 && h < n2 && h < n3 && h < n4)
                {
                    // calculate the basin size for each low point
                    basinSizes.Add(CalculateBasinSize(new Location(i,j)));
                }
            }
        }

        // find the 3 largest basin sizes and multiply them together
        var top3 = basinSizes.OrderByDescending(x => x).Take(3).ToList();
        var result = top3[0] * top3[1] * top3[2];

        return result;
    }

    private static int CalculateBasinSize(Location point)
    {
        return CalculateBasinSize(point, new List<Location>());
    }

    private static int CalculateBasinSize(Location point, List<Location> alreadyChecked)
    {
        if (point.Row < 0 || point.Row >= Input.Length || point.Col < 0 || point.Col >= Input[0].Length)
            return 0;

        if (Input[point.Row][point.Col].ToInt() == 9)
            return 0;

        if (alreadyChecked.Any(x => x.Row == point.Row && x.Col == point.Col))
            return 0;

        alreadyChecked.Add(point);

        var up = CalculateBasinSize(new Location(point.Row - 1, point.Col), alreadyChecked);
        var down = CalculateBasinSize(new Location(point.Row + 1, point.Col), alreadyChecked);
        var left = CalculateBasinSize(new Location(point.Row, point.Col - 1), alreadyChecked);
        var right = CalculateBasinSize(new Location(point.Row, point.Col + 1), alreadyChecked);

        return 1 + up + down + left + right;
    }

    class Location
    {
        public Location(int row = 0, int col = 0)
        {
            Row = row;
            Col = col;
        }

        public int Row { get; set; }
        public int Col { get; set; }
    }
}

public static class CharExtensions
{
    public static int ToInt(this char c) => int.Parse(c.ToString());
}
