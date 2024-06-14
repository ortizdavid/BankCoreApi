ALTER TABLE Customers ADD CustomerStatus INT;
ALTER TABLE Customers ADD CustomerType INT;
ALTER TABLE Customers ADD Gender VARCHAR(6) CHECK (Gender IN ('Male', 'Female'));
ALTER TABLE Customers ADD BirthDate DATE;
ALTER TABLE Customers ADD CONSTRAINT FK_CustomerStatus FOREIGN KEY (CustomerStatus) REFERENCES CustomerStatus(StatusId);
ALTER TABLE Customers ADD CONSTRAINT FK_CustomerType FOREIGN KEY (CustomerType) REFERENCES CustomerType(TypeId);