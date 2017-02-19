using System;

namespace MinimalFileDownloader.Common.SSH
{
    public class DownloadState : IEquatable<DownloadState>, IDownloadState
    {
        public enum Statuses
        {
            Unknown = 0,
            Active,
            Finished
        }

        public Statuses Status { get; }

        public int? Completion { get; }

        public string Path { get; }
        public int? Pid { get; }

        private DownloadState(Statuses status, string path, int? pid, int? completion)
        {
            Status = status;
            Path = path;
            Pid = pid;
            Completion = completion;
        }

        public static DownloadState CreateActive(string path, int pid, int? completion)
        {
            return new DownloadState(Statuses.Active, path, pid, completion);
        }

        public static DownloadState CreateFinished(string path)
        {
            return new DownloadState(Statuses.Finished, path, null, 100);
        }

        public override string ToString()
        {
            return $"Path:\"{Path}\", Status:\"{Status}\", Pid:\"{Pid}\", {Completion}%";
        }

        public bool Equals(DownloadState other)
        {
            if (other == null)
                return false;

            return this.Path == other.Path &&
                this.Status == other.Status &&
                this.Pid == other.Pid &&
                this.Completion == other.Completion;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as DownloadState);
        }

        public override int GetHashCode()
        {
            return Path.GetHashCode();
        }
    }
}