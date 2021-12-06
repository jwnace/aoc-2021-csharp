using System;

namespace aoc_2021_csharp
{
    public class Day02
    {
        private static readonly string INPUT_FILE = "input/day02.txt";
        private static readonly string[] input = System.IO.File.ReadAllLines(INPUT_FILE);

        public void Part1()
        {
            var x = 0;
            var y = 0;

            foreach (var line in input)
            {
                var values = line.Split(' ');
                var direction = values[0];
                var magnitude = int.Parse(values[1]);

                switch (direction)
                {
                    case "forward":
                        x += magnitude;
                        break;
                    
                    case "up":
                        y -= magnitude;
                        break;
                    
                    case "down":
                        y += magnitude;
                        break;
                }
            }

            Console.WriteLine($"Day 02, Part 1: x: {x}, y: {y}, answer: {x*y}");
        }

        public void Part2()
        {
            var x = 0;
            var y = 0;
            var aim = 0;

            foreach (var line in input)
            {
                var values = line.Split(' ');
                var direction = values[0];
                var magnitude = int.Parse(values[1]);

                switch (direction)
                {
                    case "forward":
                        x += magnitude;
                        y += aim * magnitude;
                        break;
                    
                    case "up":
                        aim -= magnitude;
                        break;
                    
                    case "down":
                        aim += magnitude;
                        break;
                }
            }

            Console.WriteLine($"Day 02, Part 2: x: {x}, y: {y}, answer: {x*y}");
        }
    }
}
