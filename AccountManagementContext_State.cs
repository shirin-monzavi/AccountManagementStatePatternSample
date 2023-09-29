namespace AccountManagementStatePatternSample
{
    public partial class AccountManagementContext
    {
        public abstract class AccountState : IAccountState
        {
            public AccountState(AccountManagementContext accountManagementContext)
            {
                account = accountManagementContext;
            }

            public AccountManagementContext account { get; }

            public virtual void Deposit(double amount)
            {
                account.deposit(amount);
                StateChangeCheck();
            }

            public virtual void PayInterest()
            {
                account.payInterest();
                StateChangeCheck();
            }

            public virtual void Withdraw(double amount)
            {
                account.withdraw(amount);
                StateChangeCheck();
            }

            protected abstract void StateChangeCheck();
        }

        private class GoldState : AccountState
        {
            public GoldState(AccountManagementContext accountManagementContext) : base(accountManagementContext)
            {
                account.Initialize(0.5, 1000.0, 1000000.0, 0);
            }

            protected override void StateChangeCheck()
            {
                if (account.Balance < 0.0)
                {
                    account.accountState = new RedState(account);
                    return;
                }
                if (account.Balance < account.LowerLimit)
                {
                    account.accountState = new SilverState(account);
                    return;
                }
            }
        }

        private class SilverState : AccountState
        {
            public SilverState(AccountManagementContext accountManagementContext) : base(accountManagementContext)
            {
                account.Initialize(0.0, 0.0, 1000.0, 0.0);
            }

            protected override void StateChangeCheck()
            {
                if (account.Balance < account.LowerLimit)
                {
                    account.accountState = new RedState(account);
                    return;
                }
                if (account.Balance > account.UpperLimit)
                {
                    account.accountState = new GoldState(account);
                    return;
                }
            }
        }

        private class RedState : AccountState
        {
            public RedState(AccountManagementContext accountManagementContext) : base(accountManagementContext)
            {
                account.Initialize(0.0, -100.0, 0.0, 15.00);
            }

            protected override void StateChangeCheck()
            {
                if (account.Balance > account.UpperLimit)
                    account.accountState = new SilverState(account);
            }

            public override void PayInterest()
            {
                throw new NotImplementedException();
            }

            public override void Withdraw(double amount)
            {
                throw new NotImplementedException();
            }
        }
    }
}