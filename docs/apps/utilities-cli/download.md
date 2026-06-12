# `download` Verb

Downloads MangaDex chapter images with [`IDownloadUtilityService`](../../api/MangaDexSharp.Utilities/MangaDexSharp.Utilities.Download.IDownloadUtilityService.md). The options class is [`DownloadOptions`](../../api/MangaDexSharp.Utilities.Cli/MangaDexSharp.Utilities.Cli.Verbs.DownloadOptions.md), and execution lives in [`DownloadVerb`](../../api/MangaDexSharp.Utilities.Cli/MangaDexSharp.Utilities.Cli.Verbs.DownloadVerb.md).

## Flags

| Flag | Alias | Purpose | Example |
| --- | --- | --- | --- |
| `--manga-id` | `-m` | Manga ID to download chapters from. If chapter IDs are omitted, downloads the manga feed. | `--manga-id fc0a7b86-992e-4126-b30f-ca04811979bf` |
| `--chapter-ids` | `-i` | One or more chapter IDs to download. | `--chapter-ids id-1 id-2` |
| `--cache-directory` | `-c` | Directory used to cache downloaded images. | `--cache-directory .cache/md` |
| `--purge-cache` | `-p` | Deletes cached images after download. | `--purge-cache true` |
| `--directory` | `-d` | Output directory for archives or folders. | `--directory downloads` |
| `--output` | `-o` | Output type: `ZIP`, `CBZ`, or `DIR`. `EPUB` is listed in help but not implemented by the verb. | `--output cbz` |
| `--grouping` | `-g` | Archive grouping: `SingleFile`, `Volumes`, or `Chapters`. | `--grouping volumes` |
| `--rate-limit-timeout` | `-t` | Seconds to pause after the configured image request count. | `--rate-limit-timeout 15` |
| `--rate-limit-after` | `-a` | Number of images to download before pausing. | `--rate-limit-after 35` |
| `--format-image-names` | `-f` | Renames images by page index instead of source filename. | `--format-image-names true` |
| `--language` | `-l` | Translated language code to download. | `--language en` |
| `--preferred-group-ids` | `-p` | Preferred scanlation group IDs when duplicate chapter ordinals exist. | `--preferred-group-ids group-id-1` |

> Note: the current source assigns `-p` to both `--purge-cache` and `--preferred-group-ids`. Long names are safest until the alias conflict is resolved.

## Example Commands

```powershell
# Download an English manga feed to CBZ files grouped by volume.
dotnet run --project src/MangaDexSharp.Utilities.Cli -- download --manga-id fc0a7b86-992e-4126-b30f-ca04811979bf --output cbz --grouping volumes --directory downloads --language en

# Download specific chapters and format image filenames by page index.
dotnet run --project src/MangaDexSharp.Utilities.Cli -- download --chapter-ids chapter-id-1 chapter-id-2 --output zip --format-image-names true
```

## Successful Result

```text
[Information] Finished downloading 42 images with 0 errors
```

The concrete files depend on [`ArchiveType`](../../api/MangaDexSharp.Utilities/MangaDexSharp.Utilities.Download.ArchiveType.md), [`FileGroupingType`](../../api/MangaDexSharp.Utilities/MangaDexSharp.Utilities.Download.FileGroupingType.md), and the requested manga or chapter IDs.
