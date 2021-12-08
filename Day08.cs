using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc_2021_csharp
{
    public class Day08
    {
        private static readonly string INPUT_FILE = "input/day08.txt";
        private static readonly string[] input = System.IO.File.ReadAllLines(INPUT_FILE);

        public void Part1()
        {
            var sum = 0;

            foreach (var line in input)
            {
                var values = line.Split(" | ");
                var digits = values[1].Split(' ');
                sum += digits.Count(x => new List<int> { 2, 3, 4, 7 }.Any(y => y == x.Length));
            }

            Console.WriteLine($"Day 08, Part 1: {sum}");
        }

        public void Part2()
        {
            var sum = 0;

            foreach (var line in input)
            {
                var values = line.Split(" | ");
                var patterns = values[0].Split(' ');
                var digits = values[1].Split(' ');
                var stringToInt = new Dictionary<string, int>();
                var intToString = new Dictionary<int, string>();

                // 1
                var t1 = new string(patterns.Single(x => x.Length == 2).OrderBy(c => c).ToArray());
                stringToInt[t1] = 1;
                intToString[1] = t1;

                // 7
                var t2 = new string(patterns.Single(x => x.Length == 3).OrderBy(c => c).ToArray());
                stringToInt[t2] = 7;
                intToString[7] = t2;

                // 4
                var t3 = new string(patterns.Single(x => x.Length == 4).OrderBy(c => c).ToArray());
                stringToInt[t3] = 4;
                intToString[4] = t3;

                // 8
                var t4 = new string(patterns.Single(x => x.Length == 7).OrderBy(c => c).ToArray());
                stringToInt[t4] = 8;
                intToString[8] = t4;

                foreach (var pattern in patterns)
                {
                    var temp = new string(pattern.OrderBy(c => c).ToArray());

                    if (stringToInt.ContainsKey(temp))
                    {
                        continue;
                    }

                    if (temp.Length == 5)
                    {
                        if (temp.Intersect(intToString[1]).Count() == 2)
                        {
                            // 3
                            stringToInt[temp] = 3;
                            intToString[3] = temp;
                        }
                        else if (temp.Intersect(intToString[4]).Count() == 2)
                        {
                            // 2
                            stringToInt[temp] = 2;
                            intToString[2] = temp;
                        }
                        else
                        {
                            // 5
                            stringToInt[temp] = 5;
                            intToString[5] = temp;
                        }
                    }
                    else if (temp.Length == 6)
                    {
                        if (temp.Intersect(intToString[1]).Count() != 2)
                        {
                            // 6
                            stringToInt[temp] = 6;
                            intToString[6] = temp;
                        }
                        else if (temp.Intersect(intToString[4]).Count() == 4)
                        {
                            // 9
                            stringToInt[temp] = 9;
                            intToString[9] = temp;
                        }
                        else
                        {
                            // 0
                            stringToInt[temp] = 0;
                            intToString[0] = temp;
                        }
                    }
                }

                var outputString = "";

                foreach (var digit in digits)
                {
                    var temp = new string(digit.OrderBy(c => c).ToArray());

                    outputString += stringToInt[temp];
                }

                sum += int.Parse(outputString);
            }

            Console.WriteLine($"Day 08, Part 2: {sum}");
        }
    }
}
