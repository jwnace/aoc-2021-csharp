using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace aoc_2021_csharp.Day23;

public static class Day23
{
    private static readonly string[] Input1 = File.ReadAllLines("Day23/day23_part1.txt");
    private static readonly string[] Input2 = File.ReadAllLines("Day23/day23_part2.txt");

    public static int Part1() => Solve(Input1);

    public static int Part2() => Solve(Input2);

    public static int Solve(string[] input)
    {
        var grid = BuildGrid(input);
        var initialState = BuildInitialState(grid);
        var seen = new HashSet<State>();
        var queue = new PriorityQueue<State, int>();
        queue.Enqueue(initialState, 0);

        var hallwayPositions = new (int Row, int Col)[]
        {
            (1, 1),
            (1, 2),
            (1, 4),
            (1, 6),
            (1, 8),
            (1, 10),
            (1, 11),
        };

        while (queue.Count > 0)
        {
            var state = queue.Dequeue();
            var (amphipods, energy) = state;

            if (seen.Contains(state))
            {
                continue;
            }

            seen.Add(state);

            if (IsFinalState(state))
            {
                return energy;
            }

            foreach (var amphipod in amphipods)
            {
                var possibleMoves = GetAllPossibleMoves(amphipod, amphipods, grid, hallwayPositions).ToList();

                foreach (var move in possibleMoves)
                {
                    var index = Array.IndexOf(amphipods, amphipod);
                    var newAmphipod = amphipod with { Row = move.Row, Col = move.Col };
                    var newAmphipods = amphipods[..index]
                        .Append(newAmphipod)
                        .Concat(amphipods[(index + 1)..])
                        .ToArray();

                    var newState = new State(newAmphipods, energy + amphipod.EnergyCost * move.Steps);
                    queue.Enqueue(newState, newState.Energy);
                }
            }
        }

        throw new Exception("No solution found!");
    }

    private static Dictionary<(int Row, int Col), char> BuildGrid(IReadOnlyList<string> input)
    {
        var grid = new Dictionary<(int Row, int Col), char>();

        for (var row = 0; row < input.Count; row++)
        {
            for (var col = 0; col < input[row].Length; col++)
            {
                grid[(row, col)] = input[row][col];
            }
        }

        return grid;
    }

    private static State BuildInitialState(Dictionary<(int Row, int Col), char> grid)
    {
        var amphipods = new List<Amphipod>();

        foreach (var pair in grid)
        {
            var (row, col) = pair.Key;
            var value = pair.Value;

            switch (value)
            {
                case 'A':
                {
                    amphipods.Add(new Amphipod(row, col, value, 1, 3));
                    break;
                }
                case 'B':
                {
                    amphipods.Add(new Amphipod(row, col, value, 10, 5));
                    break;
                }
                case 'C':
                {
                    amphipods.Add(new Amphipod(row, col, value, 100, 7));
                    break;
                }
                case 'D':
                {
                    amphipods.Add(new Amphipod(row, col, value, 1000, 9));
                    break;
                }
            }
        }

        return new State(amphipods.ToArray(), 0);
    }

    private static IEnumerable<Move> GetAllPossibleMoves(
        Amphipod amphipod,
        Amphipod[] amphipods,
        Dictionary<(int Row, int Col), char> grid,
        (int Row, int Col)[] hallwayPositions)
    {
        if (IsAtFinalDestination(amphipod, amphipods, grid))
        {
            yield break;
        }

        if (TryGetFinalDestination(amphipod, amphipods, grid, out var finalDestination))
        {
            if (TryToReachPosition(amphipod, finalDestination, amphipods, grid, out var move))
            {
                yield return move;
            }
        }

        if (amphipod.Row == 1)
        {
            yield break;
        }

        foreach (var position in hallwayPositions)
        {
            if (TryToReachPosition(amphipod, position, amphipods, grid, out var move))
            {
                yield return move;
            }
        }
    }

    private static bool TryToReachPosition(
        Amphipod amphipod,
        (int Row, int Col) destination,
        Amphipod[] amphipods,
        IReadOnlyDictionary<(int Row, int Col), char> grid,
        [NotNullWhen(true)] out Move? move)
    {
        var seen = new HashSet<(int Row, int Col)>();
        var queue = new Queue<(int Row, int Col, int Steps)>();
        queue.Enqueue((amphipod.Row, amphipod.Col, 0));

        while (queue.Any())
        {
            var state = queue.Dequeue();
            var (row, col, steps) = state;
            var position = (row, col);

            if (seen.Contains(position))
            {
                continue;
            }

            seen.Add(position);

            if (position == destination)
            {
                move = new Move(row, col, steps);
                return true;
            }

            // TODO: consider doing this differently do avoid allocating a ton of memory
            var neighbors = new (int Row, int Col)[]
            {
                (row - 1, col),
                (row + 1, col),
                (row, col - 1),
                (row, col + 1),
            };

            foreach (var neighbor in neighbors)
            {
                if (grid.GetValueOrDefault(neighbor) == '#')
                {
                    continue;
                }

                if (amphipods.Any(a => a.Row == neighbor.Row && a.Col == neighbor.Col))
                {
                    continue;
                }

                queue.Enqueue((neighbor.Row, neighbor.Col, steps + 1));
            }
        }

        move = default;
        return false;
    }

    private static bool TryGetFinalDestination(
        Amphipod amphipod,
        Amphipod[] amphipods,
        Dictionary<(int Row, int Col), char> grid,
        out (int Row, int Col) finalDestination)
    {
        var (_, _, type, _, targetCol) = amphipod;

        if (RoomContainsTypeThatDoesNotMatch(targetCol, type, amphipods))
        {
            finalDestination = default;
            return false;
        }

        var maxRow = grid.Keys.Max(x => x.Row) - 1;
        var minRow = 2;

        for (var r = maxRow; r >= minRow; r--)
        {
            if (IsEmptySpace(r, targetCol, amphipods))
            {
                finalDestination = (r, targetCol);
                return true;
            }
        }

        throw new UnreachableException("This should never happen!");
    }

    private static bool IsAtFinalDestination(
        Amphipod amphipod,
        Amphipod[] amphipods,
        IReadOnlyDictionary<(int Row, int Col), char> grid)
    {
        var (row, col, type, _, targetCol) = amphipod;

        if (col != targetCol)
        {
            return false;
        }

        for (var r = row + 1; r < grid.Count - 1; r++)
        {
            if (amphipods.Any(a => a.Row == r && a.Col == col && a.Type != type))
            {
                return false;
            }
        }

        return true;
    }

    private static bool IsFinalState(State state) =>
        state.Amphipods.All(a => a.Col == a.TargetCol);

    private static bool RoomContainsTypeThatDoesNotMatch(int targetCol, char type, IEnumerable<Amphipod> amphipods) =>
        amphipods.Any(a => a.Col == targetCol && a.Type != type);

    private static bool IsEmptySpace(int row, int col, IEnumerable<Amphipod> amphipods) =>
        !amphipods.Any(a => a.Row == row && a.Col == col);
}
