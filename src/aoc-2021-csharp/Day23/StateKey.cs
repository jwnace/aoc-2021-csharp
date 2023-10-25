namespace aoc_2021_csharp.Day23;

public record StateKey(
    int Row1, int Col1,
    int Row2, int Col2,
    int Row3, int Col3,
    int Row4, int Col4,
    int Row5, int Col5,
    int Row6, int Col6,
    int Row7, int Col7,
    int Row8, int Col8)
{
    public StateKey(State state) : this(
        state.Row1, state.Col1,
        state.Row2, state.Col2,
        state.Row3, state.Col3,
        state.Row4, state.Col4,
        state.Row5, state.Col5,
        state.Row6, state.Col6,
        state.Row7, state.Col7,
        state.Row8, state.Col8)
    {
    }
}
