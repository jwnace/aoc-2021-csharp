using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc_2021_csharp
{
    public class Day10
    {
        private static readonly string INPUT_FILE = "input/day10.txt";
        private static readonly string[] input = System.IO.File.ReadAllLines(INPUT_FILE);

        public void Part1()
        {
            var dict = new Dictionary<char, char> { { '(', ')' }, { '[', ']' }, { '{', '}' }, { '<', '>' } };
            var points = new Dictionary<char, int> { { ')', 3 }, { ']', 57 }, { '}', 1197 }, { '>', 25137 } };
            var illegalChars = new List<char>();

            foreach (var line in input)
            {
                var stack = new Stack<char>();

                for (int i = 0; i < line.Length; i++)
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

            var score = illegalChars.Sum(x => points[x]);

            Console.WriteLine($"Day 10, Part 1: {score}");
        }

        public void Part2()
        {
            var dict = new Dictionary<char, char> { { '(', ')' }, { '[', ']' }, { '{', '}' }, { '<', '>' } };
            var points = new Dictionary<char, int> { { '(', 1 }, { '[', 2 }, { '{', 3 }, { '<', 4 } };
            var scores = new List<long>();

            foreach (var line in input)
            {
                var stack = new Stack<char>();
                var corrupted = false;
                var score = 0L;

                for (int i = 0; i < line.Length; i++)
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

            var answer = scores.OrderBy(x => x).ElementAt((scores.Count()) / 2);

            Console.WriteLine($"Day 10, Part 2: {answer}");
        }
    }
}
