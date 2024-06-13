using BankCoreApi.Models;

namespace BankCoreApi.Repositories.Reports
{
    public class CustomersReportRepository
    {
        private readonly AppDbContext _context;

        public CustomersReportRepository(AppDbContext context)
        {
            _context = context;
        }
        
    }
}