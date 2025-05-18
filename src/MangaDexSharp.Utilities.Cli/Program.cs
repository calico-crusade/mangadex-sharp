using MangaDexSharp.Utilities.Cli.Services;
using MangaDexSharp.Utilities.Cli.Verbs;

return await new ServiceCollection()
    .AddAppSettings()
    .AddMangaDex(c => c
        .WithAuthConfig<AuthConfigurationOIDC>(false)
        .WithCredentials<AuthCredentialsService>(false))
    .AddSerilog()
    .AddSingleton<AuthOptionsCache>()
    .AddTransient<IExportReadListService, ExportReadListService>()
    .AddSingleton<IRateLimitService, RateLimitService>()
    .Cli(args, c =>
    {
        c.Add<ExportReadListVerb>();
    });