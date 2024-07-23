using System.Net;
using BankCoreApi.Exceptions;
using BankCoreApi.Helpers;
using BankCoreApi.Models.Transactions;
using BankCoreApi.Repositories.Core;
using BankCoreApi.Services.Core;
using Microsoft.AspNetCore.Mvc;

namespace BankCoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ILogger<TransactionsController> _logger;
        private readonly TransactionService _service;

        public TransactionsController(ILogger<TransactionsController> logger,TransactionService service)    
        {
            _logger = logger;
            _service = service;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllTransactions([FromQuery] PaginationParam param)
        {
            try
            {
               var transactions = await _service.GetAllTransactions(param);
               return Ok(transactions);
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
        public async Task<IActionResult> GetTransactionById(int id)
        {
            try
            {
               var transaction = await _service.GetTransactionById(id);
               return Ok(transaction);
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
        public async Task<IActionResult> GetTransactionByUniqueId(Guid uniqueId)
        {
            try
            {
               var transaction = await _service.GetTransactionByUniqueId(uniqueId);
               return Ok(transaction);
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


        [HttpPost("deposit")]
        public async Task<IActionResult> Deposit([FromBody] DepositRequest request)
        {
            try
            {
                await _service.Deposit(request);	
                var message = $"Deposit of {request.Amount} {request.Currency} to account '{request.AccountNumber}' successful.";
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
        

        [HttpPost("withdraw")]
        public async Task<IActionResult> Withdraw([FromBody] WithdrawRequest request)
        {
            try
            {
                await _service.Withdraw(request);
                var message = $"Witdrawal of {request.Amount} {request.Currency} to account '{request.AccountNumber}' successful.";
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


        [HttpPost("transfer-number")]
        public async Task<IActionResult> TranferByNumber([FromBody] TransferByNumberRequest request)
        {
            try
            {
                await _service.TransferByNumber(request);
                var message = $"Transfer of {request.Amount} {request.Currency} from '{request.SourceNumber}' to account '{request.DestinationNumber}' successful.";
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


        [HttpPost("transfer-iban")]
        public async Task<IActionResult> TranferByIban([FromBody] TransferByIbanRequest request)
        {
            try
            {
                await _service.TransferByIban(request);
                var message = $"Transfer of {request.Amount} {request.Currency} from '{request.SourceIban}' to account '{request.DestinationIban}' successful.";
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


        [HttpGet("by-account-id/{id}")]
        public async Task<IActionResult> GetAccountTransactionsById(int id, PaginationParam param)
        {
            try
            {
                var transactions = await _service.GetAccountTransactionsById(id, param);
                return Ok(200);
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


        [HttpGet("by-account-uuid/{uniqueId}")]
        public async Task<IActionResult> GetAccountTransactionsByUniqueId(Guid uniqueId, PaginationParam param) 
        {
            try
            {
                var transactions = await _service.GetAccountTransactionsByUniqueId(uniqueId, param);    
                return Ok(transactions);
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

    }
}