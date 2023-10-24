using aoc_2021_csharp.Day14;

namespace aoc_2021_csharp_tests;

public class Day14Tests
{
    [Test]
    public void Part1_ReturnsCorrectAnswer()
    {
        Day14.Part1().Should().Be(2321);
    }

    [Test]
    public void Part2_ReturnsCorrectAnswer()
    {
        Day14.Part2().Should().Be(2399822193707);
    }
}
