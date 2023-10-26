using System.Collections.Generic;
using System.Linq;

namespace aoc_2021_csharp.Day23;

public static partial class Day23
{
    private static readonly string[] Input = File.ReadAllLines("Day23/day23.txt");

    public static int Part1() => Solve1(Input);

    public static int Part2() => Solve2(Input);

    public static int Solve1(string[] input)
    {
        // build the grid from the input
        var grid = BuildGrid(input);
        DrawGrid(grid, Array.Empty<Amphipod>(), Array.Empty<(int Row, int Col)>());

        // build the initial state from the grid
        var initialState = BuildInitialState(grid);
        DrawGrid(grid, initialState.Amphipods, Array.Empty<(int Row, int Col)>());

        // set up a queue and a hashset for BFS
        var seen = new HashSet<State>();
        var queue = new PriorityQueue<State, int>();
        queue.Enqueue(initialState, 0);

        // build a list of doorways
        var doorways = new[] { (1, 3), (1, 5), (1, 7), (1, 9), };
        DrawGrid(grid, initialState.Amphipods, Array.Empty<(int Row, int Col)>());

        // while there are still states in the queue
        while (queue.Count > 0)
        {
            // get the next state to consider
            var state = queue.Dequeue();

            // pull the details out of the state
            var (amphipods, energy) = state;

            // TODO: double check that this is working as intended
            if (seen.Contains(state))
            {
                continue;
            }

            // log to the console every 100,000 states
            if (seen.Count % 100_000 == 0)
            {
                Console.WriteLine($"Seen: {seen.Count,15:N0} | Queue: {queue.Count,15:N0}");
            }

            // mark the current state as seen
            seen.Add(state);

            // if we have reached the final state, return the energy it took to get here
            if (IsFinalState(state))
            {
                // Console.WriteLine();
                // DrawGrid(grid, initialState.Amphipods, doorways);
                Console.WriteLine();
                DrawGrid(grid, amphipods, Array.Empty<(int Row, int Col)>());
                Console.WriteLine();

                return energy;
            }

            // TODO: I'm not sure this is relevant anymore
            // if any amphipods are in a doorway, throw an exception
            // if (amphipods.Any(a => doorways.Contains((a.Row, a.Col))))
            // {
            //     throw new Exception("Amphipod is in a doorway!");
            // }

            // if we are in an invalid state, skip it
            if (!IsValidState(grid, amphipods))
            {
                continue;
            }

            // give each amphipod a chance to move
            foreach (var amphipod in amphipods)
            {
                var (row, col, type, cost, targetCol) = amphipod;

                // if this amphipod is already at its destination, skip it
                if (IsAtDestination(amphipod, amphipods))
                {
                    continue;
                }

                // if this amphipod is in the hallway and its destination is available, move directly to its destination
                if (row == 1 && IsDestinationAvailable(amphipod, amphipods, out var destination, out var steps))
                {
                    // TODO: this should never happen... not sure why this is here
                    // if the destination is a doorway, throw an exception
                    // if (doorways.Contains(destination))
                    // {
                    //     throw new Exception("Destination is a doorway!");
                    // }

                    var newState = BuildNewState(amphipods, amphipod, destination, doorways, energy, cost * steps);
                    queue.Enqueue(newState, energy + cost * steps);
                    continue;
                }

                // if this amphipod is in the hallway and its destination is blocked, skip it
                if (row == 1)
                {
                    // Console.WriteLine("Not moving because I'm in the hallway and my destination is blocked!");
                    // Console.WriteLine();
                    // DrawGrid(grid, amphipods);
                    // Console.WriteLine();
                    continue;
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
                    // if we're moving into a doorway, we need to move twice
                    if (doorways.Contains(neighbor))
                    {
                        var left = neighbor with { Col = neighbor.Col - 1 };
                        var right = neighbor with { Col = neighbor.Col + 1 };

                        if (amphipods.Any(a => (a.Row, a.Col) == left) && amphipods.Any(a => (a.Row, a.Col) == right))
                        {
                            continue;
                        }

                        if (amphipods.All(a => (a.Row, a.Col) != left))
                        {
                            var leftState = BuildNewState(amphipods, amphipod, left, doorways, energy, cost * 2);
                            queue.Enqueue(leftState, energy + cost * 2);
                        }

                        if (amphipods.All(a => (a.Row, a.Col) != right))
                        {
                            var rightState = BuildNewState(amphipods, amphipod, right, doorways, energy, cost * 2);
                            queue.Enqueue(rightState, energy + cost * 2);
                        }

                        continue;
                    }

                    // if we're moving into a wall, skip it
                    if (grid.GetValueOrDefault(neighbor) == '#')
                    {
                        continue;
                    }

                    var neighborState = BuildNewState(amphipods, amphipod, neighbor, doorways, energy, cost);
                    queue.Enqueue(neighborState, energy + cost);
                }
            }
        }

        throw new Exception("No solution found!");
    }

    public static int Solve2(string[] input)
    {
        throw new NotImplementedException();
    }


    private static State BuildNewState(Amphipod[] amphipods, Amphipod amphipod, (int Row, int Col) destination,
        (int, int)[] doorways, int energy, int energyCost)
    {
        var newAmphipods = amphipods.ToList();
        newAmphipods.Remove(amphipod);

        var newAmphipod = amphipod with { Row = destination.Row, Col = destination.Col, };

        if (doorways.Contains((newAmphipod.Row, newAmphipod.Col)))
        {
            throw new Exception("Trying to move into a doorway!");
        }

        newAmphipods.Add(newAmphipod);

        return new State(newAmphipods.ToArray(), energy + energyCost);
    }

    private static bool IsDestinationAvailable(
        Amphipod amphipod,
        Amphipod[] amphipods,
        out (int Row, int Col) destination,
        out int steps)
    {
        // TODO: get rid of hard coded integers
        if (amphipods.All(a => (a.Row, a.Col) != (2, amphipod.TargetCol)) && // nothing in the front of the room
            amphipods.All(a => (a.Row, a.Col) != (3, amphipod.TargetCol)) && // nothing all the way in the room
            !amphipods.Any(a =>
                a.Row == 1 && a.Col >= amphipod.TargetCol &&
                a.Col < amphipod.Col) && // nothing blocking the hallway to the left
            !amphipods.Any(a =>
                a.Row == 1 && a.Col <= amphipod.TargetCol &&
                a.Col > amphipod.Col)) // nothing blocking the hallway to the right
        {
            destination = (3, amphipod.TargetCol);
            steps = GetManhattanDistance((amphipod.Row, amphipod.Col), destination);
            return true;
        }

        // TODO: get rid of hard coded integers
        if (amphipods.All(a => (a.Row, a.Col) != (2, amphipod.TargetCol)) && // nothing in the front of the room
            amphipods.Any(a => (a.Row, a.Col) == (3, amphipod.TargetCol)) && // something all the way in the room
            amphipods.Single(a => (a.Row, a.Col) == (3, amphipod.TargetCol)).Type ==
            amphipod.Type && // correct type all the way in the room
            !amphipods.Any(a =>
                a.Row == 1 && a.Col >= amphipod.TargetCol &&
                a.Col < amphipod.Col) && // nothing blocking the hallway to the left
            !amphipods.Any(a =>
                a.Row == 1 && a.Col <= amphipod.TargetCol &&
                a.Col > amphipod.Col)) // nothing blocking the hallway to the right
        {
            destination = (2, amphipod.TargetCol);
            steps = GetManhattanDistance((amphipod.Row, amphipod.Col), destination);
            return true;
        }

        destination = (0, 0);
        steps = 0;
        return false;
    }

    private static int GetManhattanDistance((int Row, int Col) first, (int Row, int Col) second)
    {
        return Math.Abs(first.Row - second.Row) + Math.Abs(first.Col - second.Col);
    }

    private static bool IsAtDestination(Amphipod amphipod, Amphipod[] amphipods)
    {
        var destination = (3, amphipod.TargetCol);

        if ((amphipod.Row, amphipod.Col) == destination)
        {
            return true;
        }

        var foo = amphipods.FirstOrDefault(a => (a.Row, a.Col) == destination);

        if (foo?.Type == amphipod.Type && (amphipod.Row, amphipod.Col) == (2, amphipod.TargetCol))
        {
            return true;
        }

        return false;
    }

    private static bool IsValidState(Dictionary<(int Row, int Col), char> grid, Amphipod[] amphipods)
    {
        // check if we have moved into a wall
        if (amphipods.Any(a => grid[(a.Row, a.Col)] == '#'))
        {
            return false;
        }

        // check if any spaces are occupied by more than one amphipod
        if (amphipods.DistinctBy(a => (a.Row, a.Col)).Count() != 8)
        {
            return false;
        }

        return true;
    }

    private static bool IsFinalState(State state)
    {
        return state.Amphipods.All(a => a.Col == a.TargetCol && a.Row > 1);
    }
}
