using aoc_2021_csharp.Day09;

namespace aoc_2021_csharp_tests;

public class Day09Tests
{
    [Test]
    public void Part1_ReturnsCorrectAnswer()
    {
        Day09.Part1().Should().Be(494);
    }

    [Test]
    public void Part2_ReturnsCorrectAnswer()
    {
        Day09.Part2().Should().Be(1048128);
    }
}
