namespace AccountManagementStatePatternSample
{
    public partial class AccountManagementContext : IAccountState
    {
        private AccountState accountState;

        private string owner;
        public double Interest { get; private set; }
        public double LowerLimit { get; private set; }
        public double UpperLimit { get; private set; }
        public double ServiceFee { get; private set; }
        public double Balance { get; private set; }

        public AccountManagementContext(string owner)
        {
            accountState = new SilverState(this);
            this.owner = owner;
        }

        public void Deposit(double amount)
        {
            accountState.Deposit(amount);

            Console.WriteLine("Deposited {0:C} --- ", amount);
            Console.WriteLine(" Balance = {0:C}", Balance);
            Console.WriteLine(" Status = {0}", accountState.GetType().Name);
            Console.WriteLine("");
        }

        public void Withdraw(double amount)
        {
            accountState.Withdraw(amount);

            Console.WriteLine("Withdraw {0:C} --- ", amount);
            Console.WriteLine(" Balance = {0:C}", Balance);
            Console.WriteLine(" Status = {0}\n", accountState.GetType().Name);
        }

        internal void Initialize(double interest, double lowerLimit, double upperLimit, double serviceFee)
        {
            Interest = interest;
            LowerLimit = lowerLimit;
            UpperLimit = upperLimit;
            ServiceFee = serviceFee;
        }

        private void deposit(double amount)
        {
            Balance += amount;
        }

        private void withdraw(double amount)
        {
            Balance -= amount;
        }

        private void payInterest()
        {
            Balance += Interest * Balance;
        }

        public void PayInterest()
        {
            accountState.PayInterest();
            Console.WriteLine("Interest Paid --- ");
            Console.WriteLine(" Balance = {0:C}", Balance);
            Console.WriteLine(" Status = {0}\n", accountState.GetType().Name);
        }
    }
}