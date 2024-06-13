using System.Data;
using BankCoreApi.Models.Reports;
using Dapper;
using Microsoft.Identity.Client;

namespace BankCoreApi.Repositories.Reports
{
    public class TransactionsReportRepository
    {
        private readonly IDbConnection _dapper;

        public TransactionsReportRepository(IDbConnection dapper)
        {
            _dapper = dapper;
        }

        public async Task<IEnumerable<TransactionReport>> GetTransactions(DateTime startDate, DateTime endDate)
        {
            var sql = "SELECT * FROM ViewTransactionReport WHERE TransactionDate BETWEEN @Start AND @End";
            return await _dapper.QueryAsync<TransactionReport>(sql, new { Start = startDate, End = endDate });
        }

        public async Task<IEnumerable<TransactionReport>> GetAccountTransactions(int accountId, DateTime startDate, DateTime endDate)
        {
            var sql = "SELECT * FROM ViewTransactionReport WHERE AccountId = @Id AND TransactionDate BETWEEN @Start AND @End";
            return await _dapper.QueryAsync<TransactionReport>(sql, new { Id = accountId, Start = startDate, End = endDate });
        }

        public async Task<IEnumerable<TransactionReport>> GetTransactionsByDate(DateTime date)
        {
            var sql = "SELECT * FROM ViewTransactionReport WHERE CAST(TransactionDate AS DATE) = CAST(@Date AS DATE)";
            return await _dapper.QueryAsync<TransactionReport>(sql, new { Date = date });
        }

        public async Task<IEnumerable<TransactionReport>> GetAccountTransactionsByDate(int accountId, DateTime date)
        {
            var sql = "SELECT * FROM ViewTransactionReport WHERE AccountId = @Id AND CAST(TransactionDate AS DATE) = CAST(@Date AS DATE)";
            return await _dapper.QueryAsync<TransactionReport>(sql, new { Id = accountId, Date = date });
        }

    }
}