CREATE DATABASE [PharmacyDB]

CREATE TABLE [Medicines](
[MedicineId] INT PRIMARY KEY IDENTITY (1,1),
[Name] VARCHAR (100) UNIQUE,
[Manufacturer] VARCHAR (100) NOT NULL,
[Price] DECIMAL (10,2) NOT NULL,
[QuantityInStock] INT NOT NULL
)
CREATE TABLE [Employees](
[EmployeeId] INT PRIMARY KEY IDENTITY (1,1),
[Name] VARCHAR (100),
[Position] VARCHAR (50),
[Salary] DECIMAL (10,2)
)
CREATE TABLE [Prescriptions](
[Prescription] INT PRIMARY KEY IDENTITY (1,1),
[MedicineId] INT FOREIGN KEY REFERENCES Medicines(MedicineId),
[DoctorName] VARCHAR (100) NOT NULL,
[PatientName] VARCHAR (100) NOT NULL,
[DateIssued] DATE NOT NULL,
[EmployeeId] INT FOREIGN KEY REFERENCES Employees(EmployeeId)
)
CREATE TABLE [Orders](
[OrderId] INT PRIMARY KEY IDENTITY (1,1),
[MedicineId] INT FOREIGN KEY REFERENCES Medicines(MedicineId),
[SupplierName] VARCHAR (100) NOT NULL,
[OrderDate] DATE NOT NULL,
[QuantityOrdered] int not null,
[EmployeeId] INT FOREIGN KEY REFERENCES Employees(EmployeeId)
)

INSERT INTO Employees (Name, Position, Salary) VALUES
('Elena Petrova', 'Pharmacist', 2500.00),
('Ivan Ivanov', 'Cashier', 1200.00),
('Mihail Hristov', 'Manager', 3500.00);

INSERT INTO Medicines (Name, Manufacturer, Price, QuantityInStock) VALUES
('Paracetamol', 'Bayer', 5.50, 100),
('Ibuprofen', 'Pfizer', 8.20, 150),
('Aspirin', 'Novartis', 4.75, 200);

INSERT INTO Prescriptions (MedicineId, DoctorName, PatientName, DateIssued, EmployeeId) VALUES
(1, 'Dr. Ivan Petrov', 'Georgi Dimitrov', '2025-03-10', 1),
(2, 'Dr. Maria Nikolova', 'Anna Ivanova', '2025-03-12', 1),
(3, 'Dr. Peter Stoyanov', 'Nikolay Georgiev', '2025-03-14', 2);

INSERT INTO Orders (MedicineId, SupplierName, OrderDate, QuantityOrdered, EmployeeId) VALUES
(1, 'PharmaSupply Ltd.', '2025-03-01', 500, 3),
(2, 'MedExpress', '2025-03-05', 300, 3),
(3, 'GlobalPharm', '2025-03-08', 400, 2);

