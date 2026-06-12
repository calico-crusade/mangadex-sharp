# MangaDexSharp.Utilities.Cli

`MangaDexSharp.Utilities.Cli` is the documented command-line application in this solution. It wires [`MangaDexSharp`](../packages/mangadexsharp.md), [`MangaDexSharp.Utilities`](../packages/mangadexsharp-utilities.md), and the verbs below.

## Verbs

- [`check-auth`](utilities-cli/check-auth.md) - validates authentication and prints the current user profile.
- [`download`](utilities-cli/download.md) - downloads chapters by manga ID or chapter IDs.
- [`export-read-list`](utilities-cli/export-read-list.md) - exports the authenticated user's MangaDex read list.

The hidden `default` verb starts an interactive menu and is documented in the [supporting services](#supporting-services) section because it is not intended for direct command-line use.

## Authentication Flags

[`AuthOptions`](../api/MangaDexSharp.Utilities.Cli/MangaDexSharp.Utilities.Cli.AuthOptions.md) is shared by auth-required verbs.

| Flag | Alias | Purpose |
| --- | --- | --- |
| `--access-token` | `-a` | Supplies an existing access token directly. |
| `--client-id` | `-c` | OAuth personal client ID from MangaDex settings. |
| `--client-secret` | `-s` | OAuth personal client secret from MangaDex settings. |
| `--username` | `-u` | MangaDex username for personal-client auth. |
| `--password` | `-p` | MangaDex password for personal-client auth. |

## Supporting Services

- [`AuthOptionsCache`](../api/MangaDexSharp.Utilities.Cli/MangaDexSharp.Utilities.Cli.Services.AuthOptionsCache.md) stores the current verb's auth options for credential resolution.
- [`AuthConfigurationOIDC`](../api/MangaDexSharp.Utilities.Cli/MangaDexSharp.Utilities.Cli.Services.AuthConfigurationOIDC.md) overlays command-line OAuth flags over configuration-file values.
- [`AuthCredentialsService`](../api/MangaDexSharp.Utilities.Cli/MangaDexSharp.Utilities.Cli.Services.AuthCredentialsService.md) prefers a direct access token and falls back to [`PersonalCredentialsService`](../api/MangaDexSharp/MangaDexSharp.PersonalCredentialsService.md).
- [`ExportReadListService`](../api/MangaDexSharp.Utilities.Cli/MangaDexSharp.Utilities.Cli.Services.ExportReadListService.md) fetches read statuses, fetches manga in batches, optionally fetches latest chapters, and writes JSON or CSV.
- [`IRecordWriter`](../api/MangaDexSharp.Utilities.Cli/MangaDexSharp.Utilities.Cli.Services.Writers.IRecordWriter.md), [`JsonRecordWriter`](../api/MangaDexSharp.Utilities.Cli/MangaDexSharp.Utilities.Cli.Services.Writers.JsonRecordWriter.md), and [`CsvRecordWriter`](../api/MangaDexSharp.Utilities.Cli/MangaDexSharp.Utilities.Cli.Services.Writers.CsvRecordWriter.md) abstract the export output format.
- [`DefaultVerb`](../api/MangaDexSharp.Utilities.Cli/MangaDexSharp.Utilities.Cli.Verbs.DefaultVerb.md) discovers registered verbs and runs them through an interactive Spectre.Console prompt.
- [`DownloadVerb`](../api/MangaDexSharp.Utilities.Cli/MangaDexSharp.Utilities.Cli.Verbs.DownloadVerb.md), [`ExportReadListVerb`](../api/MangaDexSharp.Utilities.Cli/MangaDexSharp.Utilities.Cli.Verbs.ExportReadListVerb.md), and [`CheckAuthVerb`](../api/MangaDexSharp.Utilities.Cli/MangaDexSharp.Utilities.Cli.Verbs.CheckAuthVerb.md) contain the verb execution logic.
