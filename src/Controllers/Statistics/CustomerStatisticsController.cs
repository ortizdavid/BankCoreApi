using BankCoreApi.Repositories.Statistics;
using Microsoft.AspNetCore.Mvc;

namespace BankCoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerStatisticsController : ControllerBase
    {
        private readonly CustomerStatisticsRepository _repository;

        public CustomerStatisticsController(CustomerStatisticsRepository repository)
        {
            _repository = repository;
        }
    }
}