using System.Net;
using BankCoreApi.Exceptions;
using BankCoreApi.Helpers;
using BankCoreApi.Models.Accounts;
using BankCoreApi.Models.Customers;
using BankCoreApi.Repositories.Core;
using BankCoreApi.Services.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankCoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly ILogger<AccountsController> _logger;
        private readonly AccountService _service;

        public AccountsController(ILogger<AccountsController> logger, AccountService service)    
        {
            _logger = logger;
            _service = service;
        }
       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AccountData>>> GetAllAccounts([FromQuery] PaginationParam param)
        {
            try
            {
               var accounts = await _service.GetAllAccounts(param);
               return StatusCode((int)HttpStatusCode.OK, accounts);
            }
            catch (ApiException ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccountById(int id)
        {
            try
            {
                var account = await _service.GetAccountById(id);
                return Ok(account);
            }
            catch (ApiException ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpGet("by-uuid/{uniqueId}")]
        public async Task<IActionResult> GetAccountByUniqueId(Guid uniqueId)
        {
            try
            {
                var account = await _service.GetAccountByUniqueId(uniqueId);
                return StatusCode(200, account);
            }
            catch (ApiException ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccountRequest request)
        {
            try
            {
                await _service.CreateAccount(request);
                var message = $"Account created successfully!";
                _logger.LogInformation(message);
                return StatusCode(201, message);
            }
            catch (ApiException ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(ex.StatusCode, ex.Message);
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
            try
            {
                await _service.CreateAccountWithCustomer(request);
                var message = $"Customer '{request.CustomerName}', created with account successfully!";
                _logger.LogInformation(message);
                return StatusCode(201, message);
            }
            catch (ApiException ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut("change-status")]
        public async Task<IActionResult> ChangeAccountStatus([FromBody] ChangeAccountStatusRequest request)
        {
            try
            {
                await _service.ChangeAccountStatus(request);
                var message = $"Account '{request.AccountNumber}', updated to {nameof(request.NewStatus)}!";
                _logger.LogInformation(message);
                return Ok(message);
            }
            catch (ApiException ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPut("change-type")]
        public async Task<IActionResult> ChangeAccountType([FromBody] ChangeAccountTypeRequest request)
        {
            try
            {
                await _service.ChangeAccountType(request);
                var message = $"Account '{request.AccountNumber}', updated  to '{nameof(request.NewType)}'!";
                _logger.LogInformation(message);
                return Ok(message);
            }
            catch (ApiException ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(ex.StatusCode, ex.Message);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}