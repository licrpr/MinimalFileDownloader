namespace MinimalFileDownloader.Common.FTP
{
    internal static class FtpPathUtils
    {
        public static string AppendPath(this string directory, string name)
        {
            if (directory.EndsWith("/"))
            {
                return directory + name;
            }
            else
            {
                return $"{directory}/{name}";
            }
        }
    }
}