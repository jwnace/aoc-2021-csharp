using aoc_2021_csharp.Day03;

namespace aoc_2021_csharp_tests;

public class Day03Tests
{
    [Test]
    public void Part1_ReturnsCorrectAnswer()
    {
        Day03.Part1().Should().Be(2724524);
    }

    [Test]
    public void Part2_ReturnsCorrectAnswer()
    {
        Day03.Part2().Should().Be(2775870);
    }
}
