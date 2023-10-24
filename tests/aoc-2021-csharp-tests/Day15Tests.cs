using aoc_2021_csharp.Day15;

namespace aoc_2021_csharp_tests;

public class Day15Tests
{
    [Test]
    public void Part1_ReturnsCorrectAnswer()
    {
        Day15.Part1().Should().Be(685);
    }

    [Test]
    public void Part2_ReturnsCorrectAnswer()
    {
        Day15.Part2().Should().Be(2995);
    }
}
