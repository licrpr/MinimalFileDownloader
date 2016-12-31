using System.Collections.Generic;

namespace BitportViewer.ConsoleTest
{
    internal interface IInitializationCommand
    {
        int Order { get; }
        bool CanFail { get; }

        void Run();
    }

    internal static class ICommandExtensions
    {
        public static void RunAll(this IEnumerable<IInitializationCommand> commands)
        {
            foreach (var cmd in commands)
            {
                if (cmd.CanFail)
                {
                    try
                    {
                        cmd.Run();
                    }
                    catch { }
                }
                else
                {
                    cmd.Run();
                }
            }
        }
    }
}