using MangaDexSharp.Utilities.Cli.Services;

namespace MangaDexSharp.Utilities.Cli.Verbs;

[Verb("export-read-list", isDefault: true, HelpText = "Export your MangaDex read list to a file.")]
public class ExportReadListOptions : AuthOptions
{
    private const string FILE_PATH = "read-list.json";
    private const bool INCLUDE_LATEST_CHAPTER = false;
    private const string PREFERRED_LANGUAGE = "en";

    [Option('f', "file-path", HelpText = "The file path to export to (supports csv or json)", Default = FILE_PATH)]
    public string FilePath { get; set; } = FILE_PATH;

    [Option('i', "include-latest-chapter", HelpText = "Whether or not to include the latest chapter's name and URL in the output (This will be VERY slow if you have a large read history).", Default = INCLUDE_LATEST_CHAPTER)]
    public bool IncludeLatestChapter { get; set; } = INCLUDE_LATEST_CHAPTER;

    [Option('r', "read-status", HelpText = "The read status to filter by (reading, on_hold, plan_to_read, dropped, re_reading, completed). Leaving empty will fetch all statuses", Default = null)]
    public string? ReadStatus { get; set; }

    [Option('l', "preferred-language", HelpText = "The preferred language to use for the latest chapters and titles", Default = PREFERRED_LANGUAGE)]
    public string PreferredLanguage { get; set; } = PREFERRED_LANGUAGE;
}

internal class ExportReadListVerb(
    ILogger<ExportReadListVerb> logger,
    IExportReadListService _export,
    AuthOptionsCache _cache) : BooleanVerb<ExportReadListOptions>(logger)
{
    public override async Task<bool> Execute(ExportReadListOptions options, CancellationToken token)
    {
        ReadStatus? status = null;
        if (!string.IsNullOrEmpty(options.ReadStatus) &&
            Enum.TryParse<ReadStatus>(options.ReadStatus, true, out var result))
            status = result;

        _cache.Auth = options;
        _logger.LogInformation("Writing read list to {FilePath}", options.FilePath);
        await _export.WriteUsersMangaStatusToFile(
            options.FilePath, 
            status, 
            options.IncludeLatestChapter, 
            options.PreferredLanguage, 
            token);
        _logger.LogInformation("Finished writing read list to {FilePath}", options.FilePath);
        return true;
    }
}
