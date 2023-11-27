USE MyShopDB
GO
CREATE TABLE [User]
(
	Username nvarchar(50) primary key not null,
	[Password] nvarchar(100) not null,
	FisrtName nvarchar(50) not null,
	LastName nvarchar(50) not null,
	[Role] nvarchar(10) not null,
	Email nvarchar(50) unique not null,
	Telephone nvarchar(11) unique not null,
	[Address] nvarchar(100) not null,
)
GO
INSERT INTO [User] values ('admin', '123456', 'Ad', 'Min', 'admin', 'example1@gmail.com', '0123456789', 'Somewhere1')
INSERT INTO [User] values ('user1', 'password1', 'Hieu', 'Nguyen', 'user', 'example2@gmail.com', '0123123123', 'Somewhere2')
INSERT INTO [User] values ('user2', 'password2', 'Hung', 'Ngo', 'user', 'example3@gmail.com', '0456456456', 'Somewhere3')
INSERT INTO [User] values ('user3', 'password3', 'Luan', 'Khuong', 'user', 'example4@gmail.com', '0789789789', 'Somewhere4')
GO

SELECT * FROM [User]

USE [MyShopDB]
GO
/****** Object:  Table [dbo].[User]    Script Date: 23/11/2023 09:56:55 ******/
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
/****** Object:  Index [UQ__User__536C85E497DE63DA]    Script Date: 23/11/2023 09:56:55 ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [UQ__User__536C85E497DE63DA] UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__User__A9D10534C176FB4B]    Script Date: 23/11/2023 09:56:55 ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [UQ__User__A9D10534C176FB4B] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__User__D9FEB744EEA6CFA2]    Script Date: 23/11/2023 09:56:55 ******/
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [UQ__User__D9FEB744EEA6CFA2] UNIQUE NONCLUSTERED 
(
	[Telephone] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
