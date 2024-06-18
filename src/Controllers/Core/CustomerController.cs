using BankCoreApi.Helpers;
using BankCoreApi.Models.Customers;
using BankCoreApi.Repositories.Customers;
using Microsoft.AspNetCore.Mvc;

namespace BankCoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<CustomersController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly CustomerRepository _customerRepository;

        public CustomersController(IConfiguration configuration, ILogger<CustomersController> logger,
            CustomerRepository customerRepository, IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _logger = logger;
            _customerRepository = customerRepository;
            _httpContextAccessor = httpContextAccessor;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllCustomers(int pageIndex = 1, int pageSize = 10)
        {
            try
            {
                var count = await _customerRepository.GetTotalDataAsync();
                if (count == 0)
                {
                    return NotFound("No customers found.");
                }
                var customers = await _customerRepository.GetAllDataAsync(pageSize, pageIndex);
                var pagination = new Pagination<CustomerData>(customers, count, pageIndex, pageSize, _httpContextAccessor);
                return Ok(pagination);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }


        [HttpGet("by-uuid/{uniqueId}")]
        public async Task<IActionResult> GetCustomerByUniqueId(Guid uniqueId)
        {
            var customer = await _customerRepository.GetByUniqueIdAsync(uniqueId);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] Customer customer)
        {
            if (customer == null)
            {
                return BadRequest();
            }
            await _customerRepository.CreateAsync(customer);
            return Ok(customer);
        }


    }
}