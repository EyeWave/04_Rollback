using System;
using System.Collections.Generic;
using System.Linq;
using TaskRollback.Commands;

namespace TaskRollback.Accounts
{
    internal class AccountsBank
    {
        internal int CountAccounts => accounts.Count;

        private readonly List<Account> accounts;
        private readonly Stack<IRollbacked> tranactions;

        public AccountsBank()
        {
            accounts = new List<Account>();
            tranactions = new Stack<IRollbacked>();
        }

        internal bool IsExistAccount(uint idAccount)
        {
            return accounts.Select(x => x.Id).Contains(idAccount);
        }

        internal bool TryAddAccount(Account account)
        {
            if (account == null)
                throw new NullReferenceException();

            if (IsExistAccount(account.Id))
                return false;
            else
                accounts.Add(account);

            return true;
        }

        internal Account GetAccountById(uint idAccount)
        {
            return accounts.FirstOrDefault(x => x.Id == idAccount);
        }

        internal Account ExtractAccountById(uint idAccount)
        {
            var account = GetAccountById(idAccount);

            if (account != null)
                accounts.Remove(account);

            return account;
        }

        internal IRollbacked ExtractLastTranaction()
        {
            if (tranactions.Count == 0)
                return null;

            return tranactions.Pop();
        }

        internal void SaveTranaction(IRollbacked tranaction)
        {
            if (tranaction == null)
                throw new NullReferenceException();

            tranactions.Push(tranaction);
        }

        internal bool TryTransferValue(uint idAccountSender, uint idAccountRecipient, uint valueTransfer)
        {
            if (!IsExistAccount(idAccountSender) || !IsExistAccount(idAccountRecipient))
                return false;

            var accountSender = GetAccountById(idAccountSender);
            var accountRecipient = GetAccountById(idAccountRecipient);

            return accountSender.TryTransferTo(accountRecipient, valueTransfer);
        }

        internal IEnumerable<string> GetStateAccounts()
        {
            return accounts.Select(account => account.ToString());
        }
    }
}
