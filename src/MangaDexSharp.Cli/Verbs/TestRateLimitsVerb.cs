namespace MangaDexSharp.Cli.Verbs;

[Verb("test-rate-limits", HelpText = "Test the rate limits of MangaDexSharp.")]
internal class TestRateLimitsOptions
{

}

internal class TestRateLimitsVerb(
	ILogger<TestRateLimitsVerb> logger) : BooleanVerb<TestRateLimitsOptions>(logger)
{
	private IMdRateLimiter _limiter = null!;

	public static MdHttpBuilder Request(string url, string method)
	{
		return new MdHttpBuilder(null!, null!, null!)
			.Uri(url)
			.Method(method);
	}

	public async Task SpamRequest(MdHttpBuilder request, CancellationToken token)
	{
		var options = new ParallelOptions
		{
			MaxDegreeOfParallelism = Environment.ProcessorCount,
			CancellationToken = token
		};

		var spam = Enumerable.Range(0, 100);
		await Parallel.ForEachAsync(spam, options, async (i, ct) =>
		{
			_logger.LogInformation("Sending request {Index}", i);
			using var _ = await _limiter.Limit(request, ct);
			_logger.LogInformation("Request {Index} completed", i);
		});
	}

	public Task TestGeneralLimits(CancellationToken token)
	{
		var request = Request("https://api.mangadex.org/test", "GET");
		return SpamRequest(request, token);
	}

	public Task TestCustomLimits(CancellationToken token)
	{
		var request = Request("https://api.mangadex.org/author", "PUT");
		return SpamRequest(request, token);
	}

	public Task TestGlobalPause(CancellationToken token)
	{
		var url = "https://api.mangadex.org/author";
		var request = Request(url, "PUT");
		_limiter.Observe(url, new()
		{
			Limit = 10, 
			Remaining = 0,
			RetryAfter = DateTime.UtcNow.AddSeconds(30)
		});
		return SpamRequest(request, token);
	}

	public override async Task<bool> Execute(TestRateLimitsOptions options, CancellationToken token)
	{
		var services = new ServiceCollection()
			.AddSerilog()
			.AddAppSettings()
			.AddMangaDex(c => c
				.WithApiConfig(rateLimits: true, conservative: true)
				.WithEventTracking<EventWatcher>())
			.BuildServiceProvider();

		_limiter = services.GetRequiredService<IMdRateLimiter>();

		await TestGlobalPause(token);
		return true;
	}

	internal class EventWatcher(
		ILogger<EventWatcher> _logger) : MdEventService
	{
		public override void OnRateLimitGlobalPaused(string url, RateLimit limits, TimeSpan span)
		{
			_logger.LogWarning("Global rate limit paused for {Url} with limits {Limits} for {Span}", url, limits, span);
		}

		public override void OnRateLimitGlobalUnpaused(string url, RateLimit limits)
		{
			_logger.LogWarning("Global rate limit unpaused for {Url} with limits {Limits}", url, limits);
		}
	}
}
