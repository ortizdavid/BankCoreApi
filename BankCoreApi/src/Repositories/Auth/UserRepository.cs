using System.Data;
using BankCoreApi.Models;
using BankCoreApi.Models.Auth;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace BankCoreApi.Repositories.Auth;

public class UserRepository
{
    private readonly AppDbContext _context;
    private readonly IDbConnection _dapper;

    public UserRepository(AppDbContext context, IDbConnection dapper)
    {
        _context = context;
        _dapper = dapper;
    }

    public async Task CreateAsync(User entity)
    {
       try
       {
            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
       }
       catch (Exception)
       {
            throw;
       }
    }

    public async Task CreateBatchAsync(IEnumerable<User> entities)
    {
        using (var transaction = _context.Database.BeginTransaction())
        {
            try
            {
                await _context.AddRangeAsync(entities);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }

    public async Task DeleteAsync(User entity)
    {
        try
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<bool> ExistsAsync(string? predicate)
    {
        if (predicate == null)
        {
            return false;
        }
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.UserName == predicate);
        return user != null;
    }

    public async Task<IEnumerable<User>> GetAllAsync(int limit, int offset)
    {
        return await _context.Users
            .Skip(offset)
            .Take(limit)
            .ToListAsync();;
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<User?> GetByUserNameAsync(string? userName)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.UserName == userName);
    }

    public async Task<User?> GetByUniqueIdAsync(Guid uniqueId)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.UniqueId == uniqueId);
    }

    public async Task UpdateAsync(User entity)
    {
        try
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<IEnumerable<UserData>> GetAllDataAsync(int limit, int offset)
    {
        var sql = "SELECT * FROM ViewUserData ORDER BY CreatedAt DESC " + 
                   $"OFFSET {offset} ROWS FETCH NEXT {limit} ROWS ONLY;";
        return await _dapper.QueryAsync<UserData>(sql);
    }

    public async Task<int> GetTotalDataAsync()
    {
        var sql = "SELECT COUNT(*) FROM ViewUserData;";
        return await _dapper.ExecuteScalarAsync<int>(sql);
    }
}