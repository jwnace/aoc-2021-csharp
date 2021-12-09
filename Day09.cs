using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc_2021_csharp
{
    public class Day09
    {
        private static readonly string INPUT_FILE = "input/day09.txt";
        private static readonly string[] input = System.IO.File.ReadAllLines(INPUT_FILE);

        public void Part1()
        {
            var sum = 0;

            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    // h is the height at the current location, n1..n4 are the heights at the 4 neighbor locations
                    var h = input[i][j].ToInt();
                    var n1 = i - 1 >= 0 ? input[i - 1][j].ToInt() : int.MaxValue;
                    var n2 = i + 1 < input.Length ? input[i + 1][j].ToInt() : int.MaxValue;
                    var n3 = j - 1 >= 0 ? input[i][j - 1].ToInt() : int.MaxValue;
                    var n4 = j + 1 < input[i].Length ? input[i][j + 1].ToInt() : int.MaxValue;

                    if (h < n1 && h < n2 && h < n3 && h < n4)
                    {
                        sum += h + 1;
                    }
                }
            }

            Console.WriteLine($"Day 09, Part 1: {sum}");
        }

        public void Part2()
        {
            var basinSizes = new List<int>();

            // find all the low points
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    // h is the height at the current location, n1..n4 are the heights at the 4 neighbor locations
                    var h = input[i][j].ToInt();
                    var n1 = i - 1 >= 0 ? input[i - 1][j].ToInt() : int.MaxValue;
                    var n2 = i + 1 < input.Length ? input[i + 1][j].ToInt() : int.MaxValue;
                    var n3 = j - 1 >= 0 ? input[i][j - 1].ToInt() : int.MaxValue;
                    var n4 = j + 1 < input[i].Length ? input[i][j + 1].ToInt() : int.MaxValue;

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

            Console.WriteLine($"Day 09, Part 2: {result}");
        }

        private int CalculateBasinSize(Location point)
        {
            return CalculateBasinSize(point, new List<Location>());
        }

        private int CalculateBasinSize(Location point, List<Location> alreadyChecked)
        {
            if (point.Row < 0 || point.Row >= input.Length || point.Col < 0 || point.Col >= input[0].Length)
                return 0;

            if (input[point.Row][point.Col].ToInt() == 9)
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

    public static class Extensions
    {
        public static int ToInt(this char c) => int.Parse(c.ToString());
    }
}
