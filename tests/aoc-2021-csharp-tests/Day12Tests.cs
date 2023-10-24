using aoc_2021_csharp.Day12;

namespace aoc_2021_csharp_tests;

public class Day12Tests
{
    [Test]
    public void Part1_ReturnsCorrectAnswer()
    {
        Day12.Part1().Should().Be(5076);
    }

    [Test]
    public void Part2_ReturnsCorrectAnswer()
    {
        Day12.Part2().Should().Be(145643);
    }
}
