return await new ServiceCollection()
    .AddAppSettings()
    .AddMangaDex()
    .AddMangaDexUtils()
    .AddSerilog()
    .AddTransient<ITokenCacheService, TokenCacheService>()
    //.AddTransient<IMdJsonService, DebuggerMdJsonService>()
    .Cli(args, c =>
    {
        c.Add<ExampleVerb>()
         .Add<MangaVerb>()
         .Add<UserVerb>()
         .Add<ClientApiTestVerb>()
         .Add<TestAggregateVerb>()
         .Add<TestFeedVerb>()
         .Add<RandomVerb>()
         .Add<UploadVerb>()
         .Add<TestStatsVerb>();
    });