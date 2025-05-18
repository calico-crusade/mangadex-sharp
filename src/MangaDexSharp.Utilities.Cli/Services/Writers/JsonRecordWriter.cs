namespace MangaDexSharp.Utilities.Cli.Services.Writers;

public class JsonRecordWriter<TIn, TOut>(
    Stream _output,
    Func<TIn, TOut> _converter,
    JsonSerializerOptions? _options = null) : IRecordWriter<TIn>
{
    public JsonRecordWriter(string file, Func<TIn, TOut> converter, JsonSerializerOptions? _options = null)
        : this(File.Create(file), converter, _options) { }

    public async Task Write(IAsyncEnumerable<TIn> records)
    {
        var items = records.Select(_converter);
        await JsonSerializer.SerializeAsync(_output, items, _options);
        await _output.FlushAsync();
    }

    public void Dispose()
    {
        _output.Dispose();
        _output = null!;
        GC.SuppressFinalize(this);
    }
}
