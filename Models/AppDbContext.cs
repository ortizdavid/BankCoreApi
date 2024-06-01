using BankCoreApi.Models.Accounts;
using BankCoreApi.Models.Customers;
using Microsoft.EntityFrameworkCore;

namespace BankCoreApi.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<SavingsAccount> SavingsAccounts { get; set; }
        public DbSet<CheckingsAcount> CheckingsAcounts { get; set; }
        public DbSet<CustomerData> CustomerData { get; set; }
        public DbSet<CheckingsAccountData> CheckingsAccountData { get; set; }
        public DbSet<SavingsAccountData> SavingsAccountData { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)  
        {
            // Handle Postgres Timestamp
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true); 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // view_checkings_account_data
            modelBuilder.Entity<CheckingsAccountData>().ToView("view_checkings_account_data");
            modelBuilder.Entity<CheckingsAccountData>().HasNoKey();

            // view_savings_account_data
            modelBuilder.Entity<SavingsAccountData>().ToView("view_savings_account_data");
            modelBuilder.Entity<SavingsAccountData>().HasNoKey();

            // view_customer_data
            modelBuilder.Entity<CustomerData>().ToView("view_customer_data");
            modelBuilder.Entity<CustomerData>().HasNoKey();

        }

    }
}