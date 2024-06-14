using BankCoreApi.Repositories.Statistics;
using Microsoft.AspNetCore.Mvc;

namespace BankCoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionStatisticsController : ControllerBase
    {
        private readonly TransactionStatisticsRepository _repository;
        
        public TransactionStatisticsController(TransactionStatisticsRepository repository)
        {
            _repository = repository;
        }
    }
}