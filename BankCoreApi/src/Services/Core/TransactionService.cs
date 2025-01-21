using BankCoreApi.Exceptions;
using BankCoreApi.Helpers;
using BankCoreApi.Models.Transactions;
using BankCoreApi.Repositories.Core;

namespace BankCoreApi.Services.Core
{
    public class TransactionService
    {
        private readonly TransactionRepository _repository;
        private readonly AccountRepository _accountRepository;
        private readonly IHttpContextAccessor _httpContext;

        public TransactionService(TransactionRepository repository, AccountRepository accountRepository, IHttpContextAccessor httpContext)
        {
            _repository = repository;
            _httpContext = httpContext;
            _accountRepository = accountRepository;
        }


        public async Task<Pagination<TransactionData>> GetAllTransactions(PaginationParam param)
        {
            var count = await _repository.GetTotalDataAsync();
            if (count == 0)
            {
                throw new NotFoundException("No transactions found.");
            }
            if (param is null)
            {
                throw new BadRequestException("Pagination params cannot be null");
            }
            var transactions = await _repository.GetAllDataAsync(param.PageSize, param.PageIndex);
            var pagination = new Pagination<TransactionData>(transactions, count, param.PageIndex, param.PageSize, _httpContext);
            return pagination;
        }
        

        public async Task<TransactionData> GetTransactionById(int transactionId)
        {
            var transaction = await _repository.GetDataByIdAsync(transactionId);
            if (transaction is null)
            {
                throw new NotFoundException("Transaction not found");
            }
            return transaction;
        }


        public async Task<TransactionData> GetTransactionByUniqueId(Guid uniqueId)
        {
            var transaction = await _repository.GetDataByUniqueIdAsync(uniqueId);
            if (transaction is null)
            {
                throw new NotFoundException("Transaction not found");
            }
            return transaction;
        }


        public async Task Deposit(DepositRequest request)
        {
            if (request is null)
            {
                throw new  BadRequestException("Deposit request cannot be null");
            }
            var account = await _accountRepository.GetByNubmerAsync(request.AccountNumber);
            if (account is null)
            {
                throw new NotFoundException($"Account '{account?.AccountNumber}' does not exists");
            }
            var balanceBefore = account.Balance;
            //Deposit
            account.Deposit(request.Amount);
            await _accountRepository.UpdateAsync(account);
            var balanceAfter = account.Balance;
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
                Description = $"Deposit of {request.Amount} {request.Currency} to account '{request.AccountNumber}' successful.",
            };
            await _repository.CreateAsync(transaction);
        }
        

        public async Task Withdraw(WithdrawRequest request)
        {
            if (request is null)
            {
                throw new BadRequestException("Withdraw request cannot be null");
            }
            var account =  await _accountRepository.GetByNubmerAsync(request.AccountNumber);
            if (account is null)
            {
                throw new NotFoundException($"Account '{account?.AccountNumber}' does not exists");
            }
            var balanceBefore = account.Balance;
            //Witdraw
            account.Withdraw(request.Amount);
            await _accountRepository.UpdateAsync(account);
            var balanceAfter = account.Balance;
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
                Description = $"Witdrawal of {request.Amount} {request.Currency} to account '{request.AccountNumber}' successful.",
            };
            await _repository.CreateAsync(transaction);
        }

        public async Task TransferByNumber(TransferByNumberRequest request)
        {
            if (request is null)
            {
                throw new BadRequestException("Transfer by nummber request cannot be null");
            }
            var sourceAccount = await _accountRepository.GetByNubmerAsync(request.SourceNumber);
            var destinationAccount = await _accountRepository.GetByNubmerAsync(request.DestinationNumber);
            if (sourceAccount is null)
            {
                throw new NotFoundException($"Account number '{request.SourceNumber}' does not exists");
            }
            if (destinationAccount is null)
            {
                throw new NotFoundException($"Account number '{request.DestinationNumber}' does not exists");
            }
            if (request.SourceNumber == request.DestinationNumber)
            {
                throw new ConflictException("Transfer failed: same account");
            }
            var balanceBefore = sourceAccount.Balance;
            //Transfer
            sourceAccount.Transfer(request.Amount, destinationAccount);
            await _accountRepository.UpdateAsync(sourceAccount);
            await _accountRepository.UpdateAsync(destinationAccount);
            var balanceAfter = sourceAccount.Balance;
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
                Description = $"Transfer of {request.Amount} {request.Currency} from '{request.SourceNumber}' to account '{request.DestinationNumber}' successful.",
            };
            await _repository.UpdateAsync(transaction);
        }

        public async Task TransferByIban(TransferByIbanRequest request)
        {
            if (request is null)
            {
                throw new BadRequestException("Transfer by Iban request cannot be null");
            }
            var sourceAccount = await _accountRepository.GetByIbanAsync(request.SourceIban);
            var destinationAccount = await _accountRepository.GetByIbanAsync(request.DestinationIban);
            if (sourceAccount is null)
            {
                throw new NotFoundException($"IBAN '{request.SourceIban}' does not exists");
            }
            if (destinationAccount is null)
            {
                throw new NotFoundException($"IBAN '{request.DestinationIban}' does not exists");
            }
            if (request.SourceIban == request.DestinationIban)
            {
                throw new ConflictException("Transfer failed: same IBAN");
            }
            var balanceBefore = sourceAccount.Balance;
            //Transfer
            sourceAccount.Transfer(request.Amount, destinationAccount);
            await _accountRepository.UpdateAsync(sourceAccount);
            await _accountRepository.UpdateAsync(destinationAccount);
            var balanceAfter = sourceAccount.Balance;
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
                Description = $"Transfer of {request.Amount} {request.Currency} from '{request.SourceIban}' to account '{request.DestinationIban}' successful.",
            };
            await _repository.UpdateAsync(transaction);
        }

        public async Task<Pagination<TransactionData>> GetAccountTransactionsById(int transactionId, PaginationParam param)
        {
            var count = await _repository.GetTotalDataAsync();
            if (count == 0)
            {
                throw new NotFoundException("No transactions found.");
            }
            if (param is null)
            {
                throw new BadRequestException("Pagination params cannot be null");
            }
            var transactions = await _repository.GetAllDataByAccountIdAsync(transactionId, param.PageSize, param.PageIndex);
            var pagination = new Pagination<TransactionData>(transactions, count, param.PageIndex, param.PageSize, _httpContext);
            return pagination;
        }

        public async Task<Pagination<TransactionData>> GetAccountTransactionsByUniqueId(Guid uniqueId, PaginationParam param) 
        {
            var count = await _repository.GetTotalDataAsync();
             if (count == 0)
            {
                throw new NotFoundException("No transactions found.");
            }
            if (param is null)
            {
                throw new BadRequestException("Pagination params cannot be null");
            }
            var transactions = await _repository.GetAllDataByAccountUniqueIdAsync(uniqueId, param.PageSize, param.PageIndex);
            var pagination = new Pagination<TransactionData>(transactions, count, param.PageIndex, param.PageSize, _httpContext);
            return pagination;
        }

    }
}