# MinimalFileDownloader
Download files using any Linux machine on network from FTP server, e.g. use Raspberry Pi with LibreElec to download files to external HDD.

## TODO
- Add possibility when syncing to not download files that have already been donwloaded. Check whether whole file was downloaded and is not currently being downloaded.
- Add all missing functionality to web UI.
- Add module, that deletes a file from BitPort after it was donwloaded. Configurable via appSettings.json .
- Add Watcher mode, so that it can run in the background and auto sync all.
- Improve ConsoleApp, add new Nuget package for console input.