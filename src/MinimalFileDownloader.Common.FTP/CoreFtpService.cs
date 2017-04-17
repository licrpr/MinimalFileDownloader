using CoreFtp;
using CoreFtp.Enum;
using CoreFtp.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MinimalFileDownloader.Common.FTP
{
    public class CoreFtpService : IFtpService
    {
        public FtpSettings AppSettings { get; }

        private FtpClient _session;

        public CoreFtpService(FtpSettings appSettings)
        {
            AppSettings = appSettings;
        }

        public async Task<IReadOnlyCollection<string>> ListFilesAsync(string path)
        {
            await EnsureConnected();
            return await EnumerateFilesAsync(path);
        }

        public async Task<IReadOnlyCollection<string>> ListFoldersAsync(string path)
        {
            await EnsureConnected();
            return await EnumerateFoldersAsync(path);
        }

        public async Task<IReadOnlyCollection<IFtpItem>> ListItemsAsync(string path, bool recursive, bool getFiles, bool getFolders)
        {
            await EnsureConnected();
            return await EnumerateItemsAsync(path, recursive, getFiles, getFolders);
        }

        private async Task<IReadOnlyCollection<IFtpItem>> EnumerateItemsAsync(string path, bool recursive, bool getFiles, bool getFolders)
        {
            List<IFtpItem> items = new List<IFtpItem>();

            await _session.ChangeWorkingDirectoryAsync(path);
            var entries = await _session.ListAllAsync();

            if (getFolders)
            {
                items.Add(new FtpItem()
                {
                    Path = path,
                    IsDirectory = true
                });
            }

            foreach (FtpNodeInformation entry in entries)
            {
                string childPath = path.AppendPath(entry.Name);
                switch (entry.NodeType)
                {
                    case FtpNodeType.File:
                        items.Add(new FtpItem()
                        {
                            Path = childPath,
                            IsFile = true
                        });
                        break;

                    case FtpNodeType.Directory:
                        var childItems = await EnumerateItemsAsync(childPath, recursive, getFiles, getFolders);
                        items.AddRange(childItems);
                        break;
                }
            }
            return items;
        }

        private async Task<IReadOnlyCollection<string>> EnumerateFilesAsync(string path)
        {
            List<string> files = new List<string>();

            await _session.ChangeWorkingDirectoryAsync(path);
            var entries = await _session.ListAllAsync();
            foreach (FtpNodeInformation entry in entries)
            {
                string fullName = path.AppendPath(entry.Name);
                switch (entry.NodeType)
                {
                    case FtpNodeType.File:
                        files.Add(fullName);
                        break;

                    case FtpNodeType.Directory:
                        var childFiles = await EnumerateFilesAsync(fullName);
                        files.AddRange(childFiles);
                        break;
                }
            }
            return files;
        }

        private async Task<IReadOnlyCollection<string>> EnumerateFoldersAsync(string path)
        {
            List<string> directories = new List<string>();
            directories.Add(path);

            await _session.ChangeWorkingDirectoryAsync(path);
            var childDirectories = await _session.ListDirectoriesAsync();
            foreach (FtpNodeInformation entry in childDirectories)
            {
                string fullName = path.AppendPath(entry.Name);
                switch (entry.NodeType)
                {
                    case FtpNodeType.Directory:
                        var directoriesOfChildDirectory = await EnumerateFoldersAsync(fullName);
                        directories.AddRange(directoriesOfChildDirectory);
                        break;
                }
            }
            return directories;
        }

        private async Task EnsureConnected()
        {
            if (_session == null || !_session.IsConnected)
            {
                _session?.Dispose();

                FtpClientConfiguration sessionOptions = new FtpClientConfiguration
                {
                    Host = AppSettings.Url,
                    Username = AppSettings.UserName,
                    Password = AppSettings.Password,
                    Port = 21,
                    EncryptionType = FtpEncryption.None,
                    IgnoreCertificateErrors = true,
                    SslProtocols = System.Security.Authentication.SslProtocols.None
                };

                _session = new FtpClient(sessionOptions);

                await _session.LoginAsync();
            }
        }

        public void Dispose()
        {
            _session?.Dispose();
        }
    }
}