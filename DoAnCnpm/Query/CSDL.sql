use master
go
create database DoAnPM
go
use DoAnPM
go
--Bang Category
CREATE TABLE [dbo].[Category] (
    [Id]       INT            IDENTITY (1, 1) NOT NULL,
    [IDCate]   NCHAR (20)     NOT NULL,
    [NameCate] NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([IDCate] ASC)
);
--Bang Customer
CREATE TABLE [dbo].[Customer] (
    [IDCus]    INT            IDENTITY (1, 1) NOT NULL,
    [NameCus]  NVARCHAR (MAX) NULL,
    [PhoneCus] NVARCHAR (15)  NULL,
    [EmailCus] NVARCHAR (MAX) NULL,
    [UserCus] NVARCHAR (MAX) NULL,
    [PassCus] NVARCHAR (MAX) NULL,
    [Diachi] nvarchar(max) Null,
    [Gioitinh] int,
    [Ngaysinh] date,		
    PRIMARY KEY CLUSTERED ([IDCus] ASC)
);
--Bang Products
CREATE TABLE [dbo].[Products] (
    [ProductID]     INT             IDENTITY (1, 1) NOT NULL,
    [NamePro]       NVARCHAR (MAX)  NULL,
    [DecriptionPro] NVARCHAR (MAX)  NULL,
    [Category]      NCHAR (20)      NULL,
    [Price]         DECIMAL (18, 2) NULL,	
    [ImagePro]      NVARCHAR (MAX)  NULL,
    [MauXe] NVARCHAR (MAX)  NULL,
    [Vitri] NVARCHAR (MAX)  NULL,	
    [Quantity]  INT        Default 0,
    [IsGiveBack] INT,	
    PRIMARY KEY CLUSTERED ([ProductID] ASC),
    CONSTRAINT [FK_Pro_Category] FOREIGN KEY ([Category]) REFERENCES [dbo].[Category] ([IDCate])
);
--Bang OrderPro
CREATE TABLE [dbo].[OrderPro] (
    [ID]               INT            IDENTITY (1, 1) NOT NULL,
    [DateOrder]        DATE           NULL,
    [IDCus]            INT            NULL,
    [AddressDeliverry] NVARCHAR (MAX) NULL,
    [NameCusNonAccount] NVARCHAR (MAX) NULL,
    [PhoneCusNonAccount] NVARCHAR (MAX) NULL,
    [TypePayment]            INT            NULL,
    [OrderDate] DATETIME,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    FOREIGN KEY ([IDCus]) REFERENCES [dbo].[Customer] ([IDCus])
);
--Bang OrderDetail
CREATE TABLE [dbo].[OrderDetail] (
    [ID]        INT        IDENTITY (1, 1) NOT NULL,
    [IDProduct] INT        NULL,
    [IDOrder]   INT        NULL,
    [Quantity]  INT        NULL,
    [UnitPrice] FLOAT (53) NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    FOREIGN KEY ([IDProduct]) REFERENCES [dbo].[Products] ([ProductID]),
    FOREIGN KEY ([IDOrder]) REFERENCES [dbo].[OrderPro] ([ID])
);
CREATE TABLE Vouchers (
    Id INT PRIMARY KEY,
    Code NVARCHAR(50) NOT NULL,
    DiscountAmount DECIMAL(18, 2) NOT NULL,
);
create table Admins  
(  
   Id int primary key identity(1,1),  
   Username nvarchar(50),  
   Password nvarchar(50)  
)  
  
create table Roles  
(  
   Id int primary key identity(1,1),  
   RoleName nvarchar(50)  
)  


create table UserRoleMapping  
(  
   Id int primary key identity(1,1),  
   UserId int,  
   RoleId int  
)  

alter table UserRoleMapping Add foreign key(UserId)  
references Admins(Id)  
  
alter table UserRoleMapping Add foreign key(RoleId)  
references Roles(id)  
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[HighChart](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[ThoiGian] [nvarchar](50) NULL,
 CONSTRAINT [PK_HighChart] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
Create table Comments
(
  ID bigint IDENTITY(1,1) NOT NULL,
  CommentMsg nvarchar(max),
  CommentDate datetime,
  XeId int,
  UserID int,
  ParentID int,	
  Rate int,
  [UserName] NVARCHAR(MAX) NULL,	
  PRIMARY KEY CLUSTERED ([ID] ASC),
  FOREIGN KEY (XeId) REFERENCES [dbo].[Products] ([ProductID]),
  FOREIGN KEY (UserID) REFERENCES [dbo].[Customer] ([IDCus]),
)

