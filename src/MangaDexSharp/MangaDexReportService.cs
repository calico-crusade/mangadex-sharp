namespace MangaDexSharp;

public interface IMangaDexReportService
{
	Task<ReportReasonList> Reasons(ReportCategory category, string? token = null);

	Task<ReportList> Reports(ReportFilter? filters = null, string? token = null);

	Task<MangaDexRoot> Create(ReportCreate report, string? token = null);
}

public class MangaDexReportService : IMangaDexReportService
{
	private readonly IApiService _api;
	private readonly ICredentialsService _creds;

	public string Root => $"{API_ROOT}/report";

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
