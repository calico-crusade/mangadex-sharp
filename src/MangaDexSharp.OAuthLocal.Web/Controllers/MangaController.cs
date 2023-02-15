using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MangaDexSharp.OAuthLocal.Web.Controllers
{
	[ApiController]
	public class MangaController : ControllerBase
	{
		private readonly IMangaDex _md;

		public MangaController(IMangaDex md)
		{
			_md = md;
		}

		[Route("api/manga/{id}"), HttpGet]
		public async Task<IActionResult> Get([FromRoute] string id)
		{
			var manga = await _md.Manga.Get(id);
			return Ok(manga);
		}

		[Route("api/me"), HttpGet, Authorize]
		public async Task<IActionResult> Me()
		{
			//You can also use it like this:
			//var token = Request.Cookies["access_token"];
			//var me = await _md.User.Me(token);

			//However, this uses the WebCredentialService.cs to provide it at an API level
			var me = await _md.User.Me();
			return Ok(me);
		}
	}
}
