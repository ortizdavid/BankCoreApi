using BankCoreApi.Models.Accounts;
using BankCoreApi.Models.Customers;
using BankCoreApi.Repositories.Accounts;
using BankCoreApi.Repositories.Customers;
using Microsoft.AspNetCore.Mvc;

namespace BankCoreApi.Controllers
{
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AccountsController> _logger;
        private readonly AccountRepository _accountRepository;
        private readonly CustomerRepository _customerRepository;

        public AccountsController(IConfiguration configuration, ILogger<AccountsController> logger,
            AccountRepository accountRepository, CustomerRepository customerRepository)    
        {
            _configuration = configuration;
            _logger = logger;
            _accountRepository = accountRepository;
            _customerRepository = customerRepository;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllAccounts()
        {
            var accounts = await _accountRepository.GetAllAsync();
            if (!accounts.Any())
            {
                return NotFound();
            }
            return Ok(accounts);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccountById(int id)
        {
            var account = await _accountRepository.GetByIdAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            return Ok(account);
        }


        [HttpGet("by-uuid/{uniqueId}")]
        public async Task<IActionResult> GetAccountByUniqueId(Guid uniqueId)
        {
            var account = await _accountRepository.GetByUniqueIdAsync(uniqueId);
            if (account == null)
            {
                return NotFound();
            }
            return Ok(account);
        }


        [HttpPost("create-with-customer")]
        public async Task<IActionResult> CreateAccountAndCustomer([FromBody] AccountRequest request)
        {
            
            if (request == null)
            {
                return BadRequest();
            }
            try
            {
                //Verify Customer
                if (await _customerRepository.ExistsRecordAsync("identification_number", request.IdentificationNumber))
                {
                    return Conflict($"Identification number '{request.IdentificationNumber}' already exists");
                }
                if (await _customerRepository.ExistsRecordAsync("phone", request.Phone))
                {
                    return Conflict($"Phone '{request.Phone}' already exists");
                }  
                if (await _customerRepository.ExistsRecordAsync("email", request.Phone))
                {
                    return Conflict($"Email '{request.Email}' already exists");
                }
                var customer = new Customer()
                {
                    CustomerName = request.CustomerName,
                    IdentificationNumber = request.IdentificationNumber,
                    Phone = request.Phone,
                    Email = request.Email,
                    Address = request.Address,
                };
                await _customerRepository.CreateAsync(customer);   
              

                var account = new Account()
                {
                    CustomerId = customer.CustomerId,
                    AccountTypeId = request.AccountTypeId,
                    AccountStatusId = (int)AccountStatus.Pending,
                    HolderName = customer.CustomerName,
                    AccountNumber = Generator.GenerateAccountNumber(),
                    Iban = Generator.GenerateIban(),
                    Currency = request.Currency,
                };       
                await _accountRepository.CreateAsync(account);
                _logger.LogInformation($"Customer '{customer.CustomerName}', account '{account.AccountNumber}' created successfully!");
                return StatusCode(21, $"Customer '{customer.CustomerName}', account '{account.AccountNumber}' created successfully!");
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPut("{uniqueId}")]
        public async Task<IActionResult> UpdateAccount(int id, [FromBody] Account account)
        {
            return Ok();
        }

    }
}