CREATE DATABASE EcommerceDB;
GO

USE EcommerceDB;
GO

-- Customers table
CREATE TABLE Customers (
    id INT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(255) UNIQUE NOT NULL,
    email NVARCHAR(255) UNIQUE NOT NULL,
    phone NVARCHAR(20),
    address NVARCHAR(500),
    registration_date DATETIME DEFAULT GETDATE()
);

-- Products table
CREATE TABLE Products (
    id INT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(255) UNIQUE NOT NULL,
    description NVARCHAR(1000),
    price FLOAT NOT NULL,
    stock_quantity INT NOT NULL
);

-- Orders table
CREATE TABLE Orders (
    id INT IDENTITY(1,1) PRIMARY KEY,
    customer_id INT NOT NULL,
    order_date DATETIME DEFAULT GETDATE(),
    status NVARCHAR(50) DEFAULT 'Pending',
    total_price FLOAT NOT NULL,
    order_code INT UNIQUE,
    FOREIGN KEY (customer_id) REFERENCES Customers(id) ON DELETE CASCADE
);

-- OrderItems table (Many-to-Many relationship between Orders and Products)
CREATE TABLE OrderItems (
    id INT IDENTITY(1,1) PRIMARY KEY,
    order_id INT NOT NULL,
    product_id INT NOT NULL,
    quantity INT NOT NULL,
    price FLOAT NOT NULL,
    FOREIGN KEY (order_id) REFERENCES Orders(id) ON DELETE CASCADE,
    FOREIGN KEY (product_id) REFERENCES Products(id) ON DELETE CASCADE
);

-- Payments table
CREATE TABLE Payments (
    id INT IDENTITY(1,1) PRIMARY KEY,
    order_id INT NOT NULL,
    payment_date DATETIME DEFAULT GETDATE(),
    amount FLOAT NOT NULL,
    payment_method NVARCHAR(50) NOT NULL,
    status NVARCHAR(50) DEFAULT 'Pending',
    FOREIGN KEY (order_id) REFERENCES Orders(id) ON DELETE CASCADE
);


-- test data 

INSERT INTO Customers (name, email, phone, address)
VALUES
('John Doe', 'johndoe@example.com', '123-456-7890', '123 Elm St, Springfield'),
('Jane Smith', 'janesmith@example.com', '987-654-3210', '456 Oak St, Shelbyville');

INSERT INTO Products (name, description, price, stock_quantity)
VALUES
('Laptop', 'High-performance laptop with 16GB RAM', 999.99, 50),
('Smartphone', 'Latest smartphone with 128GB storage', 699.99, 100);

INSERT INTO Orders (customer_id, order_date, status, total_price)
VALUES
(1, '2025-02-20', 'Pending', 1699.98),
(2, '2025-02-21', 'Shipped', 699.99);

INSERT INTO OrderItems (order_id, product_id, quantity, price)
VALUES
(1, 1, 1, 999.99),
(1, 2, 1, 699.99),
(2, 2, 1, 699.99);

INSERT INTO Payments (order_id, payment_date, amount, payment_method, status)
VALUES
(1, '2025-02-20', 1699.98, 'Credit Card', 'Completed'),
(2, '2025-02-21', 699.99, 'PayPal', 'Pending');
