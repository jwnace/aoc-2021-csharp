using System.Collections.Generic;

namespace aoc_2021_csharp.Day15;

public static class Day15
{
    private static readonly string[] Input = File.ReadAllLines("Day15/day15.txt");

    private static readonly Dictionary<(int, int), int> Nodes = new();
    private static readonly PriorityQueue<(int, int), int> Queue = new();

    private static void Dijkstra(int multiplier)
    {
        while (Queue.Count > 0)
        {
            var node = Queue.Dequeue();
            var (row, col) = node;

            if (row == 0 && col == 0)
            {
                Nodes[node] = 0;
            }

            var n1 = (row - 1, col);
            var n2 = (row + 1, col);
            var n3 = (row, col - 1);
            var n4 = (row, col + 1);
            var neighbors = new List<(int, int)> {n1, n2, n3, n4};

            foreach (var neighbor in neighbors)
            {
                var (r, c) = neighbor;

                if (r < 0 || r >= Input.Length * multiplier || c < 0 || c >= Input[0].Length * multiplier)
                {
                    continue;
                }

                var rows = Input.Length;
                var cols = Input[0].Length;
                var temp = Input[r % rows][c % cols].ToInt() + (r / rows) + (c / cols);

                if (temp > 9)
                {
                    temp -= 9;
                }

                var d = Nodes[node] + temp;

                if (d < Nodes[neighbor])
                {
                    Nodes[neighbor] = d;
                    Queue.Enqueue(neighbor, d);
                }
            }
        }
    }

    private static int Solve(int multiplier)
    {
        Queue.Enqueue((0, 0), 0);

        // populate the unvisited nodes
        for (var row = 0; row < Input.Length * multiplier; row++)
        {
            for (var col = 0; col < Input[0].Length * multiplier; col++)
            {
                var node = (row, col);
                // HACK: use something else for the arbitrarily large number
                Nodes[node] = node == (0, 0) ? 0 : int.MaxValue / 2;
            }
        }

        var end = (Input.Length * multiplier - 1, Input[0].Length * multiplier - 1);

        Dijkstra(multiplier);

        return Nodes[end];
    }

    public static int Part1() => Solve(1);

    public static int Part2() => Solve(5);
}
