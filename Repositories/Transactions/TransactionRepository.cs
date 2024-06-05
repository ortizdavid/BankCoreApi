using System.Data;
using System.Data.SqlClient;
using BankCoreApi.Models;
using BankCoreApi.Models.Transactions;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace BankCoreApi.Repositories.Transactions
{
    public class TransactionRepository : IRepository<Transaction>
    {
        private readonly AppDbContext _context;
        private readonly IDbConnection _dapper;
        
        public TransactionRepository(AppDbContext context, IDbConnection dapper)
        {
            _context = context;
            _dapper = dapper;
        }

        public async Task CreateAsync(Transaction transaction)
        {
            try
            {
                await _context.Transactions.AddAsync(transaction);
                await _context.SaveChangesAsync();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task CreateBatchAsync(IEnumerable<Transaction> transactions)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    await _context.Transactions.AddRangeAsync(transactions);
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (System.Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task DeleteAsync(Transaction transaction)
        {
            try
            {
                _context.Transactions.Remove(transaction);
                await _context.SaveChangesAsync();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<bool> ExistsRecordAsync(string? field, string? value)
        {
            if (string.IsNullOrEmpty(field) || string.IsNullOrEmpty(value))
            {
                return false;
            }

            var validFieldNames = new List<string> { "transaction_id", "code" };
            if (!validFieldNames.Contains(field.ToLower()))
            {
                throw new ArgumentException("Invalid field name");
            }

            var sql = $"SELECT TOP 1 1 FROM Transactions WHERE {field} = @Value";

            await using var conn = new SqlConnection(_context.Database.GetDbConnection().ConnectionString);
            await conn.OpenAsync();

            var result = await conn.ExecuteScalarAsync<int>(sql, new { Value = value });
            return result == 1;
        }
        

        public async Task<IEnumerable<Transaction>> GetAllAsync()
        {
            return await _context.Transactions.ToListAsync();
        }

        public async Task<Transaction?> GetByIdAsync(int id)
        {
            return await _context.Transactions.FindAsync(id);
        }

        public Task<Transaction?> GetByUniqueIdAsync(Guid uniqueId)
        {
            return _context.Transactions
                .FirstOrDefaultAsync(t => t.UniqueId == uniqueId);
        }

        public Task<Transaction?> GetByCodeAsync(string? code)
        {
            return _context.Transactions
                .FirstOrDefaultAsync(t => t.Code == code);
        }

        
        public async Task<List<Transaction>> GetAllByAccountIdAsync(int id)
        {
            return await _context.Transactions
                .Where(t => t.AccountId == id)
                .ToListAsync();
        }

        public async Task UpdateAsync(Transaction transaction)
        {
            try
            {
                _context.Transactions.Update(transaction);
                await _context.SaveChangesAsync();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<TransactionData>> GetAllDataAsync()
        {
            var sql = "SELECT * FROM ViewTransactionData ORDER BY CreatedAt DESC;";
            return await _dapper.QueryAsync<TransactionData>(sql);
        }


        public async Task<IEnumerable<TransactionData>> GetAllDataByAccountIdAsync(int id)
        {
            var sql = "SELECT * FROM ViewTransactionData WHERE AccountId = @AccountId ORDER BY CreatedAt DESC;";
            return await _dapper.QueryAsync<TransactionData>(sql, new { AccountId = id });
        }


        public async Task<IEnumerable<TransactionData>> GetAllDataByAccountUniqueIdAsync(string uniqueId)
        {
            var sql = "SELECT * FROM ViewTransactionData WHERE UniqueId = @UniqueId ORDER BY CreatedAt DESC;";
            return await _dapper.QueryAsync<TransactionData>(sql, new { UniqueId = uniqueId });
        }
        

        public async Task<TransactionData?> GetDataByIdAsync(int id)
        {
           var sql = "SELECT * FROM ViewTransactionData WHERE TransactionId = @TransactionId;";
            return await _dapper.QueryFirstOrDefaultAsync<TransactionData>(sql, new { TransactionId = id });
        }


    }
}