# `check-auth` Verb

Validates the configured MangaDex authentication by calling `api.User.Me()` through [`IMangaDex`](../../api/MangaDexSharp/MangaDexSharp.IMangaDex.md). The options class is [`CheckAuthOptions`](../../api/MangaDexSharp.Utilities.Cli/MangaDexSharp.Utilities.Cli.Verbs.CheckAuthOptions.md), which inherits [`AuthOptions`](../../api/MangaDexSharp.Utilities.Cli/MangaDexSharp.Utilities.Cli.AuthOptions.md).

## Flags

| Flag | Alias | Purpose | Example |
| --- | --- | --- | --- |
| `--access-token` | `-a` | Uses an existing account access token. | `--access-token "$env:MD_TOKEN"` |
| `--client-id` | `-c` | OAuth personal client ID. | `--client-id "$env:MD_CLIENT_ID"` |
| `--client-secret` | `-s` | OAuth personal client secret. | `--client-secret "$env:MD_CLIENT_SECRET"` |
| `--username` | `-u` | MangaDex username. | `--username "my-user"` |
| `--password` | `-p` | MangaDex password. | `--password "$env:MD_PASSWORD"` |

## Example Command

```powershell
dotnet run --project src/MangaDexSharp.Utilities.Cli -- check-auth --access-token "$env:MD_TOKEN"
```

## Successful Result

```text
[Information] Authentication successful! Profile: {
  "result": "ok",
  "data": {
    "id": "00000000-0000-0000-0000-000000000000",
    "type": "user",
    "attributes": {
      "username": "example-user"
    }
  }
}
```
