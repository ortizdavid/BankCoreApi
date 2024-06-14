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
        private readonly CustomerRepository _customerRepository;

        public CustomersController(IConfiguration configuration, ILogger<CustomersController> logger,
            CustomerRepository customerRepository)
        {
            _configuration = configuration;
            _logger = logger;
            _customerRepository = customerRepository;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            var customers = await _customerRepository.GetAllAsync();
            if (!customers.Any())
            {
                return NotFound();
            }
            return Ok(customers);
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