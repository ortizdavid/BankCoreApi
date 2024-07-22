using BankCoreApi.Services.Core;

namespace BankCoreApi.Extensions
{
    public static class BankServiceExtensions
    {
        public static void AddBankServices(this IServiceCollection services)
        {
            services.AddTransient<CustomerService>();
        }
    }
}