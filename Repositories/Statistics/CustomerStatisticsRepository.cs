using System.Data;

namespace BankCoreApi.Repositories.Statistics
{
    public class CustomerStatisticsRepository
    {
        private readonly IDbConnection _dapper;

        public CustomerStatisticsRepository(IDbConnection dapper)
        {
            _dapper = dapper;
        }
    }
}