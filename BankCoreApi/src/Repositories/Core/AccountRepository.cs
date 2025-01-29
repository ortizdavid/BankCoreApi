using System.Data;
using System.Data.SqlClient;
using BankCoreApi.Models;
using BankCoreApi.Models.Accounts;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace BankCoreApi.Repositories.Core;

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
        var sql = $"SELECT COUNT(*) FROM Accounts WHERE {field} = @Value";
        var count = await _dapper.ExecuteScalarAsync<int>(sql, new { Value = value });
        return count > 0;
    }


    public async Task<IEnumerable<Account>> GetAllAsync(int limit, int offset)
    {
        return await _context.Accounts
            .Skip(offset)
            .Take(limit)
            .ToListAsync();
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

    public async Task<IEnumerable<AccountData>> GetAllDataAsync(int limit, int offset)
    {
        var sql = "SELECT * FROM ViewAccountData ORDER BY CreatedAt DESC "+
                $"OFFSET {offset} ROWS FETCH NEXT {limit} ROWS ONLY;";
        return await _dapper.QueryAsync<AccountData>(sql);
    }

    public async Task<int> GetTotalDataAsync()
    {
        var sql = "SELECT COUNT(*) FROM ViewAccountData;";
        return await _dapper.ExecuteScalarAsync<int>(sql);
    }

    public async Task<AccountData?> GetDataByIdAsync(int id)
    {
       var sql = "SELECT * FROM ViewTransactionData WHERE AccountId = @AccountId;";
        return await _dapper.QueryFirstOrDefaultAsync<AccountData>(sql, new { AccountId = id });
    }

    public async Task<AccountData?> GetDataByUniqueIdAsync(Guid unqiueId)
    {
       var sql = "SELECT * FROM ViewAccountData WHERE UniqueId = @UnqiueId;";
        return await _dapper.QueryFirstOrDefaultAsync<AccountData>(sql, new { UniqueId = unqiueId });
    }

    public async Task<bool> ExistsAsync(int accountId)
    {
        var sql = "SELECT COUNT(*) FROM Accounts WHERE AccountId = @AccountId";
        var count = await _dapper.ExecuteScalarAsync<int>(sql, new { AccountId = accountId });
        return count > 0;
    }

}