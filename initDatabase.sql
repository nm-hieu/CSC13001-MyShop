USE [master]
GO
/****** Object:  Database [MyShopDB]    Script Date: 03/01/2024 18:21:13 ******/
CREATE DATABASE [MyShopDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MyShopDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\MyShopDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MyShopDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\MyShopDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [MyShopDB] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MyShopDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MyShopDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MyShopDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MyShopDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MyShopDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MyShopDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [MyShopDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MyShopDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MyShopDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MyShopDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MyShopDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MyShopDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MyShopDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MyShopDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MyShopDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MyShopDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [MyShopDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MyShopDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MyShopDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MyShopDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MyShopDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MyShopDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MyShopDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MyShopDB] SET RECOVERY FULL 
GO
ALTER DATABASE [MyShopDB] SET  MULTI_USER 
GO
ALTER DATABASE [MyShopDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MyShopDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MyShopDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MyShopDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MyShopDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [MyShopDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'MyShopDB', N'ON'
GO
ALTER DATABASE [MyShopDB] SET QUERY_STORE = ON
GO
ALTER DATABASE [MyShopDB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [MyShopDB]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 03/01/2024 18:21:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [ntext] NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetails]    Script Date: 03/01/2024 18:21:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetails](
	[OrdinalNumber] [int] NOT NULL,
	[OrderID] [int] NOT NULL,
	[ProductID] [int] NULL,
 CONSTRAINT [PK_OrderDetails] PRIMARY KEY CLUSTERED 
(
	[OrdinalNumber] ASC,
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 03/01/2024 18:21:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[ID] [int] NOT NULL,
	[Date] [date] NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 03/01/2024 18:21:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ID] [int] NOT NULL,
	[Name] [ntext] NOT NULL,
	[Price] [int] NOT NULL,
	[Color] [ntext] NOT NULL,
	[Category] [int] NULL,
	[Image] [ntext] NULL,
	[AvailableQuantity] [int] NULL,
	[MarkUpPercent] [float] NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 03/01/2024 18:21:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[ID] [int] NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](100) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Role] [nvarchar](10) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Telephone] [nvarchar](11) NOT NULL,
	[Address] [nvarchar](100) NOT NULL,
	[Avatar] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([ID], [Name]) VALUES (1, N'Điện thoại')
INSERT [dbo].[Categories] ([ID], [Name]) VALUES (2, N'Tablet')
INSERT [dbo].[Categories] ([ID], [Name]) VALUES (3, N'Laptop')
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
INSERT [dbo].[OrderDetails] ([OrdinalNumber], [OrderID], [ProductID]) VALUES (1, 1, 21)
INSERT [dbo].[OrderDetails] ([OrdinalNumber], [OrderID], [ProductID]) VALUES (1, 2, 7)
INSERT [dbo].[OrderDetails] ([OrdinalNumber], [OrderID], [ProductID]) VALUES (1, 3, 18)
INSERT [dbo].[OrderDetails] ([OrdinalNumber], [OrderID], [ProductID]) VALUES (1, 4, 10)
INSERT [dbo].[OrderDetails] ([OrdinalNumber], [OrderID], [ProductID]) VALUES (1, 5, 20)
INSERT [dbo].[OrderDetails] ([OrdinalNumber], [OrderID], [ProductID]) VALUES (1, 6, 9)
INSERT [dbo].[OrderDetails] ([OrdinalNumber], [OrderID], [ProductID]) VALUES (1, 7, 17)
INSERT [dbo].[OrderDetails] ([OrdinalNumber], [OrderID], [ProductID]) VALUES (1, 8, 30)
INSERT [dbo].[OrderDetails] ([OrdinalNumber], [OrderID], [ProductID]) VALUES (1, 9, 12)
INSERT [dbo].[OrderDetails] ([OrdinalNumber], [OrderID], [ProductID]) VALUES (1, 10, 7)
INSERT [dbo].[OrderDetails] ([OrdinalNumber], [OrderID], [ProductID]) VALUES (1, 11, 23)
INSERT [dbo].[OrderDetails] ([OrdinalNumber], [OrderID], [ProductID]) VALUES (1, 12, 12)
INSERT [dbo].[OrderDetails] ([OrdinalNumber], [OrderID], [ProductID]) VALUES (1, 13, 35)
INSERT [dbo].[OrderDetails] ([OrdinalNumber], [OrderID], [ProductID]) VALUES (1, 14, 36)
INSERT [dbo].[OrderDetails] ([OrdinalNumber], [OrderID], [ProductID]) VALUES (1, 15, 35)
INSERT [dbo].[OrderDetails] ([OrdinalNumber], [OrderID], [ProductID]) VALUES (1, 16, 6)
INSERT [dbo].[OrderDetails] ([OrdinalNumber], [OrderID], [ProductID]) VALUES (2, 1, 30)
INSERT [dbo].[OrderDetails] ([OrdinalNumber], [OrderID], [ProductID]) VALUES (2, 4, 2)
INSERT [dbo].[OrderDetails] ([OrdinalNumber], [OrderID], [ProductID]) VALUES (2, 5, 32)
INSERT [dbo].[OrderDetails] ([OrdinalNumber], [OrderID], [ProductID]) VALUES (2, 9, 22)
INSERT [dbo].[OrderDetails] ([OrdinalNumber], [OrderID], [ProductID]) VALUES (2, 10, 12)
INSERT [dbo].[OrderDetails] ([OrdinalNumber], [OrderID], [ProductID]) VALUES (2, 13, 34)
INSERT [dbo].[OrderDetails] ([OrdinalNumber], [OrderID], [ProductID]) VALUES (2, 14, 30)
INSERT [dbo].[OrderDetails] ([OrdinalNumber], [OrderID], [ProductID]) VALUES (2, 15, 29)
INSERT [dbo].[OrderDetails] ([OrdinalNumber], [OrderID], [ProductID]) VALUES (3, 1, 23)
INSERT [dbo].[OrderDetails] ([OrdinalNumber], [OrderID], [ProductID]) VALUES (3, 5, 25)
INSERT [dbo].[OrderDetails] ([OrdinalNumber], [OrderID], [ProductID]) VALUES (3, 9, 33)
INSERT [dbo].[OrderDetails] ([OrdinalNumber], [OrderID], [ProductID]) VALUES (3, 14, 12)
INSERT [dbo].[OrderDetails] ([OrdinalNumber], [OrderID], [ProductID]) VALUES (3, 15, 12)
GO
INSERT [dbo].[Orders] ([ID], [Date]) VALUES (1, CAST(N'2023-09-05' AS Date))
INSERT [dbo].[Orders] ([ID], [Date]) VALUES (2, CAST(N'2023-09-09' AS Date))
INSERT [dbo].[Orders] ([ID], [Date]) VALUES (3, CAST(N'2023-09-13' AS Date))
INSERT [dbo].[Orders] ([ID], [Date]) VALUES (4, CAST(N'2023-09-27' AS Date))
INSERT [dbo].[Orders] ([ID], [Date]) VALUES (5, CAST(N'2023-10-01' AS Date))
INSERT [dbo].[Orders] ([ID], [Date]) VALUES (6, CAST(N'2023-10-04' AS Date))
INSERT [dbo].[Orders] ([ID], [Date]) VALUES (7, CAST(N'2023-10-22' AS Date))
INSERT [dbo].[Orders] ([ID], [Date]) VALUES (8, CAST(N'2023-11-02' AS Date))
INSERT [dbo].[Orders] ([ID], [Date]) VALUES (9, CAST(N'2023-11-11' AS Date))
INSERT [dbo].[Orders] ([ID], [Date]) VALUES (10, CAST(N'2023-11-11' AS Date))
INSERT [dbo].[Orders] ([ID], [Date]) VALUES (11, CAST(N'2023-11-17' AS Date))
INSERT [dbo].[Orders] ([ID], [Date]) VALUES (12, CAST(N'2023-11-27' AS Date))
INSERT [dbo].[Orders] ([ID], [Date]) VALUES (13, CAST(N'2023-11-29' AS Date))
INSERT [dbo].[Orders] ([ID], [Date]) VALUES (14, CAST(N'2023-11-30' AS Date))
INSERT [dbo].[Orders] ([ID], [Date]) VALUES (15, CAST(N'2023-12-02' AS Date))
INSERT [dbo].[Orders] ([ID], [Date]) VALUES (16, CAST(N'2024-01-03' AS Date))
GO
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image], [AvailableQuantity], [MarkUpPercent]) VALUES (1, N'iPhone 15 Pro Max 1TB', 46490000, N'Grey', 1, N'assets/images/phone/phone01.jpg', 12, 0.1)
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image], [AvailableQuantity], [MarkUpPercent]) VALUES (2, N'iPhone 15 Pro', 28490000, N'White', 1, N'assets/images/phone/phone02.jpg', 10, 0.1)
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image], [AvailableQuantity], [MarkUpPercent]) VALUES (3, N'iPhone 15', 22490000, N'Pink', 1, N'assets/images/phone/phone03.jpg', 10, 0.1)
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image], [AvailableQuantity], [MarkUpPercent]) VALUES (4, N'OPPO Find N3 5G', 44990000, N'Gold', 1, N'assets/images/phone/phone04.jpg', 10, 0.1)
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image], [AvailableQuantity], [MarkUpPercent]) VALUES (5, N'OPPO Find N3 Flip 5G', 22990000, N'Black', 1, N'assets/images/phone/phone05.jpg', 10, 0.1)
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image], [AvailableQuantity], [MarkUpPercent]) VALUES (6, N'Vivo Y17S', 3990000, N'Purple', 1, N'assets/images/phone/phone06.jpg', 10, 0.1)
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image], [AvailableQuantity], [MarkUpPercent]) VALUES (7, N'Xiaomi 13T 5G', 10990000, N'Blue', 1, N'assets/images/phone/phone07.jpg', 10, 0.1)
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image], [AvailableQuantity], [MarkUpPercent]) VALUES (8, N'
OPPO Reno10 5G', 9990000, N'Blue', 1, N'assets/images/phone/phone08.jpg', 10, 0.1)
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image], [AvailableQuantity], [MarkUpPercent]) VALUES (9, N'OPPO Reno10 Pro+ 5G', 19990000, N'Pink', 1, N'assets/images/phone/phone09.jpg', 10, 0.1)
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image], [AvailableQuantity], [MarkUpPercent]) VALUES (10, N'Samsung Galaxy S23 FE 5G', 13890000, N'Green', 1, N'assets/images/phone/phone10.jpg', 10, 0.1)
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image], [AvailableQuantity], [MarkUpPercent]) VALUES (11, N'OPPO Reno10 Pro 5G', 13990000, N'Black', 1, N'assets/images/phone/phone11.jpg', 10, 0.1)
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image], [AvailableQuantity], [MarkUpPercent]) VALUES (12, N'Vivo V25 series', 6990000, N'Yellow', 1, N'assets/images/phone/phone12.jpg', 10, 0.1)
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image], [AvailableQuantity], [MarkUpPercent]) VALUES (13, N'iPad 9 WiFi
', 7790000, N'White', 2, N'assets/images/tablet/tablet01.jpg', 21, 0.05)
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image], [AvailableQuantity], [MarkUpPercent]) VALUES (14, N'Samsung Galaxy Tab A8 (2022)', 5890000, N'Black', 2, N'assets/images/tablet/tablet02.jpg', 21, 0.05)
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image], [AvailableQuantity], [MarkUpPercent]) VALUES (15, N'iPad Air 5 M1 WiFi 64GB', 14990000, N'Bkack', 2, N'assets/images/tablet/tablet03.jpg', 21, 0.12)
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image], [AvailableQuantity], [MarkUpPercent]) VALUES (16, N'iPad 10 WiFi 64GB', 11390000, N'Blue', 2, N'assets/images/tablet/tablet04.jpg', 21, 0.12)
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image], [AvailableQuantity], [MarkUpPercent]) VALUES (17, N'Samsung Galaxy Tab A7 Lite', 3590000, N'Black', 2, N'assets/images/tablet/tablet05.jpg', 21, 0.05)
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image], [AvailableQuantity], [MarkUpPercent]) VALUES (18, N'Lenovo Tab M9
', 4390000, N'Blue', 2, N'assets/images/tablet/tablet06.jpg', 21, 0.12)
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image], [AvailableQuantity], [MarkUpPercent]) VALUES (19, N'Lenovo Tab M8 Gen 4', 2990000, N'White', 2, N'assets/images/tablet/tablet07.jpg', 21, 0.12)
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image], [AvailableQuantity], [MarkUpPercent]) VALUES (20, N'Samsung Galaxy Tab S9+ 5G', 26990000, N'Black', 2, N'assets/images/tablet/tablet08.jpg', 21, 0.05)
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image], [AvailableQuantity], [MarkUpPercent]) VALUES (21, N'iPad Pro M2 11 inch WiFi Cellular 128GB', 24090000, N'White', 2, N'assets/images/tablet/tablet09.jpg', 21, 0.12)
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image], [AvailableQuantity], [MarkUpPercent]) VALUES (22, N'Samsung Galaxy Tab S9+ WiFi', 23990000, N'White', 2, N'assets/images/tablet/tablet10.jpg', 21, 0.12)
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image], [AvailableQuantity], [MarkUpPercent]) VALUES (23, N'iPad 10 WiFi Cellular 64GB', 14390000, N'Pink', 2, N'assets/images/tablet/tablet11.jpg', 21, 0.12)
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image], [AvailableQuantity], [MarkUpPercent]) VALUES (24, N'OPPO Pad 2', 13990000, N'Grey', 2, N'assets/images/tablet/tablet12.jpg', 21, 0.05)
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image], [AvailableQuantity], [MarkUpPercent]) VALUES (25, N'MSI Gaming GF63 Thin 11SC i5 11400H (664VN)', 14990000, N'Black', 3, N'assets/images/laptop/laptop01.jpg', 34, 0.12)
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image], [AvailableQuantity], [MarkUpPercent]) VALUES (26, N'Acer Aspire 5 Gaming A515 58GM 51LB i5 13420H', 16990000, N'Black', 3, N'assets/images/laptop/laptop02.jpg', 34, 0.12)
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image], [AvailableQuantity], [MarkUpPercent]) VALUES (27, N'Asus TUF Gaming F15 FX506HF i5 11400H (HN014W)', 16990000, N'Black', 3, N'assets/images/laptop/laptop03.jpg', 34, 0.12)
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image], [AvailableQuantity], [MarkUpPercent]) VALUES (28, N'HP Gaming VICTUS 15 fa0155TX i5 12450H (81P00PA)', 20290000, N'Black', 3, N'assets/images/laptop/laptop04.jpg', 34, 0.05)
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image], [AvailableQuantity], [MarkUpPercent]) VALUES (29, N'Acer Nitro 5 Gaming AN515 57 5669 i5 11400H', 17990000, N'Black', 3, N'assets/images/laptop/laptop05.jpg', 34, 0.12)
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image], [AvailableQuantity], [MarkUpPercent]) VALUES (30, N'Lenovo LOQ Gaming 15IRH8 i5 13420H (82XV00Q4VN)', 27990000, N'Black', 3, N'assets/images/laptop/laptop06.jpg', 34, 0.12)
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image], [AvailableQuantity], [MarkUpPercent]) VALUES (31, N'MSI Gaming GF63 Thin 12VE i5 12450H (460VN)', 20490000, N'Black', 3, N'assets/images/laptop/laptop07.jpg', 34, 0.05)
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image], [AvailableQuantity], [MarkUpPercent]) VALUES (32, N'Lenovo Legion 5 15IAH7 i5 12500H (82RC003WVN)', 31590000, N'Grey', 3, N'assets/images/laptop/laptop08.jpg', 34, 0.05)
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image], [AvailableQuantity], [MarkUpPercent]) VALUES (33, N'Acer Aspire 7 Gaming A715 43G R8GA R5 5625U', 16490000, N'Grey', 3, N'assets/images/laptop/laptop09.jpg', 34, 0.05)
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image], [AvailableQuantity], [MarkUpPercent]) VALUES (34, N'HP Gaming VICTUS 16 e1105AX R5 6600H (7C0T0PA)', 19490000, N'White', 3, N'assets/images/laptop/laptop10.jpg', 34, 0.05)
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image], [AvailableQuantity], [MarkUpPercent]) VALUES (35, N'Gigabyte Gaming G5 i5 12500H (GE-51VN263SH)', 18990000, N'Grey', 3, N'assets/images/laptop/laptop11.jpg', 34, 0.05)
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image], [AvailableQuantity], [MarkUpPercent]) VALUES (36, N'Acer Nitro 5 Tiger AN515 58 52SP i5 12500H', 22990000, N'Black', 3, N'assets/images/laptop/laptop12.jpg', 34, 0.05)
GO
INSERT [dbo].[User] ([ID], [Username], [Password], [FirstName], [LastName], [Role], [Email], [Telephone], [Address], [Avatar]) VALUES (1, N'admin', N'123456', N'Ad', N'Min', N'admin', N'example1@gmail.com', N'0123456789', N'Somewhere1', N'assets/images/avatar/Avatar_1.png')
INSERT [dbo].[User] ([ID], [Username], [Password], [FirstName], [LastName], [Role], [Email], [Telephone], [Address], [Avatar]) VALUES (2, N'user1', N'password1', N'Hieu', N'Nguyen', N'user', N'example2@gmail.com', N'0123123123', N'Somewhere2', N'assets/images/avatar/Avatar_3.png')
INSERT [dbo].[User] ([ID], [Username], [Password], [FirstName], [LastName], [Role], [Email], [Telephone], [Address], [Avatar]) VALUES (3, N'user2', N'password2', N'Hung', N'Ngo', N'user', N'example3@gmail.com', N'0456456456', N'Somewhere3', N'assets/images/avatar/Avatar_5.png')
INSERT [dbo].[User] ([ID], [Username], [Password], [FirstName], [LastName], [Role], [Email], [Telephone], [Address], [Avatar]) VALUES (4, N'user3', N'password3', N'Luan', N'Khuong', N'user', N'example4@gmail.com', N'0789789789', N'Somewhere4', N'assets/images/avatar/Avatar_7.png')
INSERT [dbo].[User] ([ID], [Username], [Password], [FirstName], [LastName], [Role], [Email], [Telephone], [Address], [Avatar]) VALUES (5, N'user4', N'password4', N'First', N'Last', N'user', N'example5@gmail.com', N'0123456780', N'Somewhere', N'assets/images/avatar/Avatar_2.png')
INSERT [dbo].[User] ([ID], [Username], [Password], [FirstName], [LastName], [Role], [Email], [Telephone], [Address], [Avatar]) VALUES (6, N'user5', N'password5', N'First', N'Last', N'user', N'example6@gmail.com', N'0123456781', N'Somewhere', N'assets/images/avatar/Avatar_4.png')
INSERT [dbo].[User] ([ID], [Username], [Password], [FirstName], [LastName], [Role], [Email], [Telephone], [Address], [Avatar]) VALUES (7, N'user6', N'password6', N'First', N'Last', N'user', N'example7@gmail.com', N'0123456782', N'Somewhere', N'assets/images/avatar/Avatar_6.png')
INSERT [dbo].[User] ([ID], [Username], [Password], [FirstName], [LastName], [Role], [Email], [Telephone], [Address], [Avatar]) VALUES (8, N'user7', N'password7', N'First', N'Last', N'user', N'example8@gmail.com', N'0123456783', N'Somewhere', N'assets/images/avatar/Avatar_8.png')
INSERT [dbo].[User] ([ID], [Username], [Password], [FirstName], [LastName], [Role], [Email], [Telephone], [Address], [Avatar]) VALUES (9, N'user8', N'password8', N'First', N'Last', N'user', N'example9@gmail.com', N'0123456784', N'Somewhere', N'assets/images/avatar/Avatar_2.png')
INSERT [dbo].[User] ([ID], [Username], [Password], [FirstName], [LastName], [Role], [Email], [Telephone], [Address], [Avatar]) VALUES (10, N'user9', N'password9', N'First', N'Last', N'user', N'example10@gmail.com', N'0123456785', N'Somewhere', N'assets/images/avatar/Avatar_7.png')
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__User__536C85E497DE63DA]    Script Date: 03/01/2024 18:21:13 ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [UQ__User__536C85E497DE63DA] UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__User__A9D10534C176FB4B]    Script Date: 03/01/2024 18:21:13 ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [UQ__User__A9D10534C176FB4B] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__User__D9FEB744EEA6CFA2]    Script Date: 03/01/2024 18:21:13 ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [UQ__User__D9FEB744EEA6CFA2] UNIQUE NONCLUSTERED 
(
	[Telephone] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_Order] FOREIGN KEY([OrderID])
REFERENCES [dbo].[Orders] ([ID])
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrderDetails_Order]
GO
ALTER TABLE [dbo].[OrderDetails]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetails_Products] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products] ([ID])
GO
ALTER TABLE [dbo].[OrderDetails] CHECK CONSTRAINT [FK_OrderDetails_Products]
GO
USE [master]
GO
ALTER DATABASE [MyShopDB] SET  READ_WRITE 
GO
