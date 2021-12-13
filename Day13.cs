using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc_2021_csharp
{
    public class Day13
    {
        private static readonly string INPUT_FILE = "input/day13.txt";
        private static readonly string[] input = System.IO.File.ReadAllLines(INPUT_FILE);

        public void Part1()
        {
            Run(1);
        }

        public void Part2()
        {
            Run(2);
        }

        void Run(int part)
        {
            var grid = new Grid();
            var folds = new List<Point>();

            foreach (var line in input)
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

            for (int i = 0; i < folds.Count(); i++)
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
                    var answer = grid.Points.GroupBy(p => new { p.X, p.Y }).Count();
                    Console.WriteLine($"Day 13, Part 1: {answer}");
                    return;
                }
            }

            if (part == 2)
            {
                Console.WriteLine($"Day 13, Part 2: ");
                Console.WriteLine(grid);
            }
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

                for (int row = 0; row <= this.Points.Max(p => p.Y); row++)
                {
                    for (int col = 0; col <= this.Points.Max(p => p.X); col++)
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

                    result += Environment.NewLine;
                }

                return result;
            }
        }
    }
}
