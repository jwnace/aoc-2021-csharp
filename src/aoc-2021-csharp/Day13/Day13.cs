using System.Collections.Generic;
using System.Linq;

namespace aoc_2021_csharp.Day13;

public static class Day13
{
    private static readonly string[] Input = File.ReadAllLines("Day13/day13.txt");

    public static int Part1() => Run(1).Count;

    public static string Part2() => Run(2).Text;

    private static (int Count, string Text) Run(int part)
    {
        var grid = new Grid();
        var folds = new List<Point>();

        foreach (var line in Input)
        {
            if (line.Contains(','))
            {
                var values = line.Split(',');
                var x = int.Parse(values[0]);
                var y = int.Parse(values[1]);
                grid.Points.Add(new Point(x, y));
            }
            else if (line.Contains("fold"))
            {
                var values = line.Split('=');
                var axis = values[0].Substring(values[0].Length - 1);
                var position = int.Parse(values[1]);
                var point = axis == "x" ? new Point(position, 0) : new Point(0, position);
                folds.Add(point);
            }
        }

        for (var i = 0; i < folds.Count(); i++)
        {
            var fold = folds[i];

            if (fold.X > 0)
            {
                grid.Points.Where(p => p.X > fold.X)
                    .ToList()
                    .ForEach(p => { p.X = fold.X - (p.X - fold.X); });
            }
            else
            {
                grid.Points.Where(p => p.Y > fold.Y)
                    .ToList()
                    .ForEach(p => { p.Y = fold.Y - (p.Y - fold.Y); });
            }

            if (part == 1 && i == 0)
            {
                return (grid.Points.GroupBy(p => new { p.X, p.Y }).Count(), "");
            }
        }

        if (part == 2)
        {
            return (-1, grid.ToString());
        }

        throw new Exception("No solution found!");
    }

    class Point
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }
    }

    class Grid
    {
        public List<Point> Points { get; set; } = new List<Point>();

        public override string ToString()
        {
            var result = "";

            for (var row = 0; row <= this.Points.Max(p => p.Y); row++)
            {
                result += Environment.NewLine;

                for (var col = 0; col <= this.Points.Max(p => p.X); col++)
                {
                    if (this.Points.Any(p => p.X == col && p.Y == row))
                    {
                        result += "#";
                    }
                    else
                    {
                        result += " ";
                    }
                }
            }

            return result;
        }
    }
}
