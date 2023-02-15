using MangaDexSharp;
using MangaDexSharp.OAuthLocal.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddMangaDex<WebCredentialService>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(opt =>
{
	opt.DefaultScheme = "cookie";
	opt.DefaultChallengeScheme = "oidc";
})
	.AddCookie("cookie")
	.AddOpenIdConnect("oidc", opts =>
	{
		opts.Authority = "https://auth.mangadex.dev/realms/mangadex";
		opts.ClientId = "thirdparty-oauth-client";
		opts.ResponseType = "code";
		opts.UsePkce = true;
		opts.ResponseMode = "query";
		opts.Scope.Add("openid");
		opts.Scope.Add("email");
		opts.Scope.Add("offline_access");
		opts.Scope.Add("roles");
		opts.Scope.Add("profile");
		opts.Scope.Add("address");
		opts.Scope.Add("phone");

		opts.SaveTokens = true;

		opts.Events.OnTokenValidated = (ctx) =>
		{
			var token = ctx.TokenEndpointResponse?.AccessToken ?? "";
			ctx.Response.Cookies.Append("access_token", token);
			return Task.CompletedTask;
		};
	});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
