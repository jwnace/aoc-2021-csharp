using System;
using System.Collections.Generic;
using System.Linq;

namespace aoc_2021_csharp
{
    public class Day21
    {
        private static readonly string INPUT_FILE = "input/day21.txt";
        private static readonly string[] input = System.IO.File.ReadAllLines(INPUT_FILE);

        private readonly Dictionary<(int, int, int, int), (long, long)> memo = new Dictionary<(int, int, int, int), (long, long)>();

        public void Part1()
        {
            var d = new DeterministicDie();

            var p1 = input[0].Split(" starting position: ");
            var player1 = new Player(int.Parse(p1[1]));

            var p2 = input[1].Split(" starting position: ");
            var player2 = new Player(int.Parse(p2[1]));

            var turn = 1;

            while (player1.Score < 1000 && player2.Score < 1000)
            {
                var player = turn % 2 == 0 ? player2 : player1;

                player.Position += d.Roll() + d.Roll() + d.Roll();

                while (player.Position > 10)
                {
                    player.Position -= 10;
                }

                player.Score += player.Position;
                turn++;
            }

            var answer = Math.Min(player1.Score, player2.Score) * (turn - 1) * 3;

            Console.WriteLine($"Day 21, Part 1: {answer}");
        }

        public void Part2()
        {
            var p1 = int.Parse(input[0].Split(" starting position: ")[1]) - 1;
            var p2 = int.Parse(input[1].Split(" starting position: ")[1]) - 1;

            var (p1Wins, p2Wins) = CountWins(p1, p2, 0, 0);

            Console.WriteLine($"Day 21, Part 2: {Math.Max(p1Wins, p2Wins)}");
        }

        private (long, long) CountWins(int p1, int p2, int s1, int s2)
        {
            if (s1 >= 21)
            {
                return (1, 0);
            }

            if (s2 >= 21)
            {
                return (0, 1);
            }

            if (memo.ContainsKey((p1, p2, s1, s2)))
            {
                return memo[(p1, p2, s1, s2)];
            }

            var result = (0L, 0L);

            for (var i = 1; i <= 3; i++)
            {
                for (var j = 1; j <= 3; j++)
                {
                    for (var k = 1; k <= 3; k++)
                    {
                        var newPosition = (p1 + i + j + k) % 10;
                        var newScore = s1 + newPosition + 1;

                        var (a, b) = CountWins(p2, newPosition, s2, newScore);
                        result = (result.Item1 + b, result.Item2 + a);
                    }
                }
            }

            memo[(p1, p2, s1, s2)] = result;
            return result;
        }

        private class DeterministicDie
        {
            private int _previousNumber = 0;

            public int Roll()
            {
                var next = ++_previousNumber;

                if (next > 100)
                {
                    next -= 100;
                    _previousNumber = next;
                }

                return next;
            }
        }

        private class Player
        {
            public int Position { get; set; }
            public int Score { get; set; }

            public Player(int startingPosition)
            {
                Position = startingPosition;
            }
        }
    }
}