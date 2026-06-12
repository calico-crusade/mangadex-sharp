# MangaDexSharp.MdHttpBuilder

- **Kind:** `class`
- **Access:** `public`
- **Assembly/project:** [MangaDexSharp](../../packages/mangadexsharp.md)
- **Source:** [src/MangaDexSharp/Helpers/ApiLayer/MdHttpBuilder.cs:11](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Helpers/ApiLayer/MdHttpBuilder.cs#L11)

## Purpose

A customized instance of HttpBuilder that handles rate limits

This page exists so references to [MdHttpBuilder](https://github.com/calico-crusade/mangadex-sharp/blob/main/src/MangaDexSharp/Helpers/ApiLayer/MdHttpBuilder.cs#L11) have a stable documentation target in the repo. It records what the type is used for, why it is present in the solution, and the members exposed by the current source.

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
| Property | `RequestUri` | `Uri?` | public |
| Property | `RequestMethod` | `string?` | public |
| Method/ctor | `Uri` | `public MdHttpBuilder Uri(string uri) {` | public |
| Method/ctor | `InvalidOperationException` | `throw new InvalidOperationException("Request URI has already been set and cannot be modified.");` | public |
| Method/ctor | `Uri` | `RequestUri = new Uri(uri);` | public |
| Method/ctor | `Message` | `Message(c => { c.RequestUri = RequestUri; });` | public |
| Method/ctor | `Method` | `public MdHttpBuilder Method(string method) {` | public |
| Method/ctor | `InvalidOperationException` | `throw new InvalidOperationException("Request method has already been set and cannot be modified.");` | public |
| Method/ctor | `Message` | `RequestMethod = method.ToUpper().Trim(); Message(c => { c.Method = new HttpMethod(RequestMethod);` | public |
| Method/ctor | `MakeRequest` | `public override async Task<HttpResponseMessage> MakeRequest(HttpClient client, bool ensureAccept, CancellationToken token) {` | public |

## Related

- [API Reference Index](../README.md)
- [Documentation Home](../../README.md)




