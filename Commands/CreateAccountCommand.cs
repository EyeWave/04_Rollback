using TaskRollback.Accounts;

namespace TaskRollback.Commands
{
    internal class CreateAccountCommand : Command, IRollbacked
    {
        private uint idAccount;
        private uint valueAccount;

        internal sealed override void Do(AccountsBank accountsBank, UserInterface userInterface)
        {
            do
                idAccount = userInterface.GetUint("Enter ID account");
            while (accountsBank.IsExistAccount(idAccount));

            valueAccount = userInterface.GetUint("Enter value account");

            var account = Account.Create()
                .SetId(idAccount)
                .SetValue(valueAccount)
                .Build();

            if (accountsBank.TryAddAccount(account))
                accountsBank.SaveTranaction(this as IRollbacked);
        }

        public void DoBack(AccountsBank accountsBank, UserInterface userInterface)
        {
            var extractedAccount = accountsBank.ExtractAccountById(idAccount);
            if (extractedAccount != null)
                userInterface.ShowMessage("Deleted account with id =", extractedAccount.Id.ToString());
        }
    }
}
