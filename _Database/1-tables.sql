IF OBJECT_ID('AccountStatus', 'U') IS NOT NULL
    DROP TABLE AccountStatus;
GO
CREATE TABLE AccountStatus (
    StatusId INT IDENTITY(1,1) PRIMARY KEY,
    StatusName VARCHAR(20) UNIQUE NOT NULL,
    Description VARCHAR(150)
);
GO

IF OBJECT_ID('AccountType', 'U') IS NOT NULL
    DROP TABLE AccountType;
GO
CREATE TABLE AccountType (
    TypeId INT IDENTITY(1,1) PRIMARY KEY,
    TypeName VARCHAR(20) UNIQUE NOT NULL,
    Description VARCHAR(150)
);
GO

IF OBJECT_ID('TransactionStatus', 'U') IS NOT NULL
    DROP TABLE TransactionStatus;
GO
CREATE TABLE TransactionStatus (
    StatusId INT IDENTITY(1,1) PRIMARY KEY,
    StatusName VARCHAR(20) UNIQUE NOT NULL,
    Description VARCHAR(150)
);
GO

IF OBJECT_ID('TransactionType', 'U') IS NOT NULL
    DROP TABLE TransactionType;
GO
CREATE TABLE TransactionType (
    TypeId INT IDENTITY(1,1) PRIMARY KEY,
    TypeName VARCHAR(20) UNIQUE NOT NULL,
    Description VARCHAR(150)
);
GO

IF OBJECT_ID('Customers', 'U') IS NOT NULL
    DROP TABLE Customers;
GO
CREATE TABLE Customers (
    CustomerId INT IDENTITY(1,1) PRIMARY KEY,
    CustomerName VARCHAR(150) NOT NULL,
    IdentificationNumber VARCHAR(30) UNIQUE NOT NULL,
    Email VARCHAR(150) UNIQUE,
    Phone VARCHAR(20) UNIQUE,
    Address VARCHAR(200),
    UniqueId UNIQUEIDENTIFIER DEFAULT NEWID(),
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE()
);
GO

IF OBJECT_ID('Accounts', 'U') IS NOT NULL
    DROP TABLE Accounts;
GO
CREATE TABLE Accounts (
    AccountId INT IDENTITY(1,1) PRIMARY KEY,
    CustomerId INT NOT NULL,
    AccountType INT NOT NULL,
    AccountStatus INT NOT NULL,
    AccountNumber VARCHAR(18) UNIQUE NOT NULL,
    Iban VARCHAR(31) UNIQUE NOT NULL,
    HolderName VARCHAR(150),
    Balance DECIMAL(18, 2) NOT NULL DEFAULT 0.0,
    Currency VARCHAR(3),
    UniqueId UNIQUEIDENTIFIER DEFAULT NEWID(),
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    CONSTRAINT fk_AccountType FOREIGN KEY(AccountType) REFERENCES AccountType(TypeId),
    CONSTRAINT fk_AccountStatus FOREIGN KEY(AccountStatus) REFERENCES AccountStatus(StatusId),
    CONSTRAINT fk_CustomerAccount FOREIGN KEY(CustomerId) REFERENCES Customers(CustomerId)
);
GO

IF OBJECT_ID('Transactions', 'U') IS NOT NULL
    DROP TABLE Transactions;
GO
CREATE TABLE Transactions (
    TransactionId INT IDENTITY(1,1) PRIMARY KEY,
    AccountId INT NOT NULL,
    TransactionType INT NOT NULL,
    TransactionStatus INT NOT NULL,
    Code VARCHAR(30) UNIQUE NOT NULL,
    Amount DECIMAL(18, 2) NOT NULL,
    Currency VARCHAR(3),
    BalanceBefore DECIMAL(18, 2) NOT NULL,
    BalanceAfter DECIMAL(18, 2) NOT NULL,
    Description VARCHAR(150),
    TransactionDate DATETIME NOT NULL,
    UniqueId UNIQUEIDENTIFIER DEFAULT NEWID(),
    CreatedAt DATETIME DEFAULT GETDATE(),
    UpdatedAt DATETIME DEFAULT GETDATE(),
    CONSTRAINT fk_TransactionType FOREIGN KEY(TransactionType) REFERENCES TransactionType(TypeId),
    CONSTRAINT fk_TransactionStatus FOREIGN KEY(TransactionStatus) REFERENCES TransactionStatus(StatusId)
);
GO
