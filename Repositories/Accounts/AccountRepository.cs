using System.Data;
using System.Data.SqlClient;
using BankCoreApi.Models;
using BankCoreApi.Models.Accounts;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace BankCoreApi.Repositories.Accounts
{
    public class AccountRepository : IRepository<Account>, IAccountRepository<Account>
    {
        private readonly AppDbContext _context;
        private readonly IDbConnection _dapper;

        public AccountRepository(AppDbContext context, IDbConnection dapper)
        {
            _context = context;
            _dapper = dapper;
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

            var validFieldNames = new[] { "Iban", "AccountNumber" };
            if (!validFieldNames.Contains(field))
            {
                throw new ArgumentException($"Invalid field name {field}");
            }

            var sql = $"SELECT TOP 1 1 FROM Accounts WHERE {field} = @Value";

            await using var conn = new SqlConnection(_context.Database.GetDbConnection().ConnectionString);
            await conn.OpenAsync();

            var result = await conn.ExecuteScalarAsync<int>(sql, new { Value = value });
            return result == 1;
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

        public async Task<IEnumerable<AccountData>> GetAllDataAsync()
        {
            var sql = "SELECT * FROM ViewAccountData ORDER BY CreatedAt DESC;";
            return await _dapper.QueryAsync<AccountData>(sql);
        }

        public async Task<AccountData?> GetDataByIdAsync(int id)
        {
           var sql = "SELECT * FROM ViewTransactionData WHERE AccountId = @AccountId;";
            return await _dapper.QueryFirstOrDefaultAsync<AccountData>(sql, new { AccountId = id });
        }

        public async Task<AccountData?> GetDataByUniqueIdAsync(string unqiueId)
        {
           var sql = "SELECT * FROM ViewAccountData WHERE UniqueId = @UnqiueId;";
            return await _dapper.QueryFirstOrDefaultAsync<AccountData>(sql, new { UniqueId = unqiueId });
        }

    }
}