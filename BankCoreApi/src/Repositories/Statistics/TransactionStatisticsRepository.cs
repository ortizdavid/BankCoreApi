using System.Data;
using BankCoreApi.Models.Statistics;
using Dapper;

namespace BankCoreApi.Repositories.Statistics;

public class TransactionStatisticsRepository
{
    private readonly IDbConnection _dapper;

    public TransactionStatisticsRepository(IDbConnection dapper)
    {
        _dapper = dapper;
    }

    public async Task<IEnumerable<TransactionStatisticsCountByType>> CountByTypeAsync()
    {
        var sql = "SELECT * FROM ViewTransactionStatisticsCountByType";
        return await _dapper.QueryAsync<TransactionStatisticsCountByType>(sql);
    }

    public async Task<IEnumerable<TransactionStatisticsCountByStatus>> CountByStatusAsync()
    {
        var sql = "SELECT * FROM ViewTransactionStatisticsCountByStatus";
        return await _dapper.QueryAsync<TransactionStatisticsCountByStatus>(sql);
    }

    public async Task<IEnumerable<TransactionStatisticsTotalAmountByType>> TotalAmountByType()
    {
        var sql = "SELECT * FROM ViewTransactionStatisticsTotalAmountByType";
        return await _dapper.QueryAsync<TransactionStatisticsTotalAmountByType>(sql);
    }

    public async Task<IEnumerable<TransactionStatisticsTotalAmountByStatus>> TotalAmountByStatus()
    {
        var sql = "SELECT * FROM ViewTransactionStatisticsTotalAmountByStatus";
        return await _dapper.QueryAsync<TransactionStatisticsTotalAmountByStatus>(sql);
    }
}