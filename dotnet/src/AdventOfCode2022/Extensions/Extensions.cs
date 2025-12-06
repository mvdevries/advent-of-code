using System;
using System.Collections.Generic;
using System.Linq;

namespace Solutions.Extensions;

public static class PairUpExtensions {
    public static IEnumerable<T[]> PairUp<T>(this IEnumerable<T> enumerable, int size)
    {
        return PairUpItems(enumerable, size);
    }

    private static IEnumerable<T[]> PairUpItems<T>(IEnumerable<T> enumerable, int size)
    {
        var queue = new Queue<T>(size);
        foreach (var item in enumerable)
        {
            if (queue.Count == size)
            {
                queue.Dequeue();
            }

            queue.Enqueue(item);
            if (queue.Count == size)
            {
                yield return queue.ToArray();
            }
        }
    }

    public static IEnumerable<IEnumerable<T>> Batch<T>(this IEnumerable<T> source, int size)
    {
        T[]? bucket = null;
        var count = 0;

        foreach (var item in source)
        {
            bucket ??= new T[size];

            bucket[count++] = item;

            if (count != size)
                continue;

            yield return bucket.Select(x => x);

            bucket = null;
            count = 0;
        }

        if (bucket != null && count > 0)
        {
            Array.Resize(ref bucket, count);
            yield return bucket.Select(x => x);
        }
    }
}
