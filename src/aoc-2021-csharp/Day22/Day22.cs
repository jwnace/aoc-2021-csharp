using System.Collections.Generic;
using System.Linq;

namespace aoc_2021_csharp.Day22;

public static class Day22
{
    private static readonly string[] Input = File.ReadAllLines("Day22/day22.txt");

    public static int Part1()
    {
        var cuboids = new Dictionary<(int x1, int x2, int y1, int y2, int z1, int z2), bool>();

        foreach (var line in Input)
        {
            var values = line.Split(' ');
            var on = values[0] == "on";
            var ranges = values[1].Split(',')
                .Select(x => x.Substring(2))
                .SelectMany(x => x.Split(".."))
                .Select(int.Parse)
                .ToList();

            if (ranges.Any(x => x < -50 || x > 50))
            {
                continue;
            }

            cuboids.Add((ranges[0], ranges[1], ranges[2], ranges[3], ranges[4], ranges[5]), on);
        }

        var cubes = new Dictionary<(int, int, int), bool>();

        foreach (var cuboid in cuboids)
        {
            var range = cuboid.Key;

            for (var i = range.x1; i <= range.x2; i++)
            {
                for (var j = range.y1; j <= range.y2; j++)
                {
                    for (var k = range.z1; k <= range.z2; k++)
                    {
                        cubes[(i, j, k)] = cuboid.Value;
                    }
                }
            }
        }

        return cubes.Count(x => x.Value);
    }

    public static long Part2()
    {
        var cuboids = new List<Cuboid>();

        foreach (var line in Input)
        {
            var values = line.Split(' ');
            var on = values[0] == "on";
            var ranges = values[1].Split(',')
                .Select(x => x.Substring(2))
                .SelectMany(x => x.Split(".."))
                .Select(int.Parse)
                .ToList();

            var b = new Cuboid(ranges[0], ranges[1], ranges[2], ranges[3], ranges[4], ranges[5], on);
            cuboids.Add(b);
        }

        var volumes = new List<Cuboid>();

        cuboids.ForEach(cuboid =>
        {
            var query = volumes.Where(x => x.IntersectsWith(cuboid)).Select(x => x.GetIntersection(cuboid)).ToList();

            volumes.AddRange(query);

            if (cuboid.On)
            {
                volumes.Add(cuboid);
            }
        });

        return volumes.Sum(x => x.Volume());
    }

    private class Cuboid
    {
        public int X1 { get; set; }
        public int X2 { get; set; }
        public int Y1 { get; set; }
        public int Y2 { get; set; }
        public int Z1 { get; set; }
        public int Z2 { get; set; }
        public bool On { get; set; }

        private Cuboid() { }

        public Cuboid(int x1, int x2, int y1, int y2, int z1, int z2, bool on)
        {
            X1 = x1;
            X2 = x2;
            Y1 = y1;
            Y2 = y2;
            Z1 = z1;
            Z2 = z2;
            On = on;
        }

        public bool IntersectsWith(Cuboid b)
        {
            var a = this;

            return a.X1 <= b.X2 && a.X2 >= b.X1 &&
                   a.Y1 <= b.Y2 && a.Y2 >= b.Y1 &&
                   a.Z1 <= b.Z2 && a.Z2 >= b.Z1;
        }

        public Cuboid GetIntersection(Cuboid b)
        {
            var a = this;

            if (!a.IntersectsWith(b))
            {
                throw new Exception("The two cuboids do not intersect.");
            }

            return new Cuboid
            {
                X1 = Math.Max(a.X1, b.X1), X2 = Math.Min(a.X2, b.X2),
                Y1 = Math.Max(a.Y1, b.Y1), Y2 = Math.Min(a.Y2, b.Y2),
                Z1 = Math.Max(a.Z1, b.Z1), Z2 = Math.Min(a.Z2, b.Z2),
                On = !On
            };
        }

        public long Volume()
        {
            return (X2 - X1 + 1L) * (Y2 - Y1 + 1L) * (Z2 - Z1 + 1L) * (On ? 1 : -1);
        }
    }
}
