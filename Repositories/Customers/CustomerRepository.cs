using BankCoreApi.Models;
using BankCoreApi.Models.Customers;
using Microsoft.EntityFrameworkCore;

namespace BankCoreApi.Repositories.Customers
{
    public class CustomerRepository : IRepository<Customer>
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
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
            var validFieldNames = new List<string> { "identification_number", "phone", "email"};
            if (!validFieldNames.Contains(field))
            {
                throw new ArgumentException("Invalid field name");
            }
            var sql = $"SELECT 1 FROM customers WHERE {field} = @value LIMIT 1";
            var exists = await _context.Customers
                .FromSqlRaw(sql, new Npgsql.NpgsqlParameter("@value", value))
                .AnyAsync();
            return exists;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _context.Customers.ToListAsync();
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