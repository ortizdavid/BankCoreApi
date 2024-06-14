using System.Data;

namespace BankCoreApi.Repositories.Statistics
{
    public class AccountStatisticsRepository
    {
        private readonly IDbConnection _dapper;

        public AccountStatisticsRepository(IDbConnection dapper)
        {
            _dapper = dapper;
        }
        
    }
}