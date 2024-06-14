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

        public async Task<IEnumerable<TransactionReport>> GetTransactionsByDate(DateTime date)
        {
            var sql = "SELECT * FROM ViewTransactionReport WHERE CAST(TransactionDate AS DATE) = CAST(@Date AS DATE)";
            return await _dapper.QueryAsync<TransactionReport>(sql, new { Date = date });
        }

        public async Task<IEnumerable<TransactionReport>> GetTransactionsByType(int typeId, DateTime startDate, DateTime endDate)
        {
           var sql = "SELECT * FROM ViewTransactionReport WHERE AccountType = @Id AND TransactionDate BETWEEN @Start AND @End";
            return await _dapper.QueryAsync<TransactionReport>(sql, new { Id = typeId, Start = startDate, End = endDate });
        }

        public async Task<IEnumerable<TransactionReport>> GetTransactionsByStatus(int statusId, DateTime startDate, DateTime endDate)
        {
           var sql = "SELECT * FROM ViewTransactionReport WHERE AccountStatus = @Id AND TransactionDate BETWEEN @Start AND @End";
            return await _dapper.QueryAsync<TransactionReport>(sql, new { Id = statusId, Start = startDate, End = endDate });
        }

        public async Task<IEnumerable<TransactionReport>> GetAccountTransactions(int accountId, DateTime startDate, DateTime endDate)
        {
            var sql = "SELECT * FROM ViewTransactionReport WHERE SourceAccountId = @Id AND TransactionDate BETWEEN @Start AND @End";
            return await _dapper.QueryAsync<TransactionReport>(sql, new { Id = accountId, Start = startDate, End = endDate });
        }

        public async Task<IEnumerable<TransactionReport>> GetAccountTransactionsByDate(int accountId, DateTime date)
        {
            var sql = "SELECT * FROM ViewTransactionReport WHERE SourceAccountId = @Id AND CAST(TransactionDate AS DATE) = CAST(@Date AS DATE)";
            return await _dapper.QueryAsync<TransactionReport>(sql, new { Id = accountId, Date = date });
        }

        public async Task<IEnumerable<TransactionReport>> GetAccountTransactionsByType(int accountId, int typeId, DateTime startDate, DateTime endDate)
        {
            var sql = "SELECT * FROM ViewTransactionReport WHERE SourceAccountId = @Id AND AccountType = @Type AND TransactionDate BETWEEN @Start AND @End";
            return await _dapper.QueryAsync<TransactionReport>(sql, new { Id = accountId, Type = typeId, Start = startDate, End = endDate });
        }

        public async Task<IEnumerable<TransactionReport>> GetAccountTransactionsByStatus(int accountId, int statusId, DateTime startDate, DateTime endDate)
        {
            var sql = "SELECT * FROM ViewTransactionReport WHERE SourceAccountId = @Id AND AccountStatus = @Status AND TransactionDate BETWEEN @Start AND @End";
            return await _dapper.QueryAsync<TransactionReport>(sql, new { Id = accountId, Status = statusId, Start = startDate, End = endDate });
        }

        public async Task<IEnumerable<TransactionReport>> GetAccountTransactionsByCustomer(int customerId, int accountId, DateTime startDate, DateTime endDate)
        {
            var sql = "SELECT * FROM ViewTransactionReport WHERE CustomerId = @Customer AND SourceAccountId = @Account AND TransactionDate BETWEEN @Start AND @End";
            return await _dapper.QueryAsync<TransactionReport>(sql, new { Customer = customerId, Account = accountId, Start = startDate, End = endDate });
        }

    }
}