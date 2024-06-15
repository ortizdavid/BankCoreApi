-- ViewCustomerData
IF OBJECT_ID('ViewCustomerData', 'V') IS NOT NULL
    DROP VIEW ViewCustomerData;
GO
CREATE VIEW ViewCustomerData AS 
SELECT * FROM Customers;
GO


-- ViewAccountData
IF OBJECT_ID('ViewAccountData', 'V') IS NOT NULL
    DROP VIEW ViewAccountData;
GO
CREATE VIEW ViewAccountData AS 
SELECT 
    acc.AccountId, acc.UniqueId,
    acc.AccountNumber, acc.Iban,
    acc.Balance, acc.Currency,
    acc.CreatedAt, acc.UpdatedAt,
    cu.CustomerId, cu.CustomerName, 
    cu.IdentificationNumber,
    act.TypeId, act.TypeName,
    acs.StatusId, acs.StatusName
FROM Accounts acc
JOIN Customers cu ON(cu.CustomerId = acc.CustomerId)
JOIN AccountStatus acs ON(acs.StatusId = acc.AccountStatus)
JOIN AccountType act ON(act.TypeId = acc.AccountType);
GO


-- ViewTransactionData
IF OBJECT_ID('ViewTransactionData', 'V') IS NOT NULL
    DROP VIEW ViewTransactionData;
GO
CREATE VIEW ViewTransactionData AS 
SELECT 
    tr.TransactionId, tr.UniqueId,
    tr.Code, tr.Amount,
    tr.BalanceBefore, tr.BalanceAfter,
    tr.Currency, tr.Description,
    tr.TransactionDate,
    tr.CreatedAt, tr.UpdatedAt,
    acc.AccountId AS SourceAccountId, 
    acc.AccountNumber AS SourceAccountNumber, 
    acc.Iban AS SourceIban,
    dest.AccountId AS DestinationAccountId, 
    dest.AccountNumber AS DestinationAccountNumber, 
    dest.Iban AS DestinationIban,
    cu.CustomerName, cu.IdentificationNumber,
    tt.TypeId, tt.TypeName,
    ts.StatusId, ts.StatusName
FROM Transactions tr
JOIN Accounts acc ON acc.AccountId = tr.SourceId
JOIN Accounts dest ON dest.AccountId = tr.DestinationId
JOIN Customers cu ON cu.CustomerId = acc.CustomerId
JOIN TransactionType tt ON tt.TypeId = tr.TransactionType
JOIN TransactionStatus ts ON ts.StatusId = tr.TransactionStatus;
GO


-- View For REPORTS 

-- ViewTransactionReport
IF OBJECT_ID('ViewTransactionReport', 'V') IS NOT NULL
    DROP VIEW ViewTransactionReport;
GO
CREATE VIEW ViewTransactionReport AS 
SELECT 
    tr.Code, 
    tr.Amount,
    tr.BalanceBefore, 
    tr.BalanceAfter,
    tr.Currency, 
    tr.Description,
    tr.TransactionDate,
    acc.AccountId AS SourceAccountId, 
    acc.AccountNumber AS SourceAccountNumber, 
    acc.Iban AS SourceIban,
    dest.AccountId AS DestinationAccountId, 
    dest.AccountNumber AS DestinationAccountNumber, 
    dest.Iban AS DestinationIban,
    cu.CustomerId, cu.CustomerName, 
    cu.IdentificationNumber,
    tt.TypeName AS TransactionType, 
    ts.StatusName AS TransactionStatus
FROM Transactions tr
JOIN Accounts acc ON acc.AccountId = tr.SourceId
JOIN Accounts dest ON dest.AccountId = tr.DestinationId
JOIN Customers cu ON cu.CustomerId = acc.CustomerId
JOIN TransactionType tt ON tt.TypeId = tr.TransactionType
JOIN TransactionStatus ts ON ts.StatusId = tr.TransactionStatus;
GO


IF OBJECT_ID('ViewCustomerReport', 'V') IS NOT NULL
    DROP VIEW ViewCustomerReport;
GO
CREATE VIEW ViewCustomerReport AS
SELECT 
    cu.CustomerName, cu.IdentificationNumber,
    cu.Gender, cu.BirthDate,
    cu.Email, cu.Phone,
    cu.Address,
    cu.CreatedAt, cu.UpdatedAt,
    ct.TypeName AS CustomerType,
    cs.StatusName AS CustomerStatus
FROM Customers cu
JOIN CustomerType ct ON ct.TypeId = cu.CustomerType
JOIN CustomerStatus cs ON cs.StatusId = cu.CustomerStatus
GO


IF OBJECT_ID('ViewAccountReport', 'V') IS NOT NULL
    DROP VIEW ViewAccountReport;
GO
CREATE VIEW ViewAccountReport AS
SELECT 
    acc.AccountNumber, acc.Iban,
    acc.Balance, acc.Currency,
    acc.CreatedAt, acc.UpdatedAt,
    cu.CustomerId, cu.CustomerName, 
    cu.IdentificationNumber,
    act.TypeName as AccountType,
    acs.StatusName AS AccountStatus
FROM Accounts acc
JOIN Customers cu ON(cu.CustomerId = acc.CustomerId)
JOIN AccountStatus acs ON(acs.StatusId = acc.AccountStatus)
JOIN AccountType act ON(act.TypeId = acc.AccountType)
GO

