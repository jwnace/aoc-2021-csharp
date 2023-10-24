using System.Collections.Generic;

namespace aoc_2021_csharp.Day21;

public static class Day21
{
    private static readonly string[] Input = File.ReadAllLines("Day21/day21.txt");

    private static readonly Dictionary<(int, int, int, int), (long, long)> Memo = new();

    public static int Part1()
    {
        var d = new DeterministicDie();

        var p1 = Input[0].Split(" starting position: ");
        var player1 = new Player(int.Parse(p1[1]));

        var p2 = Input[1].Split(" starting position: ");
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

        return answer;
    }

    public static long Part2()
    {
        var p1 = int.Parse(Input[0].Split(" starting position: ")[1]) - 1;
        var p2 = int.Parse(Input[1].Split(" starting position: ")[1]) - 1;

        var (p1Wins, p2Wins) = CountWins(p1, p2, 0, 0);

        return Math.Max(p1Wins, p2Wins);
    }

    private static (long, long) CountWins(int p1, int p2, int s1, int s2)
    {
        if (s1 >= 21)
        {
            return (1, 0);
        }

        if (s2 >= 21)
        {
            return (0, 1);
        }

        if (Memo.ContainsKey((p1, p2, s1, s2)))
        {
            return Memo[(p1, p2, s1, s2)];
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

        Memo[(p1, p2, s1, s2)] = result;
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
