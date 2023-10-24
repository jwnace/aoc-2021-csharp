using aoc_2021_csharp.Day20;

namespace aoc_2021_csharp_tests;

public class Day20Tests
{
    [Test]
    public void Part1_ReturnsCorrectAnswer()
    {
        Day20.Part1().Should().Be(5349);
    }

    [Test]
    public void Part2_ReturnsCorrectAnswer()
    {
        Day20.Part2().Should().Be(15806);
    }
}
