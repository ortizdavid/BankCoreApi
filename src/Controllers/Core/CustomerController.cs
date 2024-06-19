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


        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerRequest request)
        {
            if (request == null)
            {
                return BadRequest();
            }
            try
            {
                if (!Enum.IsDefined(typeof(CustomerType), request.CustomerType))
                {
                    return BadRequest($"Customer type does not exists.");
                }
                if (await _customerRepository.ExistsRecordAsync("IdentificationNumber", request.IdentificationNumber))
                {
                    return Conflict($"Identification '{request.IdentificationNumber}' already exists.");
                }
                if (await _customerRepository.ExistsRecordAsync("Phone", request.Phone))
                {
                    return Conflict($"Phone '{request.Phone}' already exists.");
                }
                if (await _customerRepository.ExistsRecordAsync("Email", request.Email))
                {
                    return Conflict($"Email '{request.Phone}' already exists.");
                }
                var customer = new Customer()
                {
                    CustomerType = request.CustomerType,
                    CustomerStatus = CustomerStatus.Pending,
                    CustomerName = request.CustomerName,
                    Gender = request.Gender,
                    BirthDate = request.BirthDate,
                    IdentificationNumber = request.IdentificationNumber,
                    Phone = request.Phone,
                    Email = request.Email,
                    Address = request.Address
                };
                await _customerRepository.CreateAsync(customer);
                var message = $"Customer '{customer.CustomerName}' Created.";
                _logger.LogInformation(message);
                return StatusCode(500, message);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetAllCustomers(int pageIndex = 0, int pageSize = 10)
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
            var customer = await _customerRepository.GetDataByIdAsync(id);
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


        [HttpPut("change-status")]
        public async Task<IActionResult> ChangeCustomerStatus([FromBody] ChangeCustomerStatusRequest request)
        {
            if (request == null)
            {
                return BadRequest();
            }
            try
            {
                var customer = await _customerRepository.GetByIdentNumberAsync(request.IdentificationNumber);
                if (customer == null)
                {
                    return NotFound("Invalid customer");
                }
                var currentStatus = customer.CustomerStatus.ToString();
                if (request.NewStatus == customer.CustomerStatus)
                {
                    return Conflict($"Customer Status '{currentStatus}' is already set");
                }
                if (!Enum.IsDefined(typeof(CustomerStatus), request.NewStatus))
                {
                    return BadRequest("Invalid Customer Status.");
                }
                customer.CustomerStatus = request.NewStatus;
                customer.UpdatedAt = DateTime.UtcNow;
                await _customerRepository.UpdateAsync(customer);
                var message = $"Customer '{customer.IdentificationNumber}' Status changed from '{currentStatus}' to '{request.NewStatus.ToString()}'";
                _logger.LogInformation(message);
                return Ok(message);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("change-type")]
        public async Task<IActionResult> ChangeCustomerType([FromBody] ChangeCustomerTypeRequest request)
        {
            if (request == null)
            {
                return BadRequest();
            }
            try
            {
                var customer = await _customerRepository.GetByIdentNumberAsync(request.IdentificationNumber);
                if (customer == null)
                {
                    return NotFound("Invalid customer");
                }
                var currentType = customer.CustomerType.ToString();
                if (request.NewType == customer.CustomerType)
                {
                    return Conflict($"Customer Type '{currentType}' is already set");
                }
                if (!Enum.IsDefined(typeof(CustomerType), request.NewType))
                {
                    return BadRequest("Invalid Customer Type.");
                }
                customer.CustomerType = request.NewType;
                customer.UpdatedAt = DateTime.UtcNow;
                await _customerRepository.UpdateAsync(customer);
                var message = $"Customer '{customer.IdentificationNumber}' Type changed from '{currentType}' to '{request.NewType.ToString()}'";
                _logger.LogInformation(message);
                return Ok(message);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }


    }
}