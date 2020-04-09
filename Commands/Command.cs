using TaskRollback.Accounts;

namespace TaskRollback.Commands
{
    internal abstract class Command
    {
        internal abstract void Do(AccountsBank accountsBank, UserInterface userInterface);
    }
}
