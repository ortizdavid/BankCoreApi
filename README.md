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
    - [ ] Customers Report
    - [ ] Account Reports
    - [ ] Transactions Report
- Statistics
    - [ ] Customers Statistics
    - [ ] Account Statistics
    - [ ] Transactions Report


## How to run
- Download or clone repository: `git clone https://github.com/ortizdavid/BankCoreApi`
- Copy database scripts from [_Database folder](_Database) to SQL Server
- Change **__DefaultConnection__** from [appsettings.json](appsettings.json) file
- Import Postman Collections from [_Api_Collections](_Api_Colletions)
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

- Tansafer by Account Number
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