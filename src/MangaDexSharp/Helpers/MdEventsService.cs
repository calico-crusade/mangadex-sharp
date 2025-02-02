using System.Net.Http;

namespace MangaDexSharp;

/// <summary>
/// A service that bundles and runs all of the implemented <see cref="IMdEventService"/>s
/// </summary>
/// <remarks>You probably don't want to implement this one, you probably want: <see cref="IMdEventService"/></remarks>
public interface IMdEventsService
{
    /// <summary>
    /// Binds the events to the <see cref="IHttpBuilderConfig"/>
    /// </summary>
    /// <param name="url">The URL of the request being made</param>
    /// <param name="config">The HTTP builder to attach to</param>
    void Bind(string url, IHttpBuilderConfig config);
}

internal class MdEventsService(
    IEnumerable<IMdEventService> _events) : IMdEventsService
{
    public void RunHandlers(Action<IMdEventService> action)
    {
        foreach (var handler in _events)
            action(handler);
    }

    public void OnRateLimitDataReceived(string url, RateLimit limits)
    {
        RunHandlers(t => t.OnRateLimitDataReceived(url, limits));
    }

    public void OnRateLimitExceeded(string url, RateLimit limits)
    {
        RunHandlers(t => t.OnRateLimitExceeded(url, limits));
    }

    public void OnRequestError(string url, Exception error)
    {
        RunHandlers(t => t.OnRequestError(url, error));
    }

    public void OnRequestFinished(string url, Exception? error)
    {
        RunHandlers(t => t.OnRequestFinished(url, error));
    }

    public void OnRequestStarting(string url)
    {
        RunHandlers(t => t.OnRequestStarting(url));
    }

    public void OnResponseParsed(string url, HttpResponseMessage response, object? data)
    {
        RunHandlers(t => t.OnResponseParsed(url, response, data));
    }

    public void OnResponseReceived(string url, HttpResponseMessage response, HttpRequestMessage request)
    {
        RunHandlers(t => t.OnResponseReceived(url, response, request));
    }

    public void Bind(string url, IHttpBuilderConfig config)
    {
        if (!_events.Any()) return;

        config.OnFinished(error =>
        {
            OnRequestFinished(url, error);
            if (error is not null)
                OnRequestError(url, error);
        });
        config.OnStarting(() => OnRequestStarting(url));
        config.OnResponseReceived((response, request) => OnResponseReceived(url, response, request));
        config.OnResponseParsed((response, data) =>
        {
            OnResponseParsed(url, response, data);

            if (data is not MangaDexRateLimits limits) return;

            OnRateLimitDataReceived(url, limits.RateLimit);

            if (!limits.RateLimit.IsLimited) return;
                
            OnRateLimitExceeded(url, limits.RateLimit);
        });
    }
}
