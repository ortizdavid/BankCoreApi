-- ViewUserData
IF OBJECT_ID('ViewUserData', 'V') IS NOT NULL
    DROP VIEW ViewUserData;
GO
CREATE VIEW ViewUserData AS 
SELECT
    us.UserId, us.UniqueId,
    us.UserName, us.Password,
    us.Image, us.IsActive,
    us.Token, us.CreatedAt,
    us.UpdatedAt,
    ro.RoleId, ro.RoleName
FROM Users us
JOIN Roles ro ON us.UserRole = ro.RoleId
GO


-- ViewCustomerData
IF OBJECT_ID('ViewCustomerData', 'V') IS NOT NULL
    DROP VIEW ViewCustomerData;
GO
CREATE VIEW ViewCustomerData AS 
SELECT
    cu.CustomerId, cu.UniqueId,
    cu.CustomerName, cu.IdentificationNumber,
    cu.Gender, cu.BirthDate,
    cu.Email, cu.Phone,
    cu.Address,
    cu.CreatedAt, cu.UpdatedAt,
    ct.TypeId, ct.TypeName,
    cs.StatusId, cs.StatusName
FROM Customers cu
JOIN CustomerType ct ON ct.TypeId = cu.CustomerType
JOIN CustomerStatus cs ON cs.StatusId = cu.CustomerStatus
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


-- Views for Reports

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

-- ViewCustomerReport
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

-- ViewAccountReport
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



-- Views for Statistics
-- ViewCustomerStatisticsCountByType
IF OBJECT_ID('ViewCustomerStatisticsCountByType', 'V') IS NOT NULL
    DROP VIEW ViewCustomerStatisticsCountByType;
GO
CREATE VIEW ViewCustomerStatisticsCountByType AS
SELECT
    ct.TypeName AS CustomerType,
    COUNT(cu.CustomerType) AS Count
FROM Customers cu
JOIN CustomerType ct ON ct.TypeId = cu.CustomerType
GROUP BY ct.TypeName;
GO


-- ViewCustomerStatisticsCountByStatus
IF OBJECT_ID('ViewCustomerStatisticsCountByStatus', 'V') IS NOT NULL
    DROP VIEW ViewCustomerStatisticsCountByStatus;
GO
CREATE VIEW ViewCustomerStatisticsCountByStatus AS
SELECT
    cs.StatusName AS CustomerStatus,
    COUNT(cu.CustomerStatus) AS Count
FROM Customers cu
JOIN CustomerStatus cs ON cs.StatusId = cu.CustomerStatus
GROUP BY cs.StatusName;
GO


-- ViewCustomerStatisticsCountByGender
IF OBJECT_ID('ViewCustomerStatisticsCountByGender', 'V') IS NOT NULL
    DROP VIEW ViewCustomerStatisticsCountByGender;
GO
CREATE VIEW ViewCustomerStatisticsCountByGender AS
SELECT
    cu.Gender,
    COUNT(cu.Gender) AS Count
FROM Customers cu
GROUP BY cu.Gender;
GO


-- ViewCustomerStatisticsCountByAge
IF OBJECT_ID('ViewCustomerStatisticsCountByAge', 'V') IS NOT NULL
    DROP VIEW ViewCustomerStatisticsCountByAge;
GO
CREATE VIEW ViewCustomerStatisticsCountByAge AS
SELECT
    CASE 
        WHEN DATEDIFF(YEAR, cu.BirthDate, GETDATE()) < 18 THEN 'Under 18'
        WHEN DATEDIFF(YEAR, cu.BirthDate, GETDATE()) BETWEEN 18 AND 25 THEN '18-25'
        WHEN DATEDIFF(YEAR, cu.BirthDate, GETDATE()) BETWEEN 26 AND 35 THEN '26-35'
        WHEN DATEDIFF(YEAR, cu.BirthDate, GETDATE()) BETWEEN 36 AND 45 THEN '36-45'
        WHEN DATEDIFF(YEAR, cu.BirthDate, GETDATE()) BETWEEN 46 AND 60 THEN '46-60'
        ELSE 'Over 60'
    END AS AgeRange,
    COUNT(*) AS Count
FROM Customers cu
GROUP BY 
    CASE 
        WHEN DATEDIFF(YEAR, cu.BirthDate, GETDATE()) < 18 THEN 'Under 18'
        WHEN DATEDIFF(YEAR, cu.BirthDate, GETDATE()) BETWEEN 18 AND 25 THEN '18-25'
        WHEN DATEDIFF(YEAR, cu.BirthDate, GETDATE()) BETWEEN 26 AND 35 THEN '26-35'
        WHEN DATEDIFF(YEAR, cu.BirthDate, GETDATE()) BETWEEN 36 AND 45 THEN '36-45'
        WHEN DATEDIFF(YEAR, cu.BirthDate, GETDATE()) BETWEEN 46 AND 60 THEN '46-60'
        ELSE 'Over 60'
    END;
GO


-- ViewAccountStatisticsCountByType
IF OBJECT_ID('ViewAccountStatisticsCountByType', 'V') IS NOT NULL
    DROP VIEW ViewAccountStatisticsCountByType;
GO
CREATE VIEW ViewAccountStatisticsCountByType AS
SELECT
    act.TypeName AS AccountType,
    COUNT(acc.AccountType) AS Count
FROM Accounts acc
JOIN AccountType act ON act.TypeId = acc.AccountType
GROUP BY act.TypeName;
GO


-- ViewAccountStatisticsCountByStatus
IF OBJECT_ID('ViewAccountStatisticsCountByStatus', 'V') IS NOT NULL
    DROP VIEW ViewAccountStatisticsCountByStatus;
GO
CREATE VIEW ViewAccountStatisticsCountByStatus AS
SELECT
    acs.StatusName AS AccountStatus,
    COUNT(acc.AccountStatus) AS Count
FROM Accounts acc
JOIN AccountStatus acs ON acs.StatusId = acc.AccountStatus
GROUP BY acs.StatusName;
GO


-- ViewAccountStatisticsTotalBalanceByType
IF OBJECT_ID('ViewAccountStatisticsTotalBalanceByType', 'V') IS NOT NULL
    DROP VIEW ViewAccountStatisticsTotalBalanceByType;
GO
CREATE VIEW ViewAccountStatisticsTotalBalanceByType AS
SELECT
    act.TypeName AS AccountType,
    SUM(acc.Balance) AS TotalBalance
FROM Accounts acc
JOIN AccountType act ON act.TypeId = acc.AccountType
GROUP BY act.TypeName;
GO


-- ViewAccountStatisticsTotalBalanceByStatus
IF OBJECT_ID('ViewAccountStatisticsTotalBalanceByStatus', 'V') IS NOT NULL
    DROP VIEW ViewAccountStatisticsTotalBalanceByStatus;
GO
CREATE VIEW ViewAccountStatisticsTotalBalanceByStatus AS
SELECT
    acs.StatusName AS AccountStatus,
    SUM(acc.Balance) AS TotalBalance
FROM Accounts acc
JOIN AccountStatus acs ON acs.StatusId = acc.AccountStatus
GROUP BY acs.StatusName;
GO


-- ViewTransactionStatisticsCountByType
IF OBJECT_ID('ViewTransactionStatisticsCountByType', 'V') IS NOT NULL
    DROP VIEW ViewTransactionStatisticsCountByType;
GO
CREATE VIEW ViewTransactionStatisticsCountByType AS
SELECT
    tt.TypeName AS TransactionType,
    COUNT(tr.TransactionType) AS Count
FROM Transactions tr
JOIN TransactionType tt ON tt.TypeId = tr.TransactionType
GROUP BY tt.TypeName;
GO


-- ViewTransactionStatisticsCountByStatus
IF OBJECT_ID('ViewTransactionStatisticsCountByStatus', 'V') IS NOT NULL
    DROP VIEW ViewTransactionStatisticsCountByStatus;
GO
CREATE VIEW ViewTransactionStatisticsCountByStatus AS
SELECT
    ts.StatusName AS TransactionStatus,
    COUNT(tr.TransactionStatus) AS Count
FROM Transactions tr
JOIN TransactionStatus ts ON ts.StatusId = tr.TransactionStatus
GROUP BY ts.StatusName;
GO


-- ViewTransactionStatisticsTotalAmountByType
IF OBJECT_ID('ViewTransactionStatisticsTotalAmountByType', 'V') IS NOT NULL
    DROP VIEW ViewTransactionStatisticsTotalAmountByType;
GO
CREATE VIEW ViewTransactionStatisticsTotalAmountByType AS
SELECT
    tt.TypeName AS TransactionType,
    SUM(tr.Amount) AS TotalAmount
FROM Transactions tr
JOIN TransactionType tt ON tt.TypeId = tr.TransactionType
GROUP BY tt.TypeName;
GO


-- ViewTransactionStatisticsTotalAmountByStatus
IF OBJECT_ID('ViewTransactionStatisticsTotalAmountByStatus', 'V') IS NOT NULL
    DROP VIEW ViewTransactionStatisticsTotalAmountByStatus;
GO
CREATE VIEW ViewTransactionStatisticsTotalAmountByStatus AS
SELECT
    ts.StatusName AS TransactionStatus,
    SUM(tr.Amount) AS TotalAmount
FROM Transactions tr
JOIN TransactionStatus ts ON ts.StatusId = tr.TransactionStatus
GROUP BY ts.StatusName;
GO

