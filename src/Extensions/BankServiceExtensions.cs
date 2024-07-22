using BankCoreApi.Services.Core;
using BankCoreApi.Services.Reports;
using BankCoreApi.Services.Statistics;

namespace BankCoreApi.Extensions
{
    public static class BankServiceExtensions
    {
        public static void AddBankServices(this IServiceCollection services)
        {
            services.AddTransient<CustomerService>();
            services.AddTransient<AccountService>();
            services.AddTransient<TransactionService>();

            services.AddTransient<CustomerReportService>();
            services.AddTransient<AccountReportService>();
            services.AddTransient<TransactionReportService>();

            services.AddTransient<CustomerStatisticsService>();
            services.AddTransient<AccountStatisticsService>();
            services.AddTransient<TransactionStatisticsService>();
        }
    }
}