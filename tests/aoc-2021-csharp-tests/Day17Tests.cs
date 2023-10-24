using aoc_2021_csharp.Day17;

namespace aoc_2021_csharp_tests;

public class Day17Tests
{
    [Test]
    public void Part1_ReturnsCorrectAnswer()
    {
        Day17.Part1().Should().Be(4095);
    }

    [Test]
    public void Part2_ReturnsCorrectAnswer()
    {
        Day17.Part2().Should().Be(3773);
    }
}
