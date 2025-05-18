namespace MangaDexSharp.Utilities.Cli;

public static class Extensions
{
    public static IEnumerable<T[]> Batch<T>(this IEnumerable<T> input, int size)
    {
        var batch = new List<T>(size);
        foreach (var item in input)
        {
            batch.Add(item);
            if (batch.Count != size) continue;

            yield return batch.ToArray();
            batch.Clear();
        }

        if (batch.Count == 0) yield break;

        yield return batch.ToArray();
    }
}
