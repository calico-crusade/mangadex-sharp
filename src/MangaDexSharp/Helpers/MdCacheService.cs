namespace MangaDexSharp;

/// <summary>
/// Exposes the underlying caching mechanism for Cardboard HTTP tailed to MangaDex
/// </summary>
public interface IMdCacheService : ICacheService { }

internal class MdCacheService : DiskCacheService, IMdCacheService
{
    public MdCacheService(IMdJsonService json) : base(json) { }
}
