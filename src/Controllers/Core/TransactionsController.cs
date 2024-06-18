using BankCoreApi.Helpers;
using BankCoreApi.Models.Transactions;
using BankCoreApi.Repositories.Accounts;
using BankCoreApi.Repositories.Transactions;
using Microsoft.AspNetCore.Mvc;
using Name;

namespace BankCoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<TransactionsController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly TransactionRepository _transactionRepository;
        private readonly AccountRepository _accountRepository;
       
        public TransactionsController(IConfiguration configuration, ILogger<TransactionsController> logger,
            TransactionRepository transactionRepository, AccountRepository accountRepository, IHttpContextAccessor httpContextAccessor)    
        {
            _configuration = configuration;
            _logger = logger;
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
            _httpContextAccessor = httpContextAccessor;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllTransactions(int pageIndex = 1, int pageSize = 10)
        {
            try
            {
                var count = await _transactionRepository.GetTotalDataAsync();
                if (count == 0)
                {
                    return NotFound("No transactions found.");
                }
                var transactions = await _transactionRepository.GetAllDataAsync(pageSize, pageIndex);
                var pagination = new Pagination<TransactionData>(transactions, count, pageIndex, pageSize, _httpContextAccessor);
                return Ok(pagination);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
        

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransactionById(int id)
        {
            var transaction = await _transactionRepository.GetDataByIdAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }
            return Ok(transaction);
        }


        [HttpGet("by-uuid/{uniqueId}")]
        public async Task<IActionResult> GetTransactionByUniqueId(Guid uniqueId)
        {
            var transaction = await _transactionRepository.GetByUniqueIdAsync(uniqueId);
            if (transaction == null)
            {
                return NotFound();
            }
            return Ok(transaction);
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
                if (account == null)
                {
                    return NotFound($"Account '{account?.AccountNumber}' does not exists");
                }
                var balanceBefore = account.Balance;
                //Deposit
                account.Deposit(request.Amount);
                await _accountRepository.UpdateAsync(account);
                
                var balanceAfter = account.Balance;
                var message = $"Deposit of {request.Amount} {request.Currency} to account '{request.AccountNumber}' successful.";
                 //Transaction
                var transaction = new Transaction()
                {
                    SourceId = account.AccountId,
                    DestinationId = account.AccountId,
                    TransactionType = TransactionType.Deposit,
                    TransactionStatus = TransactionStatus.Completed,
                    Amount = request.Amount,
                    Currency = request.Currency,
                    BalanceBefore = balanceBefore,
                    BalanceAfter = balanceAfter,
                    Code = TransactionHelper.GenerateTransactionCode(),
                    TransactionDate = DateTime.UtcNow,
                    Description = message,
                };
                await _transactionRepository.CreateAsync(transaction);
                _logger.LogInformation(message);
                return Ok(message);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }
        

        [HttpPost("withdraw")]
        public async Task<IActionResult> Withdraw([FromBody] WithdrawRequest request)
        {
            if (request == null)
            {
                return BadRequest();
            }
            try
            {
                var account =  await _accountRepository.GetByNubmerAsync(request.AccountNumber);
                if (account == null)
                {
                    return NotFound($"Account '{account?.AccountNumber}' does not exists");
                }
                var balanceBefore = account.Balance;
                //Witdraw
                account.Withdraw(request.Amount);
                await _accountRepository.UpdateAsync(account);
                
                var balanceAfter = account.Balance;
                var message = $"Witdrawal of {request.Amount} {request.Currency} to account '{request.AccountNumber}' successful.";
                //Transaction
                var transaction = new Transaction()
                {
                    SourceId = account.AccountId,
                    DestinationId = account.AccountId,
                    TransactionType = TransactionType.Withdrawal,
                    TransactionStatus = TransactionStatus.Completed,
                    Amount = request.Amount,
                    Currency = request.Currency,
                    BalanceBefore = balanceBefore,
                    BalanceAfter = balanceAfter,
                    Code = TransactionHelper.GenerateTransactionCode(),
                    TransactionDate = DateTime.UtcNow,
                    Description = message,
                };
                await _transactionRepository.CreateAsync(transaction);
                _logger.LogInformation(message);
                return Ok(message);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost("transfer-number")]
        public async Task<IActionResult> TranferByNumber([FromBody] TransferNumberRequest request)
        {
            if (request == null)
            {
                return BadRequest();
            }
            try
            {
                var sourceAccount = await _accountRepository.GetByNubmerAsync(request.SourceNumber);
                var destinationAccount = await _accountRepository.GetByNubmerAsync(request.DestinationNumber);
                if (sourceAccount == null)
                {
                    return NotFound($"Account number '{request.SourceNumber}' does not exists");
                }
                if (destinationAccount == null)
                {
                    return NotFound($"Account number '{request.DestinationNumber}' does not exists");
                }
                if (request.SourceNumber == request.DestinationNumber)
                {
                    return Conflict("Transfer failed: same account");
                }
                var balanceBefore = sourceAccount.Balance;
                //Transfer
                sourceAccount.Transfer(request.Amount, destinationAccount);
                await _accountRepository.UpdateAsync(sourceAccount);
                await _accountRepository.UpdateAsync(destinationAccount);
                
                var balanceAfter = sourceAccount.Balance;
                var message = $"Transfer of {request.Amount} {request.Currency} from '{request.SourceNumber}' to account '{request.DestinationNumber}' successful.";
                //Transaction
                var transaction = new Transaction()
                {
                    SourceId = sourceAccount.AccountId,
                    DestinationId = destinationAccount.AccountId,
                    TransactionType = TransactionType.Transfer,
                    TransactionStatus = TransactionStatus.Completed,
                    Amount = request.Amount,
                    Currency = request.Currency,
                    BalanceBefore = balanceBefore,
                    BalanceAfter = balanceAfter,
                    Code = TransactionHelper.GenerateTransactionCode(),
                    TransactionDate = DateTime.UtcNow,
                    Description = message,
                };
                await _transactionRepository.UpdateAsync(transaction);
                _logger.LogInformation(message);
                return Ok(message);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost("transfer-iban")]
        public async Task<IActionResult> TranferByIban([FromBody] TransferIbanRequest request)
        {
            if (request == null)
            {
                return BadRequest();
            }
            try
            {
                var sourceAccount = await _accountRepository.GetByIbanAsync(request.SourceIban);
                var destinationAccount = await _accountRepository.GetByIbanAsync(request.DestinationIban);
                if (sourceAccount == null)
                {
                    return NotFound($"IBAN '{request.SourceIban}' does not exists");
                }
                if (destinationAccount == null)
                {
                    return NotFound($"IBAN '{request.DestinationIban}' does not exists");
                }
                if (request.SourceIban == request.DestinationIban)
                {
                    return Conflict("Transfer failed: same IBAN");
                }
                var balanceBefore = sourceAccount.Balance;
                //Transfer
                sourceAccount.Transfer(request.Amount, destinationAccount);
                await _accountRepository.UpdateAsync(sourceAccount);
                await _accountRepository.UpdateAsync(destinationAccount);
                
                var balanceAfter = sourceAccount.Balance;
                var message = $"Transfer of {request.Amount} {request.Currency} from '{request.SourceIban}' to account '{request.DestinationIban}' successful.";
                //Transaction
                var transaction = new Transaction()
                {
                    SourceId = sourceAccount.AccountId,
                    TransactionType = TransactionType.Transfer,
                    TransactionStatus = TransactionStatus.Completed,
                    Amount = request.Amount,
                    Currency = request.Currency,
                    BalanceBefore = balanceBefore,
                    BalanceAfter = balanceAfter,
                    Code = TransactionHelper.GenerateTransactionCode(),
                    TransactionDate = DateTime.UtcNow,
                    Description = message,
                };
                await _transactionRepository.UpdateAsync(transaction);
                _logger.LogInformation(message);
                return Ok(message);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("by-account-id/{id}")]
        public async Task<IActionResult> GetAccountTransactionsById(int id, int pageIndex = 1, int pageSize = 10)
        {
            try
            {
                var count = await _transactionRepository.GetTotalDataAsync();
                if (count == 0)
                {
                    return NotFound("No transactions found.");
                }
                var transactions = await _transactionRepository.GetAllDataByAccountIdAsync(id, pageSize, pageIndex);
                var pagination = new Pagination<TransactionData>(transactions, count, pageIndex, pageSize, _httpContextAccessor);
                return Ok(pagination);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("by-account-uuid/{uniqueId}")]
        public async Task<IActionResult> GetAccountTransactionsByUniqueId(Guid uniqueId, int pageIndex = 1, int pageSize = 10) 
        {
            try
            {
                var count = await _transactionRepository.GetTotalDataAsync();
                if (count == 0)
                {
                    return NotFound("No transactions found.");
                }
                var transactions = await _transactionRepository.GetAllDataByAccountUniqueIdAsync(uniqueId, pageSize, pageIndex);
                var pagination = new Pagination<TransactionData>(transactions, count, pageIndex, pageSize, _httpContextAccessor);
                return Ok(pagination);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

    }
}