using System;
using System.Collections.Generic;

namespace aoc_2021_csharp
{
    public class Day15
    {
        private static readonly string INPUT_FILE = "input/day15.txt";
        private static readonly string[] input = System.IO.File.ReadAllLines(INPUT_FILE);

        Dictionary<(int, int), int> nodes = new Dictionary<(int, int), int>();
        PriorityQueue<(int, int), int> queue = new PriorityQueue<(int, int), int>();

        private void Dijkstra(int multiplier)
        {
            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                var (row, col) = node;

                if (row == 0 && col == 0)
                {
                    nodes[node] = 0;
                }

                var n1 = (row - 1, col);
                var n2 = (row + 1, col);
                var n3 = (row, col - 1);
                var n4 = (row, col + 1);
                var neighbors = new List<(int, int)> {n1, n2, n3, n4};

                foreach (var neighbor in neighbors)
                {
                    var (r, c) = neighbor;

                    if (r < 0 || r >= input.Length * multiplier || c < 0 || c >= input[0].Length * multiplier)
                    {
                        continue;
                    }

                    var rows = input.Length;
                    var cols = input[0].Length;
                    var temp = input[r % rows][c % cols].ToInt() + (r / rows) + (c / cols);

                    if (temp > 9)
                    {
                        temp -= 9;
                    }

                    var d = nodes[node] + temp;

                    if (d < nodes[neighbor])
                    {
                        nodes[neighbor] = d;
                        queue.Enqueue(neighbor, d);
                    }
                }
            }
        }

        private int Solve(int multiplier)
        {
            queue.Enqueue((0, 0), 0);

            // populate the unvisited nodes
            for (var row = 0; row < input.Length * multiplier; row++)
            {
                for (var col = 0; col < input[0].Length * multiplier; col++)
                {
                    var node = (row, col);
                    // HACK: use something else for the arbitrarily large number
                    nodes[node] = node == (0, 0) ? 0 : int.MaxValue / 2;
                }
            }

            var end = (input.Length * multiplier - 1, input[0].Length * multiplier - 1);

            Dijkstra(multiplier);

            return nodes[end];
        }

        public void Part1()
        {
            Console.WriteLine($"Day 15, Part 1: {Solve(1)}");
        }

        public void Part2()
        {
            Console.WriteLine($"Day 15, Part 2: {Solve(5)}");
        }
    }
}