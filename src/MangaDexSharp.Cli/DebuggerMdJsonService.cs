namespace MangaDexSharp.Cli;

internal class DebuggerMdJsonService(
    ILogger<DebuggerMdJsonService> _logger) : MdJsonService, IMdJsonService
{
    public new async Task<T?> Deserialize<T>(Stream stream, CancellationToken token)
    {
        string? text = null;
        try
        {
            using var sr = new StreamReader(stream);
            text = await sr.ReadToEndAsync(token);

            return JsonSerializer.Deserialize<T>(text, DEFAULT_OPTIONS);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while deserializing content: {text}", text);
            throw;
        }
    }
}
