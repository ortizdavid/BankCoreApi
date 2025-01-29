using System.Data;
using BankCoreApi.Models.Statistics;
using Dapper;

namespace BankCoreApi.Repositories.Statistics;

public class CustomerStatisticsRepository
{
    private readonly IDbConnection _dapper;

    public CustomerStatisticsRepository(IDbConnection dapper)
    {
        _dapper = dapper;
    }

    public async Task<IEnumerable<CustomerStatisticsCountByType>> CountByTypeAsync()
    {
        var sql = "SELECT * FROM ViewCustomerStatisticsCountByType";
        return await _dapper.QueryAsync<CustomerStatisticsCountByType>(sql);
    }

    public async Task<IEnumerable<CustomerStatisticsCountByStatus>> CountByStatusAsync()
    {
        var sql = "SELECT * FROM ViewCustomerStatisticsCountByStatus";
        return await _dapper.QueryAsync<CustomerStatisticsCountByStatus>(sql);
    }

    public async Task<IEnumerable<CustomerStatisticsCountByGender>> CountByGenderAsync()
    {
        var sql = "SELECT * FROM ViewCustomerStatisticsCountByGender";
        return await _dapper.QueryAsync<CustomerStatisticsCountByGender>(sql);
    }

    public async Task<IEnumerable<CustomerStatisticsCountByAge>> CountByAgeAsync()
    {
        var sql = "SELECT * FROM ViewCustomerStatisticsCountByAge";
        return await _dapper.QueryAsync<CustomerStatisticsCountByAge>(sql);
    }
}