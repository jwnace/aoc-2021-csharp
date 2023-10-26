using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace aoc_2021_csharp.Day23;

public static partial class Day23
{
    private static readonly string[] Input = File.ReadAllLines("Day23/day23.txt");

    public static int Part1() => Solve1(Input);

    public static int Part2() => Solve2(Input);

    public static int Solve1(string[] input)
    {
        var grid = BuildGrid(input);
        // DrawGrid(grid, Array.Empty<Amphipod>(), Array.Empty<(int Row, int Col)>());

        var initialState = BuildInitialState(grid);
        // DrawGrid(grid, initialState.Amphipods, Array.Empty<(int Row, int Col)>());

        var seen = new HashSet<State>();
        var queue = new PriorityQueue<State, int>();
        queue.Enqueue(initialState, 0);

        var doorways = new[] { (1, 3), (1, 5), (1, 7), (1, 9), };
        DrawGrid(grid, initialState.Amphipods);

        while (queue.Count > 0)
        {
            var state = queue.Dequeue();
            var (amphipods, energy) = state;

            if (seen.Contains(state))
            {
                continue;
            }

            if (seen.Count % 10_000 == 0)
            {
                Console.WriteLine($"Seen: {seen.Count,15:N0} | Queue: {queue.Count,15:N0}");
            }

            seen.Add(state);

            if (IsFinalState(state))
            {
                return energy;
            }

            foreach (var amphipod in amphipods)
            {
                var possibleMoves = GetAllPossibleMoves(amphipod, amphipods, grid).ToList();

                foreach (var move in possibleMoves)
                {
                    var index = Array.IndexOf(amphipods, amphipod);
                    var newAmphipod = amphipod with { Row = move.Row, Col = move.Col };
                    var newAmphipods = amphipods[..index].Append(newAmphipod).Concat(amphipods[(index + 1)..]).ToArray();
                    var newState = new State(newAmphipods, energy + amphipod.EnergyCost * move.Steps);
                    queue.Enqueue(newState, newState.Energy);
                }
            }
        }

        return -1;
    }

    private static IEnumerable<Move> GetAllPossibleMoves(
        Amphipod amphipod,
        Amphipod[] amphipods,
        Dictionary<(int Row, int Col), char> grid)
    {
        // if I am in my final destination, I can't move
        if (IsAtFinalDestination(amphipod, amphipods, grid))
        {
            yield break;
        }

        // if I am not at my final destination, I need to figure out where my final destination is
        if (TryGetFinalDestination(amphipod, amphipods, grid, out var finalDestination))
        {
            // if I can reach my final destination, I should move there
            if (TryToReachPosition(amphipod, finalDestination, amphipods, grid, out var move))
            {
                // TODO: figure out how to stop iterating entirely if we reach this point
                yield return move;
            }
        }

        // if we reach this point, it means that I'm not at my final destination, and I can't reach my final destination
        // so... I can only move if I'm not in the hallway
        if (amphipod.Row == 1)
        {
            // throw new Exception("not sure if this will ever happen, but I think it can...");
            yield break;
        }

        // if I am in a room, I have to move to the hallway
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
        Dictionary<(int Row, int Col), char> grid,
        [NotNullWhen(true)] out Move? move)
    {
        var queue = new Queue<(int Row, int Col, int Steps)>();
        var seen = new HashSet<(int Row, int Col)>();

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

            var neighbors = new (int Row, int Col)[]
            {
                (row - 1, col),
                (row + 1, col),
                (row, col - 1),
                (row, col + 1),
            };

            foreach (var neighbor in neighbors)
            {
                // don't move into walls
                if (grid.GetValueOrDefault(neighbor) == '#')
                {
                    continue;
                }

                // don't move into other amphipods
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

    private static bool TryToReachFinalDestination(
        Amphipod amphipod,
        (int Row, int Col) finalDestination,
        Amphipod[] amphipods,
        IReadOnlyDictionary<(int Row, int Col), char> grid,
        [NotNullWhen(true)] out Move? move)
    {
        var queue = new Queue<(int Row, int Col, int Steps)>();
        var seen = new HashSet<(int Row, int Col)>();

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

            if (position == finalDestination)
            {
                move = new Move(row, col, steps);
                return true;
            }

            var neighbors = new (int Row, int Col)[]
            {
                (row - 1, col),
                (row + 1, col),
                (row, col - 1),
                (row, col + 1),
            };

            foreach (var neighbor in neighbors)
            {
                // don't move into walls
                if (grid.GetValueOrDefault(neighbor) == '#')
                {
                    continue;
                }

                // don't move into other amphipods
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

        // look inside my target room for any amphipods that are the wrong type
        // if there are NONE, then my final destination is the deepest available row in the target room
        if (RoomContainsTypeThatDoesNotMatch(targetCol, type, amphipods))
        {
            finalDestination = default;
            return false;
        }

        var maxRow = grid.Keys.Max(x => x.Row) - 1;
        var minRow = 2;

        // rooms extend from row `2` to row `grid.Count - 2`
        for (var r = maxRow; r >= minRow; r--)
        {
            if (IsEmptySpace(r, targetCol, amphipods))
            {
                finalDestination = (r, targetCol);
                return true;
            }
        }

        DrawGrid(grid, amphipods);

        throw new UnreachableException("This should never happen!");
        finalDestination = default;
        return false;
    }

    private static bool IsAtFinalDestination(
        Amphipod amphipod,
        Amphipod[] amphipods,
        Dictionary<(int Row, int Col), char> grid)
    {
        var (row, col, type, energy, targetCol) = amphipod;

        // if I am in my target column...
        if (col != targetCol)
        {
            return false;
        }

        if (row is 0 or 1 || row == grid.Count - 1)
        {
            throw new UnreachableException("This should never happen!");
            return false;
        }

        // look below me for any amphipods that I am blocking
        // if there are NONE, then I am done moving
        for (var r = row + 1; r < grid.Count - 1; r++)
        {
            if (amphipods.Any(a => a.Row == r && a.Col == col && a.Type != type))
            {
                return false;
            }
        }

        return true;
    }

    public static int Solve2(string[] input)
    {
        throw new NotImplementedException();
    }

    private static bool IsFinalState(State state) =>
        state.Amphipods.All(a => a.Col == a.TargetCol);

    private static bool RoomContainsTypeThatDoesNotMatch(int targetCol, char type, IEnumerable<Amphipod> amphipods) =>
        amphipods.Any(a => a.Col == targetCol && a.Type != type);

    private static bool IsEmptySpace(int row, int col, IEnumerable<Amphipod> amphipods) =>
        !amphipods.Any(a => a.Row == row && a.Col == col);
}
