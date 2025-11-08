Create database RestaurantManagement

USE [RestaurantManagement]
GO

drop table  Account
drop table RoleAccount
drop table Rolee
drop table Ban
drop table Bills
drop table BillDetails
drop table Category
drop table Food

select * from  Account
select * from RoleAccount
select * from Rolee
select * from Ban
select * from Bills
select * from BillDetails
select * from Category
select * from Food



select * from Account
select * from RoleAccount
select * from Rolee
Select Account.AccountName, Password, FullName, Email, Tell, DateCreated, Actived from Account, RoleAccount where Account.AccountName = RoleAccount.AccountName and Actived = 0
select c.AccountName, c.Password, c.FullName, c.Email, c.Tell, c.DateCreated, a.Actived from RoleAccount a, Rolee b, Account c  where a.RoleID = b.ID and a.AccountName = c.AccountName and b.RoleName = N'Thu ngân'

/****** Object:  Table [dbo].[Account]    Script Date: 10/11/2025 12:50:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[fn_TinhTongTien] (@BillID INT)
RETURNS DECIMAL(18,2)
AS
BEGIN
    DECLARE @TongTien DECIMAL(18,2);

    SELECT @TongTien = SUM(f.Price * bd.Quantity)
    FROM BillDetails bd
    JOIN Food f ON bd.FoodID = f.ID
    WHERE bd.InvoiceID = @BillID;

    RETURN ISNULL(@TongTien, 0);
END;
GO
/****** Object:  Table [dbo].[Account]    Script Date: 10/25/2025 11:20:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[AccountName] [nvarchar](100) NOT NULL,
	[Password] [nvarchar](200) NOT NULL,
	[FullName] [nvarchar](1000) NOT NULL,
	[Email] [nvarchar](1000) NULL,
	[Tell] [nvarchar](200) NULL,
	[DateCreated] [smalldatetime] NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[AccountName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ban]    Script Date: 10/25/2025 11:20:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ban](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](1000) NULL,
	[Status] [int] NOT NULL,
	[Capacity] [int] NULL,
 CONSTRAINT [PK_Ban] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BillDetails]    Script Date: 10/25/2025 11:20:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BillDetails](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceID] [int] NOT NULL,
	[FoodID] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
 CONSTRAINT [PK_BillDetails] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bills]    Script Date: 10/25/2025 11:20:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bills](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](1000) NOT NULL,
	[TableID] [int] NOT NULL,
	[Amount] [int] NOT NULL,
	[Discount] [float] NULL,
	[Tax] [float] NULL,
	[Status] [bit] NOT NULL,
	[CheckoutDate] [smalldatetime] NULL,
	[Account] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Bills] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 10/25/2025 11:20:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](1000) NOT NULL,
	[Type] [int] NOT NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Food]    Script Date: 10/25/2025 11:20:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Food](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](1000) NOT NULL,
	[Unit] [nvarchar](100) NOT NULL,
	[FoodCategoryID] [int] NOT NULL,
	[Price] [int] NOT NULL,
	[Notes] [nvarchar](3000) NULL,
 CONSTRAINT [PK_Food] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleAccount]    Script Date: 10/25/2025 11:20:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleAccount](
	[RoleID] [int] NOT NULL,
	[AccountName] [nvarchar](100) NOT NULL,
	[Actived] [bit] NOT NULL,
	[Notes] [nvarchar](3000) NULL,
 CONSTRAINT [PK_RoleAccount] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC,
	[AccountName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rolee]    Script Date: 10/25/2025 11:20:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rolee](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](1000) NOT NULL,
	[Path] [nvarchar](3000) NULL,
	[Notes] [nvarchar](3000) NULL,
 CONSTRAINT [PK_Rolee] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Account] ([AccountName], [Password], [FullName], [Email], [Tell], [DateCreated]) VALUES (N'Đạt', N'NhaHang123', N'Nguyễn Hiếu Đạt', N'dat@gmail.com', N'0123456789', CAST(N'2025-11-11T00:00:00' AS SmallDateTime))
INSERT [dbo].[Account] ([AccountName], [Password], [FullName], [Email], [Tell], [DateCreated]) VALUES (N'Hiệp', N'Hiep123', N'Nguyễn Trung Hiệp', N'Hiep@gmail.com', N'0987654321', CAST(N'2023-01-01T00:00:00' AS SmallDateTime))
INSERT [dbo].[Account] ([AccountName], [Password], [FullName], [Email], [Tell], [DateCreated]) VALUES (N'Hoàng', N'NhaHang123', N'Như Lâm', N'lam@gmail.com', N'0234567891', NULL)
INSERT [dbo].[Account] ([AccountName], [Password], [FullName], [Email], [Tell], [DateCreated]) VALUES (N'khang', N'NhaHang123', N'Như Lâm', N'lam@gmail.com', N'0345678912', CAST(N'2025-10-24T00:51:00' AS SmallDateTime))
INSERT [dbo].[Account] ([AccountName], [Password], [FullName], [Email], [Tell], [DateCreated]) VALUES (N'Lâm', N'Lam123', N'Như Lâm', N'lam@gmail.com', N'0456789123', CAST(N'2025-10-24T00:37:00' AS SmallDateTime))
INSERT [dbo].[Account] ([AccountName], [Password], [FullName], [Email], [Tell], [DateCreated]) VALUES (N'Minh', N'NhaHang123', N'Như Lâm', N'lam@gmail.com', N'0567891234', CAST(N'2024-02-02T00:00:00' AS SmallDateTime))
INSERT [dbo].[Account] ([AccountName], [Password], [FullName], [Email], [Tell], [DateCreated]) VALUES (N'Nam', N'NhaHang123', N'Như Lâm', N'lam@gmail.com', N'0678912345', NULL)
INSERT [dbo].[Account] ([AccountName], [Password], [FullName], [Email], [Tell], [DateCreated]) VALUES (N'Quân', N'NhaHang123', N'Như Lâm', N'lam@gmail.com', N'0789123456', CAST(N'2025-10-24T00:30:00' AS SmallDateTime))
INSERT [dbo].[Account] ([AccountName], [Password], [FullName], [Email], [Tell], [DateCreated]) VALUES (N'Thiên', N'NhaHang123', N'Như Lâm', N'lam@gmail.com', N'0891234567', CAST(N'2025-10-24T00:47:00' AS SmallDateTime))
INSERT [dbo].[Account] ([AccountName], [Password], [FullName], [Email], [Tell], [DateCreated]) VALUES (N'Tuấn', N'Tuan123', N'Như Lâm', N'lam@gmail.com', N'0912345678', CAST(N'2021-11-01T00:00:00' AS SmallDateTime))
GO
SET IDENTITY_INSERT [dbo].[Ban] ON 

INSERT [dbo].[Ban] ([ID], [Name], [Status], [Capacity]) VALUES (1, N'Table 1', 1, 4)
INSERT [dbo].[Ban] ([ID], [Name], [Status], [Capacity]) VALUES (2, N'Table 2', 1, 3)
INSERT [dbo].[Ban] ([ID], [Name], [Status], [Capacity]) VALUES (3, N'Table 3', 1, 1)
INSERT [dbo].[Ban] ([ID], [Name], [Status], [Capacity]) VALUES (4, N'Table 4', 0, 6)
INSERT [dbo].[Ban] ([ID], [Name], [Status], [Capacity]) VALUES (5, N'Table 5', 0, 15)
SET IDENTITY_INSERT [dbo].[Ban] OFF
GO
SET IDENTITY_INSERT [dbo].[BillDetails] ON 

INSERT [dbo].[BillDetails] ([ID], [InvoiceID], [FoodID], [Quantity]) VALUES (1, 1, 1, 20)
INSERT [dbo].[BillDetails] ([ID], [InvoiceID], [FoodID], [Quantity]) VALUES (2, 1, 4, 2)
INSERT [dbo].[BillDetails] ([ID], [InvoiceID], [FoodID], [Quantity]) VALUES (3, 1, 5, 2)
INSERT [dbo].[BillDetails] ([ID], [InvoiceID], [FoodID], [Quantity]) VALUES (4, 1, 6, 3)
INSERT [dbo].[BillDetails] ([ID], [InvoiceID], [FoodID], [Quantity]) VALUES (5, 2, 3, 5)
INSERT [dbo].[BillDetails] ([ID], [InvoiceID], [FoodID], [Quantity]) VALUES (6, 2, 5, 1)
INSERT [dbo].[BillDetails] ([ID], [InvoiceID], [FoodID], [Quantity]) VALUES (7, 3, 2, 2)
INSERT [dbo].[BillDetails] ([ID], [InvoiceID], [FoodID], [Quantity]) VALUES (8, 3, 5, 4)
SET IDENTITY_INSERT [dbo].[BillDetails] OFF
GO
SET IDENTITY_INSERT [dbo].[Bills] ON 

INSERT [dbo].[Bills] ([ID], [Name], [TableID], [Amount], [Discount], [Tax], [Status], [CheckoutDate], [Account]) VALUES (1, N'Tuấn', 1, 630200, 10, 5, 1, CAST(N'2025-02-07T00:00:00' AS SmallDateTime), N'Tuấn')
INSERT [dbo].[Bills] ([ID], [Name], [TableID], [Amount], [Discount], [Tax], [Status], [CheckoutDate], [Account]) VALUES (2, N'Hiếu', 2, 110000, NULL, 5, 0, CAST(N'2025-03-21T00:00:00' AS SmallDateTime), N'Hiếu')
INSERT [dbo].[Bills] ([ID], [Name], [TableID], [Amount], [Discount], [Tax], [Status], [CheckoutDate], [Account]) VALUES (3, N'Hiệp', 3, 440000, 5, 5, 1, CAST(N'2025-01-11T00:00:00' AS SmallDateTime), N'Hiệp')
INSERT [dbo].[Bills] ([ID], [Name], [TableID], [Amount], [Discount], [Tax], [Status], [CheckoutDate], [Account]) VALUES (5, N'Minh', 1, 60000, 2, 5, 0, CAST(N'2025-01-11T00:00:00' AS SmallDateTime), N'Minh')
SET IDENTITY_INSERT [dbo].[Bills] OFF
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([ID], [Name], [Type]) VALUES (1, N'Món rau', 1)
INSERT [dbo].[Category] ([ID], [Name], [Type]) VALUES (2, N'Món thịt', 1)
INSERT [dbo].[Category] ([ID], [Name], [Type]) VALUES (3, N'Món tinh bột', 1)
INSERT [dbo].[Category] ([ID], [Name], [Type]) VALUES (4, N'Nước ngọt', 0)
INSERT [dbo].[Category] ([ID], [Name], [Type]) VALUES (5, N'Món cồn', 0)
INSERT [dbo].[Category] ([ID], [Name], [Type]) VALUES (6, N'Trái cây', 1)
INSERT [dbo].[Category] ([ID], [Name], [Type]) VALUES (7, N'Suối', 0)
INSERT [dbo].[Category] ([ID], [Name], [Type]) VALUES (11, N'Bánh', 1)
INSERT [dbo].[Category] ([ID], [Name], [Type]) VALUES (12, N'Kẹo', 1)
INSERT [dbo].[Category] ([ID], [Name], [Type]) VALUES (13, N'Cà phê', 0)
INSERT [dbo].[Category] ([ID], [Name], [Type]) VALUES (14, N'Trà', 0)
INSERT [dbo].[Category] ([ID], [Name], [Type]) VALUES (15, N'Sữa', 0)
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[Food] ON 

INSERT [dbo].[Food] ([ID], [Name], [Unit], [FoodCategoryID], [Price], [Notes]) VALUES (1, N'Bia', N'lon', 5, 24000, N'Cấm trẻ dưới 18')
INSERT [dbo].[Food] ([ID], [Name], [Unit], [FoodCategoryID], [Price], [Notes]) VALUES (2, N'Rượu', N'lít', 5, 100000, N'Bán khách quen')
INSERT [dbo].[Food] ([ID], [Name], [Unit], [FoodCategoryID], [Price], [Notes]) VALUES (3, N'Cháo trắng', N'tô', 3, 10000, N'Bán thêm')
INSERT [dbo].[Food] ([ID], [Name], [Unit], [FoodCategoryID], [Price], [Notes]) VALUES (4, N'Rau trộn', N'dĩa', 1, 100, N'chua')
INSERT [dbo].[Food] ([ID], [Name], [Unit], [FoodCategoryID], [Price], [Notes]) VALUES (5, N'Bò nướng tảng', N'dĩa', 2, 60000, NULL)
INSERT [dbo].[Food] ([ID], [Name], [Unit], [FoodCategoryID], [Price], [Notes]) VALUES (6, N'Nước suối', N'chai', 4, 10000, NULL)
INSERT [dbo].[Food] ([ID], [Name], [Unit], [FoodCategoryID], [Price], [Notes]) VALUES (7, N'coca', N'lon', 4, 15500, N'Ngon hon khi uong voi da.')
INSERT [dbo].[Food] ([ID], [Name], [Unit], [FoodCategoryID], [Price], [Notes]) VALUES (8, N'pepsi', N'lon', 4, 15000, N'Ngon hon khi uong voi da.')
INSERT [dbo].[Food] ([ID], [Name], [Unit], [FoodCategoryID], [Price], [Notes]) VALUES (9, N'Đăng', N'con', 2, 5000, N'chấm nước mắm')
INSERT [dbo].[Food] ([ID], [Name], [Unit], [FoodCategoryID], [Price], [Notes]) VALUES (10, N'Rau xao', N'dĩa', 1, 100, N'man')
SET IDENTITY_INSERT [dbo].[Food] OFF
GO
INSERT [dbo].[RoleAccount] ([RoleID], [AccountName], [Actived], [Notes]) VALUES (1, N'Minh', 1, N'PartTime')
INSERT [dbo].[RoleAccount] ([RoleID], [AccountName], [Actived], [Notes]) VALUES (2, N'Hiệp', 1, N'Làm lâu')
INSERT [dbo].[RoleAccount] ([RoleID], [AccountName], [Actived], [Notes]) VALUES (2, N'Thiên', 1, N'Test lần 1')
INSERT [dbo].[RoleAccount] ([RoleID], [AccountName], [Actived], [Notes]) VALUES (3, N'khang', 1, N'sgfd')
INSERT [dbo].[RoleAccount] ([RoleID], [AccountName], [Actived], [Notes]) VALUES (3, N'Minh', 0, N'PartTime')
INSERT [dbo].[RoleAccount] ([RoleID], [AccountName], [Actived], [Notes]) VALUES (3, N'Quân', 1, NULL)
INSERT [dbo].[RoleAccount] ([RoleID], [AccountName], [Actived], [Notes]) VALUES (4, N'Tuấn', 1, NULL)
INSERT [dbo].[RoleAccount] ([RoleID], [AccountName], [Actived], [Notes]) VALUES (5, N'Hiệp', 0, NULL)
INSERT [dbo].[RoleAccount] ([RoleID], [AccountName], [Actived], [Notes]) VALUES (5, N'Minh', 0, NULL)
INSERT [dbo].[RoleAccount] ([RoleID], [AccountName], [Actived], [Notes]) VALUES (5, N'Tuấn', 0, NULL)
INSERT [dbo].[RoleAccount] ([RoleID], [AccountName], [Actived], [Notes]) VALUES (7, N'Nam', 1, NULL)
GO
SET IDENTITY_INSERT [dbo].[Rolee] ON 

INSERT [dbo].[Rolee] ([ID], [RoleName], [Path], [Notes]) VALUES (1, N'Phục vụ', N'v', N'Dọn bàn và bưng món')
INSERT [dbo].[Rolee] ([ID], [RoleName], [Path], [Notes]) VALUES (2, N'Bếp', N'v', N'Nấu ăn')
INSERT [dbo].[Rolee] ([ID], [RoleName], [Path], [Notes]) VALUES (3, N'Kiểm thực', N'v', N'Kiểm tra và trang trí món ăn')
INSERT [dbo].[Rolee] ([ID], [RoleName], [Path], [Notes]) VALUES (4, N'Order', N'v', N'Gọi món')
INSERT [dbo].[Rolee] ([ID], [RoleName], [Path], [Notes]) VALUES (5, N'Thu ngân', N'v', N'Thu tiền và in hóa đơn')
INSERT [dbo].[Rolee] ([ID], [RoleName], [Path], [Notes]) VALUES (6, N'Bảo vệ', NULL, N'Giữ và dắt xe cho khách')
INSERT [dbo].[Rolee] ([ID], [RoleName], [Path], [Notes]) VALUES (7, N'Vệ sinh', N'v', N'Lau và quét')
SET IDENTITY_INSERT [dbo].[Rolee] OFF
GO
ALTER TABLE [dbo].[BillDetails]  WITH CHECK ADD  CONSTRAINT [FK_BillDetails_Bills] FOREIGN KEY([InvoiceID])
REFERENCES [dbo].[Bills] ([ID])
GO
ALTER TABLE [dbo].[BillDetails] CHECK CONSTRAINT [FK_BillDetails_Bills]
GO
ALTER TABLE [dbo].[BillDetails]  WITH CHECK ADD  CONSTRAINT [FK_BillDetails_Food] FOREIGN KEY([FoodID])
REFERENCES [dbo].[Food] ([ID])
GO
ALTER TABLE [dbo].[BillDetails] CHECK CONSTRAINT [FK_BillDetails_Food]
GO
ALTER TABLE [dbo].[Bills]  WITH CHECK ADD  CONSTRAINT [FK_Bills_Ban] FOREIGN KEY([TableID])
REFERENCES [dbo].[Ban] ([ID])
GO
ALTER TABLE [dbo].[Bills] CHECK CONSTRAINT [FK_Bills_Ban]
GO
ALTER TABLE [dbo].[Food]  WITH CHECK ADD  CONSTRAINT [FK_Food_Category] FOREIGN KEY([FoodCategoryID])
REFERENCES [dbo].[Category] ([ID])
GO
ALTER TABLE [dbo].[Food] CHECK CONSTRAINT [FK_Food_Category]
GO
ALTER TABLE [dbo].[RoleAccount]  WITH CHECK ADD  CONSTRAINT [FK_RoleAccount_Account] FOREIGN KEY([AccountName])
REFERENCES [dbo].[Account] ([AccountName])
GO
ALTER TABLE [dbo].[RoleAccount] CHECK CONSTRAINT [FK_RoleAccount_Account]
GO
ALTER TABLE [dbo].[RoleAccount]  WITH CHECK ADD  CONSTRAINT [FK_RoleAccount_Rolee] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Rolee] ([ID])
GO
ALTER TABLE [dbo].[RoleAccount] CHECK CONSTRAINT [FK_RoleAccount_Rolee]
GO

Select * from Bills
Select * from BillDetails
SELECT RoleName FROM Rolee, RoleAccount WHERE RoleID = ID and AccountName = N'Hiệp'

select sum(Amount + (Amount*Tax/100) - (Amount * Discount / 100)) from Bills where CheckoutDate = N'1/11/2025' and Account = N'Hiệp'

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[pList]
	@ngay datetime
as
	select c.Name as [Tên khách], c.TableID, b.Name as [Tên món], a.Quantity, b.Price, a.Quantity*b.Price as [Thành tiền], c.CheckoutDate 
	from BillDetails a, Food b, Bills c  
	where a.FoodID = b.ID and a.InvoiceID = c.ID and c.CheckoutDate = @ngay
GO


/****** Object:  StoredProcedure [dbo].[Account_Delete]    Script Date: 10/11/2025 12:50:53 PM ******/

CREATE PROCEDURE [dbo].[Account_Delete]
(
    @Name nvarchar(100)
)
AS
BEGIN
    set nocount on
    if exists (select * from RoleAccount where AccountName = @Name)
    begin
        print N'Không thể xóa vì tồn tại Name Account trong bảng RoleAccount'
        return;
    end
    DELETE FROM Account
    WHERE AccountName = @Name
END
GO
/****** Object:  StoredProcedure [dbo].[Account_Insert]    Script Date: 10/11/2025 12:50:53 PM ******/

create PROCEDURE [dbo].[Account_Insert]
(
    @name nvarchar(100), @password nvarchar(200), @fullname nvarchar(1000),
    @email nvarchar(1000) = NULL, @tell nvarchar(200) = NULL, @datecreated smalldatetime = NULL
)
as
begin
    if not exists (select * from Account where AccountName = @name)
    begin
        insert into Account(AccountName, Password, FullName, Email, Tell, DateCreated)
        values (@name, @password, @fullname, @email, @tell, @datecreated)
    end
    else
    begin
        raiserror(N'Da ton tai account name.', 16, 1)
        return;
    end
end
GO
/****** Object:  StoredProcedure [dbo].[Account_Update]    Script Date: 10/11/2025 12:50:53 PM ******/

CREATE PROCEDURE [dbo].[Account_Update]
    @AccountName NVARCHAR(100), @Password NVARCHAR(200), @FullName NVARCHAR(1000),
    @Email NVARCHAR(1000) = NULL, @Tell NVARCHAR(200) = NULL, @DateCreated SMALLDATETIME = NULL
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT * FROM Account WHERE AccountName = @AccountName)
    BEGIN
        RAISERROR(N'Tài khoản không tồn tại', 16, 1);
        RETURN;
    END
    UPDATE Account
    SET Password = @Password, FullName = @FullName, Email = @Email,
        Tell = @Tell, DateCreated = @DateCreated
    WHERE AccountName = @AccountName;
END
GO
/****** Object:  StoredProcedure [dbo].[AddRoleWithAssign]    Script Date: 10/11/2025 12:50:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddRoleWithAssign]
    @RoleName NVARCHAR(100),
    @Path NVARCHAR(50) = NULL,
    @Notes NVARCHAR(255) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NewRoleID INT;

    -- 1. Thêm Role mới
    INSERT INTO Rolee(RoleName, Path, Notes)
    VALUES (@RoleName, @Path, @Notes);

    SET @NewRoleID = SCOPE_IDENTITY();

    -- 2. Gán Role này cho tất cả User hiện có (Active = 0, Notes = NULL)
    INSERT INTO RoleAccount(RoleID, AccountName, Actived, Notes)
    SELECT @NewRoleID, u.AccountName, 0, NULL
    FROM (SELECT DISTINCT AccountName FROM RoleAccount) u;
END;
GO

CREATE PROCEDURE [dbo].[Ban_Delete]
    @ID INT
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT * FROM Ban WHERE ID = @ID)
    BEGIN
        RAISERROR(N'ID không tồn tại!', 16, 1)
        RETURN;
    END;
    if exists (select * from Bills where TableID = @ID)
    begin
        print N'Không thể xóa vì tồn tại ID table trong bảng Bills'
        return;
    end
    DELETE FROM Ban
    WHERE ID = @ID
END
GO
/****** Object:  StoredProcedure [dbo].[Ban_Insert]    Script Date: 10/11/2025 12:50:53 PM ******/
create PROCEDURE [dbo].[Ban_Insert]
(
    @id int output, @Name NVARCHAR(50), @Status INT, @Capacity INT                   
)
AS
BEGIN
    IF NOT EXISTS (SELECT * FROM Ban WHERE Name = @Name)
    begin
        INSERT INTO Ban(Name, Status, Capacity)
        VALUES (@Name, @Status, @Capacity)

        SET @ID = @@IDENTITY
    end
    else
    begin
        RAISERROR(N'Da ton tai ten ban.', 16, 1)
        RETURN;
    end
END
GO
/****** Object:  StoredProcedure [dbo].[Ban_Update]    Script Date: 10/11/2025 12:50:53 PM ******/
CREATE PROCEDURE [dbo].[Ban_Update]
    @ID INT,
    @Name NVARCHAR(1000) = NULL,
    @Status INT,
    @Capacity INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE Ban
    SET Name = @Name, Status = @Status, Capacity = @Capacity
    WHERE ID = @ID;
END
GO
/****** Object:  StoredProcedure [dbo].[BillDetails_Delete]    Script Date: 10/11/2025 12:50:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[BillDetails_Delete]
(
    @IDivc INT, @IDfd INT
)
AS
BEGIN
    set nocount on
    if exists (select * from BillDetails where InvoiceID = @IDivc and FoodID = @IDfd)
    begin
        DELETE FROM BillDetails
        WHERE InvoiceID = @IDivc and FoodID = @IDfd
    end
    else
    begin
        print N'Không tìm thấy InvoiceID và FoodID trong bảng BillDetails'
        return;
    end
END
GO
/****** Object:  StoredProcedure [dbo].[BillDetails_Insert]    Script Date: 10/11/2025 12:50:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[BillDetails_Insert]
(
    @id int output,            
    @invoiceid int, @foodid int, @quantity int                
)
as
begin
    if exists (select * from Bills where ID = @invoiceid)
    begin
        if exists (select * from Food where ID = @foodid)
        begin
            insert into BillDetails(InvoiceID, FoodID, Quantity)
            values (@invoiceid, @foodid, @quantity)

            SET @ID = @@IDENTITY
        end
        else
        begin
            raiserror(N'Khong ton tai food id', 16, 1)
            return;
        end 
    end
    else
    begin
        raiserror(N'Khong ton tai bill id', 16, 1)
        return;
    end
end
GO
/****** Object:  StoredProcedure [dbo].[BillDetails_Update]    Script Date: 10/11/2025 12:50:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[BillDetails_Update]
    @ID INT, @InvoiceID INT, @FoodID INT, @Quantity INT
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT * FROM Bills WHERE ID = @InvoiceID)
    BEGIN
        RAISERROR(N'InvoiceID không tồn tại', 16, 1);
        RETURN;
    END
    IF NOT EXISTS (SELECT * FROM Food WHERE ID = @FoodID)
    BEGIN
        RAISERROR(N'FoodID không tồn tại', 16, 1);
        RETURN;
    END
    UPDATE BillDetails
    SET InvoiceID = @InvoiceID, FoodID = @FoodID, Quantity = @Quantity
    WHERE ID = @ID;
END
GO
/****** Object:  StoredProcedure [dbo].[Bills_Delete]    Script Date: 10/11/2025 12:50:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Bills_Delete]
    @ID INT
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT * FROM Bills WHERE ID = @ID)
    BEGIN
        PRINT N'Không tìm thấy hóa đơn ID'
        return;
    END
    IF EXISTS (SELECT * FROM BillDetails WHERE InvoiceID = @ID)
    BEGIN
        PRINT N'Không thể xóa vì tồn tại ID invoice trong bảng BillDetails'
        return;
    END
    DELETE FROM Bills
        WHERE ID = @ID;
END;
GO
/****** Object:  StoredProcedure [dbo].[Bills_Insert]    Script Date: 10/11/2025 12:50:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[Bills_Insert]
(
    @id int output, @Name nvarchar(1000), @tableid int, @amount int, 
    @discount float = NULL, @tax float = NULL, @status bit, @codate smalldatetime, @account nvarchar(1000)                 
)
AS
BEGIN
    IF EXISTS (SELECT * FROM Ban WHERE ID = @tableid)
    begin
        INSERT INTO Bills(Name, TableID, Amount, Discount, Tax, Status, CheckoutDate, Account)
        VALUES (@Name, @tableid, @amount, @discount, @tax, @status, @codate, @account)

        SET @ID = @@IDENTITY
    end
    else
    begin
        RAISERROR(N'Khong ton tai ID ban.', 16, 1)
        RETURN;
    end
END
GO
/****** Object:  StoredProcedure [dbo].[Bills_Update]    Script Date: 10/11/2025 12:50:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Bills_Update]
    @ID INT, @Name NVARCHAR(1000), @TableID INT, @Amount INT, @Discount FLOAT = NULL,
    @Tax FLOAT = NULL, @Status BIT, @CheckoutDate SMALLDATETIME = NULL, @Account NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    IF NOT EXISTS (SELECT * FROM Bills WHERE ID = @ID)
    BEGIN
        RAISERROR(N'ID kHONG TON TAI', 16, 1);
        RETURN;
    END;
    UPDATE Bills
    SET 
        Name = @Name, TableID = @TableID, Amount = @Amount, Discount = @Discount, Tax = @Tax,
        Status = @Status, CheckoutDate = @CheckoutDate, Account = @Account
    WHERE ID = @ID;
END
GO
/****** Object:  StoredProcedure [dbo].[Category_Delete]    Script Date: 10/11/2025 12:50:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Category_Delete]
(
    @ID INT
)
AS
BEGIN
    set nocount on
    if exists (select * from Food where ID = @ID)
    begin
        print N'Không thể xóa vì tồn tại ID Category trong bảng Food'
        return;
    end
    DELETE FROM Category
    WHERE ID = @ID
END
GO
/****** Object:  StoredProcedure [dbo].[Category_Insert]    Script Date: 10/11/2025 12:50:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[Category_Insert]
(
    @ID INT OUTPUT,            
    @Name NVARCHAR(1000),      
    @Type INT                   
)
AS
BEGIN
    IF NOT EXISTS (SELECT Name FROM Category WHERE Name = @Name)
        INSERT INTO Category (Name, Type)
        VALUES (@Name, @Type)

        SET @ID = @@IDENTITY
END
GO
/****** Object:  StoredProcedure [dbo].[Category_Update]    Script Date: 10/11/2025 12:50:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Category_Update]
    @ID INT, @Name NVARCHAR(1000), @Type INT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE Category
    SET 
        Name = @Name, Type = @Type
    WHERE ID = @ID;
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteByID]    Script Date: 10/11/2025 12:50:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[DeleteByID]
(
    @TableName nvarchar(200),  
    @id int                     
)
as
begin
    set nocount on
    declare @sql nvarchar(1000);

    if @TableName = N'Ban'
    begin
        if exists (select * from Bills where TableID = @id)
        begin
            print N'Tồn tại ID bàn trong bảng Bills'
            return;
        end
    end
    else if @TableName = N'Bills'
    begin
        if exists (select * from BillDetails where InvoiceID = @id)
        begin
            print N'Tồn tại ID bill trong bảng BillDetails'
            return;
        end
    end
    else if @TableName = N'Category'
    begin
        if exists (select * from Food where FoodCategoryID = @id)
        begin
            print N'Tồn tại ID category trong bảng Food'
            return;
        end
    end
    else if @TableName = N'Food'
    begin
        if exists (select * from BillDetails where FoodID = @id)
        begin
            print N'Tồn tại ID Food trong bảng BillDetails'
            return;
        end
    end
    else if @TableName = N'Rolee'
    begin
        if exists (select * from RoleAccount where RoleID = @id)
        begin
            print N'Tồn tại ID Role trong bảng RoleAccount'
            return;
        end
    end
    if @TableName = N'Account' or @TableName = N'RoleAccount'
    begin
        print N'Không có ID tăng tự động.'
        return;
    end

    set @sql = 'delete from ' + @TableName + ' where ID = ' + cast(@id as nvarchar);

    exec (@sql);
end;
GO
/****** Object:  StoredProcedure [dbo].[Food_Delete]    Script Date: 10/11/2025 12:50:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Food_Delete]
(
    @ID INT
)
AS
BEGIN
    set nocount on
    if exists (select * from BillDetails where FoodID = @ID)
    begin
        print N'Không thể xóa vì tồn tại ID Food trong bảng BillDetails'
        return;
    end
    DELETE FROM Food
    WHERE ID = @ID
END
GO
/****** Object:  StoredProcedure [dbo].[Food_Insert]    Script Date: 10/11/2025 12:50:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[Food_Insert]
(
    @id int output,            
    @name nvarchar(1000), @unit nvarchar(100), @fcid int,      
    @price INT, @notes nvarchar(3000) = NULL                   
)
AS
BEGIN
    IF EXISTS (SELECT * FROM Category WHERE ID = @fcid)
    begin
        INSERT INTO Food(Name, Unit, FoodCategoryID, Price, Notes)
        VALUES (@name, @unit, @fcid, @price, @notes)

        SET @ID = @@IDENTITY
    end
    else
    begin
        raiserror(N'Khong ton tai category id', 16, 1)
        return;
    end
END
GO

exec Food_Insert  0, N'Nuoc gi do', N'Cais', 4, 8000, N'Co mui'

select * from Category
select * from Food

/****** Object:  StoredProcedure [dbo].[Food_Update]    Script Date: 10/11/2025 12:50:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Food_Update]
    @ID INT, @Name NVARCHAR(1000), @Unit NVARCHAR(100),
    @FoodCategoryID INT, @Price INT, @Notes NVARCHAR(3000) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT * FROM Category WHERE ID = @FoodCategoryID)
    BEGIN
        RAISERROR(N'Category ID không tồn tại', 16, 1);
        RETURN;
    END
    UPDATE Food
    SET 
        Name = @Name, Unit = @Unit, FoodCategoryID = @FoodCategoryID,
        Price = @Price, Notes = @Notes
    WHERE ID = @ID;
END
GO

exec Food_Update 9, N'Nuoc co mui', N'Cais', 4, 10000, N'Khai'


/****** Object:  StoredProcedure [dbo].[GetAll]    Script Date: 10/11/2025 12:50:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[GetAll]
(
    @TableName nvarchar(200)
)
as
begin
    set nocount on
    declare @sql nvarchar(1000)
    set @sql = 'select * from ' + @TableName
    exec (@sql)
end
GO
/****** Object:  StoredProcedure [dbo].[GetByID]    Script Date: 10/11/2025 12:50:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[GetByID]
(
    @TableName nvarchar(200),  
    @id int                     
)
as
begin
    set nocount on
    declare @sql nvarchar(1000);

    set @sql = 'select * from ' + @TableName + ' where ID = ' + cast(@id as nvarchar);

    exec (@sql);
end;
GO
/****** Object:  StoredProcedure [dbo].[RoleAccount_Delete]    Script Date: 10/11/2025 12:50:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RoleAccount_Delete]
(
    @IDrl INT, @name nvarchar(100)
)
AS
BEGIN
    set nocount on
    if exists (select * from RoleAccount where RoleID = @IDrl and AccountName = @name)
    begin
        DELETE FROM RoleAccount
        WHERE RoleID = @IDrl and AccountName = @name
    end
    else
    begin
        print N'Không tìm thấy Role ID và Account Name'
        return;
    end
END
GO
/****** Object:  StoredProcedure [dbo].[RoleAccount_Insert]    Script Date: 10/11/2025 12:50:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[RoleAccount_Insert]
(
    @roleid int, @aname nvarchar(100), @actived bit, @notes nvarchar(3000) = NULL               
)
as
begin
    if exists (select * from Rolee where ID = @roleid)
    begin
        if exists (select * from Account where AccountName = @aname)
        begin
            insert into RoleAccount(RoleID, AccountName, Actived, Notes)
            values (@roleid, @aname, @actived, @notes)
        end
        else
        begin
            raiserror(N'Khong ton tai ten account', 16, 1)
            return;
        end 
    end
    else
    begin
        raiserror(N'Khong ton tai role id', 16, 1)
        return;
    end
end
GO
/****** Object:  StoredProcedure [dbo].[RoleAccount_Update]    Script Date: 10/11/2025 12:50:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RoleAccount_Update]
    @RoleID INT, @AccountName NVARCHAR(100),
    @IsActive BIT, @Notes NVARCHAR(3000) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT * FROM Rolee WHERE ID = @RoleID)
    BEGIN
        RAISERROR(N'RoleID không tồn tại', 16, 1);
        RETURN;
    END
    IF NOT EXISTS (SELECT * FROM Account WHERE AccountName = @AccountName)
    BEGIN
        RAISERROR(N'AccountName không tồn tại', 16, 1);
        RETURN;
    END
    UPDATE RoleAccount
    SET Actived = @IsActive, Notes = @Notes
    WHERE RoleID = @RoleID AND AccountName = @AccountName;
END
GO
/****** Object:  StoredProcedure [dbo].[Rolee_Delete]    Script Date: 10/11/2025 12:50:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Rolee_Delete]
(
    @ID INT
)
AS
BEGIN
    set nocount on
    if exists (select * from RoleAccount where RoleID = @ID)
    begin
        print N'Không thể xóa vì tồn tại ID Role trong bảng RoleAccount'
        return;
    end
    DELETE FROM Rolee
    WHERE ID = @ID
END
GO
/****** Object:  StoredProcedure [dbo].[Rolee_Insert]    Script Date: 10/11/2025 12:50:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[Rolee_Insert]
(
    @id int output,            
    @rname nvarchar(1000), @path nvarchar(3000) = NULL, 
    @notes nvarchar(3000) = NULL               
)
as
begin
    if not exists (select * from Rolee where RoleName = @rname)
    begin
        insert into Rolee(RoleName, Path, Notes)
        values (@rname, @path, @notes)

        SET @id = @@IDENTITY
    end
    else
    begin
        raiserror(N'Da ton tai ten role.', 16, 1)
        return;
    end
end
GO
exec Rolee_Insert
/****** Object:  StoredProcedure [dbo].[Rolee_Update]    Script Date: 10/11/2025 12:50:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Rolee_Update]
    @ID INT, @RoleName NVARCHAR(1000),
    @Path NVARCHAR(3000) = NULL,
    @Notes NVARCHAR(3000) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    IF NOT EXISTS (SELECT * FROM Rolee WHERE ID = @ID)
    BEGIN
        RAISERROR(N'Role ID không tồn tại', 16, 1);
        RETURN;
    END
    UPDATE Rolee
    SET RoleName = @RoleName, Path = @Path, Notes = @Notes
    WHERE ID = @ID;
END
GO


create function TimTheoNgay(@ngayBD smalldatetime, @ngayKT smalldatetime) returns table
as
return(
    Select *
    From Bills
    WHERE 
           TRY_CONVERT(DATE, @ngayBD, 103) <= TRY_CONVERT(DATE, CheckoutDate, 103) and 
              TRY_CONVERT(DATE, CheckoutDate, 103)  <= TRY_CONVERT(DATE, @ngayKT, 103)
)
go

drop function TimTheoNgay

Select * from TimTheoNgay(CAST(N'2025-08-21T00:00:00' AS SmallDateTime), CAST(N'2025-10-21T00:00:00' AS SmallDateTime))