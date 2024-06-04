using BankCoreApi.Models.Accounts;
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
        public DbSet<CustomerData> CustomerData { get; set; }
        public DbSet<TransactionData> TransactionData { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)  
        {
            // Handle Postgres Timestamp
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true); 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // view_account_data
            modelBuilder.Entity<AccountData>().ToView("view_account_data");
            modelBuilder.Entity<AccountData>().HasNoKey();

            // view_transaction_data
            modelBuilder.Entity<TransactionData>().ToView("view_transaction_data");
            modelBuilder.Entity<TransactionData>().HasNoKey();

            // view_customer_data
            modelBuilder.Entity<CustomerData>().ToView("view_customer_data");
            modelBuilder.Entity<CustomerData>().HasNoKey();

            modelBuilder.Entity<Customer>()
                .Property(c => c.UniqueId)
                .HasColumnName("unique_id");

        }

    }
}