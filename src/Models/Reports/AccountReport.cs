namespace BankCoreApi.Models.Reports
{
    public class AccountReport
    {
        public string? AccountNumber { get; set; }
        public string? Iban { get; set; }
        public decimal Balance { get; set; }
        public string? Currency { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int CustomerId { get; set; }
        public string? CustomerName { get; set; }
        public string? IdentificationNumber { get; set; }
        public string? AccountType { get; set; }
        public string? AccountStatus { get; set; }
    }
}