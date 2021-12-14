using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc_2021_csharp
{
    public class Day14
    {
        private static readonly string INPUT_FILE = "input/day14.txt";
        private static readonly string[] input = System.IO.File.ReadAllLines(INPUT_FILE);

        public void Part1()
        {
            var seed = input[0];
            var rules = new Dictionary<string, string>();

            for (int i = 2; i < input.Length; i++)
            {
                var line = input[i];
                var values = line.Split(" -> ");
                var pair = values[0];
                var insert = values[1];
                rules.Add(pair, insert);
            }

            for (int step = 0; step < 10; step++)
            {
                var temp = seed[0].ToString();

                for (int i = 0; i < seed.Length - 1; i++)
                {
                    var pair = seed.Substring(i, 2);
                    var insert = rules[pair];
                    temp += insert + pair[1];
                }

                seed = temp;
            }

            var max = seed.GroupBy(x => x).Max(g => g.Count());
            var min = seed.GroupBy(x => x).Min(g => g.Count());
            var answer = max - min;

            Console.WriteLine($"Day 14, Part 1: {answer}");
        }

        public void Part2()
        {
            var seed = input[0];
            var pairs = new Dictionary<(char, char), long>();
            var rules = new Dictionary<(char, char), char>();

            for (int i = 0; i < seed.Length - 1; i++)
            {
                var pair = (seed[i], seed[i + 1]);

                if (pairs.TryGetValue(pair, out _))
                {
                    pairs[pair]++;
                }
                else
                {
                    pairs[pair] = 1;
                }
            }

            for (int i = 2; i < input.Length; i++)
            {
                var line = input[i];
                var values = line.Split(" -> ");
                var pair = values[0];
                var insert = values[1].Single();
                rules.Add((pair[0], pair[1]), insert);
            }

            for (var step = 0; step < 40; step++)
            {
                var newPairs = new Dictionary<(char, char), long>();

                foreach (var pair in pairs)
                {
                    var insert = rules[pair.Key];
                    var p1 = (pair.Key.Item1, insert);
                    var p2 = (insert, pair.Key.Item2);

                    if (newPairs.TryGetValue(p1, out _))
                    {
                        newPairs[p1] += pair.Value;
                    }
                    else
                    {
                        newPairs[p1] = pair.Value;
                    }

                    if (newPairs.TryGetValue(p2, out _))
                    {
                        newPairs[p2] += pair.Value;
                    }
                    else
                    {
                        newPairs[p2] = pair.Value;
                    }
                }

                pairs = newPairs;
            }

            var query = pairs.Select(x => (C: x.Key.Item1, Count: x.Value))
                .Concat(pairs.Select(x => (C: x.Key.Item2, Count: x.Value)))
                .GroupBy(x => x.C)
                .Select(g => new { C = g.Key, Count = g.Sum(x => x.Count) })
                .ToList();

            var max = (query.Max(x => x.Count) + 1) / 2;
            var min = (query.Min(x => x.Count) + 1) / 2;
            var answer = max - min;

            Console.WriteLine($"Day 14, Part 2: {answer}");
        }
    }
}
