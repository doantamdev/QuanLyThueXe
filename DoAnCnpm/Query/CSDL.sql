use master
go
create database DoAnPM
go
USE [DoAnPM]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admins](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IDCate] [nchar](20) NOT NULL,
	[NameCate] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[IDCate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comments]    Script Date: 12/25/2023 10:40:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comments](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[CommentMsg] [nvarchar](max) NULL,
	[CommentDate] [datetime] NULL,
	[XeId] [int] NULL,
	[UserID] [int] NULL,
	[ParentID] [int] NULL,
	[Rate] [int] NULL,
	[UserName] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 12/25/2023 10:40:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[IDCus] [int] IDENTITY(1,1) NOT NULL,
	[NameCus] [nvarchar](max) NULL,
	[PhoneCus] [nvarchar](15) NULL,
	[EmailCus] [nvarchar](max) NULL,
	[UserCus] [nvarchar](max) NULL,
	[PassCus] [nvarchar](max) NULL,
	[Diachi] [nvarchar](max) NULL,
	[Gioitinh] [int] NULL,
	[Ngaysinh] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[IDCus] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HighChart]    Script Date: 12/25/2023 10:40:30 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 12/25/2023 10:40:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetail](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[IDProduct] [int] NULL,
	[IDOrder] [int] NULL,
	[Quantity] [int] NULL,
	[UnitPrice] [float] NULL,
	[CreateDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderPro]    Script Date: 12/25/2023 10:40:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderPro](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[DateOrder] [date] NULL,
	[IDCus] [int] NULL,
	[AddressDeliverry] [nvarchar](max) NULL,
	[NameCusNonAccount] [nvarchar](max) NULL,
	[PhoneCusNonAccount] [nvarchar](max) NULL,
	[TypePayment] [int] NULL,
	[OrderDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 12/25/2023 10:40:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[NamePro] [nvarchar](max) NULL,
	[DecriptionPro] [nvarchar](max) NULL,
	[Category] [nchar](20) NULL,
	[Price] [decimal](18, 2) NULL,
	[ImagePro] [nvarchar](max) NULL,
	[MauXe] [nvarchar](max) NULL,
	[Vitri] [nvarchar](max) NULL,
	[Quantity] [int] NULL,
	[IsGiveBack] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 12/25/2023 10:40:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRoleMapping]    Script Date: 12/25/2023 10:40:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoleMapping](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[RoleId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vouchers]    Script Date: 12/25/2023 10:40:30 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vouchers](
	[Id] [int] NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[DiscountAmount] [decimal](18, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Admins] ON 

INSERT [dbo].[Admins] ([Id], [Username], [Password]) VALUES (1, N'Phat21', N'0947690637')
INSERT [dbo].[Admins] ([Id], [Username], [Password]) VALUES (2, N'tam123', N'tam123')
SET IDENTITY_INSERT [dbo].[Admins] OFF
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([Id], [IDCate], [NameCate]) VALUES (1, N'001', N'Xe 2 bánh')
INSERT [dbo].[Category] ([Id], [IDCate], [NameCate]) VALUES (6, N'002', N'Xe 4 bánh')
INSERT [dbo].[Category] ([Id], [IDCate], [NameCate]) VALUES (7, N'003', N'Xe Khách')
INSERT [dbo].[Category] ([Id], [IDCate], [NameCate]) VALUES (8, N'1236', N'Xe Đạp')
INSERT [dbo].[Category] ([Id], [IDCate], [NameCate]) VALUES (9, N'1523', N'Xe Vận Chuyển')
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[Comments] ON 

INSERT [dbo].[Comments] ([ID], [CommentMsg], [CommentDate], [XeId], [UserID], [ParentID], [Rate], [UserName]) VALUES (1, N'xe dep', CAST(N'2023-10-01T16:23:24.573' AS DateTime), 1, NULL, NULL, 4, NULL)
INSERT [dbo].[Comments] ([ID], [CommentMsg], [CommentDate], [XeId], [UserID], [ParentID], [Rate], [UserName]) VALUES (2, N'xe dep', CAST(N'2023-10-01T16:23:26.870' AS DateTime), 1, NULL, NULL, 4, NULL)
INSERT [dbo].[Comments] ([ID], [CommentMsg], [CommentDate], [XeId], [UserID], [ParentID], [Rate], [UserName]) VALUES (3, N'xe rat dep', CAST(N'2023-10-01T16:24:45.117' AS DateTime), 1, NULL, NULL, 4, NULL)
INSERT [dbo].[Comments] ([ID], [CommentMsg], [CommentDate], [XeId], [UserID], [ParentID], [Rate], [UserName]) VALUES (4, N'xe dep', CAST(N'2023-10-01T16:24:52.397' AS DateTime), 1, NULL, NULL, 4, NULL)
INSERT [dbo].[Comments] ([ID], [CommentMsg], [CommentDate], [XeId], [UserID], [ParentID], [Rate], [UserName]) VALUES (5, N'xe ', CAST(N'2023-10-01T17:32:45.277' AS DateTime), 1, NULL, NULL, 3, NULL)
INSERT [dbo].[Comments] ([ID], [CommentMsg], [CommentDate], [XeId], [UserID], [ParentID], [Rate], [UserName]) VALUES (6, N'xe ', CAST(N'2023-10-01T17:32:48.333' AS DateTime), 1, NULL, NULL, 3, NULL)
INSERT [dbo].[Comments] ([ID], [CommentMsg], [CommentDate], [XeId], [UserID], [ParentID], [Rate], [UserName]) VALUES (7, N'xe ok', CAST(N'2023-10-01T17:35:59.630' AS DateTime), 1, NULL, NULL, 4, NULL)
INSERT [dbo].[Comments] ([ID], [CommentMsg], [CommentDate], [XeId], [UserID], [ParentID], [Rate], [UserName]) VALUES (8, N'ok ok', CAST(N'2023-10-02T10:15:37.587' AS DateTime), 1, NULL, NULL, 3, NULL)
INSERT [dbo].[Comments] ([ID], [CommentMsg], [CommentDate], [XeId], [UserID], [ParentID], [Rate], [UserName]) VALUES (9, N'xe dep', CAST(N'2023-10-02T10:19:09.477' AS DateTime), 2, NULL, NULL, 4, NULL)
INSERT [dbo].[Comments] ([ID], [CommentMsg], [CommentDate], [XeId], [UserID], [ParentID], [Rate], [UserName]) VALUES (10, N'xe ok la', CAST(N'2023-10-02T10:43:37.123' AS DateTime), 2, NULL, NULL, 5, NULL)
INSERT [dbo].[Comments] ([ID], [CommentMsg], [CommentDate], [XeId], [UserID], [ParentID], [Rate], [UserName]) VALUES (11, N'ok xe nggon', CAST(N'2023-10-09T10:38:36.730' AS DateTime), NULL, NULL, NULL, 3, N'tam123456')
INSERT [dbo].[Comments] ([ID], [CommentMsg], [CommentDate], [XeId], [UserID], [ParentID], [Rate], [UserName]) VALUES (12, N'ok xe nhon', CAST(N'2023-10-09T10:41:33.250' AS DateTime), NULL, NULL, NULL, 3, N'tam123456')
INSERT [dbo].[Comments] ([ID], [CommentMsg], [CommentDate], [XeId], [UserID], [ParentID], [Rate], [UserName]) VALUES (13, N'ok xe nhon', CAST(N'2023-10-09T10:41:36.683' AS DateTime), NULL, NULL, NULL, 3, N'tam123456')
INSERT [dbo].[Comments] ([ID], [CommentMsg], [CommentDate], [XeId], [UserID], [ParentID], [Rate], [UserName]) VALUES (14, N'ok xe ngon', CAST(N'2023-10-09T10:41:42.717' AS DateTime), NULL, NULL, NULL, 3, N'tam123456')
INSERT [dbo].[Comments] ([ID], [CommentMsg], [CommentDate], [XeId], [UserID], [ParentID], [Rate], [UserName]) VALUES (15, N'ok ngon', CAST(N'2023-10-09T10:43:44.143' AS DateTime), NULL, NULL, NULL, 5, N'tam123456')
INSERT [dbo].[Comments] ([ID], [CommentMsg], [CommentDate], [XeId], [UserID], [ParentID], [Rate], [UserName]) VALUES (16, N'xe ok', CAST(N'2023-10-09T10:46:30.853' AS DateTime), NULL, NULL, NULL, 5, N'tam123456')
INSERT [dbo].[Comments] ([ID], [CommentMsg], [CommentDate], [XeId], [UserID], [ParentID], [Rate], [UserName]) VALUES (17, N'asdasd', CAST(N'2023-10-09T10:49:46.490' AS DateTime), NULL, NULL, NULL, 3, N'tam123456')
INSERT [dbo].[Comments] ([ID], [CommentMsg], [CommentDate], [XeId], [UserID], [ParentID], [Rate], [UserName]) VALUES (18, N'afadsasd', CAST(N'2023-10-09T10:50:07.443' AS DateTime), NULL, NULL, NULL, 3, N'tam123456')
INSERT [dbo].[Comments] ([ID], [CommentMsg], [CommentDate], [XeId], [UserID], [ParentID], [Rate], [UserName]) VALUES (19, N'xe ok vai', CAST(N'2023-10-09T10:57:03.517' AS DateTime), 2, NULL, NULL, 3, N'doantam')
INSERT [dbo].[Comments] ([ID], [CommentMsg], [CommentDate], [XeId], [UserID], [ParentID], [Rate], [UserName]) VALUES (20, N'10 diem', CAST(N'2023-10-09T10:58:01.330' AS DateTime), 2, NULL, NULL, 5, N'Phat23')
INSERT [dbo].[Comments] ([ID], [CommentMsg], [CommentDate], [XeId], [UserID], [ParentID], [Rate], [UserName]) VALUES (21, N'Xe AB ngon', CAST(N'2023-10-09T11:04:56.423' AS DateTime), 3, NULL, NULL, 5, N'Phat23')
INSERT [dbo].[Comments] ([ID], [CommentMsg], [CommentDate], [XeId], [UserID], [ParentID], [Rate], [UserName]) VALUES (25, N'ngon', CAST(N'2023-10-10T21:39:46.940' AS DateTime), 3, NULL, NULL, 3, N'doantam')
INSERT [dbo].[Comments] ([ID], [CommentMsg], [CommentDate], [XeId], [UserID], [ParentID], [Rate], [UserName]) VALUES (28, N'ok ngon', CAST(N'2023-10-12T11:18:05.940' AS DateTime), 2, NULL, NULL, 3, N'doantam')
INSERT [dbo].[Comments] ([ID], [CommentMsg], [CommentDate], [XeId], [UserID], [ParentID], [Rate], [UserName]) VALUES (30, N'ok', CAST(N'2023-10-12T11:27:16.537' AS DateTime), 3, NULL, NULL, 3, N'doantam')
INSERT [dbo].[Comments] ([ID], [CommentMsg], [CommentDate], [XeId], [UserID], [ParentID], [Rate], [UserName]) VALUES (37, N'ngonnnn', CAST(N'2023-10-12T23:29:28.793' AS DateTime), 2, NULL, NULL, 4, N'doantam')
INSERT [dbo].[Comments] ([ID], [CommentMsg], [CommentDate], [XeId], [UserID], [ParentID], [Rate], [UserName]) VALUES (38, N'ngonnn', CAST(N'2023-10-12T23:34:43.590' AS DateTime), 2, NULL, NULL, NULL, N'doantam')
INSERT [dbo].[Comments] ([ID], [CommentMsg], [CommentDate], [XeId], [UserID], [ParentID], [Rate], [UserName]) VALUES (39, N'adasdsad', CAST(N'2023-10-12T23:38:31.310' AS DateTime), 2, NULL, NULL, NULL, N'doantam')
INSERT [dbo].[Comments] ([ID], [CommentMsg], [CommentDate], [XeId], [UserID], [ParentID], [Rate], [UserName]) VALUES (40, N'4', CAST(N'2023-10-12T23:38:40.007' AS DateTime), 2, NULL, NULL, NULL, N'doantam')
INSERT [dbo].[Comments] ([ID], [CommentMsg], [CommentDate], [XeId], [UserID], [ParentID], [Rate], [UserName]) VALUES (41, NULL, CAST(N'2023-10-12T23:38:53.000' AS DateTime), 2, NULL, NULL, NULL, N'doantam')
INSERT [dbo].[Comments] ([ID], [CommentMsg], [CommentDate], [XeId], [UserID], [ParentID], [Rate], [UserName]) VALUES (42, N'sadasd', CAST(N'2023-10-12T23:39:47.417' AS DateTime), 2, NULL, NULL, 4, N'doantam')
INSERT [dbo].[Comments] ([ID], [CommentMsg], [CommentDate], [XeId], [UserID], [ParentID], [Rate], [UserName]) VALUES (43, N'đâsdsd', CAST(N'2023-10-12T23:39:57.573' AS DateTime), 2, NULL, NULL, 5, N'doantam')
INSERT [dbo].[Comments] ([ID], [CommentMsg], [CommentDate], [XeId], [UserID], [ParentID], [Rate], [UserName]) VALUES (44, N'vjp', CAST(N'2023-10-13T00:16:20.913' AS DateTime), 2, NULL, NULL, 4, N'doantam')
INSERT [dbo].[Comments] ([ID], [CommentMsg], [CommentDate], [XeId], [UserID], [ParentID], [Rate], [UserName]) VALUES (45, N'ok xe', CAST(N'2023-10-13T00:26:15.730' AS DateTime), 1, NULL, NULL, 3, N'tam123456')
INSERT [dbo].[Comments] ([ID], [CommentMsg], [CommentDate], [XeId], [UserID], [ParentID], [Rate], [UserName]) VALUES (46, N'xe ngon hjhj', CAST(N'2023-10-13T19:16:31.770' AS DateTime), 2, 2, NULL, 5, N'tam123456')
INSERT [dbo].[Comments] ([ID], [CommentMsg], [CommentDate], [XeId], [UserID], [ParentID], [Rate], [UserName]) VALUES (74, N'xe dinh vai', CAST(N'2023-11-09T17:17:00.017' AS DateTime), 5, 1, NULL, 5, N'doantam')
INSERT [dbo].[Comments] ([ID], [CommentMsg], [CommentDate], [XeId], [UserID], [ParentID], [Rate], [UserName]) VALUES (76, N'ok dc', CAST(N'2023-11-16T14:25:30.583' AS DateTime), 4, 5, NULL, 4, N'tuilanu')
INSERT [dbo].[Comments] ([ID], [CommentMsg], [CommentDate], [XeId], [UserID], [ParentID], [Rate], [UserName]) VALUES (78, N'xe chay cham', CAST(N'2023-11-19T23:31:56.417' AS DateTime), 4, 1, NULL, 1, N'doantam')
INSERT [dbo].[Comments] ([ID], [CommentMsg], [CommentDate], [XeId], [UserID], [ParentID], [Rate], [UserName]) VALUES (79, N'xe de thuong ghe a', CAST(N'2023-11-19T23:54:20.800' AS DateTime), 9, 1, NULL, 5, N'doantam')
INSERT [dbo].[Comments] ([ID], [CommentMsg], [CommentDate], [XeId], [UserID], [ParentID], [Rate], [UserName]) VALUES (81, N'hihi qua de thuong', CAST(N'2023-11-20T00:08:34.817' AS DateTime), 9, 5, NULL, 5, N'tuilanu')
INSERT [dbo].[Comments] ([ID], [CommentMsg], [CommentDate], [XeId], [UserID], [ParentID], [Rate], [UserName]) VALUES (83, N'saadas', CAST(N'2023-11-25T13:19:54.713' AS DateTime), 9, 1, NULL, 5, N'doantam')
SET IDENTITY_INSERT [dbo].[Comments] OFF
GO
SET IDENTITY_INSERT [dbo].[Customer] ON 

INSERT [dbo].[Customer] ([IDCus], [NameCus], [PhoneCus], [EmailCus], [UserCus], [PassCus], [Diachi], [Gioitinh], [Ngaysinh]) VALUES (1, N'tam123', N'0583331528', N'dt@123.com', N'doantam', N'150203T@m', NULL, NULL, NULL)
INSERT [dbo].[Customer] ([IDCus], [NameCus], [PhoneCus], [EmailCus], [UserCus], [PassCus], [Diachi], [Gioitinh], [Ngaysinh]) VALUES (2, N'tamne123', N'0583331522', N'dt@123.com', N'tam123456', N'150203T@m', NULL, NULL, NULL)
INSERT [dbo].[Customer] ([IDCus], [NameCus], [PhoneCus], [EmailCus], [UserCus], [PassCus], [Diachi], [Gioitinh], [Ngaysinh]) VALUES (3, N'Phat23', N'0987654321', N'Phat23@gmail.com', N'Phat23', N'Phat304032', NULL, NULL, NULL)
INSERT [dbo].[Customer] ([IDCus], [NameCus], [PhoneCus], [EmailCus], [UserCus], [PassCus], [Diachi], [Gioitinh], [Ngaysinh]) VALUES (4, N'Phat', N'0947690637', N'Phat23@gmail.com', N'Phat2304', N'Phat2304', NULL, NULL, NULL)
INSERT [dbo].[Customer] ([IDCus], [NameCus], [PhoneCus], [EmailCus], [UserCus], [PassCus], [Diachi], [Gioitinh], [Ngaysinh]) VALUES (5, N'tui là nữ', N'0123456789', N'tuilanu@gmail.com', N'tuilanu', N'15022003T@m', N'ql22 hoc mon', 0, CAST(N'2003-06-12' AS Date))
SET IDENTITY_INSERT [dbo].[Customer] OFF
GO
SET IDENTITY_INSERT [dbo].[HighChart] ON 

INSERT [dbo].[HighChart] ([Id], [Name], [ThoiGian]) VALUES (1, N'Xe 2 bánh', N'Thang10')
INSERT [dbo].[HighChart] ([Id], [Name], [ThoiGian]) VALUES (2, N'Xe 4 bánh', N'Thang9')
INSERT [dbo].[HighChart] ([Id], [Name], [ThoiGian]) VALUES (3, N'Xe 16 chỗ', N'Thang8')
INSERT [dbo].[HighChart] ([Id], [Name], [ThoiGian]) VALUES (4, N'Xe 4 bánh ', N'Thang10')
SET IDENTITY_INSERT [dbo].[HighChart] OFF
GO
SET IDENTITY_INSERT [dbo].[OrderDetail] ON 

INSERT [dbo].[OrderDetail] ([ID], [IDProduct], [IDOrder], [Quantity], [UnitPrice], [CreateDate]) VALUES (1, 1, 1, 4, 70000, NULL)
INSERT [dbo].[OrderDetail] ([ID], [IDProduct], [IDOrder], [Quantity], [UnitPrice], [CreateDate]) VALUES (2, 2, 2, 9, 120000, NULL)
INSERT [dbo].[OrderDetail] ([ID], [IDProduct], [IDOrder], [Quantity], [UnitPrice], [CreateDate]) VALUES (3, 1, 3, 4, 100000, NULL)
INSERT [dbo].[OrderDetail] ([ID], [IDProduct], [IDOrder], [Quantity], [UnitPrice], [CreateDate]) VALUES (4, 4, 4, 3, 170000, NULL)
INSERT [dbo].[OrderDetail] ([ID], [IDProduct], [IDOrder], [Quantity], [UnitPrice], [CreateDate]) VALUES (5, 2, 5, 23, 120000, NULL)
INSERT [dbo].[OrderDetail] ([ID], [IDProduct], [IDOrder], [Quantity], [UnitPrice], [CreateDate]) VALUES (6, 4, 6, 1, 140000, NULL)
INSERT [dbo].[OrderDetail] ([ID], [IDProduct], [IDOrder], [Quantity], [UnitPrice], [CreateDate]) VALUES (7, 4, 7, 2, 170000, NULL)
INSERT [dbo].[OrderDetail] ([ID], [IDProduct], [IDOrder], [Quantity], [UnitPrice], [CreateDate]) VALUES (8, 5, 8, 2, 300000, NULL)
INSERT [dbo].[OrderDetail] ([ID], [IDProduct], [IDOrder], [Quantity], [UnitPrice], [CreateDate]) VALUES (9, 4, 9, 1, 200000, NULL)
INSERT [dbo].[OrderDetail] ([ID], [IDProduct], [IDOrder], [Quantity], [UnitPrice], [CreateDate]) VALUES (10, 4, 10, 2, 200000, NULL)
INSERT [dbo].[OrderDetail] ([ID], [IDProduct], [IDOrder], [Quantity], [UnitPrice], [CreateDate]) VALUES (11, 4, 11, 2, 200000, NULL)
INSERT [dbo].[OrderDetail] ([ID], [IDProduct], [IDOrder], [Quantity], [UnitPrice], [CreateDate]) VALUES (12, 5, 12, 2, 300000, NULL)
INSERT [dbo].[OrderDetail] ([ID], [IDProduct], [IDOrder], [Quantity], [UnitPrice], [CreateDate]) VALUES (13, 5, 13, 1, 300000, NULL)
INSERT [dbo].[OrderDetail] ([ID], [IDProduct], [IDOrder], [Quantity], [UnitPrice], [CreateDate]) VALUES (14, 2, 14, 3, 90000, NULL)
INSERT [dbo].[OrderDetail] ([ID], [IDProduct], [IDOrder], [Quantity], [UnitPrice], [CreateDate]) VALUES (15, 10, 15, 2, 100000, NULL)
INSERT [dbo].[OrderDetail] ([ID], [IDProduct], [IDOrder], [Quantity], [UnitPrice], [CreateDate]) VALUES (16, 6, 16, 2, 300000, NULL)
INSERT [dbo].[OrderDetail] ([ID], [IDProduct], [IDOrder], [Quantity], [UnitPrice], [CreateDate]) VALUES (17, 9, 17, 2, 40000, NULL)
INSERT [dbo].[OrderDetail] ([ID], [IDProduct], [IDOrder], [Quantity], [UnitPrice], [CreateDate]) VALUES (18, 3, 18, 2, 270000, NULL)
INSERT [dbo].[OrderDetail] ([ID], [IDProduct], [IDOrder], [Quantity], [UnitPrice], [CreateDate]) VALUES (19, 9, 19, 2, 40000, NULL)
INSERT [dbo].[OrderDetail] ([ID], [IDProduct], [IDOrder], [Quantity], [UnitPrice], [CreateDate]) VALUES (20, 9, 20, 2, 100000, NULL)
SET IDENTITY_INSERT [dbo].[OrderDetail] OFF
GO
SET IDENTITY_INSERT [dbo].[OrderPro] ON 

INSERT [dbo].[OrderPro] ([ID], [DateOrder], [IDCus], [AddressDeliverry], [NameCusNonAccount], [PhoneCusNonAccount], [TypePayment], [OrderDate]) VALUES (1, CAST(N'2023-09-30' AS Date), 1, N'ben tre', NULL, NULL, 1, NULL)
INSERT [dbo].[OrderPro] ([ID], [DateOrder], [IDCus], [AddressDeliverry], [NameCusNonAccount], [PhoneCusNonAccount], [TypePayment], [OrderDate]) VALUES (2, CAST(N'2023-10-09' AS Date), 1, N'828 SVH', NULL, NULL, 1, NULL)
INSERT [dbo].[OrderPro] ([ID], [DateOrder], [IDCus], [AddressDeliverry], [NameCusNonAccount], [PhoneCusNonAccount], [TypePayment], [OrderDate]) VALUES (3, CAST(N'2023-10-27' AS Date), 1, NULL, N'tam123', N'0583331528', 2, NULL)
INSERT [dbo].[OrderPro] ([ID], [DateOrder], [IDCus], [AddressDeliverry], [NameCusNonAccount], [PhoneCusNonAccount], [TypePayment], [OrderDate]) VALUES (4, CAST(N'2023-10-30' AS Date), 1, NULL, N'tam123', N'0583331528', 2, NULL)
INSERT [dbo].[OrderPro] ([ID], [DateOrder], [IDCus], [AddressDeliverry], [NameCusNonAccount], [PhoneCusNonAccount], [TypePayment], [OrderDate]) VALUES (5, CAST(N'2023-10-30' AS Date), 4, NULL, N'Phat', N'0947690637', 2, NULL)
INSERT [dbo].[OrderPro] ([ID], [DateOrder], [IDCus], [AddressDeliverry], [NameCusNonAccount], [PhoneCusNonAccount], [TypePayment], [OrderDate]) VALUES (6, CAST(N'2023-11-01' AS Date), 5, NULL, N'tui là nữ', N'0123456789', 2, NULL)
INSERT [dbo].[OrderPro] ([ID], [DateOrder], [IDCus], [AddressDeliverry], [NameCusNonAccount], [PhoneCusNonAccount], [TypePayment], [OrderDate]) VALUES (7, CAST(N'2023-11-09' AS Date), 5, N'ql22 hoc mon', N'tui là nữ', N'0123456789', 2, NULL)
INSERT [dbo].[OrderPro] ([ID], [DateOrder], [IDCus], [AddressDeliverry], [NameCusNonAccount], [PhoneCusNonAccount], [TypePayment], [OrderDate]) VALUES (8, CAST(N'2023-11-09' AS Date), 1, NULL, N'tam123', N'0583331528', 2, NULL)
INSERT [dbo].[OrderPro] ([ID], [DateOrder], [IDCus], [AddressDeliverry], [NameCusNonAccount], [PhoneCusNonAccount], [TypePayment], [OrderDate]) VALUES (9, CAST(N'2023-11-15' AS Date), 1, N'ben tre', NULL, NULL, 1, NULL)
INSERT [dbo].[OrderPro] ([ID], [DateOrder], [IDCus], [AddressDeliverry], [NameCusNonAccount], [PhoneCusNonAccount], [TypePayment], [OrderDate]) VALUES (10, CAST(N'2023-11-15' AS Date), 5, N'ql22 hoc mon', N'tui là nữ', N'0123456789', 2, NULL)
INSERT [dbo].[OrderPro] ([ID], [DateOrder], [IDCus], [AddressDeliverry], [NameCusNonAccount], [PhoneCusNonAccount], [TypePayment], [OrderDate]) VALUES (11, CAST(N'2023-11-16' AS Date), 5, N'ben tre', NULL, NULL, 1, NULL)
INSERT [dbo].[OrderPro] ([ID], [DateOrder], [IDCus], [AddressDeliverry], [NameCusNonAccount], [PhoneCusNonAccount], [TypePayment], [OrderDate]) VALUES (12, CAST(N'2023-11-16' AS Date), 1, N'ben tre', NULL, NULL, 1, NULL)
INSERT [dbo].[OrderPro] ([ID], [DateOrder], [IDCus], [AddressDeliverry], [NameCusNonAccount], [PhoneCusNonAccount], [TypePayment], [OrderDate]) VALUES (13, CAST(N'2023-11-16' AS Date), 5, N'123', NULL, NULL, 1, NULL)
INSERT [dbo].[OrderPro] ([ID], [DateOrder], [IDCus], [AddressDeliverry], [NameCusNonAccount], [PhoneCusNonAccount], [TypePayment], [OrderDate]) VALUES (14, CAST(N'2023-11-19' AS Date), 5, N'ql22 hoc mon', N'tui là nữ', N'0123456789', 2, NULL)
INSERT [dbo].[OrderPro] ([ID], [DateOrder], [IDCus], [AddressDeliverry], [NameCusNonAccount], [PhoneCusNonAccount], [TypePayment], [OrderDate]) VALUES (15, CAST(N'2023-11-20' AS Date), 1, NULL, N'tam123', N'0583331528', 2, NULL)
INSERT [dbo].[OrderPro] ([ID], [DateOrder], [IDCus], [AddressDeliverry], [NameCusNonAccount], [PhoneCusNonAccount], [TypePayment], [OrderDate]) VALUES (16, CAST(N'2023-11-20' AS Date), 1, NULL, N'tam123', N'0583331528', 2, NULL)
INSERT [dbo].[OrderPro] ([ID], [DateOrder], [IDCus], [AddressDeliverry], [NameCusNonAccount], [PhoneCusNonAccount], [TypePayment], [OrderDate]) VALUES (17, CAST(N'2023-11-20' AS Date), 5, N'ql22 hoc mon', N'tui là nữ', N'0123456789', 2, NULL)
INSERT [dbo].[OrderPro] ([ID], [DateOrder], [IDCus], [AddressDeliverry], [NameCusNonAccount], [PhoneCusNonAccount], [TypePayment], [OrderDate]) VALUES (18, CAST(N'2023-11-20' AS Date), 5, N'ben tre', NULL, NULL, 1, NULL)
INSERT [dbo].[OrderPro] ([ID], [DateOrder], [IDCus], [AddressDeliverry], [NameCusNonAccount], [PhoneCusNonAccount], [TypePayment], [OrderDate]) VALUES (19, CAST(N'2023-11-20' AS Date), 5, N'ben tre', NULL, NULL, 1, NULL)
INSERT [dbo].[OrderPro] ([ID], [DateOrder], [IDCus], [AddressDeliverry], [NameCusNonAccount], [PhoneCusNonAccount], [TypePayment], [OrderDate]) VALUES (20, CAST(N'2023-11-20' AS Date), 1, N'ben tre', NULL, NULL, 1, NULL)
SET IDENTITY_INSERT [dbo].[OrderPro] OFF
GO
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([ProductID], [NamePro], [DecriptionPro], [Category], [Price], [ImagePro], [MauXe], [Vitri], [Quantity], [IsGiveBack]) VALUES (1, N'Xe Wave ', N'Wave Alpha được trang bị động cơ 110cc với hiệu suất vượt trội nhưng vẫn đảm bảo tiết kiệm nhiên liệu tối ưu, cho bạn thêm tự tin và trải nghiệm tốt nhất trên mọi hành trình. Thêm vào đó, 4 màu - 2 phiên bản cùng thiết kế bộ tem mới phong cách đầy ấn tượng trên xe giúp bạn thể hiện sự trẻ trung, năng động, thu hút mọi ánh nhìn.', N'001                 ', CAST(100000.00 AS Decimal(18, 2)), N'~/Content/images/xewave.jpg', N'Honda', N'Quận 6', 920, NULL)
INSERT [dbo].[Products] ([ProductID], [NamePro], [DecriptionPro], [Category], [Price], [ImagePro], [MauXe], [Vitri], [Quantity], [IsGiveBack]) VALUES (2, N'Xe SH 150i', N'Honda lần đầu giới thiệu dòng xe SH50 tại thị trường Châu Âu nhiều tiềm năng và đầy tính cạnh tranh vào năm 1984. Dòng xe này được trang bị bánh xe rộng 16 inch có khả năng vận hành cao với sàn để chân phẳng cho phép người lái với tư thế thoải mái nhất và công nghệ V-Matic truyền động liên tục phù hợp cho mọi lứa tuổi, giới tính. SH50 đã mở ra một thị trường xe gắn máy mới ở Châu Âu về dòng xe thời trang và cao cấp.', N'001                 ', CAST(150000.00 AS Decimal(18, 2)), N'~/Content/images/xesh.jpg', N'Honda', N'Quận 5', 965, NULL)
INSERT [dbo].[Products] ([ProductID], [NamePro], [DecriptionPro], [Category], [Price], [ImagePro], [MauXe], [Vitri], [Quantity], [IsGiveBack]) VALUES (3, N'Xe AB', N'Air Blade là dòng xe đang giữ vị trí số 01 về số lượng xe bán ra tại Việt Nam. Kể từ khi ra mắt lần đầu tiên vào tháng 4 năm 2007 đến nay, đã có hơn 2,7 triệu chiếc xe Air Blade đến tay người tiêu dùng. Thiết kế thể thao, hiện đại cùng công nghệ mang tính đột phá, khả năng vận hành ưu việt mà Air Blade luôn hướng tới đã và đang được khách hàng Việt Nam tiếp nhận một cách nồng nhiệt.  Vào ngày 27 tháng 11 năm 2015, tại thành phố Hồ Chí Minh, Honda Việt Nam chính thức giới thiệu ra thị trường phiên bản hoàn toàn mới của dòng xe Air Blade 125cc.', N'001                 ', CAST(300000.00 AS Decimal(18, 2)), N'~/Content/images/xe ab.jpg', N'Honda', N'Quận 7', 1998, NULL)
INSERT [dbo].[Products] ([ProductID], [NamePro], [DecriptionPro], [Category], [Price], [ImagePro], [MauXe], [Vitri], [Quantity], [IsGiveBack]) VALUES (4, N'Xe 16 chỗ', N'Xe gia đình, hoàn toàn không có logo taxi hay bất kì hình ảnh nào cho thấy bạn đi xe thuê. Rất thuận tiện và uy tín khi bạn đi gặp đối tác làm ăn, đi thăm xui gia hay đi các mục đích khác.', N'003                 ', CAST(200000.00 AS Decimal(18, 2)), N'~/Content/images/xe16cho.jpg', N'Vận chuyển khách', N'Quận 3', 989, NULL)
INSERT [dbo].[Products] ([ProductID], [NamePro], [DecriptionPro], [Category], [Price], [ImagePro], [MauXe], [Vitri], [Quantity], [IsGiveBack]) VALUES (5, N'Xe Mec', N'Mercedes là một trong những hãng xe lâu đời nhất thế giới, ở phân khúc hạng sang. Thương hiệu ngôi sao ba cánh nước Đức đến Việt Nam năm 1995 và hiện là hãng xe sang duy nhất có nhà máy lắp ráp nơi đây. ', N'002                 ', CAST(300000.00 AS Decimal(18, 2)), N'~/Content/images/mec.jpg', N'Thể thao', N'Quận 1', 95, NULL)
INSERT [dbo].[Products] ([ProductID], [NamePro], [DecriptionPro], [Category], [Price], [ImagePro], [MauXe], [Vitri], [Quantity], [IsGiveBack]) VALUES (6, N'Xe BMW', N'BMW là hãng xe của Đức, cùng Mercedes và Audi tạo nên bộ tam thống trị phân khúc xe hạng sang toàn cầu. Hãng xe có trụ sở tại Munich nổi tiếng với triết lý nam tính, thể thao trong thiết kế. ', N'002                 ', CAST(300000.00 AS Decimal(18, 2)), N'~/Content/images/bmw.jpg', N'Thể thao', N'Quận 1', 98, NULL)
INSERT [dbo].[Products] ([ProductID], [NamePro], [DecriptionPro], [Category], [Price], [ImagePro], [MauXe], [Vitri], [Quantity], [IsGiveBack]) VALUES (7, N'Xe Lamborghini', N'Giá xe Lamborghini Aventador tại Việt Nam sẽ gấp khoảng 4 lần so với giá bán tại Mỹ bởi các loại thuế phí khá cao như thuế nhập khẩu ô tô loại dung tích lớn (47 – 52%), thuế tiêu thụ đặc biệt (150%), thuế VAT (10%), thuế trước bạ (10 – 12%)…  Về giá Lamborghini Aventador cũ, tuỳ theo phiên bản (loại xe) cũng như tình trạng mà giá xe Aventador cũ có sự chênh lệch khác nhau. Tuy nhiên, trong các kỳ mua bán xe Lamborghini Aventador đã qua sử dụng gần đây, hầu như xe Aventador cũ thường có giá bán tầm trên 15 tỷ đồng.', N'002                 ', CAST(5000000.00 AS Decimal(18, 2)), N'~/Content/images/xelambor.jpg', N'Thể thao', N'Quận 1', 1000, NULL)
INSERT [dbo].[Products] ([ProductID], [NamePro], [DecriptionPro], [Category], [Price], [ImagePro], [MauXe], [Vitri], [Quantity], [IsGiveBack]) VALUES (8, N'Xe Hoa Lam', N'Giá xe Hoa Lâm hiện nay khoảng bao nhiêu? Được khá nhiều người quan tâm. Vì xe ba bánh Hoa Lâm hiện nay đang là phương tiện thông dụng để vận chuyển hàng hóa. Đối với thành phố lớn. Địa hình nhiều ngõ hẹp khiến xe có kích thước lớn không vào được. Hoặc các vùng nông thôn, nhu cầu vận chuyển ngày càng lên cao. Thì xe 3 bánh với thiết kế nhỏ gọn là sự lựa chọn phù hợp nhất.', N'1523                ', CAST(100000.00 AS Decimal(18, 2)), N'~/Content/images/xehoalam.jpg', N'Vận chuyển hàng', N'Quận 6', 10000, NULL)
INSERT [dbo].[Products] ([ProductID], [NamePro], [DecriptionPro], [Category], [Price], [ImagePro], [MauXe], [Vitri], [Quantity], [IsGiveBack]) VALUES (9, N'Xe Đạp ', N'Hệ thống chuyển động của xe được trang bị bộ tay đề Shimano Claris 2×8 và các bộ chuyển động Shimano Claris 2-speed ở phía trước và Shimano Claris 8-speed ở phía sau. Bộ thắng Tektro mechanical rim brakes và tay thắng Shimano Claris giúp đảm bảo khả năng phanh an toàn và hiệu quả.  Bộ líp xe được sử dụng là Shimano CS-HG50, 11×34, cùng với sên xe KMC X9 và giò đĩa FSA Tempo, 34/50, giúp tạo ra một hệ thống truyền động mạnh mẽ và linh hoạt.', N'1236                ', CAST(100000.00 AS Decimal(18, 2)), N'~/Content/images/xedaphong.jpg', N'Xe tự đạp', N'Quận 8', 2869, NULL)
INSERT [dbo].[Products] ([ProductID], [NamePro], [DecriptionPro], [Category], [Price], [ImagePro], [MauXe], [Vitri], [Quantity], [IsGiveBack]) VALUES (10, N'Xe 32 Chỗ', N'8 lý do nên chọn Thaco TOWN TB82S, xe khách 29 chỗ của Trường Hải. - Có thiết kế sang trọng, kiểu dáng hiện đại giống như dòng Universe nổi tiếng của hãng Huyndai. - Có không gian nội thất rộng rãi, ghế ngồi cao cấp có tựa đầu bằng hơi. - Có hệ thống điều hoà bao lạnh, hệ thống giải trí bao đã. - Có hệ thống treo sử dụng 6 bầu hơi, tạo sự êm ái trên mọi nẻo đường. Không như Samco Felix dùng hệ thống treo nhíp. - Có thiết kế khung gầm chuyên biệt cho dòng xe khách không như Samco Felix sử dụng khung gầm xe tải. - Có động cơ đặt phía sau không như Samco Felix đặt động cơ phía trước. Nếu đặt động cơ phía trước sẽ làm xấu không gian bên trong xe, tiếng ồn và mùi dầu ảnh hưởng đến hành khách.', N'003                 ', CAST(100000.00 AS Decimal(18, 2)), N'~/Content/images/xe32cho.jpg', N'Vận chuyển khách', N'Quận 3', 998, NULL)
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([Id], [RoleName]) VALUES (1, N'Admin')
INSERT [dbo].[Roles] ([Id], [RoleName]) VALUES (2, N'Employee')
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
INSERT [dbo].[Vouchers] ([Id], [Code], [DiscountAmount]) VALUES (1, N'TAM123', CAST(30000.00 AS Decimal(18, 2)))
INSERT [dbo].[Vouchers] ([Id], [Code], [DiscountAmount]) VALUES (12603, N'SUKIENPHUNU', CAST(60000.00 AS Decimal(18, 2)))
GO
ALTER TABLE [dbo].[Products] ADD  DEFAULT ((0)) FOR [Quantity]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[Customer] ([IDCus])
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD FOREIGN KEY([XeId])
REFERENCES [dbo].[Products] ([ProductID])
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD FOREIGN KEY([IDOrder])
REFERENCES [dbo].[OrderPro] ([ID])
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD FOREIGN KEY([IDProduct])
REFERENCES [dbo].[Products] ([ProductID])
GO
ALTER TABLE [dbo].[OrderPro]  WITH CHECK ADD FOREIGN KEY([IDCus])
REFERENCES [dbo].[Customer] ([IDCus])
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Pro_Category] FOREIGN KEY([Category])
REFERENCES [dbo].[Category] ([IDCate])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Pro_Category]
GO
ALTER TABLE [dbo].[UserRoleMapping]  WITH CHECK ADD FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([Id])
GO
ALTER TABLE [dbo].[UserRoleMapping]  WITH CHECK ADD FOREIGN KEY([UserId])
REFERENCES [dbo].[Admins] ([Id])
GO
