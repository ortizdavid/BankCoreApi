namespace BankCoreApi.Models.Statistics;

public class CustomerStatisticsCountByType
{
    public string? CustomerType { get; }
    public int Count { get; }
}

public class CustomerStatisticsCountByStatus
{
    public string? CustomerStatus { get; }
    public int Count { get; }
}

public class CustomerStatisticsCountByGender
{
    public string? Gender { get; }
    public int Count { get; }
}

public class CustomerStatisticsCountByAge
{
    public string? AgeRange { get; }
    public int Count { get; }
}
