namespace BankCoreApi.Models.Statistics
{
    public class CustomerStatisticsCountByType
    {
        public string? CustomerType { get; set; }
        public int Count { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CustomerStatisticsCountByStatus
    {
        public string? CustomerStatus { get; set; }
        public int Count { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CustomerStatisticsCountByGender
    {
        public string? Gender { get; set; }
        public int Count { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class CustomerStatisticsCountByAge
    {
        public int AgeGroup { get; set; }
        public int Count { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
