namespace BankCoreApi.Models.Accounts
{
    public interface IAccount
    {
        int AccountId { get; }
        int CustomerId { get; }
        string? AccountNumber { get; }
        string? Iban { get; }
        string? HolderName { get; }
        int AccountTypeId { get; set; }
        int AccountStatusId { get; set; }
        void Deposit(decimal amount);
        void Withdraw(decimal amount);
        void Transfer(decimal amount, IAccount destination);
    }
}