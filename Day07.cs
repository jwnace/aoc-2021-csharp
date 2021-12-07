using System;
using System.Linq;

namespace aoc_2021_csharp
{
    public class Day07
    {
        private static readonly string INPUT_FILE = "input/day07.txt";
        private static readonly string[] input = System.IO.File.ReadAllLines(INPUT_FILE);

        public void Part1()
        {
            var positions = input[0].Split(',').Select(x => int.Parse(x)).ToList();
            var min = positions.Min();
            var max = positions.Max();
            var answer = int.MaxValue;

            for (var i = min; i <= max; i++)
            {
                var fuel = positions.Sum(x => Math.Abs(x - i));
                answer = fuel < answer ? fuel : answer;
            }

            Console.WriteLine($"Day 07, Part 1: {answer}");
        }

        public void Part2()
        {
            var positions = input[0].Split(',').Select(x => int.Parse(x)).ToList();
            var min = positions.Min();
            var max = positions.Max();
            var answer = int.MaxValue;

            for (var i = min; i <= max; i++)
            {
                var totalFuel = 0;

                foreach (var position in positions)
                {
                    var steps = Math.Abs(position - i);
                    var fuel = 0;

                    for (var j = 1; j <= steps; j++)
                    {
                        fuel += j;
                    }

                    totalFuel += fuel;
                }

                answer = totalFuel < answer ? totalFuel : answer;
            }

            Console.WriteLine($"Day 07, Part 2: {answer}");        
        }
    }
}
