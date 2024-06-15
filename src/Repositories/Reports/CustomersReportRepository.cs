using System.Data;
using BankCoreApi.Models.Reports;
using Dapper;

namespace BankCoreApi.Repositories.Reports
{
    public class CustomersReportRepository
    {
        private readonly IDbConnection _dapper;

        public CustomersReportRepository(IDbConnection dapper)
        {
            _dapper = dapper;
        }
        
        public async Task<IEnumerable<CustomerReport>> GetAllAsync(DateTime starDate, DateTime endDate)
        {
            var sql = "SELECT * FROM ViewCustomerReport WHERE CreatedAt BETWEEN @Start AND @End";
            return await _dapper.QueryAsync<CustomerReport>(sql, new { Start = starDate, End = endDate });
        }

        public async Task<IEnumerable<CustomerReport>> GetCustomerByTypeAsync(string type, DateTime starDate, DateTime endDate)
        {
            var sql = "SELECT * FROM ViewCustomerReport WHERE CustomerType = @Type AND CreatedAt BETWEEN @Start AND @End";
            return await _dapper.QueryAsync<CustomerReport>(sql, new { @Type = type, Start = starDate, End = endDate });
        }

        public async Task<IEnumerable<CustomerReport>> GetCustomerByStatusAsync(string status, DateTime starDate, DateTime endDate)
        {
            var sql = "SELECT * FROM ViewCustomerReport WHERE CustomerStatus = @Status AND CreatedAt BETWEEN @Start AND @End";
            return await _dapper.QueryAsync<CustomerReport>(sql, new { @Status = status, Start = starDate, End = endDate });
        }

        public async Task<IEnumerable<CustomerReport>> GetCustomerByGenderAsync(string gender, DateTime starDate, DateTime endDate)
        {
            var sql = "SELECT * FROM ViewCustomerReport WHERE Gender = @Gender AND CreatedAt BETWEEN @Start AND @End";
            return await _dapper.QueryAsync<CustomerReport>(sql, new { @Gender = gender, Start = starDate, End = endDate });
        }

    }
}