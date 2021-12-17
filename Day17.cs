using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc_2021_csharp
{
    public class Day17
    {
        private static readonly string INPUT_FILE = "input/day17.txt";
        private static readonly string input = System.IO.File.ReadAllText(INPUT_FILE);

        public void Part1()
        {
            var a = input.IndexOf("x=") + 2;
            var b = input.IndexOf(",");
            var c = input.IndexOf("y=") + 2;

            var xRange = input.Substring(a, b - a);
            var yRange = input.Substring(c);

            var xValues = xRange.Split("..");
            var minX = int.Parse(xValues[0]);
            var maxX = int.Parse(xValues[1]);

            var yValues = yRange.Split("..");
            var minY = int.Parse(yValues[0]);
            var maxY = int.Parse(yValues[1]);

            var maxHeight = 0;

            for (int x = 0; x < 500; x++)
            {
                for (int y = -500; y < 500; y++)
                {
                    var position = (x: 0, y: 0);
                    var initialVelocity = (x, y);
                    var velocity = initialVelocity;
                    var h = 0;

                    for (int step = 0; step < 500; step++)
                    {
                        position.x += velocity.x;
                        position.y += velocity.y;
                        velocity.x += velocity.x > 0 ? -1 : velocity.x < 0 ? 1 : 0;
                        velocity.y--;

                        if (position.y > h)
                        {
                            h = position.y;
                        }

                        if (position.x >= minX && position.x <= maxX && position.y >= minY && position.y <= maxY)
                        {
                            if (h > maxHeight)
                            {
                                maxHeight = h;
                            }
                        }
                    }
                }
            }

            Console.WriteLine($"Day 17, Part 1: {maxHeight}");
        }

        public void Part2()
        {
            var a = input.IndexOf("x=") + 2;
            var b = input.IndexOf(",");
            var c = input.IndexOf("y=") + 2;

            var xRange = input.Substring(a, b - a);
            var yRange = input.Substring(c);

            var xValues = xRange.Split("..");
            var minX = int.Parse(xValues[0]);
            var maxX = int.Parse(xValues[1]);

            var yValues = yRange.Split("..");
            var minY = int.Parse(yValues[0]);
            var maxY = int.Parse(yValues[1]);

            var hits = new List<ValueTuple<int, int>>();

            for (int x = 0; x < 500; x++)
            {
                for (int y = -500; y < 500; y++)
                {
                    var position = (x: 0, y: 0);
                    var initialVelocity = (x, y);
                    var velocity = initialVelocity;

                    for (int step = 0; step < 500; step++)
                    {
                        position.x += velocity.x;
                        position.y += velocity.y;
                        velocity.x += velocity.x > 0 ? -1 : velocity.x < 0 ? 1 : 0;
                        velocity.y--;

                        if (position.x >= minX && position.x <= maxX && position.y >= minY && position.y <= maxY)
                        {
                            hits.Add(initialVelocity);
                        }
                    }
                }
            }

            var answer = hits.GroupBy(x => x).Count();

            Console.WriteLine($"Day 17, Part 2: {answer}");
        }
    }
}
