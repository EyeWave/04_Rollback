using TaskRollback.Accounts;

namespace TaskRollback.Commands
{
    internal class DummyCommand : Command
    {
        internal sealed override void Do(AccountsBank accountsBank, UserInterface userInterface)
        {
        }
    }
}
