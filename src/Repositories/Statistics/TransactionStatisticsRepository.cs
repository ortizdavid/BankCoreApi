using System.Data;
using BankCoreApi.Models.Statistics;
using Dapper;

namespace BankCoreApi.Repositories.Statistics
{
    public class TransactionStatisticsRepository
    {
        private readonly IDbConnection _dapper;

        public TransactionStatisticsRepository(IDbConnection dapper)
        {
            _dapper = dapper;
        }

        public async Task<IEnumerable<TransactionStatisticsCountByType>> CountByTypeAsync(DateTime startDate, DateTime endDate)
        {
            var sql = "SELECT * FROM ViewTransactionStatisticsCountByType WHERE CreatedAt BETWEEN @Start AND @End";
            return await _dapper.QueryAsync<TransactionStatisticsCountByType>(sql, new { Start = startDate, End = endDate });
        }

        public async Task<IEnumerable<TransactionStatisticsCountByStatus>> CountByStatusAsync(DateTime startDate, DateTime endDate)
        {
            var sql = "SELECT * FROM ViewTransactionStatisticsCountByStatus WHERE CreatedAt BETWEEN @Start AND @End";
            return await _dapper.QueryAsync<TransactionStatisticsCountByStatus>(sql, new { Start = startDate, End = endDate });
        }

        public async Task<IEnumerable<TransactionStatisticsTotalAmountByType>> TotalAmountByType(DateTime startDate, DateTime endDate)
        {
            var sql = "SELECT * FROM ViewTransactionStatisticsTotalAmountByType WHERE CreatedAt BETWEEN @Start AND @End";
            return await _dapper.QueryAsync<TransactionStatisticsTotalAmountByType>(sql, new { Start = startDate, End = endDate });
        }

        public async Task<IEnumerable<TransactionStatisticsTotalAmountByStatus>> TotalAmountByStatus(DateTime startDate, DateTime endDate)
        {
            var sql = "SELECT * FROM ViewTransactionStatisticsTotalAmountByStatus WHERE CreatedAt BETWEEN @Start AND @End";
            return await _dapper.QueryAsync<TransactionStatisticsTotalAmountByStatus>(sql, new { Start = startDate, End = endDate });
        }
    }
}