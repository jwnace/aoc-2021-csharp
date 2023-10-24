using aoc_2021_csharp.Day13;

namespace aoc_2021_csharp_tests;

public class Day13Tests
{
    [Test]
    public void Part1_ReturnsCorrectAnswer()
    {
        Day13.Part1().Should().Be(661);
    }

    [Test]
    public void Part2_ReturnsCorrectAnswer()
    {
        var expected =
            Environment.NewLine + "###  #### #  # #    #  #  ##  #### ### " +
            Environment.NewLine + "#  # #    # #  #    # #  #  # #    #  #" +
            Environment.NewLine + "#  # ###  ##   #    ##   #    ###  #  #" +
            Environment.NewLine + "###  #    # #  #    # #  #    #    ### " +
            Environment.NewLine + "#    #    # #  #    # #  #  # #    #   " +
            Environment.NewLine + "#    #    #  # #### #  #  ##  #    #   ";

            Day13.Part2().Should().Be(expected);
    }
}
