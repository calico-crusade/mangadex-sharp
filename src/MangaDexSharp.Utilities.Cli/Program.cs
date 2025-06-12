using MangaDexSharp.Utilities.Cli.Services;
using MangaDexSharp.Utilities.Cli.Verbs;

return await new ServiceCollection()
    .AddAppSettings()
    .AddMangaDex(c => c
        .WithAuthConfig<AuthConfigurationOIDC>(false)
        .WithCredentials<AuthCredentialsService>(false))
    .AddMangaDexUtils()
    .AddSerilog()
    .AddSingleton<AuthOptionsCache>()
    .AddTransient<IExportReadListService, ExportReadListService>()
    .Cli(args, c =>
    {
        c.Add<ExportReadListVerb>()
         .Add<CheckAuthVerb>()
         .Add<DefaultVerb>()
         .Add<DownloadVerb>();
    });