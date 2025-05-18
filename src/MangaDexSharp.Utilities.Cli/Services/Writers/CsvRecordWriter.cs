using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace MangaDexSharp.Utilities.Cli.Services.Writers;

public class CsvRecordWriter<TIn, TOut>(
    Stream _output,
    Func<TIn, TOut> _converter,
    Func<CsvConfiguration>? _config = null) : IRecordWriter<TIn>
{
    public CsvRecordWriter(string file, Func<TIn, TOut> converter, Func<CsvConfiguration>? config = null)
        : this(File.Create(file), converter, config) { }

    public async Task Write(IAsyncEnumerable<TIn> records)
    {
        var config = _config?.Invoke() ?? new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
        };
        using var writer = new CsvWriter(new StreamWriter(_output), config, true);
        writer.WriteHeader<TOut>();
        writer.NextRecord();

        await foreach (var record in records)
        {
            var item = _converter(record);
            writer.WriteRecord(item);
            writer.NextRecord();
        }
        await writer.FlushAsync();
        await _output.FlushAsync();
    }

    public void Dispose()
    {
        _output.Dispose();
        _output = null!;
        GC.SuppressFinalize(this);
    }
}
