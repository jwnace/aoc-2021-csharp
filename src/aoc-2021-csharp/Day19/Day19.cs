using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace aoc_2021_csharp.Day19;

public static class Day19
{
    private static readonly string[] Input = File.ReadAllLines("Day19/day19.txt");

    public static int Part1()
    {
        var scanners = new List<Scanner>();

        // populate scanners and the beacons they can detect from the input
        foreach (var line in Input)
        {
            if (line.Contains("---"))
            {
                scanners.Add(new Scanner { Id = int.Parse(line.Split(' ')[2]) });
                continue;
            }

            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            var values = line.Split(',').Select(x => int.Parse(x)).ToList();
            var x = values[0];
            var y = values[1];
            var z = values[2];

            var scanner = scanners.Last();
            scanner.Beacons.Add(new Beacon(x, y, z));
        }

        // populate distances to help find matches
        foreach (var scanner in scanners)
        {
            for (var i = 0; i < scanner.Beacons.Count - 1; i++)
            {
                for (var j = i + 1; j < scanner.Beacons.Count; j++)
                {
                    var a = scanner.Beacons[i];
                    var b = scanner.Beacons[j];

                    var distance = Math.Sqrt(Math.Pow(a.Position.X - b.Position.X, 2) + Math.Pow(a.Position.Y - b.Position.Y, 2) + Math.Pow(a.Position.Z - b.Position.Z, 2));

                    a.Distances.Add(distance);
                    b.Distances.Add(distance);
                }
            }
        }

        // look for overlapping beacons, based on the distances we calculated
        for (var i = 0; i < scanners.Count() - 1; i++)
        {
            for (var j = i + 1; j < scanners.Count(); j++)
            {
                var a = scanners[i];
                var b = scanners[j];

                var q1 = a.Beacons.SelectMany(x => x.Distances)
                    .Intersect(b.Beacons.SelectMany(y => y.Distances))
                    .ToList();

                var q2 = a.Beacons.Where(x =>
                    b.Beacons.Any(y => x.Distances.Intersect(y.Distances).Count() > 10)
                ).ToList();

                if (q2.Count() > 0)
                {
                    // Console.WriteLine($"scanner {a.Id} & scanner {b.Id}");
                    // Console.WriteLine($"common beacons: {q2.Count()}");
                }

                // `q2` appears to successfully find common beacons between two scanners `a` and `b`

                // this loop pairs beacons between scanner `a` and `b` based on having 11+ distances in common
                for (var k = 0; k < a.Beacons.Count; k++)
                {
                    for (var l = 0; l < b.Beacons.Count; l++)
                    {
                        var c = a.Beacons[k];
                        var d = b.Beacons[l];

                        var query = a.Beacons[k].Distances
                            .Intersect(b.Beacons[l].Distances)
                            .ToList();

                        // TODO: try every rotation & translation until the beacons line up perfectly

                        if (query.Count > 10)
                        {
                            //Console.WriteLine($"scanner {a.Id} & scanner {b.Id}");
                            // Console.WriteLine($"common beacon: a -> {c.Position}, b -> {d.Position}");
                        }
                    }
                }
            }
        }

        return 1;
    }

    public static int Part2() => 2;

    private class Scanner
    {
        public int Id { get; set; }
        public List<Beacon> Beacons { get; set; } = new List<Beacon>();
    }

    private class Beacon
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Point Position { get; set; }
        public List<double> Distances { get; set; } = new List<double>();

        public Beacon(int x, int y, int z)
        {
            Position = new Point(x, y, z);
        }
    }

    private class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }

        public Point(int x, int y, int z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public override string ToString()
        {
            return $"({X}, {Y}, {Z})";
        }
    }
}
