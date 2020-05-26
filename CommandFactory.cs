using System;
using System.Collections.Generic;
using System.Linq;
using TaskRollback.Commands;

namespace TaskRollback
{
    internal static class CommandFactory
    {
        private static readonly Dictionary<string, Func<Command>> mapStringToClass = new Dictionary<string, Func<Command>>()
        {
            { "create",     () => new CreateAccountCommand() },
            { "close",      () => new CloseAccountCommand() },
            { "transfer",   () => new TransferCommand() },
            { "bank",       () => new StatusBankCommand() },
            { "undo" ,      () => new UndoCommand() },
            { "quit" ,      () => new QuitCommand() },
        };

        internal static Command Create(string commandName)
        {
            return GetCreator(commandName).Invoke();
        }

        internal static IEnumerable<Command> Create(IEnumerable<string> commandNames)
        {
            return commandNames
                .Where(commandName => mapStringToClass.ContainsKey(commandName))
                .Select(commandName => Create(commandName));
        }

        private static Func<Command> GetCreator(string commandName)
        {
            Func<Command> creator;
            if (mapStringToClass.TryGetValue(commandName, out creator))
                return creator;
            else
                return () => new DummyCommand();
        }
    }
}
