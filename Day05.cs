using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc_2021_csharp
{
    public class Day05
    {
        private static readonly string INPUT_FILE = "input/day05.txt";
        private static readonly string[] input = System.IO.File.ReadAllLines(INPUT_FILE);

        public void Part1()
        {
            Console.WriteLine($"Day 05, Part 1: {Solve(Part.One)}");
        }

        public void Part2()
        {
            Console.WriteLine($"Day 05, Part 2: {Solve(Part.Two)}");
        }

        private int Solve(Part part)
        {
            var points = new List<Coordinate>();

            foreach (var line in input)
            {
                var values = line.Split(" -> ");
                var start = values[0].Split(',').Select(x => int.Parse(x)).ToArray();
                var end = values[1].Split(',').Select(x => int.Parse(x)).ToArray();

                PopulatePoints(part, new Coordinate(start[0], start[1]), new Coordinate(end[0], end[1]), points);
            }

            var query = points.GroupBy(p => new { p.X, p.Y })
                .Select(g => new
                {
                    Point = new Coordinate(g.Key.X, g.Key.Y),
                    Count = g.Count()
                });

            return query.Count(x => x.Count > 1);
        }

        private class Coordinate
        {
            public Coordinate(int x, int y)
            {
                X = x;
                Y = y;
            }

            public int X { get; set; }
            public int Y { get; set; }
        }

        private void PopulatePoints(Part part, Coordinate start, Coordinate end, List<Coordinate> points)
        {
            var minX = Math.Min(start.X, end.X);
            var maxX = Math.Max(start.X, end.X);
            var minY = Math.Min(start.Y, end.Y);
            var maxY = Math.Max(start.Y, end.Y);

            if (minX == maxX || minY == maxY)
            {
                // populate horizontal and vertical lines
                for (var i = minX; i <= maxX; i++)
                {
                    for (var j = minY; j <= maxY; j++)
                    {
                        points.Add(new Coordinate(i, j));
                    }
                }
            }
            else
            {
                // for part 1, we are supposed to only consider horizontal and vertical lines
                if (part == Part.One)
                {
                    return;
                }

                // all diagonal lines are guaranteed to be exactly 45 degrees
                for (int i = 0; i <= maxX - minX; i++)
                {
                    var x = start.X + i * (end.X - start.X > 0 ? 1 : -1);
                    var y = start.Y + i * (end.Y - start.Y > 0 ? 1 : -1);

                    points.Add(new Coordinate(x, y));
                }
            }
        }

        enum Part
        {
            One = 1,
            Two = 2
        }
    }
}
