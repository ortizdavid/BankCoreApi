namespace BankCoreApi.Models.Accounts
{
    public interface IAccount
    {
        int AccountId { get; }
        int CustomerId { get; }
        string? AccountNumber { get; }
        string? Iban { get; }
        AccountStatus Status { get; set; }
        Guid UniqueId { get; }
        DateTime CreatedAt { get; set; } 
        DateTime UpdatedAt { get; set; }
        void Deposit(decimal amount);
        void Withdraw(decimal amount);
        void Transfer(decimal amount, IAccount destination);
    }
}