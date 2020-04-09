using System;

namespace TaskRollback.Accounts
{
    internal class Account
    {
        internal uint Id => id.Value;
        internal uint Value { get; private set; }

        private uint? id;

        private Account()
        {
        }

        internal bool TryTransferTo(Account accountRecipient, uint valueTransfer)
        {
            if (Value < valueTransfer)
                return false;

            Value -= valueTransfer;
            accountRecipient.Value += valueTransfer;
            return true;
        }

        public override string ToString()
        {
            return string.Concat("Account ", Id, " : [ ", nameof(Value), "=", Value, " ]");
        }

        #region BUILDER

        internal static AccountBuilder Create()
        {
            return new AccountBuilder(new Account());
        }

        internal sealed class AccountBuilder
        {
            private Account account;

            internal AccountBuilder(Account account)
            {
                this.account = account;
            }

            internal AccountBuilder SetId(uint id)
            {
                if (account == null)
                    throw new Exception(nameof(account) + " not setted");

                if (account.id == null)
                    account.id = id;
                else
                    throw new AccessViolationException(nameof(account.id) + " allready setted");

                return this;
            }

            internal AccountBuilder SetValue(uint value)
            {
                if (account == null)
                    throw new Exception(nameof(account) + " not setted");

                account.Value = value;

                return this;
            }

            internal Account Build()
            {
                if (account.id == null)
                    throw new Exception(nameof(account.id) + " not setted");

                var outAccount = account;
                account = null;
                return outAccount;
            }
        }

        #endregion BUILDER
    }
}
