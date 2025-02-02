using Microsoft.Extensions.Logging;

namespace MangaDexSharp.UpdatesPoll;

public interface IUpdatesPollService
{
	/// <summary>
	/// Setup a task that will keep iterating until it is cancelled that will poll MangaDex for chapter updates
	/// </summary>
	/// <param name="callback">What to do when we fetch a list of chapters</param>
	/// <param name="since">When to start fetching the chapters from</param>
	/// <param name="langs">An optional list of languages to fetch chapters for</param>
	/// <param name="includePages">Whether or include manga page URLs</param>
	/// <param name="token">The optional cancellation token for cancelling the task</param>
	/// <returns>A task representing the loop</returns>
	Task Poll(Func<ChapterPages[], Task> callback, DateTime? since = null, string[]? langs = null, bool includePages = true, CancellationToken? token = null);
}

public class UpdatesPollService : IUpdatesPollService
{
	private readonly IMangaDex _api;
	private readonly ILogger _logger;

	/// <summary>
	/// How long to wait between the resolution of each poll
	/// </summary>
	private const int POLL_DELAY = 5 * 1000;

	/// <summary>
	/// How long to wait after hitting the request cap to avoid rate-limiting
	/// </summary>
	private const int RATE_LIMIT_DELAY = 5 * 1000;
	/// <summary>
	/// How many requests to do before waiting to avoid rate-limiting
	/// </summary>
	private const int RATE_LIMIT_REQUESTS = 3;

	/// <summary>
	/// How many chapter-page requests to do before waiting to avoid rate-limiting
	/// </summary>
	private const int RATE_LIMIT_PAGE_REQUESTS = 35;
	/// <summary>
	/// How long to wait after hitting the chapter-page request cap to avoid rate-limiting
	/// </summary>
	private const int RATE_LIMIT_PAGE_DELAY = 60 * 1000;

	public UpdatesPollService(
		IMangaDex api, 
		ILogger<UpdatesPollService> logger)
	{
		_api = api;
		_logger = logger;
	}

	/// <summary>
	/// Get all of the latest chapters since the given date
	/// </summary>
	/// <param name="since">The <see cref="DateTime"/> to fetch the chapters from</param>
	/// <param name="langs">An optional collection of language codes to fetch chapters for</param>
	/// <param name="offset">The offset for this request of chapters (for pagination resolution)</param>
	/// <param name="totalRequests">How many requests we've done so far in this round of pagination</param>
	/// <returns>A collection of every new chapter</returns>
	public IAsyncEnumerable<Chapter> Latest(DateTime since, string[] langs)
	{
		//Create our base filter
		var filter = new ChaptersFilter
		{
			Order = new() { [ChaptersFilter.OrderKey.updatedAt] = OrderValue.desc },
			UpdatedAtSince = since,
			TranslatedLanguage = langs
		};
		return _api.Chapter.ListAll(filter, RATE_LIMIT_DELAY, RATE_LIMIT_REQUESTS);
	}

	/// <summary>
	/// Get all of the latest chapters + their page urls
	/// </summary>
	/// <param name="since">The <see cref="DateTime"/> to fetch the chapters from</param>
	/// <param name="langs">An optional collection of language codes to fetch chapters for</param>
	/// <returns>A collection of chapters and their page data</returns>
	public async IAsyncEnumerable<ChapterPages> PollForUpdatesWithPages(DateTime since, string[] langs)
	{
		//Request all of the latest chapters
		var chapters = Latest(since, langs);

		int pageRequests = 0;
		//Iterate through each chapter
		await foreach(var chap in chapters)
		{
			//Skip external URLs
			if (!string.IsNullOrEmpty(chap.Attributes?.ExternalUrl))
			{
				yield return new(chap, [], []);
				continue;
			}

			//Slow down the requests to avoid rate limiting. 
			//We could use the rate-limit headers... but... That's too much work... 
			//Are there even rate-limit headers for page requests? I don't think so.
			if (pageRequests >= RATE_LIMIT_PAGE_REQUESTS)
			{
				_logger.LogDebug("Page Rate Limit Delay Init.");
				await Task.Delay(RATE_LIMIT_PAGE_DELAY);
				pageRequests = 0;
			}

			//Fetch all of the pages
			var pages = await _api.Pages.Pages(chap.Id);
			//Increment our rate limit counter
			pageRequests++;
			//Return the current result
			yield return new(chap, pages.Images, pages.DataSaverImages);
		}
	}

	/// <summary>
	/// Get all of the latest chapters since the given date
	/// </summary>
	/// <param name="since">The <see cref="DateTime"/> to fetch the chapters from</param>
	/// <param name="langs">An optional collection of language codes to fetch chapters for</param>
	/// <returns>A collection of chapters</returns>
	public async Task<ChapterPages[]> PollForUpdatesWithoutPages(DateTime since, string[] langs)
	{
		return await Latest(since, langs)
			.Select(t => new ChapterPages(t, [], []))
			.ToArrayAsync();
	}

	/// <summary>
	/// Setup a task that will keep iterating until it is cancelled that will poll MangaDex for chapter updates
	/// </summary>
	/// <param name="callback">What to do when we fetch a list of chapters</param>
	/// <param name="since">When to start fetching the chapters from</param>
	/// <param name="langs">An optional list of languages to fetch chapters for</param>
	/// <param name="includePages">Whether or include manga page URLs</param>
	/// <param name="token">The optional cancellation token for cancelling the task</param>
	/// <returns>A task representing the loop</returns>
	public async Task Poll(Func<ChapterPages[], Task> callback, DateTime? since = null, string[]? langs = null, bool includePages = true, CancellationToken? token = null)
	{
		token ??= CancellationToken.None;
		langs ??= [];
		since ??= DateTime.Now.AddMinutes(-30);

		while (true)
		{
			if (token.Value.IsCancellationRequested) break;

			var res = includePages ? 
				await PollForUpdatesWithPages(since.Value, langs).ToArrayAsync() :
				await PollForUpdatesWithoutPages(since.Value, langs);

			since = DateTime.Now;

			if (res.Length != 0)
				await callback(res);

			_logger.LogDebug("Poll executed: Chapter Count - {0}", res.Length);
			await Task.Delay(POLL_DELAY, token.Value);
		}
	}
}

/// <summary>
/// Represents a chapter and it's associated page URLs
/// </summary>
/// <param name="Chapter">The chapter data</param>
/// <param name="PageUrls">The full-resolution page URLs</param>
/// <param name="DataSaverPageUrls">The data-saver page URLs</param>
public record class ChapterPages(Chapter Chapter, string[] PageUrls, string[] DataSaverPageUrls);