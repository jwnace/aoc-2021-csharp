using aoc_2021_csharp.Day02;

namespace aoc_2021_csharp_tests;

public class Day02Tests
{
    [Test]
    public void Part1_ReturnsCorrectAnswer()
    {
        Day02.Part1().Should().Be(1507611);
    }

    [Test]
    public void Part2_ReturnsCorrectAnswer()
    {
        Day02.Part2().Should().Be(1880593125);
    }
}
