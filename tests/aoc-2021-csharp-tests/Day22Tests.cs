using aoc_2021_csharp.Day22;

namespace aoc_2021_csharp_tests;

public class Day22Tests
{
    [Test]
    public void Part1_ReturnsCorrectAnswer()
    {
        Day22.Part1().Should().Be(533863);
    }

    [Test]
    public void Part2_ReturnsCorrectAnswer()
    {
        Day22.Part2().Should().Be(1261885414840992);
    }
}
