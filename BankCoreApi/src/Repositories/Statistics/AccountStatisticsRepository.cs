using System.Data;
using BankCoreApi.Models.Statistics;
using Dapper;

namespace BankCoreApi.Repositories.Statistics;

public class AccountStatisticsRepository
{
    private readonly IDbConnection _dapper;

    public AccountStatisticsRepository(IDbConnection dapper)
    {
        _dapper = dapper;
    }

    public async Task<IEnumerable<AccountStatisticsCountByType>> CountByTypeAsync()
    {
        var sql = @"SELECT * FROM ViewAccountStatisticsCountByType";
        return await _dapper.QueryAsync<AccountStatisticsCountByType>(sql);
    }

    public async Task<IEnumerable<AccountStatisticsCountByStatus>> CountByStatusAsync()
    {
        var sql = "SELECT * FROM ViewAccountStatisticsCountByStatus";
        return await _dapper.QueryAsync<AccountStatisticsCountByStatus>(sql);
    }

    public async Task<IEnumerable<AccountStatisticsTotalBalanceByType>> TotalBalanceByTypeAsync()
    {
        var sql = "SELECT * FROM ViewAccountStatisticsTotalBalanceByType";
        return await _dapper.QueryAsync<AccountStatisticsTotalBalanceByType>(sql);
    }

    public async Task<IEnumerable<AccountStatisticsTotalBalanceByStatus>> TotalBalanceByStatusAsync()
    {
        var sql = "SELECT * FROM ViewAccountStatisticsTotalBalanceByStatus";
        return await _dapper.QueryAsync<AccountStatisticsTotalBalanceByStatus>(sql);
    }
}
