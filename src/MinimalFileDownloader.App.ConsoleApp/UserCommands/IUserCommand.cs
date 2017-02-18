using System.Collections.Generic;
using System.Linq;

namespace MinimalFileDownloader.App.ConsoleApp
{
    internal interface IUserCommand
    {
        string Name { get; }
        string Shortcut { get; }

        void Run();

        void RunSilent(IReadOnlyDictionary<string, string> parameters);
    }

    internal static class IUserCommandExtensions
    {
        public static void RunAll(this IEnumerable<IUserCommand> commands)
        {
            foreach (var cmd in commands)
            {
                cmd.Run();
            }
        }

        public static int MinimalDiference(this IEnumerable<IUserCommand> commands)
        {
            IReadOnlyCollection<string> commandNames = commands.Select(o => o.Shortcut).ToArray();

            int maxLength = commandNames.Select(o => o.Length).DefaultIfEmpty(0).Max();
            for (int i = 0; i < maxLength; i++)
            {
                if (commandNames.Select(o => o.Substring(0, i)).Distinct().Count() == commandNames.Count)
                {
                    return i;
                }
            }
            return maxLength;
        }
    }
}