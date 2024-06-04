using BankCoreApi.Models;
using BankCoreApi.Models.Accounts;
using Microsoft.EntityFrameworkCore;

namespace BankCoreApi.Repositories.Accounts
{
    public class AccountRepository : IRepository<Account>, IAccountRepository<Account>
    {
        private readonly AppDbContext _context;

        public AccountRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task ChangeStatus(Account account, AccountStatus newStatus)
        {
            try
            {
                account.AccountStatus = newStatus;
                await UpdateAsync(account);
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task CreateAsync(Account account)
        {
            try
            {
                await _context.Accounts.AddAsync(account);
                await _context.SaveChangesAsync();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public async Task CreateBatchAsync(IEnumerable<Account> accounts)
        {
        
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                { 
                    await _context.Accounts.AddRangeAsync(accounts);
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

        public async Task DeleteAsync(Account account)
        {
            try
            {
                _context.Accounts.Remove(account);
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
            var validFieldNames = new List<string> { "iban", "account_number"};
            if (!validFieldNames.Contains(field))
            {
                throw new ArgumentException($"Invalid field name {field}");
            }
            var sql = $"SELECT 1 FROM accounts WHERE {field} = @value LIMIT 1";
            var exists = await _context.Accounts
                .FromSqlRaw(sql, new Npgsql.NpgsqlParameter("@value", value))
                .AnyAsync();
            return exists;
        }

        public async Task<IEnumerable<Account>> GetAllAsync()
        {
            return await _context.Accounts.ToListAsync();
        }

        public async Task<Account?> GetByIbanAsync(string? iban)
        {
            return await _context.Accounts
                .FirstOrDefaultAsync(acc => acc.Iban == iban);
        }

        public async Task<Account?> GetByIdAsync(int id)
        {
            return await _context.Accounts.FindAsync(id);
        }

        public async Task<Account?> GetByNubmerAsync(string? number)
        {
            return await _context.Accounts
                .FirstOrDefaultAsync(acc => acc.AccountNumber == number);
        }

        public async Task<Account?> GetByUniqueIdAsync(Guid uniqueId)
        {
            return await _context.Accounts
                .FirstOrDefaultAsync(acc => acc.UniqueId == uniqueId);
        }

        public async Task UpdateAsync(Account account)
        {
            try
            {
                _context.Accounts.Update(account);
                await _context.SaveChangesAsync();
            }
            catch (System.Exception)
            {
                throw;
            }
        }

    }
}