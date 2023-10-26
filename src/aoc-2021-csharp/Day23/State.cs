using System.Linq;

namespace aoc_2021_csharp.Day23;

public record State(Amphipod[] Amphipods, int Energy)
{
    public virtual bool Equals(State? other) => other is not null && Amphipods.SequenceEqual(other.Amphipods);

    public override int GetHashCode() => Amphipods.Aggregate(0, (hash, amphipod) => hash ^ amphipod.GetHashCode());
}
