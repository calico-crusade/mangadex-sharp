# MangaDexSharp.IMangaDexSettingsService

- **Kind:** `interface`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp](../../packages/mangadexsharp.md)
- **Source:** [src/MangaDexSharp/MangaDexSettingsService.cs](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/MangaDexSettingsService.cs)

## Purpose

Represents all requests related to MangaDex user settings and settings templates.

## How To Get Or Use It

Resolve it through `IMangaDex.Settings` or inject `IMangaDexSettingsService` from a service provider configured with `AddMangaDex()`.

## Members

| Kind | Name | Signature/type | Access |
| --- | --- | --- | --- |
| Method/ctor | `LatestTemplate` | `Task<SettingsTemplate> LatestTemplate(string? token = null);` | public |
| Method/ctor | `CreateTemplate` | `Task<SettingsTemplate> CreateTemplate(JsonElement template, string? token = null);` | public |
| Method/ctor | `GetTemplate` | `Task<SettingsTemplate> GetTemplate(string version, string? token = null);` | public |
| Method/ctor | `Get` | `Task<UserSettings> Get(string? token = null);` | public |
| Method/ctor | `Update` | `Task<UserSettings> Update(UserSettingsUpdate settings, string? token = null);` | public |

## Related

- [IMangaDex](MangaDexSharp.IMangaDex.md)
- [API Reference Index](../README.md)

