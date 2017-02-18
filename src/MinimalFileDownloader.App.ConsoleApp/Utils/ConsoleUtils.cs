using System;
using System.Diagnostics;

namespace MinimalFileDownloader.App.ConsoleApp
{
    internal static class ConsoleUtils
    {
        public static void WriteLine(string line)
        {
            Console.WriteLine(line);
#if DEBUG
            Debug.WriteLine(line);
#endif
        }

        public static void WriteOption(string line, int lengthToUnderline = 0)
        {
            var foregroundColor = Console.ForegroundColor;
            var backgroundColor = Console.BackgroundColor;

            lengthToUnderline = Math.Min(Math.Max(lengthToUnderline, 0), line.Length);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.Write(line.Substring(0, lengthToUnderline));

            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(line.Substring(lengthToUnderline));

            Console.ForegroundColor = foregroundColor;
            Console.BackgroundColor = backgroundColor;
#if DEBUG
            Debug.WriteLine(line);
#endif
        }

        public static void Clear()
        {
            Console.Clear();
        }

        public static string GetCommand()
        {
            Console.CancelKeyPress += (o, e) =>
            {
                if (e.SpecialKey == ConsoleSpecialKey.ControlC)
                {
                    e.Cancel = true;
                }
            };

            string readLine = Console.ReadLine();
            if (readLine == null)
            {
                throw new EscapePressedException();
            }
            return (readLine ?? "").Trim();
        }

        public static void DoCommandsWhileNotEscape(string label, Action<string> action)
        {
            DoCommandsWhileNotEscape(() =>
            {
                if (!string.IsNullOrEmpty(label))
                    ConsoleUtils.WriteLine(label);
            }, action);
        }

        public static void DoCommandsWhileNotEscape(Action initialization, Action<string> action)
        {
            do
            {
                try
                {
                    if (initialization != null)
                    {
                        initialization();
                    }

                    var path = ConsoleUtils.GetCommand();
                    action(path);
                }
                catch (EscapePressedException)
                {
                    break;
                }
            }
            while (true);
        }

        //public static void DoWhileNotEscape(Action action)
        //{
        //    do
        //    {
        //        try
        //        {
        //            action();
        //            bool isEscape = false;
        //            Console.CancelKeyPress += (o, e) =>
        //            {
        //                if (e.SpecialKey == ConsoleSpecialKey.ControlC)
        //                {
        //                    isEscape = true;
        //                    e.Cancel = true;
        //                }
        //            };

        //            Console.ReadLine();
        //            if (isEscape)
        //            {
        //                throw new EscapePressedException();
        //            }
        //        }
        //        catch (EscapePressedException ex)
        //        {
        //            break;
        //        }
        //    }
        //    while (true);
        //}
    }
}