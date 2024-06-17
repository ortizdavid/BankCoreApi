using System.Data;
using System.Data.SqlClient;
using BankCoreApi.Models;
using BankCoreApi.Models.Customers;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace BankCoreApi.Repositories.Customers
{
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

        public async Task<int> CreateAsyncV2(Customer customer)
        {
           try
           {
                await _context.Customers.AddAsync(customer);
                await _context.SaveChangesAsync();
                return customer.CustomerId;
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
            if (string.IsNullOrEmpty(field) || string.IsNullOrEmpty(value))
            {
                return false;
            }
            var validFieldNames = new[] { "IdentificationNumber", "Phone", "Email" };
            if (!validFieldNames.Contains(field))
            {
                throw new ArgumentException($"Invalid field name {field}");
            }
            var sql = $"SELECT COUNT(*) FROM Customers WHERE {field} = @Value";
            var count = await _dapper.ExecuteScalarAsync<int>(sql, new { Value = value });
            return count > 0;
        }


        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<IEnumerable<CustomerData>> GetAllDataAsync()
        {
            var sql = "SELECT * FROM ViewCustomerData ORDER BY CreatedAt DESC;";
            return await _dapper.QueryAsync<CustomerData>(sql);
        }

        public async Task<Customer?> GetByIdAsync(int id)
        {
            return await _context.Customers.FindAsync(id);
        }

        public async Task<Customer?> GetByUniqueIdAsync(Guid uniqueId)
        {
            return await _context.Customers
                .FirstOrDefaultAsync(cu => cu.UniqueId == uniqueId);
        }

        public async Task UpdateAsync(Customer customer)
        {
            try
            {
                await _context.Customers.AddAsync(customer);
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
}