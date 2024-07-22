using BankCoreApi.Exceptions;
using BankCoreApi.Extensions;
using BankCoreApi.Helpers;
using BankCoreApi.Models.Customers;
using BankCoreApi.Repositories.Core;

namespace BankCoreApi.Services.Core
{
    public class CustomerService
    {
        private readonly CustomerRepository _repository;
        private readonly IHttpContextAccessor _httpContext;

        public CustomerService(CustomerRepository repository, IHttpContextAccessor httpContext)
        {
            _repository = repository;
            _httpContext = httpContext;
        }


        public async Task CreateCustomer(CreateCustomerRequest request)
        {
            if (request == null)
            {
                throw new BadRequestException("Create customer request cannot be  null");
            }
            if (!Enum.IsDefined(typeof(CustomerType), request.CustomerType))
            {
                throw new BadRequestException("Customer type does not exists.");
            }
            if (await _repository.ExistsRecordAsync("IdentificationNumber", request.IdentificationNumber))
            {
                throw new ConflictException($"Identification '{request.IdentificationNumber}' already exists.");
            }
            if (await _repository.ExistsRecordAsync("Phone", request.Phone))
            {
                throw new ConflictException($"Phone '{request.Phone}' already exists.");
            }
            if (await _repository.ExistsRecordAsync("Email", request.Email))
            {
                throw new ConflictException($"Email '{request.Phone}' already exists.");
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
            await _repository.CreateAsync(customer);
        }


        public async Task<Pagination<CustomerData>> GetAllCustomers(PaginationParam param)
        {
            var count = await _repository.GetTotalDataAsync();
            if (count == 0)
            {
                throw new NotFoundException("No customers found");
            }
            if (param is null)
            {
                throw new BadRequestException("Pagination params cannot be null");
            }
            var customers = await _repository.GetAllDataAsync(param.PageSize, param.PageIndex);
            var pagination = new Pagination<CustomerData>(customers, count, param.PageIndex, param.PageSize, _httpContext);
            return pagination;
        }


        public async Task<CustomerData> GetCustomerById(int customerId)
        {
            var customer = await _repository.GetDataByIdAsync(customerId);
            if (customer is null)
            {
                throw new NotFoundException("Customer not found");
            }
            return customer;
        }


        public async Task<CustomerData> GetCustomerByUniqueId(Guid uniqueId)
        {
            var customer = await _repository.GetDataByUniqueIdAsync(uniqueId);
            if (customer is null)
            {
                throw new NotFoundException("Customer not found");
            }
            return customer;
        }


        public async Task ChangeCustomerStatus(ChangeCustomerStatusRequest request)
        {
            if (request is null)
            {
                throw new BadRequestException("Change customer status cannot be null");
            }
            var customer = await _repository.GetByIdentNumberAsync(request.IdentificationNumber);
            if (customer is null)
            {
                throw new NotFoundException("Invalid customer");
            }
            var currentStatus = nameof(customer.CustomerStatus);
            if (request.NewStatus == customer.CustomerStatus)
            {
                throw new ConflictException($"Customer Status '{currentStatus}' is already set");
            }
            if (!Enum.IsDefined(typeof(CustomerStatus), request.NewStatus))
            {
                throw new BadRequestException("Invalid Customer Status.");
            }
            customer.CustomerStatus = request.NewStatus;
            customer.UpdatedAt = DateTime.UtcNow;
            await _repository.UpdateAsync(customer);
        }


        public async Task ChangeCustomerType(ChangeCustomerTypeRequest request)
        {
            if (request is null)
            {
                throw new BadRequestException("Change customer type cannot be null");
            }
            var customer = await _repository.GetByIdentNumberAsync(request.IdentificationNumber);
            if (customer is null)
            {
                throw new NotFoundException("Invalid customer");
            }
            var currentType = nameof(customer.CustomerType);
            if (request.NewType == customer.CustomerType)
            {
                throw new ConflictException($"Customer Type '{currentType}' is already set");
            }
            if (!Enum.IsDefined(typeof(CustomerType), request.NewType))
            {
                throw new BadRequestException("Invalid Customer Status.");
            }
            customer.CustomerType = request.NewType;
            customer.UpdatedAt = DateTime.UtcNow;
            await _repository.UpdateAsync(customer);
        }

    }
}

