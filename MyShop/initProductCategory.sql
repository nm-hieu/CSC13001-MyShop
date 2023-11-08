USE [master]
GO
/****** Object:  Database [Project1]    Script Date: 11/8/2023 10:58:37 PM ******/
CREATE DATABASE [Project1]
GO
ALTER DATABASE [Project1] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Project1].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Project1] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Project1] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Project1] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Project1] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Project1] SET ARITHABORT OFF 
GO
ALTER DATABASE [Project1] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Project1] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Project1] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Project1] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Project1] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Project1] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Project1] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Project1] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Project1] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Project1] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Project1] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Project1] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Project1] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Project1] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Project1] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Project1] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Project1] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Project1] SET RECOVERY FULL 
GO
ALTER DATABASE [Project1] SET  MULTI_USER 
GO
ALTER DATABASE [Project1] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Project1] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Project1] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Project1] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Project1] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Project1] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Project1', N'ON'
GO
ALTER DATABASE [Project1] SET QUERY_STORE = OFF
GO
USE [Project1]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 11/8/2023 10:58:37 PM ******/
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
/****** Object:  Table [dbo].[Products]    Script Date: 11/8/2023 10:58:37 PM ******/
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
/****** Object:  Table [dbo].[User]    Script Date: 11/8/2023 10:58:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](100) NOT NULL,
	[FisrtName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Role] [nvarchar](10) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Telephone] [nvarchar](11) NOT NULL,
	[Address] [nvarchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([ID], [Name]) VALUES (1, N'Điện thoại')
INSERT [dbo].[Categories] ([ID], [Name]) VALUES (2, N'Tablet')
INSERT [dbo].[Categories] ([ID], [Name]) VALUES (3, N'Laptop')
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image], [AvailableQuantity], [MarkUpPercent]) VALUES (1, N'iPhone 15 Pro Max 1TB', 46490000, N'Grey', 1, N'assets/images/phone/phone01.jpg', 10, 0.1)
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
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image], [AvailableQuantity], [MarkUpPercent]) VALUES (40, N'iPhone test', 22000, N'Blue', 2, N'assets/images/phone/phone01.jpg', 32, 0.05)
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image], [AvailableQuantity], [MarkUpPercent]) VALUES (41, N'iPhone test 2', 32000, N'Blue', 1, N'assets/images/phone/phone02.jpg', 32, 0.05)
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image], [AvailableQuantity], [MarkUpPercent]) VALUES (42, N'Iphone X', 15999, N'Yellow', 1, N'assets/images/phone/phone03.jpg', NULL, NULL)
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image], [AvailableQuantity], [MarkUpPercent]) VALUES (43, N'IphoneXS', 27000000, N'Blue', 1, N'assets/images/phone/phone06.jpg', 13, 0.1)
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image], [AvailableQuantity], [MarkUpPercent]) VALUES (44, N'IPhone XSMAX', 30200000, N'Green', 1, N'assets/images/phone/phone05.jpg', 2, 0.03)
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image], [AvailableQuantity], [MarkUpPercent]) VALUES (46, N'Samsung s21', 27000000, N'White', 1, N'assets/images/phone/phone10.jpg', 2, 0.03)
GO
INSERT [dbo].[User] ([Username], [Password], [FisrtName], [LastName], [Role], [Email], [Telephone], [Address]) VALUES (N'admin', N'123456', N'Ad', N'Min', N'admin', N'example1@gmail.com', N'0123456789', N'Somewhere1')
INSERT [dbo].[User] ([Username], [Password], [FisrtName], [LastName], [Role], [Email], [Telephone], [Address]) VALUES (N'user1', N'password1', N'Hieu', N'Nguyen', N'user', N'example2@gmail.com', N'0123123123', N'Somewhere2')
INSERT [dbo].[User] ([Username], [Password], [FisrtName], [LastName], [Role], [Email], [Telephone], [Address]) VALUES (N'user2', N'password2', N'Hung', N'Ngo', N'user', N'example3@gmail.com', N'0456456456', N'Somewhere3')
INSERT [dbo].[User] ([Username], [Password], [FisrtName], [LastName], [Role], [Email], [Telephone], [Address]) VALUES (N'user3', N'password3', N'Luan', N'Khuong', N'user', N'example4@gmail.com', N'0789789789', N'Somewhere4')
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__User__A9D10534729EFC2B]    Script Date: 11/8/2023 10:58:37 PM ******/
ALTER TABLE [dbo].[User] ADD UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__User__D9FEB7449E15E03F]    Script Date: 11/8/2023 10:58:37 PM ******/
ALTER TABLE [dbo].[User] ADD UNIQUE NONCLUSTERED 
(
	[Telephone] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [Project1] SET  READ_WRITE 
GO
