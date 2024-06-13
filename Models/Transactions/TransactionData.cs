namespace BankCoreApi.Models.Transactions
{
    public class TransactionData
    {
        public int TransactionId { get; set; }
        public Guid UniqueId { get; set; }
        public string? Code { get; set; }
        public decimal Amount { get; set; }
        public decimal BalanceBefore { get; set; }
        public decimal BalanceAfter { get; set; }
        public string? Currency { get; set; }
        public string? Description { get; set; }
        public DateTime TransactionDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public int SourceAccountId { get; set; }
        public string? SourceAccountNumber { get; set; }
        public string? SourceIban { get; set; }

        public int DestinationAccountId { get; set; }
        public string? DestinationAccountNumber { get; set; }
        public string? DestinationIban { get; set; }

        public string? CustomerName { get; set; }
        public string? IdentificationNumber { get; set; }

        public int TypeId { get; set; }
        public string? TypeName { get; set; }

        public int StatusId { get; set; }
        public string? StatusName { get; set; }
    }
}
