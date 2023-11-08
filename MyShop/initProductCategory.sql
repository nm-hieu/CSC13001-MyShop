USE [master]
GO
/****** Object:  Database [Project1]    Script Date: 10/29/2023 6:44:25 PM ******/
CREATE DATABASE [Project1]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Project1', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLSERVER\MSSQL\DATA\Project1.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Project1_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLSERVER\MSSQL\DATA\Project1_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
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
/****** Object:  Table [dbo].[Categories]    Script Date: 10/29/2023 6:44:25 PM ******/
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
/****** Object:  Table [dbo].[Products]    Script Date: 10/29/2023 6:44:25 PM ******/
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
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([ID], [Name]) VALUES (1, N'Điện thoại')
INSERT [dbo].[Categories] ([ID], [Name]) VALUES (2, N'Tablet')
INSERT [dbo].[Categories] ([ID], [Name]) VALUES (3, N'Laptop')
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image]) VALUES (1, N'iPhone 15 Pro Max 1TB', 46490000, N'Grey', 1, N'assets/images/phone/phone01.jpg')
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image]) VALUES (2, N'iPhone 15 Pro', 28490000, N'White', 1, N'assets/images/phone/phone02.jpg')
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image]) VALUES (3, N'iPhone 15', 22490000, N'Pink', 1, N'assets/images/phone/phone03.jpg')
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image]) VALUES (4, N'OPPO Find N3 5G', 44990000, N'Gold', 1, N'assets/images/phone/phone04.jpg')
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image]) VALUES (5, N'OPPO Find N3 Flip 5G', 22990000, N'Black', 1, N'assets/images/phone/phone05.jpg')
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image]) VALUES (6, N'Vivo Y17S', 3990000, N'Purple', 1, N'assets/images/phone/phone06.jpg')
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image]) VALUES (7, N'Xiaomi 13T 5G', 10990000, N'Blue', 1, N'assets/images/phone/phone07.jpg')
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image]) VALUES (8, N'
OPPO Reno10 5G', 9990000, N'Blue', 1, N'assets/images/phone/phone08.jpg')
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image]) VALUES (9, N'OPPO Reno10 Pro+ 5G', 19990000, N'Pink', 1, N'assets/images/phone/phone09.jpg')
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image]) VALUES (10, N'Samsung Galaxy S23 FE 5G', 13890000, N'Green', 1, N'assets/images/phone/phone10.jpg')
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image]) VALUES (11, N'OPPO Reno10 Pro 5G', 13990000, N'Black', 1, N'assets/images/phone/phone11.jpg')
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image]) VALUES (12, N'Vivo V25 series', 6990000, N'Yellow', 1, N'assets/images/phone/phone12.jpg')
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image]) VALUES (13, N'iPad 9 WiFi
', 7790000, N'White', 2, N'assets/images/tablet/tablet01.jpg')
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image]) VALUES (14, N'Samsung Galaxy Tab A8 (2022)', 5890000, N'Black', 2, N'assets/images/tablet/tablet02.jpg')
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image]) VALUES (15, N'iPad Air 5 M1 WiFi 64GB', 14990000, N'Bkack', 2, N'assets/images/tablet/tablet03.jpg')
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image]) VALUES (16, N'iPad 10 WiFi 64GB', 11390000, N'Blue', 2, N'assets/images/tablet/tablet04.jpg')
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image]) VALUES (17, N'Samsung Galaxy Tab A7 Lite', 3590000, N'Black', 2, N'assets/images/tablet/tablet05.jpg')
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image]) VALUES (18, N'Lenovo Tab M9
', 4390000, N'Blue', 2, N'assets/images/tablet/tablet06.jpg')
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image]) VALUES (19, N'Lenovo Tab M8 Gen 4', 2990000, N'White', 2, N'assets/images/tablet/tablet07.jpg')
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image]) VALUES (20, N'Samsung Galaxy Tab S9+ 5G', 26990000, N'Black', 2, N'assets/images/tablet/tablet08.jpg')
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image]) VALUES (21, N'iPad Pro M2 11 inch WiFi Cellular 128GB', 24090000, N'White', 2, N'assets/images/tablet/tablet09.jpg')
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image]) VALUES (22, N'Samsung Galaxy Tab S9+ WiFi', 23990000, N'White', 2, N'assets/images/tablet/tablet10.jpg')
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image]) VALUES (23, N'iPad 10 WiFi Cellular 64GB', 14390000, N'Pink', 2, N'assets/images/tablet/tablet11.jpg')
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image]) VALUES (24, N'OPPO Pad 2', 13990000, N'Grey', 2, N'assets/images/tablet/tablet12.jpg')
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image]) VALUES (25, N'MSI Gaming GF63 Thin 11SC i5 11400H (664VN)', 14990000, N'Black', 3, N'assets/images/laptop/laptop01.jpg')
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image]) VALUES (26, N'Acer Aspire 5 Gaming A515 58GM 51LB i5 13420H', 16990000, N'Black', 3, N'assets/images/laptop/laptop02.jpg')
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image]) VALUES (27, N'Asus TUF Gaming F15 FX506HF i5 11400H (HN014W)', 16990000, N'Black', 3, N'assets/images/laptop/laptop03.jpg')
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image]) VALUES (28, N'HP Gaming VICTUS 15 fa0155TX i5 12450H (81P00PA)', 20290000, N'Black', 3, N'assets/images/laptop/laptop04.jpg')
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image]) VALUES (29, N'Acer Nitro 5 Gaming AN515 57 5669 i5 11400H', 17990000, N'Black', 3, N'assets/images/laptop/laptop05.jpg')
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image]) VALUES (30, N'Lenovo LOQ Gaming 15IRH8 i5 13420H (82XV00Q4VN)', 27990000, N'Black', 3, N'assets/images/laptop/laptop06.jpg')
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image]) VALUES (31, N'MSI Gaming GF63 Thin 12VE i5 12450H (460VN)', 20490000, N'Black', 3, N'assets/images/laptop/laptop07.jpg')
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image]) VALUES (32, N'Lenovo Legion 5 15IAH7 i5 12500H (82RC003WVN)', 31590000, N'Grey', 3, N'assets/images/laptop/laptop08.jpg')
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image]) VALUES (33, N'Acer Aspire 7 Gaming A715 43G R8GA R5 5625U', 16490000, N'Grey', 3, N'assets/images/laptop/laptop09.jpg')
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image]) VALUES (34, N'HP Gaming VICTUS 16 e1105AX R5 6600H (7C0T0PA)', 19490000, N'White', 3, N'assets/images/laptop/laptop10.jpg')
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image]) VALUES (35, N'Gigabyte Gaming G5 i5 12500H (GE-51VN263SH)', 18990000, N'Grey', 3, N'assets/images/laptop/laptop11.jpg')
INSERT [dbo].[Products] ([ID], [Name], [Price], [Color], [Category], [Image]) VALUES (36, N'Acer Nitro 5 Tiger AN515 58 52SP i5 12500H', 22990000, N'Black', 3, N'assets/images/laptop/laptop12.jpg')
GO
USE [master]
GO
ALTER DATABASE [Project1] SET  READ_WRITE 
GO
