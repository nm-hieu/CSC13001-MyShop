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
