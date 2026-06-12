# `export-read-list` Verb

Exports the authenticated user's MangaDex read statuses to JSON or CSV using [`ExportReadListService`](../../api/MangaDexSharp.Utilities.Cli/MangaDexSharp.Utilities.Cli.Services.ExportReadListService.md). The options class is [`ExportReadListOptions`](../../api/MangaDexSharp.Utilities.Cli/MangaDexSharp.Utilities.Cli.Verbs.ExportReadListOptions.md).

## Flags

| Flag | Alias | Purpose | Example |
| --- | --- | --- | --- |
| `--file-path` | `-f` | Output file path. Extension selects writer: `.json` or `.csv`. | `--file-path read-list.csv` |
| `--include-latest-chapter` | `-i` | Fetches the latest chapter for each manga. This can be slow for large libraries. | `--include-latest-chapter true` |
| `--read-status` | `-r` | Filters by `reading`, `on_hold`, `plan_to_read`, `dropped`, `re_reading`, or `completed`. | `--read-status completed` |
| `--preferred-language` | `-l` | Language code used for latest chapters and title selection. | `--preferred-language en` |
| `--access-token` | `-a` | Uses an existing account access token. | `--access-token "$env:MD_TOKEN"` |
| `--client-id` | `-c` | OAuth personal client ID. | `--client-id "$env:MD_CLIENT_ID"` |
| `--client-secret` | `-s` | OAuth personal client secret. | `--client-secret "$env:MD_CLIENT_SECRET"` |
| `--username` | `-u` | MangaDex username. | `--username "my-user"` |
| `--password` | `-p` | MangaDex password. | `--password "$env:MD_PASSWORD"` |

## Example Command

```powershell
dotnet run --project src/MangaDexSharp.Utilities.Cli -- export-read-list --access-token "$env:MD_TOKEN" --file-path read-list.csv --read-status reading --preferred-language en
```

## Successful Result

```text
[Information] Writing read list to read-list.csv
[Information] Wrote read list to read-list.csv
[Information] Finished writing read list to read-list.csv
```

A CSV result contains columns similar to `Id`, `Title`, `CoverUrl`, `TitleUrl`, `Status`, and, when requested, latest chapter fields.
