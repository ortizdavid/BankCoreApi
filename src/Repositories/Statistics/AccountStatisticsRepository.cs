using System.Data;
using BankCoreApi.Models.Statistics;
using Dapper;

namespace BankCoreApi.Repositories.Statistics
{
    public class AccountStatisticsRepository
    {
        private readonly IDbConnection _dapper;

        public AccountStatisticsRepository(IDbConnection dapper)
        {
            _dapper = dapper;
        }

        public async Task<IEnumerable<AccountStatisticsCountByType>> CountByTypeAsync(DateTime startDate, DateTime endDate)
        {
            var sql = "SELECT * FROM ViewAccountStatisticsCountByType WHERE CreatedAt BETWEEN @Start AND @End";
            return await _dapper.QueryAsync<AccountStatisticsCountByType>(sql, new { Start = startDate, End = endDate });
        }

        public async Task<IEnumerable<AccountStatisticsCountByStatus>> CountByStatusAsync(DateTime startDate, DateTime endDate)
        {
            var sql = "SELECT * FROM ViewAccountStatisticsCountByStatus WHERE CreatedAt BETWEEN @Start AND @End";
            return await _dapper.QueryAsync<AccountStatisticsCountByStatus>(sql, new { Start = startDate, End = endDate });
        }

        public async Task<IEnumerable<AccountStatisticsTotalBalanceByType>> TotalBalanceByTypeAsync(DateTime startDate, DateTime endDate)
        {
            var sql = "SELECT * FROM ViewAccountStatisticsTotalBalanceByType WHERE CreatedAt BETWEEN @Start AND @End";
            return await _dapper.QueryAsync<AccountStatisticsTotalBalanceByType>(sql, new { Start = startDate, End = endDate });
        }

        public async Task<IEnumerable<AccountStatisticsTotalBalanceByStatus>> TotalBalanceByStatusAsync(DateTime startDate, DateTime endDate)
        {
            var sql = "SELECT * FROM ViewAccountStatisticsTotalBalanceByStatus WHERE CreatedAt BETWEEN @Start AND @End";
            return await _dapper.QueryAsync<AccountStatisticsTotalBalanceByStatus>(sql, new { Start = startDate, End = endDate });
        }
    }
}
