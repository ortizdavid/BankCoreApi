using System.Data;
using BankCoreApi.Models.Statistics;
using Dapper;

namespace BankCoreApi.Repositories.Statistics
{
    public class CustomerStatisticsRepository
    {
        private readonly IDbConnection _dapper;

        public CustomerStatisticsRepository(IDbConnection dapper)
        {
            _dapper = dapper;
        }

        public async Task<IEnumerable<CustomerStatisticsCountByType>> CountByTypeAsync(DateTime startDate, DateTime endDate)
        {
            var sql = "SELECT * FROM ViewCustomerStatisticsCountByType WHERE CreatedAt BETWEEN @Start AND @End";
            return await _dapper.QueryAsync<CustomerStatisticsCountByType>(sql, new { Start = startDate, End = endDate });
        }

        public async Task<IEnumerable<CustomerStatisticsCountByStatus>> CountByStatusAsync(DateTime startDate, DateTime endDate)
        {
            var sql = "SELECT * FROM ViewCustomerStatisticsCountByStatus WHERE CreatedAt BETWEEN @Start AND @End";
            return await _dapper.QueryAsync<CustomerStatisticsCountByStatus>(sql, new { Start = startDate, End = endDate });
        }

        public async Task<IEnumerable<CustomerStatisticsCountByGender>> CountByGenderAsync(DateTime startDate, DateTime endDate)
        {
            var sql = "SELECT * FROM ViewCustomerStatisticsCountByGender WHERE CreatedAt BETWEEN @Start AND @End";
            return await _dapper.QueryAsync<CustomerStatisticsCountByGender>(sql, new { Start = startDate, End = endDate });
        }

        public async Task<IEnumerable<CustomerStatisticsCountByAge>> CountByAgeAsync(DateTime startDate, DateTime endDate)
        {
            var sql = "SELECT * FROM ViewCustomerStatisticsCountByAge WHERE CreatedAt BETWEEN @Start AND @End";
            return await _dapper.QueryAsync<CustomerStatisticsCountByAge>(sql, new { Start = startDate, End = endDate });
        }
    }
}