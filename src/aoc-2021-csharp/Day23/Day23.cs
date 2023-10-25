using System.Collections.Generic;
using System.Linq;

namespace aoc_2021_csharp.Day23;

public static class Day23
{
    private static readonly string[] Input = File.ReadAllLines("Day23/day23.txt");

    public static int Part1() => Solve1(Input);

    public static int Part2() => Solve2(Input);

    public static int Solve1(string[] input)
    {
        var grid = BuildGrid(input);
        var initialState = BuildInitialState(grid);
        var seen = new HashSet<StateKey>();
        var queue = new PriorityQueue<State, int>();
        queue.Enqueue(initialState, 0);

        while (queue.Count > 0)
        {
            var state = queue.Dequeue();
            var stateKey = new StateKey(state);
            var (row1, col1, row2, col2, row3, col3, row4, col4, row5, col5, row6, col6, row7, col7, row8, col8,
                energy) = state;

            if (seen.Contains(stateKey))
            {
                continue;
            }

            if (seen.Count % 100_000 == 0)
            {
                Console.WriteLine($"Seen: {seen.Count,15:N0} | Queue: {queue.Count,15:N0}");
            }

            seen.Add(stateKey);

            if (IsFinalState(stateKey))
            {
                return energy;
            }

            if (!IsValidState(stateKey, grid))
            {
                continue;
            }

            for (var i = 0; i < 8; i++)
            {
                var (row, col) = i switch
                {
                    0 => (row1, col1),
                    1 => (row2, col2),
                    2 => (row3, col3),
                    3 => (row4, col4),
                    4 => (row5, col5),
                    5 => (row6, col6),
                    6 => (row7, col7),
                    7 => (row8, col8),
                    _ => throw new IndexOutOfRangeException("i must be between 0 and 7"),
                };

                // don't try to move if I'm already at my destination
                if (IsAtDestination(row, col, i, stateKey))
                {
                    continue;
                }

                var energyCost = i switch
                {
                    0 or 1 => 1,
                    2 or 3 => 10,
                    4 or 5 => 100,
                    6 or 7 => 1000,
                    _ => throw new IndexOutOfRangeException("i must be between 0 and 7"),
                };

                var neighbors = new (int Row, int Col)[]
                {
                    (row - 1, col),
                    (row + 1, col),
                    (row, col - 1),
                    (row, col + 1),
                };

                foreach (var neighbor in neighbors)
                {
                    // if we're moving into a doorway, we need to move twice
                    var doorways = new (int row, int Col)[]
                    {
                        (1, 3),
                        (1, 5),
                        (1, 7),
                        (1, 9),
                    };

                    if (doorways.Contains(neighbor))
                    {
                        var left = (Row: neighbor.Row, Col: neighbor.Col - 1);
                        var right = (Row: neighbor.Row, Col: neighbor.Col + 1);

                        // TODO: handle the rules for moving down into a side room from the hallway!!
                        var down = (Row: neighbor.Row + 1, Col: neighbor.Col);
                        // throw new NotImplementedException("TODO: handle the rules for moving down into a side room from the hallway!!");

                        var occupiedSpaces = new (int Row, int Col)[]
                        {
                            (row1, col1),
                            (row2, col2),
                            (row3, col3),
                            (row4, col4),
                            (row5, col5),
                            (row6, col6),
                            (row7, col7),
                            (row8, col8),
                        };

                        if (occupiedSpaces.Contains(left) && occupiedSpaces.Contains(right) && occupiedSpaces.Contains(down))
                        {
                            continue;
                        }

                        if (!occupiedSpaces.Contains(left))
                        {
                            var leftState = BuildNewState(i, left, energy, energyCost * 2, row1, col1, row2, col2, row3, col3,
                                row4, col4, row5, col5, row6, col6, row7, col7, row8, col8);

                            queue.Enqueue(leftState, energy + energyCost * 2);
                        }

                        if (!occupiedSpaces.Contains(right))
                        {
                            var rightState = BuildNewState(i, right, energy, energyCost * 2, row1, col1, row2, col2, row3, col3,
                                row4, col4, row5, col5, row6, col6, row7, col7, row8, col8);

                            queue.Enqueue(rightState, energy + energyCost * 2);
                        }

                        // TODO: this is incomplete... there are more rules about when we're allowed to do this
                        if (!occupiedSpaces.Contains(down) && IsDestinationRoom(down.Col, i, stateKey, occupiedSpaces))
                        {
                            var downState = BuildNewState(i, down, energy, energyCost * 2, row1, col1, row2, col2, row3, col3,
                                row4, col4, row5, col5, row6, col6, row7, col7, row8, col8);

                            queue.Enqueue(downState, energy + energyCost * 2);
                        }

                        continue;
                    }

                    var newState = BuildNewState(i, neighbor, energy, energyCost, row1, col1, row2, col2, row3, col3,
                        row4, col4, row5, col5, row6, col6, row7, col7, row8, col8);

                    queue.Enqueue(newState, energy + energyCost);
                }
            }
        }

        DrawGrid(grid);

        throw new Exception("No solution found!");
    }

    private static bool IsDestinationRoom(int col, int i, StateKey stateKey, (int Row, int Col)[] occupiedSpaces)
    {
        var (row1, col1, row2, col2, row3, col3, row4, col4, row5, col5, row6, col6, row7, col7, row8, col8) = stateKey;

        // TODO: for part 2, get the intersection of `occupiedSpaces` and `roomSpaces`

        return i switch
        {
            0 => col is 3 && !occupiedSpaces.Contains((3, col)) || (row2 is 3 && col2 is 3),
            1 => col is 3 && !occupiedSpaces.Contains((3, col)) || (row1 is 3 && col1 is 3),
            2 => col is 5 && !occupiedSpaces.Contains((3, col)) || (row4 is 3 && col4 is 5),
            3 => col is 5 && !occupiedSpaces.Contains((3, col)) || (row3 is 3 && col3 is 5),
            4 => col is 7 && !occupiedSpaces.Contains((3, col)) || (row6 is 3 && col6 is 7),
            5 => col is 7 && !occupiedSpaces.Contains((3, col)) || (row5 is 3 && col5 is 7),
            6 => col is 9 && !occupiedSpaces.Contains((3, col)) || (row8 is 3 && col8 is 9),
            7 => col is 9 && !occupiedSpaces.Contains((3, col)) || (row7 is 3 && col7 is 9),
            _ => throw new IndexOutOfRangeException("i must be between 0 and 7"),
        };
    }

    private static bool IsAtDestination(int row, int col, int i, StateKey stateKey)
    {
        var (row1, col1, row2, col2, row3, col3, row4, col4, row5, col5, row6, col6, row7, col7, row8, col8) = stateKey;

        return i switch
        {
            0 => row is 3 && col is 3 || (row is 2 && col is 3 && row2 is 3 && col2 is 3),
            1 => row is 3 && col is 3 || (row is 2 && col is 3 && row1 is 3 && col1 is 3),
            2 => row is 3 && col is 5 || (row is 2 && col is 5 && row4 is 3 && col4 is 5),
            3 => row is 3 && col is 5 || (row is 2 && col is 5 && row3 is 3 && col3 is 5),
            4 => row is 3 && col is 7 || (row is 2 && col is 7 && row6 is 3 && col6 is 7),
            5 => row is 3 && col is 7 || (row is 2 && col is 7 && row5 is 3 && col5 is 7),
            6 => row is 3 && col is 9 || (row is 2 && col is 9 && row8 is 3 && col8 is 9),
            7 => row is 3 && col is 9 || (row is 2 && col is 9 && row7 is 3 && col7 is 9),
            _ => throw new IndexOutOfRangeException("i must be between 0 and 7"),
        };
    }

    private static State BuildNewState(int i, (int, int) neighbor, int energy, int energyCost, int row1, int col1,
        int row2, int col2, int row3, int col3, int row4, int col4, int row5, int col5, int row6, int col6, int row7,
        int col7, int row8, int col8)
    {
        switch (i)
        {
            case 0:
                (row1, col1) = neighbor;
                break;
            case 1:
                (row2, col2) = neighbor;
                break;
            case 2:
                (row3, col3) = neighbor;
                break;
            case 3:
                (row4, col4) = neighbor;
                break;
            case 4:
                (row5, col5) = neighbor;
                break;
            case 5:
                (row6, col6) = neighbor;
                break;
            case 6:
                (row7, col7) = neighbor;
                break;
            case 7:
                (row8, col8) = neighbor;
                break;
        }

        var newState = new State(row1, col1, row2, col2, row3, col3, row4, col4, row5, col5, row6, col6,
            row7, col7, row8, col8, energy + energyCost);
        return newState;
    }

    private static bool IsValidState(StateKey stateKey, Dictionary<(int Row, int Col), char> grid)
    {
        var (row1, col1, row2, col2, row3, col3, row4, col4, row5, col5, row6, col6, row7, col7, row8, col8) = stateKey;

        var occupiedSpaces = new (int Row, int Col)[]
        {
            (row1, col1),
            (row2, col2),
            (row3, col3),
            (row4, col4),
            (row5, col5),
            (row6, col6),
            (row7, col7),
            (row8, col8),
        };

        // check if we have moved into a wall
        if (occupiedSpaces.Any(s => grid.TryGetValue(s, out var v) && v == '#'))
        {
            return false;
        }

        // check if any spaces are occupied by more than one amphipod
        if (occupiedSpaces.Distinct().Count() != 8)
        {
            return false;
        }

        var doorways = new[]
        {
            (1, 3),
            (1, 5),
            (1, 7),
            (1, 9),
        };

        // TODO: handle doorways properly
        // check if more than one amphipod is in a doorway
        if (occupiedSpaces.Intersect(doorways).Count() > 1)
        {
            // throw new Exception("There should never be more than one amphipod in a doorway at the same time!");
            return false;
        }

        return true;
    }

    private static bool IsFinalState(StateKey stateKey)
    {
        return stateKey.Row1 is 2 or 3 && stateKey.Col1 is 3 && stateKey.Row2 is 2 or 3 && stateKey.Col2 is 3 &&
               stateKey.Row3 is 2 or 3 && stateKey.Col3 is 5 && stateKey.Row4 is 2 or 3 && stateKey.Col4 is 5 &&
               stateKey.Row5 is 2 or 3 && stateKey.Col5 is 7 && stateKey.Row6 is 2 or 3 && stateKey.Col6 is 7 &&
               stateKey.Row7 is 2 or 3 && stateKey.Col7 is 9 && stateKey.Row8 is 2 or 3 && stateKey.Col8 is 9;
    }

    private static State BuildInitialState(Dictionary<(int Row, int Col), char> grid)
    {
        var a = grid.Where(g => g.Value == 'A').Select(g => g.Key).ToArray();
        var b = grid.Where(g => g.Value == 'B').Select(g => g.Key).ToArray();
        var c = grid.Where(g => g.Value == 'C').Select(g => g.Key).ToArray();
        var d = grid.Where(g => g.Value == 'D').Select(g => g.Key).ToArray();

        var (row1, col1) = a[0];
        var (row2, col2) = a[1];
        var (row3, col3) = b[0];
        var (row4, col4) = b[1];
        var (row5, col5) = c[0];
        var (row6, col6) = c[1];
        var (row7, col7) = d[0];
        var (row8, col8) = d[1];

        return new State(row1, col1, row2, col2, row3, col3, row4, col4, row5, col5, row6, col6, row7, col7, row8, col8,
            0);
    }

    private static Dictionary<(int Row, int Col), char> BuildGrid(string[] input)
    {
        var grid = new Dictionary<(int Row, int Col), char>();

        for (var row = 0; row < input.Length; row++)
        {
            for (var col = 0; col < input[row].Length; col++)
            {
                grid[(row, col)] = input[row][col];
            }
        }

        return grid;
    }

    private static void DrawGrid(Dictionary<(int Row, int Col), char> grid)
    {
        var minRow = grid.Keys.Min(k => k.Row);
        var maxRow = grid.Keys.Max(k => k.Row);
        var minCol = grid.Keys.Min(k => k.Col);
        var maxCol = grid.Keys.Max(k => k.Col);

        for (var row = minRow; row <= maxRow; row++)
        {
            for (var col = minCol; col <= maxCol; col++)
            {
                var value = grid.TryGetValue((row, col), out var v) ? v : ' ';

                Console.Write(value);
            }

            Console.WriteLine();
        }
    }

    public static int Solve2(string[] input)
    {
        throw new NotImplementedException();
    }
}

public record State(int Row1, int Col1, int Row2, int Col2, int Row3, int Col3, int Row4, int Col4, int Row5, int Col5,
    int Row6, int Col6, int Row7, int Col7, int Row8, int Col8, int Energy);

public record StateKey(int Row1, int Col1, int Row2, int Col2, int Row3, int Col3, int Row4, int Col4, int Row5,
    int Col5, int Row6, int Col6, int Row7, int Col7, int Row8, int Col8)
{
    public StateKey(State state) : this(state.Row1, state.Col1, state.Row2, state.Col2, state.Row3, state.Col3,
        state.Row4, state.Col4, state.Row5, state.Col5, state.Row6, state.Col6, state.Row7, state.Col7, state.Row8,
        state.Col8)
    {
    }
}
