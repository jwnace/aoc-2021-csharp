using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace aoc_2021_csharp.Day12;

public static class Day12
{
    private static readonly string[] Input = File.ReadAllLines("Day12/day12.txt");

    public static int Part1() => Run(1);

    public static int Part2() => Run(2);

    private static int Run(int part)
    {
        var caves = new List<Cave>();

        foreach (var line in Input)
        {
            var values = line.Split('-');
            var nameA = values[0];
            var nameB = values[1];
            var caveA = caves.SingleOrDefault(x => x.Name == nameA);
            var caveB = caves.SingleOrDefault(x => x.Name == nameB);

            if (caveA == null)
            {
                caveA = new Cave(nameA);
                caves.Add(caveA);
            }

            if (caveB == null)
            {
                caveB = new Cave(nameB);
                caves.Add(caveB);
            }

            caveA.Connections.Add(caveB);
            caveB.Connections.Add(caveA);
        }

        return Traverse(part, caves);
    }

    private static int Traverse(int part, List<Cave> caves)
    {
        var start = caves.Single(x => x.Name == "start");
        return Traverse(part, start);
    }

    private static int Traverse(int part, Cave cave, List<Cave> path = null)
    {
        if (cave.Name == "end")
        {
            return 1;
        }

        if (path == null)
        {
            path = new List<Cave>();
        }

        path.Add(cave);

        var count = 0;

        foreach (var connection in cave.Connections)
        {
            if (connection.Name == "start")
            {
                continue;
            }

            if (part == 1 && connection.Size == CaveSize.Small && path.Any(x => x.Name == connection.Name))
            {
                continue;
            }

            if (part == 2 && connection.Size == CaveSize.Small && path.Any(x => x.Name == connection.Name) && path.Where(x => x.Size == CaveSize.Small).GroupBy(x => x.Name).Any(x => x.Count() > 1))
            {
                continue;
            }

            count += Traverse(part, connection, new List<Cave>(path));
        }

        return count;
    }

    private class Cave
    {
        public Cave(string name, List<Cave> connections = null)
        {
            Name = name;

            if (connections == null)
            {
                Connections = new List<Cave>();
            }
        }

        public string Name { get; set; }
        public List<Cave> Connections { get; set; }
        public CaveSize Size => this.Name.ToUpper() == this.Name ? CaveSize.Big : CaveSize.Small;
    }

    private enum CaveSize
    {
        Big,
        Small
    }
}
