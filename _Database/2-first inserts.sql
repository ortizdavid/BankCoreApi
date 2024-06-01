INSERT INTO account_status (status_name, description) VALUES
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

