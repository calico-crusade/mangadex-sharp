using Spectre.Console;

namespace MangaDexSharp.Utilities.Cli;

public static class Extensions
{
    public static string Trim(this string text, int maxLength, string replacer = "...")
    {
        if (text.Length > maxLength)
            return text[..(maxLength - replacer.Length)] + replacer;
        return text;
    }

    public static string Escape(this string text, int buffer = 5)
    {
        var maxLength = Console.WindowWidth - buffer;
        return Markup.Escape(text.Trim(maxLength));
    }

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

    public static T ConsoleSelect<T>(this IEnumerable<T> items, string? prompt = null, Func<T, string>? display = null)
        where T : notnull
    {
        if (items is null || !items.Any()) throw new NullReferenceException("Items cannot be null or empty.");

        prompt = Escape(prompt ?? $"Please select a {typeof(T).Name}:");
        display ??= item => item?.ToString() ?? string.Empty;

        var select = new SelectionPrompt<T>()
            .Title(prompt)
            .PageSize(Console.WindowHeight - 5)
            .MoreChoicesText("[grey](Move up and down to reveal more choices)[/]")
            .UseConverter(t => display(t))
            .AddChoices(items);
        return AnsiConsole.Prompt(select);
    }
}
