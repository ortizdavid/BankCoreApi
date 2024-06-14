using System.Data;
using BankCoreApi.Models;

namespace BankCoreApi.Repositories.Reports
{
    public class AccountsReportRepository
    {
         private readonly IDbConnection _dapper;

        public AccountsReportRepository(IDbConnection dapper)
        {
            _dapper = dapper;
        }
    }
}