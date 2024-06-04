using BankCoreApi.Models.Transactions;
using BankCoreApi.Repositories.Accounts;
using BankCoreApi.Repositories.Transactions;
using Microsoft.AspNetCore.Mvc;
using Name;

namespace BankCoreApi.Controllers
{
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<TransactionsController> _logger;
        private readonly TransactionRepository _transactionRepository;
        private readonly AccountRepository _accountRepository;
       
        public TransactionsController(IConfiguration configuration, ILogger<TransactionsController> logger,
            TransactionRepository transactionRepository, AccountRepository accountRepository)    
        {
            _configuration = configuration;
            _logger = logger;
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
        }


        [HttpPost("deposit")]
        public async Task<IActionResult> Deposit([FromBody] DepositRequest request)
        {
            if (request == null)
            {
                return BadRequest();
            }
            try
            {
                var account = await _accountRepository.GetByNubmerAsync(request.AccountNumber);
                account.Deposit(request.Amount);
                await _accountRepository.UpdateAsync(account);
                return Ok("Deposit successfuly");
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
        

        [HttpPost("/withdraw")]
        public async Task<IActionResult> Withdraw([FromBody] WithdrawRequest request)
        {
            return Ok();
        }


        [HttpPost("/transfer-number")]
        public async Task<IActionResult> TranferByNumber([FromBody] TransferNumberRequest request)
        {
            return Ok();
        }


        [HttpPost("/transfer-iban")]
        public async Task<IActionResult> TranferByIban([FromBody] TransferIbanRequest request)
        {
            return Ok();
        }

        [HttpGet("/transactions")]
        public async Task<IActionResult> GetAllTransactions()
        {
            return Ok();
        }

        [HttpGet("/transactions/{uniqueId}")]
        public async Task<IActionResult> GetCustomerTransactions(string uniqueId)
        {
            return Ok();
        }



    }
}