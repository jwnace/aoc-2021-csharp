using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace aoc_2021_csharp.Day19;

public static class Day19
{
    private static readonly string Input = File.ReadAllText("Day19/day19.txt").Trim();

    public static int Part1() => Solve1(Input);

    public static int Part2() => Solve2(Input);

    public static int Solve1(string input)
    {
        var scanners = GetScanners(input);
        return CountBeacons(scanners);
    }

    public static int Solve2(string input)
    {
        var scanners = GetScanners(input);
        return GetMaximumDistance(scanners);
    }

    private static List<Scanner> GetScanners(string input)
    {
        var scanners = ParseScanners(input);
        var beaconDistances = CalculateDistances(scanners);
        var knownScanner = scanners.Single(s => s.Id == 0);

        while (AnyScannerHasAnUnknownPosition(scanners))
        {
            foreach (var other in scanners)
            {
                if (knownScanner == other || other.Position is not null)
                {
                    continue;
                }

                var commonBeacons = GetCommonBeacons(knownScanner, other, beaconDistances).ToList();

                if (commonBeacons.Count < 12)
                {
                    continue;
                }

                if (!TryLocateOtherScanner(knownScanner, commonBeacons, out var location, out var transformation))
                {
                    continue;
                }

                other.Position = location;

                foreach (var beacon in other.Beacons)
                {
                    var position = other.Position - beacon.Position.Transform(transformation);
                    knownScanner.Beacons.Add(new Beacon(position));
                }

                beaconDistances = CalculateDistances(scanners);
            }
        }

        return scanners;
    }

    private static List<Scanner> ParseScanners(string input) => input.Split("\n\n").Select(Scanner.Parse).ToList();

    private static int CountBeacons(IEnumerable<Scanner> scanners) =>
        scanners.Single(s => s.Id == 0).Beacons.Count;

    private static int GetMaximumDistance(IReadOnlyCollection<Scanner> scanners) =>
        scanners.Max(outer => scanners.Max(outer.DistanceTo));

    private static bool AnyScannerHasAnUnknownPosition(IEnumerable<Scanner> scanners) =>
        scanners.Any(x => x.Position is null);

    private static bool TryLocateOtherScanner(
        Scanner knownScanner,
        IEnumerable<(Position Position1, Position Position2)> commonBeacons,
        [NotNullWhen(true)] out Position? position,
        [NotNullWhen(true)] out Transformation? transformation)
    {
        var allOrientations = commonBeacons
            .Select(x => (
                x.Position1,
                TransformedPositions: GetTransformedPositions(x.Position2)))
            .ToArray();

        // TODO: why does `512` work?
        for (var i = 0; i < 512; i++)
        {
            var orientation = allOrientations
                .Select(f => (f.Position1, TransformedPosition: f.TransformedPositions.ElementAt(i)));

            var possibleScannerPositions = orientation
                .Select(b => (
                    ScannerPosition: b.Position1 + b.TransformedPosition.TransformedPosition,
                    transformation: b.TransformedPosition.Transformation)
                )
                .Distinct()
                .ToList();

            if (possibleScannerPositions.Count != 1)
            {
                continue;
            }

            position = knownScanner.Position! + possibleScannerPositions.First().ScannerPosition;
            transformation = possibleScannerPositions.First().transformation;
            return true;
        }

        position = default;
        transformation = default;
        return false;
    }

    private static List<(Position TransformedPosition, Transformation Transformation)> GetTransformedPositions(
        Position position)
    {
        var transformedPositions = new List<(Position TransformedPosition, Transformation Transformation)>();

        // TODO: there should only be 24 orientations, not 512
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

    private static IEnumerable<(Position Position1, Position Position2)> GetCommonBeacons(
        Scanner scanner,
        Scanner other,
        IReadOnlyDictionary<Beacon, List<int>> distances)
    {
        foreach (var beacon in scanner.Beacons)
        {
            foreach (var otherBeacon in other.Beacons)
            {
                // TODO: why does `>= 10` work?
                if (distances[beacon].Intersect(distances[otherBeacon]).Count() >= 10)
                {
                    yield return (beacon.Position, otherBeacon.Position);
                }
            }
        }
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

                    distances.Add(beacon.Position.DistanceTo(other.Position));
                }

                beaconDistances[beacon] = distances;
            }
        }

        return beaconDistances;
    }
}
