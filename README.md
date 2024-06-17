# BankCoreApi
REST API with Banking System


## Tools
- SQL Server 
- ASP .NET Core
- C# 


## Features
- Customers
    - [x] Create Customer
    - [x] Get All Customers
    - [x] Get Account
- Accounts
    - [x] Create Account with Customer
    - [x] Create and Associate an Account
    - [x] Get All Accounts
    - [x] Get Account
    - [x] Change Account Status
    - [x] Change Account Type
- Transactions
    - [x] Deposit
    - [x] Withdrawal
    - [x] Transfer By Account Number and Iban
    - [x] Create Transaction
    - [x] Get All Transactions
    - [x] Get Account Transactions
- Reports
    - [x] Customers Report
    - [x] Account Reports
    - [x] Transactions Report
- Statistics
    - [ ] Customers Statistics
    - [ ] Account Statistics
    - [ ] Transactions Statistics


## How to run
- Download or clone repository: `git clone https://github.com/ortizdavid/BankCoreApi`
- Copy database scripts from [_Database](_Database) folder to SQL Server
- Change **__DefaultConnection__** from [appsettings.json](appsettings.json) file
- Import Postman Collections from [_Api_Collections](_Api_Collections)
- Run Application: `dotnet run`


## Example of endpoints

- Deposit
    ```http
    POST /api/transactions/deposit
    ```
    ```json
    {
        "accountNumber": "8792529764",
        "amount": 127000.10,
        "currency": "USD"
    }
    ```

- Withdraw
    ```http
    POST /api/transactions/withdraw
    ```
    ```json
    {
        "accountNumber": "8792529764",
        "amount": 1000.95,
        "currency": "USD"
    }
    ```

- Transfer by Account Number
    ```http
    POST /api/transactions/transfer
    ```
    ```json
    {
        "sourceNumber": "8792529764",
        "destinationNumber": "7840163431",
        "amount": 529.98,
        "currency": "USD"
    }
    ```

- Transfer by Iban
    ```http
    POST /api/transactions/transfer
    ```
    ```json
    {
        "sourceIban": "XX741234565111434546",
        "destinationIban": "XX481234569540236342",
        "amount": 12300.98,
        "currency": "USD"
    }
    ```

- Create Account with Customer
    ```http
    POST /api/accounts/create-with-customer
    ```
    ```json
    {
        "customerName": "Anna Maria",
        "identificationNumber": "05455T5F644",
        "email": "ana@gmail.com",
        "phone": "+294678902348",
        "address": "Luanda, Angola",
        "customerType": 1,
        "accountType": 3,
        "currency": "USD"
    }
    ```

- Create Account And Associate Customer
    ```http
    POST /api/accounts
    ```
    ```json
    {
        "customerId": 5,
        "accountType": 1,
        "currency": "USD"
    }
    ```

- Change Account Status
    ```http
    PUT /api/accounts/change-status
    ```
    ```json
    {
        "accountNumber": "8792529764",
        "accountStatus": 6
    }
    ```

- Change Account Type
    ```http
    PUT /api/accounts/change-type
    ```
    ```json
    {
        "accountNumber": "8792529764",
        "accountType": 1
    }
    ```