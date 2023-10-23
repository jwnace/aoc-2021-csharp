using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace aoc_2021_csharp.Day04;

public static class Day04
{
    private static readonly string[] Input = File.ReadAllLines("Day04/day04.txt");

    public static int Part1()
    {
        // get the numbers to be drawn
        var numbers = Input[0].Split(',').Select(int.Parse).ToList();

        // populate the boards from the input file
        var boards = PopulateBoards();

        // iterate over all the numbers to be drawn
        foreach (var number in numbers)
        {
            // check each board to see if it has the number, and mark the cell if it is found
            foreach (var b in boards)
            {
                b.Cells.Cast<Cell>()
                    .Where(x => x.Value == number)
                    .ToList()
                    .ForEach(x => x.Marked = true);
            }

            // iterate over all the boards to check for winners
            foreach (var b in boards)
            {
                var win = CheckForWin(b);

                if (win)
                {
                    // calculate the sum of all of the unmarked cells cells
                    var sum = b.Cells.Cast<Cell>().Where(x => !x.Marked).Sum(x => x.Value);

                    return sum * number;
                }
            }
        }

        throw new Exception("No solution found!");
    }

    public static int Part2()
    {
        // get the numbers to be drawn
        var numbers = Input[0].Split(',').Select(x => int.Parse(x)).ToList();

        // populate the boards from the input file
        var boards = PopulateBoards();

        // keep track of the most recent board to win, and the winning number
        Board lastToWin = null;
        var lastNumber = 0;

        // iterate over all the numbers to be drawn
        foreach (var number in numbers)
        {
            // check each board to see if it has the number, and mark the cell if it is found
            foreach (var b in boards)
            {
                // if this board has already won, ignore it and move on
                if (b.HasWon)
                {
                    continue;
                }

                b.Cells.Cast<Cell>()
                    .Where(x => x.Value == number)
                    .ToList()
                    .ForEach(x => x.Marked = true);
            }

            // iterate over all the boards to check for winners
            foreach (var b in boards)
            {
                // if this board has already won, ignore it and move on
                if (b.HasWon)
                {
                    continue;
                }

                var win = CheckForWin(b);

                if (win)
                {
                    lastToWin = b;
                    lastNumber = number;
                }
            }
        }

        var sum = lastToWin.Cells.Cast<Cell>().Where(x => !x.Marked).Sum(x => x.Value);

        return sum * lastNumber;
    }

    private class Board
    {
        public Cell[,] Cells { get; set; } = new Cell[5, 5];
        public bool HasWon { get; set; } = false;
    }

    private class Cell
    {
        public int Value { get; set; } = 0;
        public bool Marked { get; set; } = false;
    }

    private static List<Board> PopulateBoards()
    {
        var boards = new List<Board>();
        var board = new Board();
        var row = 0;

        for (var i = 2; i < Input.Length; i++)
        {
            if (string.IsNullOrWhiteSpace(Input[i]))
            {
                boards.Add(board);
                board = new Board();
                row = 0;
                continue;
            }

            var values = Input[i].Split(null).Where(x => !string.IsNullOrWhiteSpace(x)).Select(x => int.Parse(x)).ToList();

            for (var col = 0; col < values.Count(); col++)
            {
                board.Cells[row, col] = new Cell { Value = values[col], Marked = false };
            }

            row++;
        }

        return boards;
    }

    private static bool CheckForWin(Board board)
    {
        for (var i = 0; i < 5; i++)
        {
            if ((board.Cells[i, 0].Marked && board.Cells[i, 1].Marked && board.Cells[i, 2].Marked && board.Cells[i, 3].Marked && board.Cells[i, 4].Marked)
                || (board.Cells[0, i].Marked && board.Cells[1, i].Marked && board.Cells[2, i].Marked && board.Cells[3, i].Marked && board.Cells[4, i].Marked))
            {
                board.HasWon = true;
                return true;
            }
        }

        return false;
    }
}
