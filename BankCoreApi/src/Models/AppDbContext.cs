using BankCoreApi.Models.Accounts;
using BankCoreApi.Models.Auth;
using BankCoreApi.Models.Customers;
using BankCoreApi.Models.Transactions;
using Microsoft.EntityFrameworkCore;

namespace BankCoreApi.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)  
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Customer>().Property(c => c.UniqueId).HasColumnName("UniqueId");
            
            modelBuilder.Entity<Account>().Property(acc => acc.UniqueId).HasColumnName("UniqueId");
            modelBuilder.Entity<Account>().Property(a => a.Balance).HasColumnType("decimal(18, 2)"); 

            modelBuilder.Entity<Transaction>().Property(t => t.Amount).HasColumnType("decimal(18, 2)"); 
            modelBuilder.Entity<Transaction>().Property(t => t.BalanceBefore).HasColumnType("decimal(18, 2)"); 
            modelBuilder.Entity<Transaction>().Property(t => t.BalanceAfter).HasColumnType("decimal(18, 2)");
        }

    }
}