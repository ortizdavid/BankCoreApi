using BankCoreApi.Models;

namespace BankCoreApi.Repositories.Reports
{
    public class AccountsReportRepository
    {
        private readonly AppDbContext _context;

        public AccountsReportRepository(AppDbContext context)
        {
            _context = context;
        }
    }
}