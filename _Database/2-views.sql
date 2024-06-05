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
SELECT * FROM Accounts;
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
