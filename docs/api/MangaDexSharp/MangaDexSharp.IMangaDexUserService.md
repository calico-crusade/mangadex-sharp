# MangaDexSharp.IMangaDexUserService

- **Kind:** `interface`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp](../../packages/mangadexsharp.md)
- **Source:** [src/MangaDexSharp/MangaDexUserService.cs:6](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/MangaDexUserService.cs#L6)

## Purpose

Represents all of the requests regarding the current user and the obsoleted login methods

This page exists so references to [IMangaDexUserService](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/MangaDexUserService.cs#L6) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

## How To Get Or Use It

Resolve it through `IMangaDex.User` or inject `IMangaDexUserService` from a service provider configured with `AddMangaDex()`.

```csharp
using MangaDexSharp;

var api = MangaDex.Create();
// See the members below for the calls or properties exposed by this type.
```

## Members

| Kind | Name | Signature/type | Access |
| --- | --- | --- | --- |
| Method/ctor | `List` | `Task<UserList> List(UserFilter? filter = null, string? token = null);` | public |
| Method/ctor | `Get` | `Task<MangaDexRoot<User>> Get(string id);` | public |
| Method/ctor | `Delete` | `Task<MangaDexRoot> Delete(string id, string? token = null);` | public |
| Method/ctor | `ApproveDelete` | `Task<MangaDexRoot> ApproveDelete(string code, string? token = null);` | public |
| Method/ctor | `Me` | `Task<MangaDexRoot<User>> Me(string? token = null);` | public |
| Method/ctor | `History` | `Task<ReadingHistory> History(string? token = null);` | public |
| Method/ctor | `Login` | `Task<LoginResult> Login(LoginRequest request);` | public |
| Method/ctor | `Login` | `Task<LoginResult> Login(string username, string password);` | public |
| Method/ctor | `Refresh` | `Task<LoginResult> Refresh(string token);` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




