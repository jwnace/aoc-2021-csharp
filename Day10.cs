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
            var open = new List<char> { '(', '[', '{', '<' };
            var incorrect = new List<char>();

            foreach (var line in input)
            {
                var stack = new Stack<char>();

                for (int i = 0; i < line.Length; i++)
                {
                    if (open.Contains(line[i]))
                    {
                        stack.Push(line[i]);
                    }
                    else
                    {
                        var start = stack.Pop();
                        var end = line[i];

                        if ((start == '(' && end != ')') || (start == '[' && end != ']') || (start == '{' && end != '}') || (start == '<' && end != '>'))
                        {
                            incorrect.Add(line[i]);
                            break;
                        }
                    }
                }
            }

            var t1 = incorrect.Count(x => x == ')') * 3;
            var t2 = incorrect.Count(x => x == ']') * 57;
            var t3 = incorrect.Count(x => x == '}') * 1197;
            var t4 = incorrect.Count(x => x == '>') * 25137;

            var result = t1 + t2 + t3 + t4;

            Console.WriteLine($"Day 10, Part 1: {result}");
        }

        public void Part2()
        {
            var open = new List<char> { '(', '[', '{', '<' };
            var linesToRemove = new List<int>();

            for (int i = 0; i < input.Length; i++)
            {
                var line = input[i];
                var stack = new Stack<char>();

                for (int j = 0; j < line.Length; j++)
                {
                    if (open.Contains(line[j]))
                    {
                        stack.Push(line[j]);
                    }
                    else
                    {
                        var start = stack.Pop();
                        var end = line[j];

                        if ((start == '(' && end != ')') || (start == '[' && end != ']') || (start == '{' && end != '}') || (start == '<' && end != '>'))
                        {
                            linesToRemove.Add(i);
                        }
                    }
                }
            }

            var lines = input.ToList();

            foreach (var index in linesToRemove.OrderByDescending(x => x))
            {
                lines.RemoveAt(index);
            }

            var scores = new List<ulong>();

            for (int i = 0; i < lines.Count(); i++)
            {
                var line = lines[i];
                var stack = new Stack<char>();

                for (int j = 0; j < line.Length; j++)
                {
                    if (open.Contains(line[j]))
                    {
                        stack.Push(line[j]);
                    }
                    else
                    {
                        var temp = stack.Pop();
                    }
                }

                var score = 0UL;
                var count = stack.Count();

                for (int j = 0; j < count; j++)
                {
                    var c = stack.Pop();
                    score *= 5;

                    switch (c)
                    {
                        case '(':
                            score += 1;
                            break;
                        case '[':
                            score += 2;
                            break;
                        case '{':
                            score += 3;
                            break;
                        case '<':
                            score += 4;
                            break;
                    }
                }

                scores.Add(score);
            }

            var answer = scores.OrderBy(x => x).ElementAt((scores.Count()) / 2);

            Console.WriteLine($"Day 10, Part 2: {answer}");
        }
    }
}
