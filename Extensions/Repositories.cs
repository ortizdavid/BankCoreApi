using BankCoreApi.Repositories.Accounts;
using BankCoreApi.Repositories.Customers;

namespace BankCoreApi.Extensions
{
    public static class Repositories
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<CustomerRepository>();
            services.AddScoped<SavingsAccountRepository>();
            services.AddScoped<CheckingsAccountRepository>();
        
        }
    }
}