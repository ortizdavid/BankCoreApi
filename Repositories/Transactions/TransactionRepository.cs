using BankCoreApi.Models;
using BankCoreApi.Models.Transactions;
using Microsoft.EntityFrameworkCore;

namespace BankCoreApi.Repositories.Transactions
{
    public class TransactionRepository : IRepository<Transaction>
    {
        private readonly AppDbContext _context;
        
        public TransactionRepository(AppDbContext context)
        {
            _context = context;
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
            if (!validFieldNames.Contains(field))
            {
                throw new ArgumentException("Invalid field name");
            }
            var sql = $"SELECT 1 FROM customers WHERE {field} = @value LIMIT 1";
            var exists = await _context.Transactions
                .FromSqlRaw(sql, new Npgsql.NpgsqlParameter("@value", value))
                .AnyAsync();
            return exists;;
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
    }
}