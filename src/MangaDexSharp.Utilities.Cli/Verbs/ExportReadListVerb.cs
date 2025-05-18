using MangaDexSharp.Utilities.Cli.Services;

namespace MangaDexSharp.Utilities.Cli.Verbs;

[Verb("export-read-list", isDefault: true, HelpText = "Export your MangaDex read list to a file.")]
public class ExportReadListOptions : AuthOptions
{
    private const string FILE_PATH = "read-list.json";

    [Option('f', "file-path", HelpText = "The file path to export to (supports csv or json)", Default = FILE_PATH)]
    public string FilePath { get; set; } = FILE_PATH;
}

internal class ExportReadListVerb(
    ILogger<ExportReadListVerb> logger,
    IExportReadListService _export,
    AuthOptionsCache _cache) : BooleanVerb<ExportReadListOptions>(logger)
{
    public override async Task<bool> Execute(ExportReadListOptions options, CancellationToken token)
    {
        _cache.Auth = options;
        _logger.LogInformation("Writing read list to {FilePath}", options.FilePath);
        await _export.WriteUsersMangaStatusToFile(options.FilePath, null, token);
        _logger.LogInformation("Finished writing read list to {FilePath}", options.FilePath);
        return true;
    }
}
