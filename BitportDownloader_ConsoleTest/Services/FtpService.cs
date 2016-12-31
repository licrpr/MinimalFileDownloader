using System;
using System.Collections.Generic;
using System.Linq;
using WinSCP;

namespace BitportViewer.ConsoleTest
{
    internal class FtpService : IDisposable
    {
        public AppSettings AppSettings { get; }

        private Session _session;

        public FtpService(AppSettings appSettings)
        {
            AppSettings = appSettings;
        }

        public IReadOnlyCollection<string> ListFiles(string path)
        {
            EnsureConnected();
            return EnumerateFiles(path).ToList();
        }

        public IReadOnlyCollection<string> ListFolders(string path)
        {
            EnsureConnected();
            return EnumerateFolders(path).ToList();
        }

        private IEnumerable<string> EnumerateFiles(string path)
        {
            var dirInfo = _session.ListDirectory(path);

            foreach (RemoteFileInfo entry in dirInfo.Files)
            {
                if (entry.IsParentDirectory || entry.IsThisDirectory)
                {
                    continue;
                }

                if (entry.IsDirectory)
                {
                    foreach (var childFile in EnumerateFiles(entry.FullName))
                    {
                        yield return childFile;
                    }
                }
                else
                {
                    yield return entry.FullName;
                }
            }
        }

        private IEnumerable<string> EnumerateFolders(string path)
        {
            var dirInfo = _session.ListDirectory(path);

            yield return path;

            foreach (RemoteFileInfo entry in dirInfo.Files)
            {
                if (entry.IsParentDirectory || entry.IsThisDirectory)
                {
                    continue;
                }

                if (entry.IsDirectory)
                {
                    foreach (var childFile in EnumerateFolders(entry.FullName))
                    {
                        yield return childFile;
                    }
                }
            }
        }

        private void EnsureConnected()
        {
            if (_session == null || !_session.Opened)
            {
                SessionOptions sessionOptions = new SessionOptions
                {
                    Protocol = Protocol.Ftp,
                    HostName = AppSettings.FtpUrl,
                    PortNumber = 21,
                    UserName = AppSettings.FtpUserName,
                    Password = AppSettings.FtpPassword
                };

                _session = new Session();
                _session.Open(sessionOptions);
            }
        }

        public void Dispose()
        {
            _session?.Dispose();
        }
    }
}