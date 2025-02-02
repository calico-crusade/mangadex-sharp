using System.Net.Http;

namespace MangaDexSharp;

/// <summary>
/// A service for adding custom configuration to the underlying HTTP requests that MD api makes
/// </summary>
/// <remarks>
/// You should only register one of these and you should only do it if you know what you are doing.
/// <para>Some things to avoid:</para>
/// <para>* Don't add Authorization headers, use the <see cref="ICredentialsService"/> instead</para>
/// <para>* Don't add User-Agent headers, specify it in your configuration instead (yours will be overridden)</para>
/// <para>* Don't specify the Method or URI of the request, those are handled for you (yours will be overridden)</para>
/// <para>Some things you can do with this:</para>
/// <para>* Adding proxying settings to the <see cref="HttpClient"/></para>
/// <para>* Adding custom headers that are not related to authentication</para>
/// <para>* Adding custom timeouts or other <see cref="HttpClient"/> settings</para>
/// <para>* Adding callbacks and other delegate handlers to the various stages of the request</para>
/// </remarks>
public interface IMdRequestConfigurationService
{
    /// <summary>
    /// The method for configuring the HTTP request
    /// </summary>
    /// <param name="uri">The full URI of the request being made</param>
    /// <param name="config">The builder to configure</param>
    void Configure(string uri, IHttpBuilderConfig config);
}
