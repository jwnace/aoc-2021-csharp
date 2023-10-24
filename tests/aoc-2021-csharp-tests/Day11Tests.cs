using aoc_2021_csharp.Day11;

namespace aoc_2021_csharp_tests;

public class Day11Tests
{
    [Test]
    public void Part1_ReturnsCorrectAnswer()
    {
        Day11.Part1().Should().Be(1546);
    }

    [Test]
    public void Part2_ReturnsCorrectAnswer()
    {
        Day11.Part2().Should().Be(471);
    }
}
