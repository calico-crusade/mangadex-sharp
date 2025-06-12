namespace MangaDexSharp.Utilities.Download;

/// <summary>
/// Service for downloading chapters
/// </summary>
public interface IDownloadUtilityService : IMdUtil
{
    /// <summary>
    /// Starts a new download instance with the given configuration.
    /// </summary>
    /// <param name="config">The configuration to use</param>
    /// <returns>The instance of the download utility</returns>
    IDownloadInstance Start(Action<IDownloadSettings>? config = null);
}

internal class DownloadUtilityService(
    IRateLimitService _rates,
    IMdApiService _api) : IDownloadUtilityService
{
    public IDownloadInstance Start(Action<IDownloadSettings>? config = null)
    {
        var settings = new DownloadSettings();
        config?.Invoke(settings);
        return new DownloadInstance(_rates, _api, settings);
    }
}
