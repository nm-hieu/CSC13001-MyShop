use master
go

if db_id('MyShop') is not null
	drop database MyShop
go

create database MyShop
go

use MyShop
go

create table Product
(
	ID int identity(1,1) primary key,
	Name nvarchar(50),
	Manufacturer ntext,
	Price int,
	Image ntext,
)

create table Receipt
(
	ID int,
	Date date,
	primary key (ID)
)

create table ReceiptDetails
(
	OrdinalNumber int,
	ReceiptID int,
	ProductID int,
	primary key (OrdinalNumber, ReceiptID)
)

alter table ReceiptDetails add constraint FK_ReceiptDetails_Receipt
foreign key (ReceiptID) references Receipt(ID)

alter table ReceiptDetails add constraint FK_ReceiptDetails_Product
foreign key (ProductID) references Product(ID)
go

insert into Product(Name, Image, Manufacturer, Price)
values
	('Iphone 15 Pro Max', 'assets/Images/phone01.jpg', 'Apple', 33990000),
	('Samsung Galaxy A05', 'assets/Images/phone02.jpg', 'Samsung', 3090000),
    ('Oppo Reno10 Pro 5G', 'assets/Images/phone03.jpg', 'Oppo', 13990000),
    ('Xiaomi Redmi 12', 'assets/Images/phone04.jpg', 'Xiaomi', 3440000),
    ('Vivo Y17s', 'assets/Images/phone05.jpg', 'Vivo', 3790000),
    ('Xiaomi 13T 5G', 'assets/Images/phone06.jpg', 'Xiaomi', 11990000),
    ('Samsung Galaxy S23 FE 5G', 'assets/Images/phone07.jpg', 'Samsung', 14490000),
    ('Realme C53', 'assets/Images/phone08.jpg', 'Realme', 3990000),
    ('Samsung Galaxy Z Fold5 5G', 'assets/Images/phone09.jpg', 'Samsung', 36990000),
    ('Iphone 14 Pro', 'assets/Images/phone10.jpg', 'Apple', 24190000)

insert into Receipt
values
	(1, '11/2/23'),
	(2, '11/2/23'),
	(3, '11/4/23'),
	(4, '11/5/23'),
	(5, '11/5/23')

insert into ReceiptDetails
values
	(1, 1, 5),
	(2, 1, 6),
	(1, 2, 1),
	(1, 3, 3),
	(2, 3, 4),
	(1, 4, 5),
	(2, 4, 3),
	(1, 5, 7)
go

-- get receipt total price
select sum(Product.Price)
from Receipt join ReceiptDetails on Receipt.ID = ReceiptDetails.ReceiptID
			 join Product on ReceiptDetails.ProductID = Product.ID
where Receipt.ID = 1
group by Receipt.ID