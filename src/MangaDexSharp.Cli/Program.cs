return await new ServiceCollection()
    .AddAppSettings()
    .AddMangaDex<PersonalCredentialsService>(false)
    .AddSerilog()
    .AddTransient<ITokenCacheService, TokenCacheService>()
    .Cli(args, c =>
    {
        c.Add<ExampleVerb>()
         .Add<MangaVerb>()
         .Add<UserVerb>()
         .Add<ClientApiTestVerb>()
         .Add<TestAggregateVerb>();
    });