# MangaDexSharp Documentation

This folder contains the hand-written documentation for the packages and applications in this repository, plus generated API reference pages.

Start with the core package guide if you are building against MangaDex from a .NET application. Use the utilities guide if you want higher-level chapter upload, chapter download, or rate-limit workflows.

## Packages

- [MangaDexSharp](packages/mangadexsharp.md) - core MangaDex API client, models, filters, configuration, authentication, and DI setup.
- [MangaDexSharp.Utilities](packages/mangadexsharp-utilities.md) - upload, download, and rate-limit helper workflows.
- [MangaDexSharp.UpdatesPoll](packages/mangadexsharp-updatespoll.md) - polling helper for newly updated chapters.

## Applications

- [MangaDexSharp.Utilities.Cli](apps/utilities-cli.md) - command-line utilities for authentication checks, downloading chapters, and exporting read lists.
- [MangaDexSharp.OAuthLocal.Web](apps/oauth-local-web.md) - local OAuth sample web application.

## Reference

- [API Reference](api/README.md) - generated per-type reference pages with source links.

## Common Starting Points

- Create a client directly with `MangaDex.Create(...)`.
- Register the client with dependency injection using `services.AddMangaDex(...)`.
- Use `api.Auth.Personal(...)` to request OAuth tokens for account routes.
- Use `api.Manga`, `api.Chapter`, `api.Cover`, `api.Upload`, and the other service properties on `IMangaDex` for API calls.
- Install `MangaDexSharp.Utilities` when you want managed upload/download sessions instead of calling every route manually.

All source links in generated pages target the `main` branch of [calico-crusade/mangadex-sharp](https://github.com/calico-crusade/mangadex-sharp).
