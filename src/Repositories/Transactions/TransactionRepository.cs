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
            var sql = $"SELECT COUNT(*) FROM Transactions WHERE {field} = @Value";
            var count = await _dapper.ExecuteScalarAsync<int>(sql, new { Value = value });
            return count > 0;
        }


        public async Task<IEnumerable<Transaction>> GetAllAsync(int limit, int offset)
        {
            return await _context.Transactions
                .Skip(offset)
                .Take(limit)
                .ToListAsync();
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
        
        public async Task<List<Transaction>> GetAllBySourceAccountIdAsync(int id)
        {
            return await _context.Transactions
                .Where(t => t.SourceId == id)
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

        public async Task<IEnumerable<TransactionData>> GetAllDataAsync(int limit, int offset)
        {
            var sql = "SELECT * FROM ViewTransactionData ORDER BY CreatedAt DESC " +
                $"OFFSET {offset} ROWS FETCH NEXT {limit} ROWS ONLY;";
            return await _dapper.QueryAsync<TransactionData>(sql);
        }

        public async Task<int> GetTotalDataAsync()
        {
            var sql = "SELECT COUNT(*) FROM ViewTransactionData;";
            return await _dapper.ExecuteScalarAsync<int>(sql);
        }

        public async Task<IEnumerable<TransactionData>> GetAllDataByAccountIdAsync(int id, int limit, int offset)
        {
            var sql = "SELECT * FROM ViewTransactionData WHERE SourceAccountId = @SourceAccountId ORDER BY CreatedAt DESC " +
                $"OFFSET {offset} ROWS FETCH NEXT {limit} ROWS ONLY;";
            return await _dapper.QueryAsync<TransactionData>(sql, new { SourceAccountId = id });
        }

        public async Task<IEnumerable<TransactionData>> GetAllDataByAccountUniqueIdAsync(Guid uniqueId, int limit, int offset)
        {
            var sql = "SELECT * FROM ViewTransactionData WHERE UniqueId = @UniqueId ORDER BY CreatedAt DESC " +
                $"OFFSET {offset} ROWS FETCH NEXT {limit} ROWS ONLY;";
            return await _dapper.QueryAsync<TransactionData>(sql, new { UniqueId = uniqueId });
        }
        
        public async Task<TransactionData?> GetDataByIdAsync(int id)
        {
           var sql = "SELECT * FROM ViewTransactionData WHERE TransactionId = @TransactionId;";
            return await _dapper.QueryFirstOrDefaultAsync<TransactionData>(sql, new { TransactionId = id });
        }

    }
}