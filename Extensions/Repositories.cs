using BankCoreApi.Repositories.Accounts;
using BankCoreApi.Repositories.Customers;
using BankCoreApi.Repositories.Reports;
using BankCoreApi.Repositories.Transactions;

namespace BankCoreApi.Extensions
{
    public static class Repositories
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<CustomerRepository>();
            services.AddScoped<AccountRepository>();
            services.AddScoped<TransactionRepository>();
            //Reports
            services.AddScoped<CustomersReportRepository>();
            services.AddScoped<AccountsReportRepository>();
            services.AddScoped<TransactionsReportRepository>();
        }
    }
}