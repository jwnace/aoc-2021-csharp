using aoc_2021_csharp.Day01;

namespace aoc_2021_csharp_tests;

public class Day01Tests
{
    [Test]
    public void Part1_ReturnsCorrectAnswer()
    {
        Day01.Part1().Should().Be(1393);
    }

    [Test]
    public void Part2_ReturnsCorrectAnswer()
    {
        Day01.Part2().Should().Be(1359);
    }
}
