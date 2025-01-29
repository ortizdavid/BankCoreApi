namespace BankCoreApi.Models.Statistics;

public class AccountStatisticsCountByType
{
    public string? AccountType { get; }
    public int Count { get; }
}

public class AccountStatisticsCountByStatus
{
    public string? AccountStatus { get; }
    public int Count { get; }
}

public class AccountStatisticsTotalBalanceByType
{
    public string? AccountType { get; }
    public decimal TotalBalance { get; }
}

public class AccountStatisticsTotalBalanceByStatus
{
    public string? AccountStatus { get; }
    public decimal TotalBalance { get; }
}
