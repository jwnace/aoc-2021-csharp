using System;
using System.Linq;

namespace aoc_2021_csharp
{
    public class Day01
    {
        private static readonly string INPUT_FILE = "input/day01.txt";
        private static readonly int[] input = System.IO.File.ReadAllLines(INPUT_FILE).Select(x => int.Parse(x)).ToArray();

        public void Part1()
        {
            var count = 0;

            for (int i = 1; i < input.Length; i++)
            {
                count += input[i] > input[i-1] ? 1 : 0;
            }

            Console.WriteLine($"Day 01, Part 1: {count}");   
        }

        public void Part2()
        {
            var count = 0;

            for (int i = 2; i < input.Length-1; i++)
            {
                var sumA = input[i-2] + input[i-1] + input[i];
                var sumB = input[i-1] + input[i] + input[i+1];

                count += sumB > sumA ? 1 : 0;
            }

            Console.WriteLine($"Day 01, Part 2: {count}");
        }
    }
}
