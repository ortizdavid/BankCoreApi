namespace BankCoreApi.Models.Statistics
{
    public class AccountStatisticsCountByType
    {
        public string? AccountType { get; set; }
        public int Count { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class AccountStatisticsCountByStatus
    {
        public string? AccountStatus { get; set; }
        public int Count { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class AccountStatisticsTotalBalanceByType
    {
        public string? AccountType { get; set; }
        public decimal TotalBalance { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class AccountStatisticsTotalBalanceByStatus
    {
        public string? AccountStatus { get; set; }
        public decimal TotalBalance { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
