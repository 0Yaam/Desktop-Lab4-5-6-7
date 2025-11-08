CREATE DATABASE RestaurantManagement;
GO

USE RestaurantManagement;
GO

CREATE TABLE Category
(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	Name NVARCHAR(100) NOT NULL,
	Type INT
);
GO

CREATE TABLE Food
(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	Name NVARCHAR(100) NOT NULL,
	Unit NVARCHAR(20),
	FoodCategoryID INT,
	Price FLOAT,
	Notes NVARCHAR(200),

	FOREIGN KEY (FoodCategoryID) REFERENCES Category(ID)
);
GO

CREATE TABLE Bills
(
	ID INT IDENTITY PRIMARY KEY,
	Account NVARCHAR(100),
	CreatedDate DATETIME,
	Discount FLOAT,
	TotalAmount FLOAT
);
GO

CREATE TABLE BillDetails
(
	ID INT IDENTITY PRIMARY KEY,
	BillID INT,
	FoodID INT,
	Quantity INT,
	UnitPrice FLOAT,
	FOREIGN KEY (BillID) REFERENCES Bills(ID),
	FOREIGN KEY (FoodID) REFERENCES Food(ID)
);
GO

CREATE TABLE Account
(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	Username NVARCHAR(100) UNIQUE NOT NULL,
	DisplayName NVARCHAR(100),
	Password NVARCHAR(100) NOT NULL,
	GroupID INT,
	IsActive BIT DEFAULT 1
);
GO

CREATE TABLE Role
(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	RoleName NVARCHAR(100),
	Description NVARCHAR(200)
);
GO

CREATE TABLE AccountRole
(
	ID INT IDENTITY(1,1) PRIMARY KEY,
	AccountID INT,
	RoleID INT,
	IsActive BIT DEFAULT 1,
	FOREIGN KEY (AccountID) REFERENCES Account(ID),
	FOREIGN KEY (RoleID) REFERENCES Role(ID)
);
GO

CREATE TABLE TableFood (
	ID INT IDENTITY(1,1) PRIMARY KEY,
	Name NVARCHAR(100) NOT NULL,
	Status NVARCHAR(20) DEFAULT N'Trống'
);
GO

INSERT INTO Role (RoleName, Description) VALUES
('Admin', 'Quản lý toàn bộ hệ thống'),
('Staff', 'Nhân viên phục vụ');
GO


-- Admin
INSERT INTO Account (Username, DisplayName, Password, GroupID, IsActive)
VALUES ('admin', 'Quản trị viên', '123456', 1, 1);
GO

-- Staff
INSERT INTO Account (Username, DisplayName, Password, GroupID, IsActive)
VALUES ('staff', 'Nhân viên', '123456', 2, 1);
GO

-- Admin
INSERT INTO AccountRole (AccountID, RoleID, IsActive)
SELECT a.ID, r.ID, 1
FROM Account a
JOIN Role r ON r.RoleName='Admin'
WHERE a.Username='admin';
GO

-- Staff
INSERT INTO AccountRole (AccountID, RoleID, IsActive)
SELECT a.ID, r.ID, 1
FROM Account a
JOIN Role r ON r.RoleName='Staff'
WHERE a.Username='staff';
GO

INSERT INTO TableFood (Name, Status) VALUES
('Bàn 1', N'Trống'),
('Bàn 2', N'Trống');
GO

INSERT INTO Category (Name, [Type]) VALUES
(N'Đồ uống', NULL),
(N'Đồ ăn', NULL),
(N'Tráng miệng', NULL);
GO

INSERT INTO Food (Name, Unit, FoodCategoryID, Price, Notes) VALUES
(N'Cà phê đen', N'Ly', 2, 15000, N'Nóng'),
(N'Cà phê sữa', N'Ly', 2, 20000, N'Ngọt vừa'),
(N'Trà đá', N'Ly', 2, 10000, N''),
(N'Nước cam', N'Ly', 2, 25000, N'Tươi'),
(N'Sinh tố dâu', N'Ly', 2, 30000, N'Ngọt vừa');
GO

INSERT INTO Food (Name, Unit, FoodCategoryID, Price, Notes) VALUES
(N'Phở bò', N'Tô', 2, 50000, N'Nước dùng thơm ngon'),
(N'Mì xào', N'Tô', 2, 45000, N'Rau cải tươi'),
(N'Bún chả', N'Tô', 2, 55000, N'Mặn vừa'),
(N'Gà rán', N'Phần', 2, 60000, N'Giòn rụm');
GO

INSERT INTO Food (Name, Unit, FoodCategoryID, Price, Notes) VALUES
(N'Kem vani', N'Cốc', 3, 20000, N''),
(N'Bánh flan', N'Cái', 3, 25000, N'Mịn mượt'),
(N'Chè đậu xanh', N'Tô', 3, 15000, N''),
(N'Bánh mousse socola', N'Cái', 3, 35000, N'Ngọt vừa'),
(N'Trái cây dĩa', N'Dĩa', 3, 30000, N'Tươi ngon');
GO

DELETE FROM dbo.Account WHERE Username IN ('admin', 'staff');
