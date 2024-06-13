namespace BankCoreApi.Models.Reports
{
    public class TransactionReport
    {
        public string? Code { get; }
        public decimal Amount { get; }
        public decimal BalanceBefore { get; }
        public decimal BalanceAfter { get; }
        public string? Currency { get; }
        public string? Description { get; }
        public DateTime TransactionDate { get; }
        public int AccountId { get; }
        public string? AccountNumber { get; }
        public string? Iban { get; }
        public string? CustomerName { get; }
        public string? IdentificationNumber { get; }
        public string? TransactionType { get; }
        public string? TransactionStatus { get; }
    }
}