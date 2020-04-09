using System;
using TaskRollback.Accounts;

namespace TaskRollback.Commands
{
    internal class QuitCommand : Command
    {
        internal sealed override void Do(AccountsBank accountsBank, UserInterface userInterface)
        {
            Environment.Exit(0);
        }
    }
}
