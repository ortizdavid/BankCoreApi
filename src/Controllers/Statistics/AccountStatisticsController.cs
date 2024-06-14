using BankCoreApi.Repositories.Statistics;
using Microsoft.AspNetCore.Mvc;

namespace BankCoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountStatisticsController : ControllerBase
    {
        private readonly AccountStatisticsRepository _repository;
        
        public AccountStatisticsController(AccountStatisticsRepository repository)
        {
            _repository = repository;
        }

    }
}