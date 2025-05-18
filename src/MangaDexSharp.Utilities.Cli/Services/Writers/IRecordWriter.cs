namespace MangaDexSharp.Utilities.Cli.Services.Writers;

public interface IRecordWriter<T> : IDisposable
{
    Task Write(IAsyncEnumerable<T> records);
}
