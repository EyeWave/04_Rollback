using TaskRollback.Accounts;

namespace TaskRollback.Commands
{
    internal interface IRollbacked
    {
        void DoBack(AccountsBank accountsBank, UserInterface userInterface);
    }
}
