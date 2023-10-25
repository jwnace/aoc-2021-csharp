using System.Collections.Generic;
using System.Linq;

namespace aoc_2021_csharp.Day19;

public static class Day19
{
    private static readonly string Input = File.ReadAllText("Day19/day19.txt").Trim();

    public static int Part1() => Solve1(Input);

    public static int Part2() => Solve2(Input);

    public static int Solve1(string input)
    {
        var scanners = ParseScanners(input);
        var beaconDistances = CalculateDistances(scanners);
        // var distinctBeacons = new HashSet<Position>();
        // scanners[0].Beacons.ForEach(b => distinctBeacons.Add(b.Position));

        var scanner = scanners[0];
        // foreach (var scanner in scanners)
        while (scanners.Any(x => x.Position is null))
        {
            foreach (var other in scanners)
            {
                // don't compare a scanner to itself
                if (scanner == other)
                {
                    continue;
                }

                var commonBeacons = GetCommonBeacons(scanner, other, beaconDistances);

                // only consider pairs of scanners that have at least 12 beacons in common
                if (commonBeacons.Count < 12)
                {
                    continue;
                }

                // Console.WriteLine($"Scanner {scanner.Id} has {commonBeacons.Count} beacons in common with scanner {other.Id}");

                // if the other scanner already has a position, skip it
                if (other.Position is not null)
                {
                    continue;
                }

                var success = TryCalculatePositionOfScanner(
                    knownScanner: scanner,
                    scannerToLocate: other,
                    commonBeacons,
                    out var orientation,
                    out var transformation,
                    out var positionOfOther);

                if (success)
                {
                    other.Position = positionOfOther;

                    // Console.WriteLine($"Adding beacons from scanner {other.Id} to scanner {scanner.Id}");

                    // transform all of the beacons known to scanner 2 to match scanner 1
                    foreach (var beacon in other.Beacons)
                    {
                        var p5 = other.Position! - beacon.Position.Transform(transformation);

                        // if (distinctBeacons.Add(p5))
                        // {
                            // throw new Exception($"Failed to add beacon at {beacon.Position.Transform(reverse)}");
                            scanner.Beacons.Add(new Beacon(p5));
                        // }
                    }

                    beaconDistances = CalculateDistances(scanners);

                    continue;
                }
                else
                {
                    // Console.WriteLine($"Failed to calculate position of scanner {other.Id}");
                    // Console.WriteLine(string.Join("\n", distinctBeacons.Select(b => $"{b.X},{b.Y},{b.Z}")));
                    // return distinctBeacons.Count;
                }
            }
        }

        // Console.WriteLine(string.Join("\n", distinctBeacons.Select(b => $"{b.X},{b.Y},{b.Z}")));
        return scanners[0].Beacons.Count;
    }

    public static int Solve2(string input)
    {
        var scanners = ParseScanners(input);
        var beaconDistances = CalculateDistances(scanners);
        // var distinctBeacons = new HashSet<Position>();
        // scanners[0].Beacons.ForEach(b => distinctBeacons.Add(b.Position));

        var scanner = scanners[0];
        // foreach (var scanner in scanners)
        while (scanners.Any(x => x.Position is null))
        {
            foreach (var other in scanners)
            {
                // don't compare a scanner to itself
                if (scanner == other)
                {
                    continue;
                }

                var commonBeacons = GetCommonBeacons(scanner, other, beaconDistances);

                // only consider pairs of scanners that have at least 12 beacons in common
                if (commonBeacons.Count < 12)
                {
                    continue;
                }

                // Console.WriteLine($"Scanner {scanner.Id} has {commonBeacons.Count} beacons in common with scanner {other.Id}");

                // if the other scanner already has a position, skip it
                if (other.Position is not null)
                {
                    continue;
                }

                var success = TryCalculatePositionOfScanner(
                    knownScanner: scanner,
                    scannerToLocate: other,
                    commonBeacons,
                    out var orientation,
                    out var transformation,
                    out var positionOfOther);

                if (success)
                {
                    other.Position = positionOfOther;

                    // Console.WriteLine($"Adding beacons from scanner {other.Id} to scanner {scanner.Id}");

                    // transform all of the beacons known to scanner 2 to match scanner 1
                    foreach (var beacon in other.Beacons)
                    {
                        var p5 = other.Position! - beacon.Position.Transform(transformation);

                        // if (distinctBeacons.Add(p5))
                        // {
                            // throw new Exception($"Failed to add beacon at {beacon.Position.Transform(reverse)}");
                            scanner.Beacons.Add(new Beacon(p5));
                        // }
                    }

                    beaconDistances = CalculateDistances(scanners);

                    continue;
                }
                else
                {
                    // Console.WriteLine($"Failed to calculate position of scanner {other.Id}");
                    // Console.WriteLine(string.Join("\n", distinctBeacons.Select(b => $"{b.X},{b.Y},{b.Z}")));
                    // return distinctBeacons.Count;
                }
            }
        }

        // Console.WriteLine(string.Join("\n", distinctBeacons.Select(b => $"{b.X},{b.Y},{b.Z}")));
        return CalculateAnswerForPart2(scanners);
    }

    private static int CalculateAnswerForPart2(List<Scanner> scanners)
    {
        var max = 0;

        foreach (var scanner in scanners)
        {
            foreach (var other in scanners)
            {
                if (scanner == other)
                {
                    continue;
                }

                var distance = Math.Abs(scanner.Position!.X - other.Position!.X) +
                               Math.Abs(scanner.Position!.Y - other.Position!.Y) +
                               Math.Abs(scanner.Position!.Z - other.Position!.Z);

                max = Math.Max(distance, max);
            }
        }

        return max;
    }

    private static bool TryCalculatePositionOfScanner(
        Scanner knownScanner,
        Scanner scannerToLocate,
        List<(Scanner Scanner1, Scanner Scanner2, Position Position1, Position Position2)> commonBeacons,
        out int orientation,
        out Transformation? transformation,
        out Position? position)
    {
        if (knownScanner.Position is null)
        {
            // throw new Exception("knownScanner.Position is null");
            orientation = default;
            position = default;
            transformation = default;
            return false;
        }

        var foo = commonBeacons.Select(x => (x.Position1, x.Scanner1, TransformedPositions: GetTransformedPositions(x.Position2)))
            .ToArray();

        for (var i = 0; i < /*foo.Length*/512; i++)
        {
            var bar = foo.Select(f => (f.Position1, f.Scanner1, f.TransformedPositions.ElementAt(i)));

            var baz = bar.Select(b => (b.Position1 + b.Item3.transformedPosition, b.Item3.transformation))
                .Distinct()
                .ToList();

            if (baz.Count is > 1 and < 12)
            {
                // Console.WriteLine("break!");
            }

            if (baz.Count == 1)
            {
                orientation = i;
                position = knownScanner.Position + baz.First().Item1;
                transformation = baz.First().transformation;
                return true;
            }
        }

        orientation = default;
        position = default;
        transformation = default;
        return false;
    }

    private static HashSet<(Position transformedPosition, Transformation transformation)> GetTransformedPositions(
        Position position)
    {
        var transformedPositions = new HashSet<(Position transformedPosition, Transformation transformation)>();

        for (var xRot = 0; xRot < 4; xRot++)
        {
            for (var xFlip = 0; xFlip < 2; xFlip++)
            {
                for (var yRot = 0; yRot < 4; yRot++)
                {
                    for (var yFlip = 0; yFlip < 2; yFlip++)
                    {
                        for (var zRot = 0; zRot < 4; zRot++)
                        {
                            for (var zFlip = 0; zFlip < 2; zFlip++)
                            {
                                var transformation = new Transformation(xRot, xFlip, yRot, yFlip, zRot, zFlip);
                                var transformedPosition = position.Transform(transformation);

                                transformedPositions.Add((transformedPosition, transformation));
                            }
                        }
                    }
                }
            }
        }

        return transformedPositions;
    }

    private static List<(Scanner Scanner1, Scanner Scanner2, Position Position1, Position Position2)> GetCommonBeacons(
        Scanner scanner,
        Scanner other,
        Dictionary<Beacon, List<int>> beaconDistances)
    {
        var commonBeacons = new List<(Scanner Scanner1, Scanner Scanner2, Position Position1, Position Position2)>();

        foreach (var beacon in scanner.Beacons)
        {
            foreach (var otherBeacon in other.Beacons)
            {
                if (beacon == otherBeacon)
                {
                    throw new Exception("How is this possible?");
                }

                var intersection = beaconDistances[beacon].Intersect(beaconDistances[otherBeacon]).ToList();

                // TODO: why does 10 work?
                if (beaconDistances[beacon].Intersect(beaconDistances[otherBeacon]).Count() >= 10)
                {
                    commonBeacons.Add((scanner, other, beacon.Position, otherBeacon.Position));
                }
            }
        }

        return commonBeacons;
    }

    private static Dictionary<Beacon, List<int>> CalculateDistances(List<Scanner> scanners)
    {
        var beaconDistances = new Dictionary<Beacon, List<int>>();

        foreach (var scanner in scanners)
        {
            foreach (var beacon in scanner.Beacons)
            {
                var distances = new List<int>();

                foreach (var other in scanner.Beacons)
                {
                    if (beacon == other)
                    {
                        continue;
                    }

                    var distance = Math.Abs(beacon.Position.X - other.Position.X) +
                                   Math.Abs(beacon.Position.Y - other.Position.Y) +
                                   Math.Abs(beacon.Position.Z - other.Position.Z);

                    distances.Add(distance);
                }

                beaconDistances[beacon] = distances;
            }
        }

        return beaconDistances;
    }

    private static List<Scanner> ParseScanners(string input) => input.Split("\n\n").Select(Scanner.Parse).ToList();
}
