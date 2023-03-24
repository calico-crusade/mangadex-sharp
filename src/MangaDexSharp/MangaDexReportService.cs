namespace MangaDexSharp;

/// <summary>
/// Represents all of the requests relating to object reports
/// </summary>
public interface IMangaDexReportService
{
	/// <summary>
	/// Requests a list of available reasons for the given category
	/// </summary>
	/// <param name="category">The type of object the reasons are for</param>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>A list of all of the report reasons</returns>
	Task<ReportReasonList> Reasons(ReportCategory category, string? token = null);

	/// <summary>
	/// Requests a paginated list of reports for the given filter
	/// </summary>
	/// <param name="filters">How to filter the reports</param>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>A list of reports</returns>
	Task<ReportList> Reports(ReportFilter? filters = null, string? token = null);

	/// <summary>
	/// Creates a report
	/// </summary>
	/// <param name="report">The report to create</param>
	/// <param name="token">The authentication token, if none is provided, it will fall back on the <see cref="ICredentialsService"/></param>
	/// <returns>The results of the request</returns>
	Task<MangaDexRoot> Create(ReportCreate report, string? token = null);
}

internal class MangaDexReportService : IMangaDexReportService
{
	private readonly IApiService _api;
	private readonly ICredentialsService _creds;

	public string Root => $"{_creds.ApiUrl}/report";

	public MangaDexReportService(IApiService api, ICredentialsService creds)
	{
		_api = api;
		_creds = creds;
	}

	public async Task<ReportReasonList> Reasons(ReportCategory category, string? token = null)
	{
		var c = await Auth(token, _creds, true);
		var url = $"{Root}/reasons/{category}";
		return await _api.Get<ReportReasonList>(url, c) ?? new() { Result = "error" };
	}

	public async Task<ReportList> Reports(ReportFilter? filters = null, string? token = null)
	{
		var c = await Auth(token, _creds);
		var url = $"{Root}?{(filters ?? new()).BuildQuery()}";
		return await _api.Get<ReportList>(url, c) ?? new() { Result = "error" };
	}

	public async Task<MangaDexRoot> Create(ReportCreate report, string? token = null)
	{
		var c = await Auth(token, _creds);
		return await _api.Post<MangaDexRoot, ReportCreate>(Root, report, c) ?? new() { Result = "error" };
	}
}
