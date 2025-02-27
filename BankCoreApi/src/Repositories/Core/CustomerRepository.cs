using System.Data;
using System.Data.SqlClient;
using BankCoreApi.Models;
using BankCoreApi.Models.Customers;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace BankCoreApi.Repositories.Core;

public class CustomerRepository : IRepository<Customer>
{
    private readonly AppDbContext _context;
    private readonly IDbConnection _dapper;

    public CustomerRepository(AppDbContext context, IDbConnection dapper)
    {
        _context = context;
        _dapper = dapper;
    }
    
    public async Task CreateAsync(Customer customer)
    {
       try
       {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
       }
       catch (System.Exception)
       {
            throw;
       }
    }



    public async Task CreateBatchAsync(IEnumerable<Customer> customers)
    {
        using (var transaction = _context.Database.BeginTransaction())
        {
            try
            {
                await _context.Customers.AddRangeAsync(customers);
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

    public async Task DeleteAsync(Customer customer)
    {
        try
        {
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }
        catch (System.Exception)
        {
            throw;
        }
    }

    public async Task<bool> ExistsRecordAsync(string? field, string? value)
    {
        var sql = $"SELECT COUNT(*) FROM Customers WHERE {field} = @Value";
        var count = await _dapper.ExecuteScalarAsync<int>(sql, new { Value = value });
        return count > 0;
    }


    public async Task<IEnumerable<Customer>> GetAllAsync(int limit, int offset)
    {
        return await _context.Customers
            .Skip(offset)
            .Take(limit)
            .ToListAsync();
    }

    public async Task<IEnumerable<CustomerData>> GetAllDataAsync(int limit, int offset)
    {
        var sql = "SELECT * FROM ViewCustomerData ORDER BY CreatedAt DESC " + 
                   $"OFFSET {offset} ROWS FETCH NEXT {limit} ROWS ONLY;";
        return await _dapper.QueryAsync<CustomerData>(sql);
    }

    public async Task<int> GetTotalDataAsync()
    {
        var sql = "SELECT COUNT(*) FROM ViewCustomerData;";
        return await _dapper.ExecuteScalarAsync<int>(sql);
    }

    public async Task<Customer?> GetByIdAsync(int id)
    {
        return await _context.Customers.FindAsync(id);
    }

    public async Task<CustomerData> GetDataByIdAsync(int id)
    {
        var sql = "SELECT * FROM ViewCustomerData WHERE CustomerId = @Id";
        return await _dapper.QueryFirstAsync<CustomerData>(sql, new { @Id = id });
    }

    public async Task<CustomerData> GetDataByUniqueIdAsync(Guid uniqueId)
    {
        var sql = "SELECT * FROM ViewCustomerData WHERE UniqueId = @Id";
        return await _dapper.QueryFirstAsync<CustomerData>(sql, new { @Id = uniqueId });
    }

    public async Task<Customer?> GetByUniqueIdAsync(Guid uniqueId)
    {
        return await _context.Customers
            .FirstOrDefaultAsync(cu => cu.UniqueId == uniqueId);
    }

    public async Task<Customer?> GetByIdentNumberAsync(string? identNumber)
    {
        return await _context.Customers
            .FirstOrDefaultAsync(cu => cu.IdentificationNumber == identNumber);
    }

    public async Task UpdateAsync(Customer customer)
    {
        try
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
        }
        catch (System.Exception)
        {
            throw;
        }
    }
}