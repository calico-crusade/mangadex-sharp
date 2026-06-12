# MangaDexSharp.TokenResult

- **Kind:** `class`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp](../../packages/mangadexsharp.md)
- **Source:** [src/MangaDexSharp/Models/Auth/TokenResult.cs:7](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Models/Auth/TokenResult.cs#L7)

## Purpose

The result of a token request

This page exists so references to [TokenResult](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Models/Auth/TokenResult.cs#L7) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

## How To Get Or Use It

Use the owning service method that returns this type, or construct it directly when it is a request/filter/create/update model.

```csharp
using MangaDexSharp;

var api = MangaDex.Create();
// See the members below for the calls or properties exposed by this type.
```

## Members

| Kind | Name | Signature/type | Access |
| --- | --- | --- | --- |
| Property | `AccessToken` | `string` | public |
| Property | `RefreshToken` | `string` | public |
| Property | `ExpiresIn` | `double?` | public |
| Property | `RefreshExpiresIn` | `double?` | public |
| Property | `TokenType` | `string` | public |
| Property | `NotBeforePolicy` | `double?` | public |
| Property | `SessionState` | `string?` | public |
| Property | `Scope` | `string?` | public |
| Property | `ClientType` | `string?` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




