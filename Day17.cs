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
            // process input
            var a = input.IndexOf('=') + 1;
            var b = input.IndexOf(',');
            var c = input.Substring(b + 1).IndexOf('=') + 1;

            var x = input.Substring(a, b - a);
            var y = input.Substring(b + 1 + c);

            var xValues = x.Split("..");
            var minX = int.Parse(xValues[0]);
            var maxX = int.Parse(xValues[1]);

            var yValues = y.Split("..");
            var minY = int.Parse(yValues[0]);
            var maxY = int.Parse(yValues[1]);

            // calculate initial x velocity
            var total = 0;
            var expectedSteps = 1;

            while (total < minX)
            {
                total += expectedSteps;
                expectedSteps++;
            }

            var initialVelocity = (x: expectedSteps - 1, y: 1);

            // placeholders
            var maxHeight = 0;
            var maxVelocity = (0, 0);
            var previouslyHit = false;
            var tries = 0;

            // do the simulation
            while (tries < 500) // (!done)
            {
                var hit = false;
                var position = (x: 0, y: 0);
                var velocity = initialVelocity;
                var h = 0;

                for (var step = 0; step < expectedSteps * 10; step++)
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
                            maxVelocity = initialVelocity;
                        }

                        hit = true;
                    }
                }

                if (hit == true && previouslyHit == false)
                {
                    previouslyHit = true;
                }

                initialVelocity.y++;
                tries++;
            }

            Console.WriteLine($"Day 17, Part 1: maxHeight: {maxHeight} @ velocity: {maxVelocity}");
        }

        public void Part2()
        {
            // process input
            var a = input.IndexOf('=') + 1;
            var b = input.IndexOf(',');
            var c = input.Substring(b + 1).IndexOf('=') + 1;

            var x = input.Substring(a, b - a);
            var y = input.Substring(b + 1 + c);

            var xValues = x.Split("..");
            var minX = int.Parse(xValues[0]);
            var maxX = int.Parse(xValues[1]);

            var yValues = y.Split("..");
            var minY = int.Parse(yValues[0]);
            var maxY = int.Parse(yValues[1]);

            var hits = new List<ValueTuple<int, int>>();

            // do the simulation
            for (int i = 0; i < 500; i++)
            {
                for (int j = -500; j < 500; j++)
                {
                    var startingVelocity = (x: i, y: j);
                    var velocity = startingVelocity;
                    var position = (x: 0, y: 0);

                    for (var step = 0; step < 500; step++)
                    {
                        position.x += velocity.x;
                        position.y += velocity.y;
                        velocity.x += velocity.x > 0 ? -1 : velocity.x < 0 ? 1 : 0;
                        velocity.y--;

                        if (position.x >= minX && position.x <= maxX && position.y >= minY && position.y <= maxY)
                        {
                            hits.Add(startingVelocity);
                        }
                    }
                }
            }

            var query = hits.GroupBy(x => x).Select(g => new { g.Key, Count = g.Count() });

            Console.WriteLine($"Day 17, Part 2: {query.Count()}");
        }
    }
}
