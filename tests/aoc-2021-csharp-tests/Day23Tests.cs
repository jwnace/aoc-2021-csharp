using aoc_2021_csharp.Day23;

namespace aoc_2021_csharp_tests;

public class Day23Tests
{
    [Test]
    public void Part1_Example_ReturnsCorrectAnswer()
    {
        var input = new[]
        {
            "#############",
            "#...........#",
            "###B#C#B#D###",
            "  #A#D#C#A#",
            "  #########",
        };

        Day23.Solve1(input).Should().Be(12521);
    }

    [Test]
    public void Part1_ReturnsCorrectAnswer()
    {
        Day23.Part1().Should().Be(18195);
    }

    [Test]
    public void Part2_Example_ReturnsCorrectAnswer()
    {
        var input = new[]
        {
            "#############",
            "#...........#",
            "###B#C#B#D###",
            "  #D#C#B#A#",
            "  #D#B#A#C#",
            "  #A#D#C#A#",
            "  #########",
        };

        Day23.Solve2(input).Should().Be(44169);
    }

    [Test]
    public void Part2_ReturnsCorrectAnswer()
    {
        Day23.Part2().Should().Be(0);
    }
}
