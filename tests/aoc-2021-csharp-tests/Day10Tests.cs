using aoc_2021_csharp.Day10;

namespace aoc_2021_csharp_tests;

public class Day10Tests
{
    [Test]
    public void Part1_ReturnsCorrectAnswer()
    {
        Day10.Part1().Should().Be(387363);
    }

    [Test]
    public void Part2_ReturnsCorrectAnswer()
    {
        Day10.Part2().Should().Be(4330777059);
    }
}
