using System.Linq;
using aoc_2021_csharp.Extensions;

namespace aoc_2021_csharp.Day19;

public record Beacon(Position Position)
{
    public static Beacon Parse(string line)
    {
        var (x, y, z) = line.Split(',').Select(int.Parse).ToArray();
        return new Beacon(new Position(x, y, z));
    }
}
