using System.Collections.Generic;
using System.Linq;

namespace aoc_2021_csharp.Day19;

public record Scanner(int Id, List<Beacon> Beacons)
{
    public Position? Position { get; set; }

    public static Scanner Parse(string input)
    {
        var lines = input.Split('\n');
        var id = int.Parse(lines[0].Split(' ')[2]);
        var position = id == 0 ? new Position(0, 0, 0) : null;
        var beacons = lines[1..].Select(Beacon.Parse).ToList();

        return new Scanner(id, beacons) { Position = position };
    }
}
