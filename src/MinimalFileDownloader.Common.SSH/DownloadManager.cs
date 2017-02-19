using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace MinimalFileDownloader.Common.SSH
{
    public class SshDownloadManager : IDisposable, IDownloadManager
    {
        public SshSettings Setup { get; }

        private SshClient sshClient { get; }

        private object _communicationLock = new object();

        public SshDownloadManager(SshSettings setup)
        {
            Setup = setup;

            sshClient = new SshClient(setup.HostName, setup.UserName, setup.Password);
            sshClient.ConnectionInfo.Timeout = new TimeSpan(0, 0, 10);

            try
            {
                sshClient.Connect();
            }
            catch (Exception)
            {
            }
        }

        public bool IsConnected
        {
            get
            {
                return sshClient.IsConnected;
            }
        }

        public void Reconect()
        {
            if (sshClient.IsConnected)
                sshClient.Disconnect();
            sshClient.Connect();
        }

        //private const string DownloadCommand = "wget -c \"{0}\" -O \"{1}\" 2>&1";
        private const string DownloadCommand = "curl \"{0}\" --progress-bar -o \"{1}\"";

        private static readonly string DownloadCommandRegex = "^curl (.*) --progress-bar -o (.*)$";
        //private static readonly string DownloadCommandRegex = "^wget -c (.*?) -O (.*?)$";

        public void StartDownloadingFiles(IEnumerable<DownloadInfo> infos)
        {
            ValidateIsConnected();

            string scriptName = Guid.NewGuid().ToString();

            string scriptPath = $"{Setup.DownloadPath}/{scriptName}.download.sh";

            string logPath = $"{Setup.DownloadPath}/{scriptName}.cmdlog";

            string script = string.Join("; ", infos.SelectMany(o => CreateDownloadCommand(o)));

            string createScriptCommand = $"echo \"{script.Escape()}\" > \"{scriptPath.Escape()}\"";
            RunCommand(createScriptCommand);

            string startDownloadCmd = $"nohup bash \"{scriptPath.Escape()}\" > \"{logPath.Escape()}\" &";
            RunCommand(startDownloadCmd);
        }

        private IEnumerable<string> CreateDownloadCommand(DownloadInfo info)
        {
            string downloadPath = $"{Setup.DownloadPath}/{info.Name}";

            string logName = Guid.NewGuid().ToString();
            string logPath = $"{Setup.DownloadPath}/{logName}.filelog";

            string[] dirPathParts = downloadPath.Split('/');
            string dirPath = string.Join("/", dirPathParts.Take(dirPathParts.Length - 1));

            return new string[]
            {
                $"echo \"{downloadPath}\" >>\"{logPath}\"",
                $"echo \"{info.Url}\" >>\"{logPath}\"",
                $"date >>\"{logPath}\"",
                $"mkdir -p \"{dirPath}\" >>\"{logPath}\"",
                $"{string.Format(DownloadCommand, info.Url.Escape(), downloadPath.Escape())} 2>>\"{logPath}\"",
                $"date >>\"{logPath}\"",
            };
        }

        public IReadOnlyCollection<IDownloadState> Downloads
        {
            get
            {
                ValidateIsConnected();

                string result = RunCommand("ps");
                var activeDownloads = result.SplitLines()
                    .Select(o => Regex.Match(o, "^ *([^ ]+) *([^ ]+) *([^ ]+) (.*)$"))
                    .Where(o => o.Success)
                    .Select(o => new { pid = o.Groups[1].Value.TryParseInt(), nameMatch = Regex.Match(o.Groups[4].Value, DownloadCommandRegex) })
                    .Where(o => o.pid.HasValue && o.nameMatch.Success)
                    .Select(o => new { pid = o.pid.Value, url = o.nameMatch.Groups[1].Value.TrimQuotes(), path = o.nameMatch.Groups[2].Value.TrimQuotes() })
                    .ToArray();

                result = RunCommand($"cd \"{Setup.DownloadPath}\" && ls");
                var files = result.SplitLines();
                var fileLogs = files
                    .Where(file => file.EndsWith(".filelog"))
                    .Select(logFile => $"{Setup.DownloadPath}/{logFile}")
                    .ToDictionary(logFile => logFile, logFile => GetFileName(logFile));

                var finishedDownloads = fileLogs.Values
                    .Except(activeDownloads.Select(o => o.path))
                    .ToArray();

                return Enumerable.Empty<DownloadState>()
                    .Concat(activeDownloads.Select(o => DownloadState.CreateActive(o.path, o.pid, GetCompletion(o.path, fileLogs))))
                    .Concat(finishedDownloads.Select(o => DownloadState.CreateFinished(o)))
                    .ToArray();
            }
        }

        private string GetFileName(string logPath)
        {
            string cmd = $"head -1 \"{logPath}\"";
            string result = RunCommand(cmd);
            return result.SplitLines().FirstOrDefault();
        }

        private int? GetCompletion(string downloadPath, IReadOnlyDictionary<string, string> fileLogs)
        {
            var logPath = fileLogs.Where(o => o.Value == downloadPath).FirstOrDefault().Key;

            string cmd = $"cat \"{logPath}\" | tail -n 5";
            string result = RunCommand(cmd);
            return result
                 .SplitLines()
                 .Select(o => Regex.Match(o, @"^#* *(\d+)\.\d%"))
                 .Where(o => o.Success)
                 .Select(o => o.Groups[1].Value.TryParseInt())
                 .Where(o => o.HasValue)
                 .Select(o => o.Value)
                 .LastOrDefault();
        }

        public void StopDownloadingFile(IDownloadState state)
        {
            var knownState = state as DownloadState;
            if (knownState == null)
            {
                throw new InvalidOperationException($"Only supports {typeof(DownloadState).FullName} as type of state!");
            }
            if (knownState.Pid.HasValue)
                StopDownloadingFile(knownState.Pid.Value);
        }

        public void StopDownloadingFile(int pid)
        {
            ValidateIsConnected();

            RunCommand($"kill {pid}");
        }

        private string RunCommand(string cmd)
        {
            lock (_communicationLock)
            {
                var command = sshClient.CreateCommand(cmd);
                command.CommandTimeout = TimeSpan.FromSeconds(10);
                var asyncRes = command.BeginExecute();
                while (!asyncRes.IsCompleted)
                {
                    Thread.Sleep(100);
                }
                return command.Result;
            }
        }

        private void ValidateIsConnected()
        {
            if (!IsConnected)
                throw new InvalidOperationException("Must be Connected!");
        }

        public void Dispose()
        {
            if (sshClient != null)
            {
                if (sshClient.IsConnected)
                    sshClient.Disconnect();
                sshClient.Dispose();
            }
        }
    }
}