using aoc_2021_csharp.Day16;

namespace aoc_2021_csharp_tests;

public class Day16Tests
{
    [Test]
    public void Part1_ReturnsCorrectAnswer()
    {
        Day16.Part1().Should().Be(929);
    }

    [Test]
    public void Part2_ReturnsCorrectAnswer()
    {
        Day16.Part2().Should().Be(911945136934);
    }
}
