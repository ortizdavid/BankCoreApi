using BankCoreApi.Helpers;
using BankCoreApi.Models.Accounts;
using BankCoreApi.Models.Customers;
using BankCoreApi.Repositories.Accounts;
using BankCoreApi.Repositories.Customers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankCoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AccountsController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AccountRepository _accountRepository;
        private readonly CustomerRepository _customerRepository;

        public AccountsController(IConfiguration configuration, ILogger<AccountsController> logger,
            AccountRepository accountRepository, CustomerRepository customerRepository, IHttpContextAccessor httpContextAccessor)    
        {
            _configuration = configuration;
            _logger = logger;
            _accountRepository = accountRepository;
            _customerRepository = customerRepository;
            _httpContextAccessor = httpContextAccessor;
        }

       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountData>>> GetAllAccounts(int pageIndex = 0, int pageSize = 10)
        {
            try
            {
                var count = await _accountRepository.GetTotalDataAsync();
                if (count == 0)
                {
                    return NotFound("No accounts found.");
                }
                var accounts = await _accountRepository.GetAllDataAsync(pageSize, pageIndex);
                var pagination = new Pagination<AccountData>(accounts, count, pageIndex, pageSize, _httpContextAccessor);
                return Ok(pagination);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccountById(int id)
        {
            var account = await _accountRepository.GetDataByIdAsync(id);
            if (account is null)
            {
                return NotFound();
            }
            return Ok(account);
        }


        [HttpGet("by-uuid/{uniqueId}")]
        public async Task<IActionResult> GetAccountByUniqueId(Guid uniqueId)
        {
            var account = await _accountRepository.GetByUniqueIdAsync(uniqueId);
            if (account is null)
            {
                return NotFound();
            }
            return Ok(account);
        }


        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccountRequest request)
        {
            if (request is null)
            {
                return BadRequest();
            }
            try
            {
                var customer = await _customerRepository.GetByIdAsync(request.CustomerId);
                if (customer is null)
                {
                    return NotFound("Invalid customer");
                }
                var account = new Account()
                {
                    CustomerId = customer.CustomerId,
                    AccountType = request.AccountType,
                    AccountStatus = AccountStatus.Pending,
                    HolderName = customer.CustomerName,
                    AccountNumber = AccountHelper.GenerateAccountNumber(),
                    Iban = AccountHelper.GenerateIban(),
                    Currency = request.Currency,
                };

                var message = $"Account '{account.AccountNumber}' created successfully!";
                _logger.LogInformation(message);
                return StatusCode(201, message);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost("create-with-customer")]
        public async Task<IActionResult> CreateAccountAndCustomer([FromBody] CreateAccountWithCustomerRequest request)
        {
            if (request is null)
            {
                return BadRequest();
            }
            try
            {
                //Verify Customer
                if (await _customerRepository.ExistsRecordAsync("IdentificationNumber", request.IdentificationNumber))
                {
                    return Conflict($"Identification number '{request.IdentificationNumber}' already exists");
                }
                if (await _customerRepository.ExistsRecordAsync("Phone", request.Phone))
                {
                    return Conflict($"Phone '{request.Phone}' already exists");
                }  
                if (await _customerRepository.ExistsRecordAsync("Email", request.Phone))
                {
                    return Conflict($"Email '{request.Email}' already exists");
                }
                var customer = new Customer()
                {
                    CustomerType = request.CustomerType,
                    CustomerName = request.CustomerName,
                    IdentificationNumber = request.IdentificationNumber,
                    Phone = request.Phone,
                    Email = request.Email,
                    Address = request.Address,
                    CustomerStatus = CustomerStatus.Pending,
                };
                await _customerRepository.CreateAsync(customer);   
              
                var account = new Account()
                {
                    CustomerId = customer.CustomerId,
                    AccountType = request.AccountType,
                    AccountStatus = AccountStatus.Pending,
                    HolderName = customer.CustomerName,
                    AccountNumber = AccountHelper.GenerateAccountNumber(),
                    Iban = AccountHelper.GenerateIban(),
                    Currency = request.Currency,
                };       
                await _accountRepository.CreateAsync(account);
                var message = $"Customer '{customer.CustomerName}', account '{account.AccountNumber}' created successfully!";
                _logger.LogInformation(message);
                return StatusCode(201, message);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPut("change-status")]
        public async Task<IActionResult> ChangeAccountStatus([FromBody] ChangeAccountStatusRequest request)
        {
            if (request is null)
            {
                return BadRequest();
            }
            try
            {
                var account = await _accountRepository.GetByNubmerAsync(request.AccountNumber);
                var currentStatus = account?.AccountStatus.ToString();
                if (account is null)
                {
                    return NotFound();
                }
                if (!Enum.IsDefined(typeof(AccountStatus), request.NewStatus))
                {
                    return BadRequest("Invalid Customer Status.");
                }
                if (request.NewStatus == account.AccountStatus)
                {
                    return Conflict($"The account status '{currentStatus}' is already set.");
                }
                account.AccountStatus = request.NewStatus;
                account.UpdatedAt = DateTime.UtcNow;
                await _accountRepository.UpdateAsync(account);
                var message = $"Account '{request.AccountNumber}', updated from '{currentStatus}' to {request.NewStatus.ToString()}!";
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
        public async Task<IActionResult> ChangeAccountType([FromBody] ChangeAccountTypeRequest request)
        {
            if (request is null)
            {
                return BadRequest();
            }
            try
            {
                var account = await _accountRepository.GetByNubmerAsync(request.AccountNumber);
                var currentType =  account?.AccountType.ToString();
                if (account is null)
                {
                    return NotFound();
                }
                if (!Enum.IsDefined(typeof(AccountType), request.NewType))
                {
                    return BadRequest("Invalid Customer Type.");
                }
                if (request.NewType == account.AccountType)
                {
                    return Conflict($"Account type '{currentType}' is already set for this account.");
                }
                account.AccountType = request.NewType;
                account.UpdatedAt = DateTime.UtcNow;
                await _accountRepository.UpdateAsync(account);
                var message = $"Account '{request.AccountNumber}', updated from '{currentType}' to '{request.NewType.ToString()}'!";
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