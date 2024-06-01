\c postgres;
DROP DATABASE IF EXISTS bank_core_api;
CREATE DATABASE bank_core_api;
\c bank_core_api;


DROP TABLE IF EXISTS account_status;
CREATE TABLE account_status (
    status_id SERIAL PRIMARY KEY,
    status_name VARCHAR(15) UNIQUE NOT NULL,
    description VARCHAR(100)
);


DROP TABLE IF EXISTS savings_accounts;
CREATE TABLE savings_accounts (
    account_id SERIAL PRIMARY KEY,
    customer_id BIGINT NOT NULL,
    status_id INT NOT NULL,
    account_number VARCHAR(18) UNIQUE NOT NULL,
    iban VARCHAR(34) UNIQUE NOT NULL,
    balance DECIMAL(18, 2) NOT NULL DEFAULT 0.0,
    unique_id UUID DEFAULT gen_random_uuid(),
	created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP, 
	updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    CONSTRAINT fk_status_savings FOREIGN KEY(status_id) REFERENCES account_status(status_id)
    --,CONSTRAINT fk_customer_savings FOREIGN KEY(customer_id) REFERENCES customerS
);

