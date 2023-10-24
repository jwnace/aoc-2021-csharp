using System.Collections.Generic;
using System.Linq;

namespace aoc_2021_csharp.Day19;

public static class Day19
{
    private static readonly string Input = File.ReadAllText("Day19/day19.txt").Trim();

    public static int Part1() => Solve1(Input);

    public static int Part2() => Solve2(Input);

    private static int Solve1(string input)
    {
        var scanners = ParseScanners(input);
        CalculateDistances(scanners);

        foreach (var scanner in scanners)
        {
            foreach (var other in scanners)
            {
                if (scanner == other)
                {
                    continue;
                }

                var commonBeacons = GetCommonBeacons(scanner, other);

                if (commonBeacons.Count >= 12)
                {
                    var positionOfOther = CalculatePositionOfScanner(knownScanner: scanner, scannerToLocate: other, commonBeacons);
                }
            }
        }

        return -1;
    }

    private static int Solve2(string input)
    {
        throw new NotImplementedException();
    }

    private static (int Orientation, Position Position) CalculatePositionOfScanner(
        Scanner knownScanner,
        Scanner scannerToLocate,
        List<(Scanner Scanner1, Scanner Scanner2, Position Position1, Position Position2)> commonBeacons)
    {
        if (knownScanner.Position is null)
        {
            throw new Exception("knownScanner.Position is null");
        }

        var foo = commonBeacons.Select(x => (x.Position1, TransformedPositions: GetTransformedPositions(x.Position2))).ToArray();

        for (var i = 0; i < foo.Length; i++)
        {
            var bar = foo.Select(f => (f.Position1, f.TransformedPositions.ElementAt(i)));

            var baz = bar.Select(b => b.Position1 + b.Item2).Distinct().ToList();

            if (baz.Count == 1)
            {
                return (i, knownScanner.Position + baz.First());
            }
        }

        throw new Exception("We couldn't find the position of Scanner2!");
    }

    private static HashSet<Position> GetTransformedPositions(Position position)
    {
        var transformedPositions = new HashSet<Position>();

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
                                var transformedPosition = position
                                    .RotateX(xRot)
                                    .RotateY(yRot)
                                    .RotateZ(zRot)
                                    .FlipX(xFlip)
                                    .FlipY(yFlip)
                                    .FlipZ(zFlip);

                                transformedPositions.Add(transformedPosition);
                            }
                        }
                    }
                }
            }
        }

        return transformedPositions;
    }

    private static List<(Scanner Scanner1, Scanner Scanner2, Position Position1, Position Position2)> GetCommonBeacons(
        Scanner scanner, Scanner other)
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

                // TODO: why does 10 work?
                if (beacon.Distances.Intersect(otherBeacon.Distances).Count() >= 10)
                {
                    commonBeacons.Add((scanner, other, beacon.Position, otherBeacon.Position));
                }
            }
        }

        return commonBeacons;
    }

    private static void CalculateDistances(List<Scanner> scanners)
    {
        foreach (var scanner in scanners)
        {
            foreach (var beacon in scanner.Beacons)
            {
                foreach (var other in scanner.Beacons)
                {
                    if (beacon == other)
                    {
                        continue;
                    }

                    var distance = Math.Abs(beacon.Position.X - other.Position.X) +
                                   Math.Abs(beacon.Position.Y - other.Position.Y) +
                                   Math.Abs(beacon.Position.Z - other.Position.Z);

                    beacon.Distances.Add(distance);
                }
            }
        }
    }

    private static List<Scanner> ParseScanners(string input) => input.Split("\n\n").Select(Scanner.Parse).ToList();
}
