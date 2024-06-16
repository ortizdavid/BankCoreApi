namespace BankCoreApi.Models.Statistics
{
    public class TransactionStatisticsCountByType
    {
        public string? TransactionType { get; set; }
        public int Count { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class TransactionStatisticsCountByStatus
    {
        public string? TransactionStatus { get; set; }
        public int Count { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class TransactionStatisticsTotalAmountByType
    {
        public string? TransactionType { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class TransactionStatisticsTotalAmountByStatus
    {
        public string? TransactionStatus { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
