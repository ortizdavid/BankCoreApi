-- CustomerStatus
INSERT INTO CustomerStatus (StatusName, Description) VALUES 
('Active', 'Customer is currently active and in good standing'),
('Inactive', 'Customer is not currently active'),
('Suspended', 'Customer account is temporarily suspended'),
('Closed', 'Customer account is permanently closed'),
('Pending', 'Customer application is pending approval');
GO

-- CustomerType
INSERT INTO CustomerType (TypeName, Description) VALUES 
('Individual', 'An individual customer'),
('Business', 'A business customer'),
('VIP', 'A very important customer with special privileges'),
('Non-Profit', 'A non-profit organization customer'),
('Government', 'A government entity customer');
GO

-- Account Status
INSERT INTO AccountStatus (StatusName, Description) VALUES
('Active', 'The account is open and fully functional.'),
('Inactive', 'The account has not been used for a specified period but is still open.'),
('Dormant', 'The account has been inactive for a longer period and may require specific actions to reactivate.'),
('Closed', 'The account has been permanently closed and cannot be used for transactions.'),
('Suspended', 'The account is temporarily restricted from performing transactions.'),
('Pending', 'The account is in the process of being opened or closed.'),
('Overdrawn', 'The account balance is negative due to overdrawing funds.'),
('Frozen', 'The account is restricted due to legal or regulatory actions.'),
('Restricted', 'The account has specific limitations placed on it.'),
('Verified', 'The account has been verified through a KYC process and is fully functional.'),
('Unverified', 'The account has not yet completed the KYC process and may have limitations on its usage.');
GO

-- Account Type
INSERT INTO AccountType (TypeName, Description) VALUES
('Savings', 'A savings account with interest accrual.'),
('Checking', 'A checking account for daily transactions.'),
('Business', 'An account designed for business use.'),
('Student', 'A student account with educational benefits.'),
('Joint', 'An account shared by two or more individuals.');
GO

-- Transaction Status
INSERT INTO TransactionStatus (StatusName, Description) VALUES 
('Pending', 'Transaction is pending processing'),
('Completed', 'Transaction has been completed successfully'),
('Aproved', 'Transaction has been approved by the system'),
('Failed', 'Transaction has failed'),
('Pending Approval', 'Transaction requires approval from an administrator or supervisor'),
('On Hold', 'Transaction is temporarily suspended or on hold for further review'),
('Expired', 'Transaction has expired and cannot be processed'),
('Cancelled', 'Transaction was cancelled before processing'),
('Refunded', 'Transaction amount was refunded to the customer');
GO

-- Transaction Type
INSERT INTO TransactionType (TypeName, Description) VALUES 
('Deposit', 'Deposit transaction'),
('Withdrawal', 'Withdrawal transaction'),
('Transfer', 'Transfer transaction'),
('Payment', 'Payment transaction'),
('Purchase', 'Purchase transaction'),
('Salary', 'Salary deposit transaction'),
('Expense', 'Expense withdrawal transaction'),
('Loan', 'Loan transaction'),
('Interest', 'Interest transaction');
GO

INSERT INTO Roles (RoleName, Description) VALUES 
('Administrator', 'Responsible for overall system administration and maintenance.'),
('BranchManager', 'Manages the operations of a specific bank branch.'),
('CustomerServiceRepresentative', 'Assists customers with their banking needs and queries.'),
('LoanOfficer', 'Evaluates, authorizes, or recommends approval of loan applications.'),
('Teller', 'Handles customer transactions, including deposits and withdrawals.'),
('Accountant', 'Manages financial records and transactions.'),
('Auditor', 'Conducts audits to ensure compliance and accuracy in financial operations.'),
('ITSupport', 'Provides technical support and maintains IT systems.'),
('RiskManager', 'Identifies, assesses, and mitigates risks for the bank.'),
('MarketingManager', 'Develops and implements marketing strategies to attract customers.'),
('Customer', 'Regular customer of the bank using its services.');
GO


