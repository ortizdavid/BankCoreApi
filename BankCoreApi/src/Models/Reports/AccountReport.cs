namespace BankCoreApi.Models.Reports
{
    public class AccountReport
    {
        public string? AccountNumber { get; }
        public string? Iban { get; }
        public decimal Balance { get; }
        public string? Currency { get; }
        public DateTime CreatedAt { get; }
        public DateTime UpdatedAt { get; }
        public int CustomerId { get; }
        public string? CustomerName { get; }
        public string? IdentificationNumber { get; }
        public string? AccountType { get; }
        public string? AccountStatus { get; }
    }
}