using aoc_2021_csharp.Day07;

namespace aoc_2021_csharp_tests;

public class Day07Tests
{
    [Test]
    public void Part1_ReturnsCorrectAnswer()
    {
        Day07.Part1().Should().Be(340052);
    }

    [Test]
    public void Part2_ReturnsCorrectAnswer()
    {
        Day07.Part2().Should().Be(92948968);
    }
}
