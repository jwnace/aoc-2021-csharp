using aoc_2021_csharp.Day08;

namespace aoc_2021_csharp_tests;

public class Day08Tests
{
    [Test]
    public void Part1_ReturnsCorrectAnswer()
    {
        Day08.Part1().Should().Be(421);
    }

    [Test]
    public void Part2_ReturnsCorrectAnswer()
    {
        Day08.Part2().Should().Be(986163);
    }
}
