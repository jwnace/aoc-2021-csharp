using aoc_2021_csharp.Day18;

namespace aoc_2021_csharp_tests;

public class Day18Tests
{
    [Test]
    public void Part1_ReturnsCorrectAnswer()
    {
        Day18.Part1().Should().Be(4480);
    }

    [Test]
    public void Part2_ReturnsCorrectAnswer()
    {
        Day18.Part2().Should().Be(4676);
    }
}
