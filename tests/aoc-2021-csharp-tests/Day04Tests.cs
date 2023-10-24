using aoc_2021_csharp.Day04;

namespace aoc_2021_csharp_tests;

public class Day04Tests
{
    [Test]
    public void Part1_ReturnsCorrectAnswer()
    {
        Day04.Part1().Should().Be(74320);
    }

    [Test]
    public void Part2_ReturnsCorrectAnswer()
    {
        Day04.Part2().Should().Be(17884);
    }
}
