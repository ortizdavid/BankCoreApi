using System.Data;

namespace BankCoreApi.Repositories.Statistics
{
    public class TransactionStatisticsRepository
    {
        private readonly IDbConnection _dapper;

        public TransactionStatisticsRepository(IDbConnection dapper)
        {
            _dapper = dapper;
        }
    }
}