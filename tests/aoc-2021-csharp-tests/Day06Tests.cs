using aoc_2021_csharp.Day06;

namespace aoc_2021_csharp_tests;

public class Day06Tests
{
    [Test]
    public void Part1_ReturnsCorrectAnswer()
    {
        Day06.Part1().Should().Be(355386);
    }

    [Test]
    public void Part2_ReturnsCorrectAnswer()
    {
        Day06.Part2().Should().Be(1613415325809);
    }
}
