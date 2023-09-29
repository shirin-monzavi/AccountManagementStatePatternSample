namespace AccountManagementStatePatternSample
{
    public interface IAccountState
    {
        void Withdraw(double amount);

        void PayInterest();

        void Deposit(double amount);
    }
}