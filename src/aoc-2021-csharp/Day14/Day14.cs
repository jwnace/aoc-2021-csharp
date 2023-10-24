using System.Collections.Generic;
using System.Linq;

namespace aoc_2021_csharp.Day14;

public static class Day14
{
    private static readonly string[] Input = File.ReadAllLines("Day14/day14.txt");

    public static int Part1()
    {
        var seed = Input[0];
        var rules = new Dictionary<string, string>();

        for (var i = 2; i < Input.Length; i++)
        {
            var line = Input[i];
            var values = line.Split(" -> ");
            var pair = values[0];
            var insert = values[1];
            rules.Add(pair, insert);
        }

        for (var step = 0; step < 10; step++)
        {
            var temp = seed[0].ToString();

            for (var i = 0; i < seed.Length - 1; i++)
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

        return answer;
    }

    public static long Part2()
    {
        var seed = Input[0];
        var pairs = new Dictionary<(char, char), long>();
        var rules = new Dictionary<(char, char), char>();

        for (var i = 0; i < seed.Length - 1; i++)
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

        for (var i = 2; i < Input.Length; i++)
        {
            var line = Input[i];
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

        return answer;
    }
}
