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
    acc.AccountId, acc.AccountNumber,
    acc.Iban,
    cu.CustomerName, cu.IdentificationNumber,
    tt.TypeId, tt.TypeName,
    ts.StatusId, ts.StatusName
FROM Transactions tr
JOIN Accounts acc ON acc.AccountId = tr.AccountId
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
    tr.Code, tr.Amount,
    tr.BalanceBefore, tr.BalanceAfter,
    tr.Currency, tr.Description,
    tr.TransactionDate,
    acc.AccountId, acc.AccountNumber, 
    acc.Iban,
    cu.CustomerName, cu.IdentificationNumber,
    tt.TypeName AS TransactionType, 
    ts.StatusName AS TransactionStatus
FROM Transactions tr
JOIN Accounts acc ON acc.AccountId = tr.AccountId
JOIN Customers cu ON cu.CustomerId = acc.CustomerId
JOIN TransactionType tt ON tt.TypeId = tr.TransactionType
JOIN TransactionStatus ts ON ts.StatusId = tr.TransactionStatus;
GO