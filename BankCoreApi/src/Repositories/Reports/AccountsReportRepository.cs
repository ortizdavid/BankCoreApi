using System.Data;
using BankCoreApi.Models.Reports;
using Dapper;

namespace BankCoreApi.Repositories.Reports;

public class AccountsReportRepository
{
    private readonly IDbConnection _dapper;

    public AccountsReportRepository(IDbConnection dapper)
    {
        _dapper = dapper;
    }

    public async Task<IEnumerable<AccountReport>> GetAllAsync(DateTime starDate, DateTime endDate)
    {
        var sql = "SELECT * FROM ViewAccountReport WHERE CreatedAt BETWEEN @Start AND @End";
        return await _dapper.QueryAsync<AccountReport>(sql, new { Start = starDate, End = endDate });
    }

    public async Task<IEnumerable<AccountReport>> GetByAccountStatusAsync(string status, DateTime starDate, DateTime endDate)
    {
        var sql = "SELECT * FROM ViewAccountReport WHERE AccountStatus = @Status AND CreatedAt BETWEEN @Start AND @End";
        return await _dapper.QueryAsync<AccountReport>(sql, new { Status = status, Start = starDate, End = endDate });
    }

    public async Task<IEnumerable<AccountReport>> GetByAccountTypeAsync(string type, DateTime starDate, DateTime endDate)
    {
        var sql = "SELECT * FROM ViewAccountReport WHERE AccountType = @Type AND CreatedAt BETWEEN @Start AND @End";
        return await _dapper.QueryAsync<AccountReport>(sql, new { Type = type, Start = starDate, End = endDate });
    }

    public async Task<IEnumerable<AccountReport>> GetByCustomerAsync(int customerId, DateTime starDate, DateTime endDate)
    {
        var sql = "SELECT * FROM ViewAccountReport WHERE CustomerId = @CustomerId AND CreatedAt BETWEEN @Start AND @End";
        return await _dapper.QueryAsync<AccountReport>(sql, new { CustomerId = customerId, Start = starDate, End = endDate });
    }
}