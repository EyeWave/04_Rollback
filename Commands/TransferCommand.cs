using TaskRollback.Accounts;

namespace TaskRollback.Commands
{
    internal class TransferCommand : Command, IRollbacked
    {
        private uint senderId;
        private uint recipientId;
        private uint valueTransfer;

        internal sealed override void Do(AccountsBank accountsBank, UserInterface userInterface)
        {
            if (accountsBank.CountAccounts < 2)
            {
                userInterface.ShowMessage("Not enough accounts for transaction");
                return;
            }

            do
                senderId = userInterface.GetUint("Enter sender account ID");
            while (!accountsBank.IsExistAccount(senderId));

            do
                recipientId = userInterface.GetUint("Enter recipient account ID");
            while (recipientId == senderId || !accountsBank.IsExistAccount(recipientId));

            var accountSender = accountsBank.GetAccountById(senderId);
            var requestValueString = "Enter transfer value";
            var fullRequestValueString = requestValueString;

            bool notEnoughValue()
            {
                var isNotEnough = accountSender.Value < valueTransfer;
                if (isNotEnough)
                    fullRequestValueString = string.Concat("Not enough value at account with ID = ", accountSender.Id, ". ", requestValueString);
                return isNotEnough;
            }

            do
                valueTransfer = userInterface.GetUint(fullRequestValueString);
            while (notEnoughValue());

            if (accountsBank.TryTransferValue(senderId, recipientId, valueTransfer))
            {
                accountsBank.SaveTranaction(this);
                userInterface.ShowMessage("Transfer complete");
            }
            else
                userInterface.ShowMessage("Something wrong. Transfer not completed.");
        }

        public void DoBack(AccountsBank accountsBank, UserInterface userInterface)
        {
            var message = accountsBank.TryTransferValue(recipientId, senderId, valueTransfer) ?
                "Undone transfer complete" :
                string.Concat("Сannot be undone tranaction: transfer value = ", valueTransfer, " from ", senderId, " to ", recipientId);

            userInterface.ShowMessage(message);
        }
    }
}
