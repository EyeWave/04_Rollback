using TaskRollback.Accounts;

namespace TaskRollback.Commands
{
    internal class CloseAccountCommand : Command, IRollbacked
    {
        private uint idAccount;
        private uint valueAccount;

        internal sealed override void Do(AccountsBank accountsBank, UserInterface userInterface)
        {
            idAccount = userInterface.GetUint("Enter ID account for close");
            var extractedAccount = accountsBank.ExtractAccountById(idAccount);

            if (extractedAccount == null)
                userInterface.ShowMessage("Account with ID", idAccount.ToString(), "not exist");
            else
            {
                valueAccount = extractedAccount.Value;
                accountsBank.SaveTranaction(this as IRollbacked);
                userInterface.ShowMessage("Deleted account with id =", extractedAccount.Id.ToString());
            }
        }

        public void DoBack(AccountsBank accountsBank, UserInterface userInterface)
        {
            if (accountsBank.IsExistAccount(idAccount))
                userInterface.ShowMessage("Сannot be undone tranaction: close account with ID", idAccount.ToString());
            else
            {
                var account = Account.Create()
                    .SetId(idAccount)
                    .SetValue(valueAccount)
                    .Build();

                accountsBank.TryAddAccount(account);
                userInterface.ShowMessage("Created account with id =", idAccount.ToString(), "and value =", valueAccount.ToString());
            }
        }
    }
}
