using System.Data;
using BankCoreApi.Models;

namespace BankCoreApi.Repositories.Reports
{
    public class CustomersReportRepository
    {
        private readonly IDbConnection _dapper;

        public CustomersReportRepository(IDbConnection dapper)
        {
            _dapper = dapper;
        }
        
    }
}