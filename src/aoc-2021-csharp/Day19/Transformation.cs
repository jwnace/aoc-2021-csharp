namespace aoc_2021_csharp.Day19;

public record Transformation(int XRot, int XFlip, int YRot, int YFlip, int ZRot, int ZFlip)
{
    public Transformation Reverse()
    {
        return new Transformation(
            XRot: (4 - XRot) % 4,
            XFlip: XFlip,
            YRot: (4 - YRot) % 4,
            YFlip: YFlip,
            ZRot: (4 - ZRot) % 4,
            ZFlip: ZFlip);
    }
}
