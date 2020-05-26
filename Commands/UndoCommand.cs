using TaskRollback.Accounts;

namespace TaskRollback.Commands
{
    internal class UndoCommand : Command
    {
        internal sealed override void Do(AccountsBank accountsBank, UserInterface userInterface)
        {
            var lastTranaction = accountsBank.ExtractLastTranaction();
            if (lastTranaction != null)
                lastTranaction.DoBack(accountsBank, userInterface);
        }
    }
}
