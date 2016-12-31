using BitportDownloader_SSH;
using BitportViewer.ConsoleTest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BitportDownloader.ConsoleTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Win32PlatformService.Initialize();

            //var tokenPath = Path.Combine(Environment.CurrentDirectory, "bitportToken.json");

            var initializationCommands = new List<IInitializationCommand>
            {
                //new EnsureTokenIsCreatedCommand(tokenPath)
            };

            initializationCommands.RunAll();
            string appSettingsJson = File.ReadAllText("appSettings.json");

            var settings = JsonConvert.DeserializeObject<AppSettings>(appSettingsJson);

            using (var ftpService = new FtpService(settings))
            using (var downloadManager = new DownloadManager(settings))
            {
                var userCommands = new List<IUserCommand>
                {
                    new ListActiveDownloadsCommand(settings,downloadManager,ftpService),
                    new DownloadFileCommand(settings,downloadManager,ftpService),
                    new SyncFolderCommand(settings,downloadManager,ftpService)
                };

                if (args.Any())
                {
                    IUserCommand cmd = GetNewCommand(userCommands, args[0]);
                    if (cmd == null)
                    {
                        ConsoleUtils.WriteLine("Invalid command name!");
                    }
                    else
                    {
                        var argsDict = ParseArgs(args.Skip(1));

                        cmd.RunSilent(argsDict);
                    }
                }
                else
                {
                    RunCommands(userCommands);
                }
            }
        }

        private static void RunCommands(IReadOnlyCollection<IUserCommand> userCommands)
        {

            IUserCommand result;
            string cmd;
            int iteration = 0;
            ConsoleUtils.DoCommandsWhileNotEscape(() =>
            {
                if (iteration == 0)
                {
                    ConsoleUtils.Clear();
                    ConsoleUtils.WriteLine("Write name of command!");
                    ConsoleUtils.WriteLine("Possible commands:");

                    int minDif = userCommands.MinimalDiference();

                    foreach (var userCommand in userCommands)
                    {
                        ConsoleUtils.WriteOption($"{userCommand.Name} ({userCommand.Shortcut})",minDif);
                    }
                    iteration++;
                }
                else
                {
                    ConsoleUtils.WriteLine("Invalid command!");
                }
            }, cmdName =>
            {
                result = GetNewCommand(userCommands, cmdName);
                if (result != null)
                {
                    ConsoleUtils.Clear();
                    result.Run();
                    iteration = 0;
                }
            });
        }

        private static IUserCommand GetNewCommand(IReadOnlyCollection<IUserCommand> userCommands, string commandName)
        {
            IUserCommand result;

            int minimalDif = userCommands.MinimalDiference();
            if (commandName.Length == minimalDif)
            {
                result = userCommands.FirstOrDefault(o => string.Equals(o.Shortcut.Substring(0, minimalDif), commandName, StringComparison.InvariantCultureIgnoreCase));
                if (result != null)
                    return result;
            }

            result = userCommands.FirstOrDefault(o => string.Equals(o.Shortcut, commandName, StringComparison.InvariantCultureIgnoreCase));
            if (result != null)
                return result;

            result = userCommands.FirstOrDefault(o => string.Equals(o.Name, commandName, StringComparison.InvariantCultureIgnoreCase));
            if (result != null)
                return result;

            return result;
        }

        private static IReadOnlyDictionary<string, string> ParseArgs(IEnumerable<string> args)
        {
            return args
                .Select(o => o.Split('='))
                .ToDictionary(o => o.FirstOrDefault(), o => (o.Length == 1 ? o[0] : string.Join("=", o.Skip(1))).Trim('"'));
        }
    }
}