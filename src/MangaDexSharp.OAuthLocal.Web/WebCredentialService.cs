namespace MangaDexSharp.OAuthLocal.Web
{
	public class WebCredentialService : ICredentialsService
	{
		private readonly IHttpContextAccessor _context;

		public WebCredentialService(IHttpContextAccessor context)
		{
			_context = context;
		}

		public string ApiUrl => Extensions.API_DEV_ROOT;

		public string UserAgent => Extensions.API_USER_AGENT;

		public string AuthUrl => Extensions.AUTH_DEV_URL;

		public string? ClientId => null;

		public string? ClientSecret => null;

		public string? Username => null;

		public string? Password => null;

		public bool ThrowOnError => false;

        public Task<string?> GetToken()
		{
			var token = _context.HttpContext?.Request.Cookies["access_token"];
			return Task.FromResult(token);
		}
	}
}
