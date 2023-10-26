using aoc_2021_csharp.Day23;

namespace aoc_2021_csharp_tests;

public class Day23Tests
{
    [TestCase(new[]
    {
        "#############",
        "#...........#",
        "###A#B#C#D###",
        "  #A#B#C#D#",
        "  #########",
    }, 0, TestName = "00000")]
    [TestCase(new[]
    {
        "#############",
        "#..........A#",
        "###.#B#C#D###",
        "  #A#B#C#D#",
        "  #########",
    }, 9, TestName = "00009")]
    [TestCase(new[]
    {
        "#############",
        "#...A.B.....#",
        "###.#.#C#D###",
        "  #A#B#C#D#",
        "  #########",
    }, 22, TestName = "00022")]
    [TestCase(new[]
    {
        "#############",
        "#...A.B.C...#",
        "###.#.#.#D###",
        "  #A#B#C#D#",
        "  #########",
    }, 222, TestName = "00222")]
    [TestCase(new[]
    {
        "#############",
        "#...A.B.C.D.#",
        "###.#.#.#.###",
        "  #A#B#C#D#",
        "  #########",
    }, 2222, TestName = "02222")]
    [TestCase(new[]
    {
        "#############",
        "#...D.C.B.A.#",
        "###.#.#.#.###",
        "  #A#B#C#D#",
        "  #########",
    }, 6248, TestName = "06248")]
    [TestCase(new[]
    {
        "#############",
        "#.....C.B.A.#",
        "###D#.#.#.###",
        "  #A#B#C#D#",
        "  #########",
    }, 8248, TestName = "08248")]
    [TestCase(new[]
    {
        "#############",
        "#.......B.A.#",
        "###D#C#.#.###",
        "  #A#B#C#D#",
        "  #########",
    }, 8448, TestName = "08448")]
    [TestCase(new[]
    {
        "#############",
        "#.........A.#",
        "###D#C#B#.###",
        "  #A#B#C#D#",
        "  #########",
    }, 8468, TestName = "08468")]
    [TestCase(new[]
    {
        "#############",
        "#...........#",
        "###D#C#B#A###",
        "  #A#B#C#D#",
        "  #########",
    }, 8470, TestName = "08470")]
    [TestCase(new[]
    {
        "#############",
        "#...B.......#",
        "###B#.#C#D###",
        "  #A#D#C#A#",
        "  #########",
    }, 12081, TestName = "12081")]
    [TestCase(new[]
    {
        "#############",
        "#...B.......#",
        "###B#C#.#D###",
        "  #A#D#C#A#",
        "  #########",
    }, 12481, TestName = "12481")]
    [TestCase(new[]
    {
        "#############",
        "#...........#",
        "###B#C#B#D###",
        "  #A#D#C#A#",
        "  #########",
    }, 12521, TestName = "12521")]
    public void Part1_Example_ReturnsCorrectAnswer(string[] input, int expected)
    {
        Day23.Solve1(input).Should().Be(expected);
    }

    [Test]
    public void Part1_ReturnsCorrectAnswer()
    {
        Day23.Part1().Should().Be(18195);
    }

    [TestCase(new[]
    {
        "#############",
        "#...........#",
        "###B#C#B#D###",
        "  #D#C#B#A#",
        "  #D#B#A#C#",
        "  #A#D#C#A#",
        "  #########",
    }, 44169, TestName = "44169")]
    public void Part2_Example_ReturnsCorrectAnswer(string[] input, int expected)
    {
        Day23.Solve2(input).Should().Be(expected);
    }

    [Test]
    public void Part2_ReturnsCorrectAnswer()
    {
        Day23.Part2().Should().Be(0);
    }
}
