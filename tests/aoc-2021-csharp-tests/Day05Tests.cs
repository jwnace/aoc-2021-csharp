using aoc_2021_csharp.Day05;

namespace aoc_2021_csharp_tests;

public class Day05Tests
{
    [Test]
    public void Part1_ReturnsCorrectAnswer()
    {
        Day05.Part1().Should().Be(7436);
    }

    [Test]
    public void Part2_ReturnsCorrectAnswer()
    {
        Day05.Part2().Should().Be(21104);
    }
}
