using System.Collections.Generic;
using System.Linq;

namespace aoc_2021_csharp.Day17;

public static class Day17
{
    private static readonly string Input = File.ReadAllText("Day17/day17.txt");

    public static int Part1()
    {
        var a = Input.IndexOf("x=") + 2;
        var b = Input.IndexOf(",");
        var c = Input.IndexOf("y=") + 2;

        var xRange = Input.Substring(a, b - a);
        var yRange = Input.Substring(c);

        var xValues = xRange.Split("..");
        var minX = int.Parse(xValues[0]);
        var maxX = int.Parse(xValues[1]);

        var yValues = yRange.Split("..");
        var minY = int.Parse(yValues[0]);
        var maxY = int.Parse(yValues[1]);

        var maxHeight = 0;

        for (var x = 0; x < 500; x++)
        {
            for (var y = -500; y < 500; y++)
            {
                var position = (x: 0, y: 0);
                var initialVelocity = (x, y);
                var velocity = initialVelocity;
                var h = 0;

                for (var step = 0; step < 500; step++)
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

        return maxHeight;
    }

    public static int Part2()
    {
        var a = Input.IndexOf("x=") + 2;
        var b = Input.IndexOf(",");
        var c = Input.IndexOf("y=") + 2;

        var xRange = Input.Substring(a, b - a);
        var yRange = Input.Substring(c);

        var xValues = xRange.Split("..");
        var minX = int.Parse(xValues[0]);
        var maxX = int.Parse(xValues[1]);

        var yValues = yRange.Split("..");
        var minY = int.Parse(yValues[0]);
        var maxY = int.Parse(yValues[1]);

        var hits = new List<ValueTuple<int, int>>();

        for (var x = 0; x < 500; x++)
        {
            for (var y = -500; y < 500; y++)
            {
                var position = (x: 0, y: 0);
                var initialVelocity = (x, y);
                var velocity = initialVelocity;

                for (var step = 0; step < 500; step++)
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

        return answer;
    }
}
