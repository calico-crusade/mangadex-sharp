﻿namespace MangaDexSharp.OAuthLocal.Web
{
	public class WebCredentialService : ICredentialsService
	{
		private readonly IHttpContextAccessor _context;

		public WebCredentialService(IHttpContextAccessor context)
		{
			_context = context;
		}

        public Task<string?> GetToken()
		{
			var token = _context.HttpContext?.Request.Cookies["access_token"];
			return Task.FromResult(token);
		}
	}
}
