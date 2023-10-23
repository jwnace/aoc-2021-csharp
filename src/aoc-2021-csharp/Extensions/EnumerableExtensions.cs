using System.Collections.Generic;
using System.Linq;

namespace aoc_2021_csharp.Extensions;

public static class EnumerableExtensions
{
    public static IEnumerable<T[]> Windowed<T>(this IEnumerable<T> enumerable, int size, int step = 1)
    {
        var list = enumerable.ToList();

        for (var i = 0; i <= list.Count - size; i += step)
        {
            yield return list.Skip(i).Take(size).ToArray();
        }
    }

    public static IEnumerable<IEnumerable<T>> GetCombinations<T>(this IEnumerable<T> enumerable, int length) where T : IComparable
    {
        return length switch
        {
            0 => new List<List<T>> { new() },
            1 => enumerable.Select(x => new List<T> { x }),
            _ => GetCombinations(enumerable, length - 1)
                .SelectMany(x => enumerable.Where(y => y.CompareTo(x.Last()) > 0), (a, b) => a.Concat(new[] { b }).ToList())
        };
    }

    public static IEnumerable<IEnumerable<T>> GetPermutations<T>(this IEnumerable<T> enumerable, int length)
    {
        if (length == 1)
        {
            return enumerable.Select(x => new List<T> { x });
        }

        return GetPermutations(enumerable, length - 1)
            .SelectMany(x => enumerable.Where(y => !x.Contains(y)), (a, b) => a.Concat(new[] { b }));
    }

    public static void Deconstruct<T>(this T[] array, out T first, out T second)
    {
        first = array[0];
        second = array[1];
    }

    public static void Deconstruct<T>(this T[] array, out T first, out T second, out T third)
    {
        first = array[0];
        second = array[1];
        third = array[2];
    }

    public static void Deconstruct<T>(this T[] array, out T first, out T second, out T third, out T fourth)
    {
        first = array[0];
        second = array[1];
        third = array[2];
        fourth = array[3];
    }

    public static void Deconstruct<T>(this T[] array, out T first, out T second, out T third, out T fourth, out T fifth)
    {
        first = array[0];
        second = array[1];
        third = array[2];
        fourth = array[3];
        fifth = array[4];
    }
}
