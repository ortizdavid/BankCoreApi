namespace BankCoreApi.Models.Statistics
{
    public class TransactionStatisticsCountByType
    {
        public string? TransactionType { get; }
        public int Count { get; }
    }

    public class TransactionStatisticsCountByStatus
    {
        public string? TransactionStatus { get; }
        public int Count { get; }
    }

    public class TransactionStatisticsTotalAmountByType
    {
        public string? TransactionType { get; }
        public decimal TotalAmount { get; }
    }

    public class TransactionStatisticsTotalAmountByStatus
    {
        public string? TransactionStatus { get; }
        public decimal TotalAmount { get; }
    }
}
