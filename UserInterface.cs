using System;
using System.Collections.Generic;
using System.Linq;

namespace TaskRollback
{
    internal class UserInterface
    {
        private const char COMMANDS_SEPARATOR = ' ';

        internal string GetCommandName()
        {
            Console.WriteLine("Enter command");
            return Console.ReadLine();
        }

        internal IEnumerable<string> GetCommandNames()
        {
            Console.WriteLine("Enter command or commands");

            return Console.ReadLine()
                .Trim(COMMANDS_SEPARATOR)
                .Split(COMMANDS_SEPARATOR)
                .Where(x => !string.IsNullOrEmpty(x));
        }

        internal uint GetUint(string getMessage)
        {
            Console.WriteLine(getMessage);

            uint value = 0;
            if (uint.TryParse(Console.ReadLine(), out value))
                return value;
            else
                return GetUint(getMessage);
        }

        internal void PrintCollection<T>(IEnumerable<T> collection)
        {
            if (collection.Count() > 0)
            {
                Console.WriteLine("------------------------------");
                collection.Each(item => Console.WriteLine(item.ToString()));
                Console.WriteLine("------------------------------");
            }
        }

        internal void ShowMessage(params string[] strings)
        {
            Console.WriteLine(strings.Aggregate(string.Empty, (fullString, x) => string.Concat(fullString, x, " "), fullString => fullString.TrimEnd(' ')));
        }

        internal void RequestContinuance()
        {
            Console.WriteLine("Press Enter to continue");
            while (true)
            {
                if (Console.ReadKey().Key == ConsoleKey.Enter)
                    break;
            }
            Console.Clear();
        }
    }
}
