using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace aoc_2021_csharp.Day10;

public static class Day10
{
    private static readonly string[] Input = File.ReadAllLines("Day10/day10.txt");

    public static int Part1()
    {
        var dict = new Dictionary<char, char> { { '(', ')' }, { '[', ']' }, { '{', '}' }, { '<', '>' } };
        var points = new Dictionary<char, int> { { ')', 3 }, { ']', 57 }, { '}', 1197 }, { '>', 25137 } };
        var illegalChars = new List<char>();

        foreach (var line in Input)
        {
            var stack = new Stack<char>();

            for (var i = 0; i < line.Length; i++)
            {
                if (dict.ContainsKey(line[i]))
                {
                    stack.Push(line[i]);
                }
                else
                {
                    var temp = stack.Pop();

                    if (dict[temp] != line[i])
                    {
                        illegalChars.Add(line[i]);
                        break;
                    }
                }
            }
        }

        return illegalChars.Sum(x => points[x]);
    }

    public static long Part2()
    {
        var dict = new Dictionary<char, char> { { '(', ')' }, { '[', ']' }, { '{', '}' }, { '<', '>' } };
        var points = new Dictionary<char, int> { { '(', 1 }, { '[', 2 }, { '{', 3 }, { '<', 4 } };
        var scores = new List<long>();

        foreach (var line in Input)
        {
            var stack = new Stack<char>();
            var corrupted = false;
            var score = 0L;

            for (var i = 0; i < line.Length; i++)
            {
                if (dict.ContainsKey(line[i]))
                {
                    stack.Push(line[i]);
                }
                else
                {
                    var temp = stack.Pop();

                    if (dict[temp] != line[i])
                    {
                        corrupted = true;
                        break;
                    }
                }
            }

            if (!corrupted)
            {
                while (stack.Count > 0)
                {
                    var temp = stack.Pop();

                    score *= 5;
                    score += points[temp];
                }

                scores.Add(score);
            }
        }

        return scores.OrderBy(x => x).ElementAt((scores.Count()) / 2);
    }
}
