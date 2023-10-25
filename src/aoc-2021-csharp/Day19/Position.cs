namespace aoc_2021_csharp.Day19;

public record Position(int X, int Y, int Z)
{
    public static Position operator +(Position position, Position other) =>
        new(position.X + other.X, position.Y + other.Y, position.Z + other.Z);

    public static Position operator -(Position position, Position other) =>
        new(position.X - other.X, position.Y - other.Y, position.Z - other.Z);

    public Position Transform(Transformation transformation)
    {
        var (rotX, flipX, rotY, flipY, rotZ, flipZ) = transformation;

        return RotateX(rotX).FlipX(flipX).RotateY(rotY).FlipY(flipY).RotateZ(rotZ).FlipZ(flipZ);
    }

    public Position RotateX(int xRot)
    {
        return xRot switch
        {
            0 => this,
            1 => new Position(X, Z, -Y),
            2 => new Position(X, -Y, -Z),
            3 => new Position(X, -Z, Y),
            _ => throw new ArgumentOutOfRangeException(nameof(xRot))
        };
    }

    public Position RotateY(int yRot)
    {
        return yRot switch
        {
            0 => this,
            1 => new Position(-Z, Y, X),
            2 => new Position(-X, Y, -Z),
            3 => new Position(Z, Y, -X),
            _ => throw new ArgumentOutOfRangeException(nameof(yRot))
        };
    }

    public Position RotateZ(int zRot)
    {
        return zRot switch
        {
            0 => this,
            1 => new Position(-Y, X, Z),
            2 => new Position(-X, -Y, Z),
            3 => new Position(Y, -X, Z),
            _ => throw new ArgumentOutOfRangeException(nameof(zRot))
        };
    }

    public Position FlipX(int xFlip)
    {
        return xFlip switch
        {
            0 => this,
            1 => new Position(-X, Y, Z),
            _ => throw new ArgumentOutOfRangeException(nameof(xFlip))
        };
    }

    public Position FlipY(int yFlip)
    {
        return yFlip switch
        {
            0 => this,
            1 => new Position(X, -Y, Z),
            _ => throw new ArgumentOutOfRangeException(nameof(yFlip))
        };
    }

    public Position FlipZ(int zFlip)
    {
        return zFlip switch
        {
            0 => this,
            1 => new Position(X, Y, -Z),
            _ => throw new ArgumentOutOfRangeException(nameof(zFlip))
        };
    }
}
