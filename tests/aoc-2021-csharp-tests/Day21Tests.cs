using aoc_2021_csharp.Day21;

namespace aoc_2021_csharp_tests;

public class Day21Tests
{
    [Test]
    public void Part1_ReturnsCorrectAnswer()
    {
        Day21.Part1().Should().Be(671580);
    }

    [Test]
    public void Part2_ReturnsCorrectAnswer()
    {
        Day21.Part2().Should().Be(912857726749764);
    }
}
