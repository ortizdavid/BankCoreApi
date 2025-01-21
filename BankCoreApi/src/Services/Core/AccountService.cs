using BankCoreApi.Exceptions;
using BankCoreApi.Helpers;
using BankCoreApi.Models.Accounts;
using BankCoreApi.Models.Customers;
using BankCoreApi.Repositories.Core;

namespace BankCoreApi.Services.Core
{
    public class AccountService
    {
        private readonly AccountRepository _repository;
        private readonly CustomerRepository _customerRepository;
        private readonly IHttpContextAccessor _httpContext;

        public AccountService(AccountRepository repository, CustomerRepository customerRepository, IHttpContextAccessor httpContext)   
        {
            _repository = repository;
            _customerRepository = customerRepository;
            _httpContext = httpContext;
        }


        public async Task CreateAccount(CreateAccountRequest request)
        {
            if (request is null)
            {
                throw new BadRequestException("Create account request cannot be null");
            }
            var customer = await _customerRepository.GetByIdAsync(request.CustomerId);
            if (customer is null)
            {
                throw new NotFoundException("Customer not found");
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
            await _repository.CreateAsync(account);
        }


        public async Task CreateAccountWithCustomer(CreateAccountWithCustomerRequest request)
        {
            if (request is null)
            {
                throw new BadRequestException("Create account request cannot be null");
            }
            if (await _customerRepository.ExistsRecordAsync("IdentificationNumber", request.IdentificationNumber))
            {
                throw new ConflictException($"Identification number '{request.IdentificationNumber}' already exists");
            }
            if (await _customerRepository.ExistsRecordAsync("Phone", request.Phone))
            {
                throw new ConflictException($"Phone '{request.Phone}' already exists");
            }  
            if (await _customerRepository.ExistsRecordAsync("Email", request.Phone))
            {
                throw new ConflictException($"Email '{request.Email}' already exists");
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
            await _repository.CreateAsync(account);
        }


        
        public async Task ChangeAccountStatus(ChangeAccountStatusRequest request)
        {
            if (request is null)
            {
                throw new BadRequestException("Change Account Status request Cannot be null");
            }
            var account = await _repository.GetByNubmerAsync(request.AccountNumber);
            var currentStatus = nameof(account.AccountStatus);
            if (account is null)
            {
                throw new NotFoundException("Account not found");
            }
            if (!Enum.IsDefined(typeof(AccountStatus), request.NewStatus))
            {
                throw new BadRequestException("Invalid Customer Status.");
            }
            if (request.NewStatus == account.AccountStatus)
            {
                throw new ConflictException($"The account status '{currentStatus}' is already set.");
            }
            account.AccountStatus = request.NewStatus;
            account.UpdatedAt = DateTime.UtcNow;
            await _repository.UpdateAsync(account);
        }


        public async Task ChangeAccountType(ChangeAccountTypeRequest request)
        {
            if (request is null)
            {
                throw new BadRequestException("Change Account Type request");
            }
            var account = await _repository.GetByNubmerAsync(request.AccountNumber);
            var currentType =  nameof(account.AccountType);
            if (account is null)
            {
                throw new NotFoundException("Account not found");
            }
            if (!Enum.IsDefined(typeof(AccountType), request.NewType))
            {
                throw new BadRequestException("Invalid Customer Type.");
            }
            if (request.NewType == account.AccountType)
            {
                throw new ConflictException($"Account type '{currentType}' is already set for this account.");
            }
            account.AccountType = request.NewType;
            account.UpdatedAt = DateTime.UtcNow;
            await _repository.UpdateAsync(account);   
        }


        public async Task<Pagination<AccountData>> GetAllAccounts(PaginationParam param)
        {
            var count = await _repository.GetTotalDataAsync();
            if (count == 0)
            {
                throw new NotFoundException("No accounts found.");
            }
            if (param is null)
            {
                throw new BadRequestException("Pagination params cannot be null");
            }
            var accounts = await _repository.GetAllDataAsync(param.PageSize, param.PageIndex);
            var pagination = new Pagination<AccountData>(accounts, count, param.PageIndex, param.PageSize, _httpContext);
            return pagination;
        }


        public async Task<AccountData> GetAccountById(int accountId)
        {
            var account = await _repository.GetDataByIdAsync(accountId);
            if (account is null)
            {
                throw new NotFoundException("Account not found");
            }
            return account;
        }


        public async Task<AccountData> GetAccountByUniqueId(Guid uniqueId)
        {
            var account = await _repository.GetDataByUniqueIdAsync(uniqueId);
            if (account is null)
            {
                throw new NotFoundException("Account not found");
            }
            return account;
        }

    }
}