using System.Collections.Generic;
using System.Linq;
using aoc_2021_csharp.Extensions;

namespace aoc_2021_csharp.Day19;

public static class Day19
{
    private static readonly string Input = File.ReadAllText("Day19/day19.txt").Trim();

    public static int Part1() => Solve1(Input);

    public static int Part2() => Solve2(Input);

    private static int Solve1(string input)
    {
        var scanners = ParseScanners(input);

        Console.WriteLine(string.Join("\n", scanners.Select(s => $"Scanner {s.Id} has {s.Beacons.Count} beacons")));

        return -1;
    }

    private static int Solve2(string input)
    {
        throw new NotImplementedException();
    }

    private static List<Scanner> ParseScanners(string input) => input.Split("\n\n").Select(Scanner.Parse).ToList();

    private record Scanner(int Id, Position? Position, List<Beacon> Beacons)
    {
        public static Scanner Parse(string input)
        {
            var lines = input.Split('\n');
            var id = int.Parse(lines[0].Split(' ')[2]);
            var position = id == 0 ? new Position(0, 0, 0) : null;
            var beacons = lines[1..].Select(Beacon.Parse).ToList();

            return new Scanner(id, position, beacons);
        }
    }

    private record Beacon(Position Position)
    {
        public static Beacon Parse(string line)
        {
            var (x, y, z) = line.Split(',').Select(int.Parse).ToArray();
            return new Beacon(new Position(x, y, z));
        }
    }

    private record Position(int X, int Y, int Z);
}
