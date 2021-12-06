using System;
using System.Linq;

namespace aoc_2021_csharp
{
    public class Day03
    {
        private static readonly string INPUT_FILE = "input/day03.txt";
        private static readonly string[] input = System.IO.File.ReadAllLines(INPUT_FILE);

        public void Part1()
        {
            var gamma = "";
            var epsilon = "";

            for (int i = 0; i < input[0].Length; i++)
            {
                var count0 = input.Count(x => x[i] == '0');
                var count1 = input.Count(x => x[i] == '1');

                gamma += (count1 > count0) ? '1' : '0';
                epsilon += (count1 > count0) ? '0' : '1';
            }

            var g = Convert.ToInt32(gamma, 2);
            var e = Convert.ToInt32(epsilon, 2);

            Console.WriteLine($"Day 03, Part 1: gamma: {g}, epsilon: {e}, answer: {g * e}");
        }

        public void Part2()
        {
            var oxygen = CalculateOxygenGeneratorRating();
            var co2 = CalculateCO2ScrubberRating();

            Console.WriteLine($"Day 03, Part 2: oxygen: {oxygen}, co2: {co2}, {oxygen * co2}");
        }

        private int CalculateOxygenGeneratorRating()
        {
            var temp = input.ToList();

            for (int i = 0; i < input[0].Length; i++)
            {
                var count0 = temp.Count(x => x[i] == '0');
                var count1 = temp.Count(x => x[i] == '1');

                if (count1 >= count0)
                {
                    temp.RemoveAll(x => x[i] == '0');
                }
                else
                {
                    temp.RemoveAll(x => x[i] == '1');
                }

                if (temp.Count() == 1)
                {
                    break;
                }
            }

            return temp.Select(x => Convert.ToInt32(x, 2)).Single();
        }

        private int CalculateCO2ScrubberRating()
        {
            var temp = input.ToList();

            for (int i = 0; i < input[0].Length; i++)
            {
                var count0 = temp.Count(x => x[i] == '0');
                var count1 = temp.Count(x => x[i] == '1');

                if (count1 >= count0)
                {
                    temp.RemoveAll(x => x[i] == '1');
                }
                else
                {
                    temp.RemoveAll(x => x[i] == '0');
                }

                if (temp.Count() == 1)
                {
                    break;
                }
            }

            return temp.Select(x => Convert.ToInt32(x, 2)).Single();
        }
    }
}
