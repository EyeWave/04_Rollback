using TaskRollback.Accounts;

namespace TaskRollback.Commands
{
    internal class StatusBankCommand : Command
    {
        internal sealed override void Do(AccountsBank accountsBank, UserInterface userInterface)
        {
            userInterface.PrintCollection(accountsBank.GetStateAccounts());
        }
    }
}
