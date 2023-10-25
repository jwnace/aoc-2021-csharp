using System.Collections.Generic;
using System.Linq;

namespace aoc_2021_csharp.Day19;

public class Scanner
{
    public int Id { get; }
    public Position? Position { get; set; }
    public HashSet<Beacon> Beacons { get; }

    private Scanner(int id, Position? position, HashSet<Beacon> beacons)
    {
        Id = id;
        Position = position;
        Beacons = beacons;
    }

    public static Scanner Parse(string input)
    {
        var lines = input.Split('\n');
        var id = int.Parse(lines[0].Split(' ')[2]);
        var position = id == 0 ? new Position(0, 0, 0) : null;
        var beacons = lines[1..].Select(Beacon.Parse).ToHashSet();

        return new Scanner(id, position, beacons);
    }

    public int DistanceTo(Scanner other) => Position!.DistanceTo(other.Position!);
}
