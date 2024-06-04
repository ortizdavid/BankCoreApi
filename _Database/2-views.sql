-- view_customer_data
DROP VIEW IF EXISTS view_customer_data;
CREATE VIEW view_customer_data AS 
SELECT * FROM customers;


-- view_account_data
DROP VIEW IF EXISTS view_account_data;
CREATE VIEW view_account_data AS 
SELECT * FROM accounts;


-- view_transaction_data
DROP VIEW IF EXISTS view_transaction_data;
CREATE VIEW view_transaction_data AS 
SELECT * FROM transactions;


