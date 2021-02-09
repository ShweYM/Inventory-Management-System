USE [InventoryManagementSystem3]
GO
/****** Object:  Table [dbo].[CollectionPoints]    Script Date: 2020/8/31 7:53:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CollectionPoints](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CollectionName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_CollectionPoints] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DelegationForms]    Script Date: 2020/8/31 7:53:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DelegationForms](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DepartmentHeadId] [int] NULL,
	[DelegateeId] [int] NULL,
	[startDate] [datetime2](7) NOT NULL,
	[endDate] [datetime2](7) NOT NULL,
	[delegateComment] [nvarchar](max) NULL,
	[DLStatus] [int] NOT NULL,
	[DLAssignedDate] [datetime2](7) NOT NULL,
	[DelegatedTypeId] [int] NULL,
 CONSTRAINT [PK_DelegationForms] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DeliveryOrders]    Script Date: 2020/8/31 7:53:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DeliveryOrders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DeliveryOrderNo] [nvarchar](max) NULL,
	[SupplierId] [int] NULL,
	[PurchaseOrderId] [int] NULL,
	[DOComments] [nvarchar](max) NULL,
	[DOReceivedDate] [datetime2](7) NOT NULL,
	[ReceivedById] [int] NULL,
	[DOCode] [nvarchar](max) NULL,
	[DOStatus] [int] NOT NULL,
 CONSTRAINT [PK_DeliveryOrders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DeliveryOrderSupplierProducts]    Script Date: 2020/8/31 7:53:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DeliveryOrderSupplierProducts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DeliveryOrderId] [int] NULL,
	[DOQuantityReceived] [int] NOT NULL,
	[PurchaseOrderSupplierProductId] [int] NULL,
 CONSTRAINT [PK_DeliveryOrderSupplierProducts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Departments]    Script Date: 2020/8/31 7:53:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Departments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DepartmentName] [nvarchar](max) NOT NULL,
	[DepHeadId] [int] NOT NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[CollectionPointId] [int] NULL,
	[DeptCode] [nvarchar](max) NULL,
	[DeptRepId] [int] NOT NULL,
 CONSTRAINT [PK_Departments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DisbursementFormProducts]    Script Date: 2020/8/31 7:53:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DisbursementFormProducts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DisbursementFormId] [int] NULL,
	[ProductId] [int] NULL,
	[ProductToDeliverTotal] [int] NOT NULL,
	[ProductDeliveredTotal] [int] NOT NULL,
 CONSTRAINT [PK_DisbursementFormProducts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DisbursementFormRequisitionFormProducts]    Script Date: 2020/8/31 7:53:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DisbursementFormRequisitionFormProducts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DisbursementFormId] [int] NULL,
	[RequisitionFormsProductId] [int] NULL,
	[ProductCollected] [int] NOT NULL,
 CONSTRAINT [PK_DisbursementFormRequisitionFormProducts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DisbursementFormRequisitionForms]    Script Date: 2020/8/31 7:53:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DisbursementFormRequisitionForms](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DisbursementFormId] [int] NULL,
	[RequisitionFormId] [int] NULL,
	[DFRFStatus] [int] NOT NULL,
 CONSTRAINT [PK_DisbursementFormRequisitionForms] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DisbursementForms]    Script Date: 2020/8/31 7:53:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DisbursementForms](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StoreClerkId] [int] NULL,
	[DeptRepId] [int] NULL,
	[DFStatus] [int] NOT NULL,
	[DFHandoverDate] [datetime2](7) NOT NULL,
	[DFDeliveryDate] [datetime2](7) NOT NULL,
	[DFDate] [datetime2](7) NOT NULL,
	[DFComments] [nvarchar](max) NULL,
	[CollectionPointId] [int] NULL,
	[DFCode] [nvarchar](max) NULL,
	[DFTransAssignDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_DisbursementForms] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 2020/8/31 7:53:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[Firstname] [nvarchar](max) NULL,
	[Lastname] [nvarchar](max) NULL,
	[Password] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[DeptRep] [nvarchar](max) NULL,
	[SupervisedById] [int] NULL,
	[EmployeeTypeId] [int] NULL,
	[DepartmentId] [int] NULL,
	[TempDeptHeadTypeId] [int] NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeeTypes]    Script Date: 2020/8/31 7:53:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeTypeName] [nvarchar](max) NULL,
 CONSTRAINT [PK_EmployeeTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InventoryTransactions]    Script Date: 2020/8/31 7:53:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InventoryTransactions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductId1] [int] NULL,
	[ITStatus] [int] NOT NULL,
	[InventoryChangeQuantity] [int] NOT NULL,
	[InventoryTransComments] [nvarchar](max) NOT NULL,
	[InventoryTransDate] [datetime2](7) NOT NULL,
	[ITCode] [nvarchar](450) NOT NULL,
	[EmployeeId1] [int] NULL,
	[ApprovalComment] [nvarchar](max) NULL,
	[ITApprovalDate] [datetime2](7) NOT NULL,
	[ITApprovalById] [int] NULL,
	[ProductPrice] [real] NOT NULL,
 CONSTRAINT [PK_InventoryTransactions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductCategories]    Script Date: 2020/8/31 7:53:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductCategories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductCategoryName] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_ProductCategories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 2020/8/31 7:53:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [nvarchar](max) NOT NULL,
	[ProductCode] [nvarchar](max) NOT NULL,
	[ProductCategoryId] [int] NOT NULL,
	[ProductDescription] [nvarchar](max) NULL,
	[Units] [nvarchar](max) NULL,
	[ProductImage] [nvarchar](max) NULL,
	[InventoryQuantity] [int] NOT NULL,
	[ReorderLevel] [int] NOT NULL,
	[ReorderQuantity] [int] NOT NULL,
	[InventoryLocation] [nvarchar](max) NULL,
	[MLReorderQuantity] [int] NOT NULL,
	[MLReorderLevel] [int] NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PurchaseOrders]    Script Date: 2020/8/31 7:53:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseOrders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[supplierId] [int] NULL,
	[DeliverTo] [nvarchar](max) NULL,
	[expectedDeliveryDate] [datetime2](7) NOT NULL,
	[IssuedById] [int] NULL,
	[POStatus] [int] NOT NULL,
	[POComments] [nvarchar](max) NULL,
	[POIssueDate] [datetime2](7) NOT NULL,
	[POCode] [nvarchar](max) NULL,
 CONSTRAINT [PK_PurchaseOrders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PurchaseOrderSupplierProducts]    Script Date: 2020/8/31 7:53:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseOrderSupplierProducts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SupplierProductId] [int] NULL,
	[PurchaseOrderId] [int] NULL,
	[POUnitPrice] [real] NOT NULL,
	[POQuantityRequested] [int] NOT NULL,
 CONSTRAINT [PK_PurchaseOrderSupplierProducts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RequisitionForms]    Script Date: 2020/8/31 7:53:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RequisitionForms](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NULL,
	[RFCode] [nvarchar](max) NULL,
	[RFStatus] [int] NOT NULL,
	[RFComments] [nvarchar](max) NULL,
	[RFDate] [datetime2](7) NOT NULL,
	[RFApprovalDate] [datetime2](7) NOT NULL,
	[RFApprovalById] [int] NULL,
	[RFHeadReply] [nvarchar](max) NULL,
 CONSTRAINT [PK_RequisitionForms] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RequisitionFormsProducts]    Script Date: 2020/8/31 7:53:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RequisitionFormsProducts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RequisitionFormId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[ProductRequested] [int] NOT NULL,
	[ProductApproved] [int] NOT NULL,
	[ProductCollectedTotal] [int] NOT NULL,
 CONSTRAINT [PK_RequisitionFormsProducts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StationeryRetrievalProduct]    Script Date: 2020/8/31 7:53:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StationeryRetrievalProduct](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StationeryRetrievalId] [int] NULL,
	[ProductId] [int] NULL,
	[ProductRequestedTotal] [int] NOT NULL,
	[ProductReceivedTotal] [int] NOT NULL,
 CONSTRAINT [PK_StationeryRetrievalProduct] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StationeryRetrievalRequisitionFormProducts]    Script Date: 2020/8/31 7:53:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StationeryRetrievalRequisitionFormProducts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductAssigned] [int] NOT NULL,
	[SRId] [int] NULL,
	[RFPId] [int] NULL,
 CONSTRAINT [PK_StationeryRetrievalRequisitionFormProducts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StationeryRetrievalRequisitionForms]    Script Date: 2020/8/31 7:53:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StationeryRetrievalRequisitionForms](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StationeryRetrievalId] [int] NULL,
	[RequisitionFormId] [int] NULL,
	[SRRFStatus] [int] NOT NULL,
 CONSTRAINT [PK_StationeryRetrievalRequisitionForms] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StationeryRetrievals]    Script Date: 2020/8/31 7:53:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StationeryRetrievals](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[StoreClerkId] [int] NOT NULL,
	[WarehousePackerId] [int] NULL,
	[SRCode] [nvarchar](max) NULL,
	[SRStatus] [int] NOT NULL,
	[SRComments] [nvarchar](max) NULL,
	[SRDate] [datetime2](7) NOT NULL,
	[SRAssignedDate] [datetime2](7) NOT NULL,
	[SRRetrievalDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_StationeryRetrievals] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SupplierProducts]    Script Date: 2020/8/31 7:53:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SupplierProducts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SupplierId] [int] NULL,
	[ProductId] [int] NULL,
	[ProductPrice] [real] NOT NULL,
	[SPAvailStatus] [int] NOT NULL,
	[PriorityLevel] [int] NOT NULL,
 CONSTRAINT [PK_SupplierProducts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Suppliers]    Script Date: 2020/8/31 7:53:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Suppliers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SupplierCode] [nvarchar](max) NULL,
	[SupplierName] [nvarchar](max) NULL,
	[ContactName] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[FaxNumber] [nvarchar](max) NULL,
	[Address] [nvarchar](max) NULL,
	[GSTregistrationNumber] [nvarchar](max) NULL,
 CONSTRAINT [PK_Suppliers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[CollectionPoints] ON 

INSERT [dbo].[CollectionPoints] ([Id], [CollectionName]) VALUES (1, N'Collection Point 6')
INSERT [dbo].[CollectionPoints] ([Id], [CollectionName]) VALUES (2, N'Collection Point 5')
INSERT [dbo].[CollectionPoints] ([Id], [CollectionName]) VALUES (3, N'Collection Point 4')
INSERT [dbo].[CollectionPoints] ([Id], [CollectionName]) VALUES (4, N'Collection Point 3')
INSERT [dbo].[CollectionPoints] ([Id], [CollectionName]) VALUES (5, N'Collection Point 2')
INSERT [dbo].[CollectionPoints] ([Id], [CollectionName]) VALUES (6, N'Collection Point 1')
SET IDENTITY_INSERT [dbo].[CollectionPoints] OFF
GO
SET IDENTITY_INSERT [dbo].[DeliveryOrders] ON 

INSERT [dbo].[DeliveryOrders] ([Id], [DeliveryOrderNo], [SupplierId], [PurchaseOrderId], [DOComments], [DOReceivedDate], [ReceivedById], [DOCode], [DOStatus]) VALUES (1, N'D011267', 2, 1, NULL, CAST(N'2020-09-13T09:30:20.0000000' AS DateTime2), NULL, N'DO/130920/1', 0)
INSERT [dbo].[DeliveryOrders] ([Id], [DeliveryOrderNo], [SupplierId], [PurchaseOrderId], [DOComments], [DOReceivedDate], [ReceivedById], [DOCode], [DOStatus]) VALUES (2, N'D0121567', 1, 2, NULL, CAST(N'2020-09-12T08:30:20.0000000' AS DateTime2), NULL, N'DO/120920/1', 0)
INSERT [dbo].[DeliveryOrders] ([Id], [DeliveryOrderNo], [SupplierId], [PurchaseOrderId], [DOComments], [DOReceivedDate], [ReceivedById], [DOCode], [DOStatus]) VALUES (3, NULL, 8, 5, N'test do', CAST(N'2020-08-27T06:46:38.2699515' AS DateTime2), 27, N'DO/270820/1', 1)
INSERT [dbo].[DeliveryOrders] ([Id], [DeliveryOrderNo], [SupplierId], [PurchaseOrderId], [DOComments], [DOReceivedDate], [ReceivedById], [DOCode], [DOStatus]) VALUES (4, NULL, 11, 6, N'test for notcom', CAST(N'2020-08-27T06:47:21.7278376' AS DateTime2), 27, N'DO/270820/2', 1)
SET IDENTITY_INSERT [dbo].[DeliveryOrders] OFF
GO
SET IDENTITY_INSERT [dbo].[DeliveryOrderSupplierProducts] ON 

INSERT [dbo].[DeliveryOrderSupplierProducts] ([Id], [DeliveryOrderId], [DOQuantityReceived], [PurchaseOrderSupplierProductId]) VALUES (1, 3, 50, 6)
INSERT [dbo].[DeliveryOrderSupplierProducts] ([Id], [DeliveryOrderId], [DOQuantityReceived], [PurchaseOrderSupplierProductId]) VALUES (2, 4, 20, 7)
SET IDENTITY_INSERT [dbo].[DeliveryOrderSupplierProducts] OFF
GO
SET IDENTITY_INSERT [dbo].[Departments] ON 

INSERT [dbo].[Departments] ([Id], [DepartmentName], [DepHeadId], [PhoneNumber], [Email], [CollectionPointId], [DeptCode], [DeptRepId]) VALUES (1, N'Management Department', 0, N'86957412', NULL, 1, N'MNGD', 0)
INSERT [dbo].[Departments] ([Id], [DepartmentName], [DepHeadId], [PhoneNumber], [Email], [CollectionPointId], [DeptCode], [DeptRepId]) VALUES (2, N'Science Department', 0, N'68425397', NULL, 2, N'SCID', 0)
INSERT [dbo].[Departments] ([Id], [DepartmentName], [DepHeadId], [PhoneNumber], [Email], [CollectionPointId], [DeptCode], [DeptRepId]) VALUES (3, N'Music Department', 0, N'78954120', NULL, 3, N'MSDP', 0)
INSERT [dbo].[Departments] ([Id], [DepartmentName], [DepHeadId], [PhoneNumber], [Email], [CollectionPointId], [DeptCode], [DeptRepId]) VALUES (4, N'Business Department', 0, N'15397864', NULL, 3, N'BIZD', 0)
INSERT [dbo].[Departments] ([Id], [DepartmentName], [DepHeadId], [PhoneNumber], [Email], [CollectionPointId], [DeptCode], [DeptRepId]) VALUES (5, N'HQ Department', 0, N'32540325', NULL, 4, N'HQDP', 0)
INSERT [dbo].[Departments] ([Id], [DepartmentName], [DepHeadId], [PhoneNumber], [Email], [CollectionPointId], [DeptCode], [DeptRepId]) VALUES (6, N'Arts Department', 0, N'53046012', NULL, 4, N'ARTD', 0)
INSERT [dbo].[Departments] ([Id], [DepartmentName], [DepHeadId], [PhoneNumber], [Email], [CollectionPointId], [DeptCode], [DeptRepId]) VALUES (7, N'Chinese Department', 0, N'87654321', NULL, 5, N'CHSL', 0)
INSERT [dbo].[Departments] ([Id], [DepartmentName], [DepHeadId], [PhoneNumber], [Email], [CollectionPointId], [DeptCode], [DeptRepId]) VALUES (8, N'English Department', 0, N'98765432', NULL, 5, N'ENDP', 0)
INSERT [dbo].[Departments] ([Id], [DepartmentName], [DepHeadId], [PhoneNumber], [Email], [CollectionPointId], [DeptCode], [DeptRepId]) VALUES (9, N'Engineering Department', 0, N'76543219', NULL, 6, N'ENGD', 0)
INSERT [dbo].[Departments] ([Id], [DepartmentName], [DepHeadId], [PhoneNumber], [Email], [CollectionPointId], [DeptCode], [DeptRepId]) VALUES (10, N'Store', 0, N'12345678', NULL, 6, N'STOR', 0)
SET IDENTITY_INSERT [dbo].[Departments] OFF
GO
SET IDENTITY_INSERT [dbo].[DisbursementFormProducts] ON 

INSERT [dbo].[DisbursementFormProducts] ([Id], [DisbursementFormId], [ProductId], [ProductToDeliverTotal], [ProductDeliveredTotal]) VALUES (1, 2, 2, 40, 0)
INSERT [dbo].[DisbursementFormProducts] ([Id], [DisbursementFormId], [ProductId], [ProductToDeliverTotal], [ProductDeliveredTotal]) VALUES (2, 2, 1, 10, 0)
INSERT [dbo].[DisbursementFormProducts] ([Id], [DisbursementFormId], [ProductId], [ProductToDeliverTotal], [ProductDeliveredTotal]) VALUES (3, 3, 27, 0, 0)
INSERT [dbo].[DisbursementFormProducts] ([Id], [DisbursementFormId], [ProductId], [ProductToDeliverTotal], [ProductDeliveredTotal]) VALUES (4, 3, 28, 0, 0)
INSERT [dbo].[DisbursementFormProducts] ([Id], [DisbursementFormId], [ProductId], [ProductToDeliverTotal], [ProductDeliveredTotal]) VALUES (5, 3, 29, 0, 0)
INSERT [dbo].[DisbursementFormProducts] ([Id], [DisbursementFormId], [ProductId], [ProductToDeliverTotal], [ProductDeliveredTotal]) VALUES (6, 3, 30, 0, 0)
SET IDENTITY_INSERT [dbo].[DisbursementFormProducts] OFF
GO
SET IDENTITY_INSERT [dbo].[DisbursementFormRequisitionFormProducts] ON 

INSERT [dbo].[DisbursementFormRequisitionFormProducts] ([Id], [DisbursementFormId], [RequisitionFormsProductId], [ProductCollected]) VALUES (1, 2, 7, 7)
INSERT [dbo].[DisbursementFormRequisitionFormProducts] ([Id], [DisbursementFormId], [RequisitionFormsProductId], [ProductCollected]) VALUES (2, 2, 8, 9)
INSERT [dbo].[DisbursementFormRequisitionFormProducts] ([Id], [DisbursementFormId], [RequisitionFormsProductId], [ProductCollected]) VALUES (3, 3, 17, 0)
INSERT [dbo].[DisbursementFormRequisitionFormProducts] ([Id], [DisbursementFormId], [RequisitionFormsProductId], [ProductCollected]) VALUES (4, 3, 18, 0)
INSERT [dbo].[DisbursementFormRequisitionFormProducts] ([Id], [DisbursementFormId], [RequisitionFormsProductId], [ProductCollected]) VALUES (5, 3, 19, 0)
INSERT [dbo].[DisbursementFormRequisitionFormProducts] ([Id], [DisbursementFormId], [RequisitionFormsProductId], [ProductCollected]) VALUES (6, 3, 20, 0)
SET IDENTITY_INSERT [dbo].[DisbursementFormRequisitionFormProducts] OFF
GO
SET IDENTITY_INSERT [dbo].[DisbursementFormRequisitionForms] ON 

INSERT [dbo].[DisbursementFormRequisitionForms] ([Id], [DisbursementFormId], [RequisitionFormId], [DFRFStatus]) VALUES (1, 2, 3, 0)
INSERT [dbo].[DisbursementFormRequisitionForms] ([Id], [DisbursementFormId], [RequisitionFormId], [DFRFStatus]) VALUES (2, 2, 4, 0)
INSERT [dbo].[DisbursementFormRequisitionForms] ([Id], [DisbursementFormId], [RequisitionFormId], [DFRFStatus]) VALUES (3, 3, 9, 0)
SET IDENTITY_INSERT [dbo].[DisbursementFormRequisitionForms] OFF
GO
SET IDENTITY_INSERT [dbo].[DisbursementForms] ON 

INSERT [dbo].[DisbursementForms] ([Id], [StoreClerkId], [DeptRepId], [DFStatus], [DFHandoverDate], [DFDeliveryDate], [DFDate], [DFComments], [CollectionPointId], [DFCode], [DFTransAssignDate]) VALUES (1, 18, 19, 3, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'2020-08-14T08:30:20.0000000' AS DateTime2), CAST(N'2020-08-13T08:30:20.0000000' AS DateTime2), NULL, 5, N'DF/SCID/130820/2', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[DisbursementForms] ([Id], [StoreClerkId], [DeptRepId], [DFStatus], [DFHandoverDate], [DFDeliveryDate], [DFDate], [DFComments], [CollectionPointId], [DFCode], [DFTransAssignDate]) VALUES (2, 18, 19, 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'2020-08-14T08:30:20.0000000' AS DateTime2), CAST(N'2020-08-13T08:30:20.0000000' AS DateTime2), NULL, 6, N'DF/SCID/130820/1', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[DisbursementForms] ([Id], [StoreClerkId], [DeptRepId], [DFStatus], [DFHandoverDate], [DFDeliveryDate], [DFDate], [DFComments], [CollectionPointId], [DFCode], [DFTransAssignDate]) VALUES (3, 28, 4, 0, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'2020-08-28T06:36:45.6416270' AS DateTime2), CAST(N'2020-08-27T06:36:45.6416270' AS DateTime2), N'test 1 ', 6, N'DF/ENGD/270820/1', CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
SET IDENTITY_INSERT [dbo].[DisbursementForms] OFF
GO
SET IDENTITY_INSERT [dbo].[Employees] ON 

INSERT [dbo].[Employees] ([Id], [Username], [Email], [Firstname], [Lastname], [Password], [PhoneNumber], [DeptRep], [SupervisedById], [EmployeeTypeId], [DepartmentId], [TempDeptHeadTypeId]) VALUES (1, N'dsc', N'darellchua2@gmail.com', N'testF dsc', N'testL dsc', N'1234', N'25354558', NULL, 17, 8, 10, NULL)
INSERT [dbo].[Employees] ([Id], [Username], [Email], [Firstname], [Lastname], [Password], [PhoneNumber], [DeptRep], [SupervisedById], [EmployeeTypeId], [DepartmentId], [TempDeptHeadTypeId]) VALUES (2, N'd4et', N'darellchua2@gmail.com', N'testF d4et', N'testL d4et', N'1234', N'18698394', NULL, 23, 5, 6, 1)
INSERT [dbo].[Employees] ([Id], [Username], [Email], [Firstname], [Lastname], [Password], [PhoneNumber], [DeptRep], [SupervisedById], [EmployeeTypeId], [DepartmentId], [TempDeptHeadTypeId]) VALUES (3, N'd3e', N'darellchua2@gmail.com', N'testF d3e', N'testL d3e', N'1234', N'50843615', NULL, 5, 5, 9, NULL)
INSERT [dbo].[Employees] ([Id], [Username], [Email], [Firstname], [Lastname], [Password], [PhoneNumber], [DeptRep], [SupervisedById], [EmployeeTypeId], [DepartmentId], [TempDeptHeadTypeId]) VALUES (4, N'd3r', N'darellchua2@gmail.com', N'testF d3r', N'testL d3r', N'1234', N'83785992', NULL, 5, 3, 9, NULL)
INSERT [dbo].[Employees] ([Id], [Username], [Email], [Firstname], [Lastname], [Password], [PhoneNumber], [DeptRep], [SupervisedById], [EmployeeTypeId], [DepartmentId], [TempDeptHeadTypeId]) VALUES (5, N'd3h', N'darellchua2@gmail.com', N'testF d3h', N'testL d3h', N'1234', N'47735944', NULL, NULL, 4, 9, NULL)
INSERT [dbo].[Employees] ([Id], [Username], [Email], [Firstname], [Lastname], [Password], [PhoneNumber], [DeptRep], [SupervisedById], [EmployeeTypeId], [DepartmentId], [TempDeptHeadTypeId]) VALUES (6, N'd3et', N'darellchua2@gmail.com', N'testF d3et', N'testL d3et', N'1234', N'45088915', NULL, 5, 5, 9, 1)
INSERT [dbo].[Employees] ([Id], [Username], [Email], [Firstname], [Lastname], [Password], [PhoneNumber], [DeptRep], [SupervisedById], [EmployeeTypeId], [DepartmentId], [TempDeptHeadTypeId]) VALUES (7, N'd2e', N'darellchua2@gmail.com', N'testF d2e', N'testL d2e', N'1234', N'18486195', NULL, 9, 5, 7, NULL)
INSERT [dbo].[Employees] ([Id], [Username], [Email], [Firstname], [Lastname], [Password], [PhoneNumber], [DeptRep], [SupervisedById], [EmployeeTypeId], [DepartmentId], [TempDeptHeadTypeId]) VALUES (8, N'd2r', N'darellchua2@gmail.com', N'testF d2r', N'testL d2r', N'1234', N'23301370', NULL, 9, 3, 7, NULL)
INSERT [dbo].[Employees] ([Id], [Username], [Email], [Firstname], [Lastname], [Password], [PhoneNumber], [DeptRep], [SupervisedById], [EmployeeTypeId], [DepartmentId], [TempDeptHeadTypeId]) VALUES (9, N'd2h', N'darellchua2@gmail.com', N'testF d2h', N'testL d2h', N'1234', N'79671555', NULL, NULL, 4, 7, NULL)
INSERT [dbo].[Employees] ([Id], [Username], [Email], [Firstname], [Lastname], [Password], [PhoneNumber], [DeptRep], [SupervisedById], [EmployeeTypeId], [DepartmentId], [TempDeptHeadTypeId]) VALUES (10, N'd2et', N'darellchua2@gmail.com', N'testF d2et', N'testL d2et', N'1234', N'53889969', NULL, 9, 5, 7, 1)
INSERT [dbo].[Employees] ([Id], [Username], [Email], [Firstname], [Lastname], [Password], [PhoneNumber], [DeptRep], [SupervisedById], [EmployeeTypeId], [DepartmentId], [TempDeptHeadTypeId]) VALUES (11, N'd1e', N'darellchua2@gmail.com', N'testF d1e', N'testL d1e', N'1234', N'30247994', NULL, 13, 5, 8, NULL)
INSERT [dbo].[Employees] ([Id], [Username], [Email], [Firstname], [Lastname], [Password], [PhoneNumber], [DeptRep], [SupervisedById], [EmployeeTypeId], [DepartmentId], [TempDeptHeadTypeId]) VALUES (12, N'd1r', N'darellchua2@gmail.com', N'testF d1r', N'testL d1r', N'1234', N'49967055', NULL, 13, 3, 8, NULL)
INSERT [dbo].[Employees] ([Id], [Username], [Email], [Firstname], [Lastname], [Password], [PhoneNumber], [DeptRep], [SupervisedById], [EmployeeTypeId], [DepartmentId], [TempDeptHeadTypeId]) VALUES (13, N'd1h', N'darellchua2@gmail.com', N'testF d1h', N'testL d1h', N'1234', N'47358610', NULL, NULL, 4, 8, NULL)
INSERT [dbo].[Employees] ([Id], [Username], [Email], [Firstname], [Lastname], [Password], [PhoneNumber], [DeptRep], [SupervisedById], [EmployeeTypeId], [DepartmentId], [TempDeptHeadTypeId]) VALUES (14, N'd1et', N'darellchua2@gmail.com', N'testF d1et', N'testL d1et', N'1234', N'73587563', NULL, 13, 5, 8, 1)
INSERT [dbo].[Employees] ([Id], [Username], [Email], [Firstname], [Lastname], [Password], [PhoneNumber], [DeptRep], [SupervisedById], [EmployeeTypeId], [DepartmentId], [TempDeptHeadTypeId]) VALUES (15, N'darell', N'darellchua2@gmail.com', N'Khiong Kiat', N'Chua', N'1234', N'51081636', NULL, 17, 8, 10, NULL)
INSERT [dbo].[Employees] ([Id], [Username], [Email], [Firstname], [Lastname], [Password], [PhoneNumber], [DeptRep], [SupervisedById], [EmployeeTypeId], [DepartmentId], [TempDeptHeadTypeId]) VALUES (16, N'sheryl', N'sherylteo@u.nus.edu', N'Sheryl', N'Teo', N'1234', N'79546947', NULL, 17, 7, 10, NULL)
INSERT [dbo].[Employees] ([Id], [Username], [Email], [Firstname], [Lastname], [Password], [PhoneNumber], [DeptRep], [SupervisedById], [EmployeeTypeId], [DepartmentId], [TempDeptHeadTypeId]) VALUES (17, N'rohan', N'rohanmhatre@gmail.com', N'Rohan', N'Mhatre', N'1234', N'99991837', NULL, NULL, 6, 10, NULL)
INSERT [dbo].[Employees] ([Id], [Username], [Email], [Firstname], [Lastname], [Password], [PhoneNumber], [DeptRep], [SupervisedById], [EmployeeTypeId], [DepartmentId], [TempDeptHeadTypeId]) VALUES (18, N'xinqi', N'darellchua2@gmail.com', N'Xin Qi', N'li', N'1234', N'72763551', NULL, 17, 2, 10, NULL)
INSERT [dbo].[Employees] ([Id], [Username], [Email], [Firstname], [Lastname], [Password], [PhoneNumber], [DeptRep], [SupervisedById], [EmployeeTypeId], [DepartmentId], [TempDeptHeadTypeId]) VALUES (19, N'arjun', N'darellchua2@gmail.com', N'Arjun', N'Chirumalle', N'1234', N'61662774', NULL, 21, 3, 2, NULL)
INSERT [dbo].[Employees] ([Id], [Username], [Email], [Firstname], [Lastname], [Password], [PhoneNumber], [DeptRep], [SupervisedById], [EmployeeTypeId], [DepartmentId], [TempDeptHeadTypeId]) VALUES (20, N'thinn', N'darellchua2@gmail.com', N'Hlaing', N'Thinn', N'1234', N'52383032', NULL, 21, 5, 2, NULL)
INSERT [dbo].[Employees] ([Id], [Username], [Email], [Firstname], [Lastname], [Password], [PhoneNumber], [DeptRep], [SupervisedById], [EmployeeTypeId], [DepartmentId], [TempDeptHeadTypeId]) VALUES (21, N'darellmgr2', N'darellchua2@gmail.com', N'Khiong Kiat1', N'Chua', N'1234', N'44939331', NULL, NULL, 4, 2, NULL)
INSERT [dbo].[Employees] ([Id], [Username], [Email], [Firstname], [Lastname], [Password], [PhoneNumber], [DeptRep], [SupervisedById], [EmployeeTypeId], [DepartmentId], [TempDeptHeadTypeId]) VALUES (22, N'thinn2', N'darellchua2@gmail.com', N'Hlaing2', N'Thinn2', N'1234', N'62865817', NULL, 21, 5, 2, 1)
INSERT [dbo].[Employees] ([Id], [Username], [Email], [Firstname], [Lastname], [Password], [PhoneNumber], [DeptRep], [SupervisedById], [EmployeeTypeId], [DepartmentId], [TempDeptHeadTypeId]) VALUES (23, N'd4h', N'darellchua2@gmail.com', N'testF d4h', N'testL d4h', N'1234', N'30224955', NULL, NULL, 4, 6, NULL)
INSERT [dbo].[Employees] ([Id], [Username], [Email], [Firstname], [Lastname], [Password], [PhoneNumber], [DeptRep], [SupervisedById], [EmployeeTypeId], [DepartmentId], [TempDeptHeadTypeId]) VALUES (24, N'd4r', N'darellchua2@gmail.com', N'testF d4r', N'testL d4r', N'1234', N'40486641', NULL, 23, 3, 6, NULL)
INSERT [dbo].[Employees] ([Id], [Username], [Email], [Firstname], [Lastname], [Password], [PhoneNumber], [DeptRep], [SupervisedById], [EmployeeTypeId], [DepartmentId], [TempDeptHeadTypeId]) VALUES (25, N'd4e', N'darellchua2@gmail.com', N'testF d4e', N'testL d4e', N'1234', N'28486759', NULL, 23, 5, 6, NULL)
INSERT [dbo].[Employees] ([Id], [Username], [Email], [Firstname], [Lastname], [Password], [PhoneNumber], [DeptRep], [SupervisedById], [EmployeeTypeId], [DepartmentId], [TempDeptHeadTypeId]) VALUES (26, N'd5et', N'darellchua2@gmail.com', N'testF d5et', N'testL d5et', N'1234', N'16902531', NULL, 48, 5, 4, 1)
INSERT [dbo].[Employees] ([Id], [Username], [Email], [Firstname], [Lastname], [Password], [PhoneNumber], [DeptRep], [SupervisedById], [EmployeeTypeId], [DepartmentId], [TempDeptHeadTypeId]) VALUES (27, N'dss', N'darellchua2@gmail.com', N'testF dss', N'testL dss', N'1234', N'67176032', NULL, 17, 7, 10, NULL)
INSERT [dbo].[Employees] ([Id], [Username], [Email], [Firstname], [Lastname], [Password], [PhoneNumber], [DeptRep], [SupervisedById], [EmployeeTypeId], [DepartmentId], [TempDeptHeadTypeId]) VALUES (28, N'dsm', N'darellchua2@gmail.com', N'testF dsm', N'testL dsm', N'1234', N'96571755', NULL, NULL, 6, 10, NULL)
INSERT [dbo].[Employees] ([Id], [Username], [Email], [Firstname], [Lastname], [Password], [PhoneNumber], [DeptRep], [SupervisedById], [EmployeeTypeId], [DepartmentId], [TempDeptHeadTypeId]) VALUES (29, N'dswp', N'darellchua2@gmail.com', N'testF dsw', N'testL dsw', N'1234', N'13460056', NULL, 17, 2, 10, NULL)
INSERT [dbo].[Employees] ([Id], [Username], [Email], [Firstname], [Lastname], [Password], [PhoneNumber], [DeptRep], [SupervisedById], [EmployeeTypeId], [DepartmentId], [TempDeptHeadTypeId]) VALUES (30, N'd9e', N'darellchua2@gmail.com', N'testF d9e', N'testL d9e', N'1234', N'49057206', NULL, 32, 5, 3, NULL)
INSERT [dbo].[Employees] ([Id], [Username], [Email], [Firstname], [Lastname], [Password], [PhoneNumber], [DeptRep], [SupervisedById], [EmployeeTypeId], [DepartmentId], [TempDeptHeadTypeId]) VALUES (31, N'd9r', N'darellchua2@gmail.com', N'testF d9r', N'testL d9r', N'1234', N'77553606', NULL, 32, 3, 3, NULL)
INSERT [dbo].[Employees] ([Id], [Username], [Email], [Firstname], [Lastname], [Password], [PhoneNumber], [DeptRep], [SupervisedById], [EmployeeTypeId], [DepartmentId], [TempDeptHeadTypeId]) VALUES (32, N'd9h', N'darellchua2@gmail.com', N'testF d9h', N'testL d9h', N'1234', N'84823839', NULL, NULL, 4, 3, NULL)
INSERT [dbo].[Employees] ([Id], [Username], [Email], [Firstname], [Lastname], [Password], [PhoneNumber], [DeptRep], [SupervisedById], [EmployeeTypeId], [DepartmentId], [TempDeptHeadTypeId]) VALUES (33, N'd9et', N'darellchua2@gmail.com', N'testF d9et', N'testL d9et', N'1234', N'44197600', NULL, 32, 5, 3, 1)
INSERT [dbo].[Employees] ([Id], [Username], [Email], [Firstname], [Lastname], [Password], [PhoneNumber], [DeptRep], [SupervisedById], [EmployeeTypeId], [DepartmentId], [TempDeptHeadTypeId]) VALUES (34, N'd8e', N'darellchua2@gmail.com', N'testF d8e', N'testL d8e', N'1234', N'84582308', NULL, 36, 5, 5, NULL)
INSERT [dbo].[Employees] ([Id], [Username], [Email], [Firstname], [Lastname], [Password], [PhoneNumber], [DeptRep], [SupervisedById], [EmployeeTypeId], [DepartmentId], [TempDeptHeadTypeId]) VALUES (35, N'd8r', N'darellchua2@gmail.com', N'testF d8r', N'testL d8r', N'1234', N'66563395', NULL, 36, 3, 5, NULL)
INSERT [dbo].[Employees] ([Id], [Username], [Email], [Firstname], [Lastname], [Password], [PhoneNumber], [DeptRep], [SupervisedById], [EmployeeTypeId], [DepartmentId], [TempDeptHeadTypeId]) VALUES (36, N'd8h', N'darellchua2@gmail.com', N'testF d8h', N'testL d8h', N'1234', N'95865859', NULL, NULL, 4, 5, NULL)
INSERT [dbo].[Employees] ([Id], [Username], [Email], [Firstname], [Lastname], [Password], [PhoneNumber], [DeptRep], [SupervisedById], [EmployeeTypeId], [DepartmentId], [TempDeptHeadTypeId]) VALUES (37, N'yamone', N'darellchua2@gmail.com', N'Shwe', N'Yamone', N'1234', N'62733492', NULL, NULL, 4, 1, NULL)
INSERT [dbo].[Employees] ([Id], [Username], [Email], [Firstname], [Lastname], [Password], [PhoneNumber], [DeptRep], [SupervisedById], [EmployeeTypeId], [DepartmentId], [TempDeptHeadTypeId]) VALUES (38, N'd8et', N'darellchua2@gmail.com', N'testF d8et', N'testL d8et', N'1234', N'79983397', NULL, 36, 5, 5, 1)
INSERT [dbo].[Employees] ([Id], [Username], [Email], [Firstname], [Lastname], [Password], [PhoneNumber], [DeptRep], [SupervisedById], [EmployeeTypeId], [DepartmentId], [TempDeptHeadTypeId]) VALUES (39, N'd7r', N'darellchua2@gmail.com', N'testF d7r', N'testL d7r', N'1234', N'50196398', NULL, 37, 3, 1, NULL)
INSERT [dbo].[Employees] ([Id], [Username], [Email], [Firstname], [Lastname], [Password], [PhoneNumber], [DeptRep], [SupervisedById], [EmployeeTypeId], [DepartmentId], [TempDeptHeadTypeId]) VALUES (40, N'd7h', N'darellchua2@gmail.com', N'testF d7h', N'testL d7h', N'1234', N'89719833', NULL, NULL, 4, 1, NULL)
INSERT [dbo].[Employees] ([Id], [Username], [Email], [Firstname], [Lastname], [Password], [PhoneNumber], [DeptRep], [SupervisedById], [EmployeeTypeId], [DepartmentId], [TempDeptHeadTypeId]) VALUES (41, N'd7et', N'darellchua2@gmail.com', N'testF d7et', N'testL d7et', N'1234', N'60967910', NULL, 37, 5, 1, 1)
INSERT [dbo].[Employees] ([Id], [Username], [Email], [Firstname], [Lastname], [Password], [PhoneNumber], [DeptRep], [SupervisedById], [EmployeeTypeId], [DepartmentId], [TempDeptHeadTypeId]) VALUES (42, N'd6e', N'darellchua2@gmail.com', N'testF d6e', N'testL d6e', N'1234', N'78242532', NULL, 21, 5, 2, NULL)
INSERT [dbo].[Employees] ([Id], [Username], [Email], [Firstname], [Lastname], [Password], [PhoneNumber], [DeptRep], [SupervisedById], [EmployeeTypeId], [DepartmentId], [TempDeptHeadTypeId]) VALUES (43, N'd6r', N'darellchua2@gmail.com', N'testF d6r', N'testL d6r', N'1234', N'55365590', NULL, 21, 3, 2, NULL)
INSERT [dbo].[Employees] ([Id], [Username], [Email], [Firstname], [Lastname], [Password], [PhoneNumber], [DeptRep], [SupervisedById], [EmployeeTypeId], [DepartmentId], [TempDeptHeadTypeId]) VALUES (44, N'd6h', N'darellchua2@gmail.com', N'testF d6h', N'testL d6h', N'1234', N'90993410', NULL, NULL, 4, 2, NULL)
INSERT [dbo].[Employees] ([Id], [Username], [Email], [Firstname], [Lastname], [Password], [PhoneNumber], [DeptRep], [SupervisedById], [EmployeeTypeId], [DepartmentId], [TempDeptHeadTypeId]) VALUES (45, N'd6et', N'darellchua2@gmail.com', N'testF d6et', N'testL d6et', N'1234', N'48805214', NULL, 21, 5, 2, 1)
INSERT [dbo].[Employees] ([Id], [Username], [Email], [Firstname], [Lastname], [Password], [PhoneNumber], [DeptRep], [SupervisedById], [EmployeeTypeId], [DepartmentId], [TempDeptHeadTypeId]) VALUES (46, N'd5e', N'darellchua2@gmail.com', N'testF d5e', N'testL d5e', N'1234', N'26730275', NULL, 48, 5, 4, NULL)
INSERT [dbo].[Employees] ([Id], [Username], [Email], [Firstname], [Lastname], [Password], [PhoneNumber], [DeptRep], [SupervisedById], [EmployeeTypeId], [DepartmentId], [TempDeptHeadTypeId]) VALUES (47, N'd5r', N'darellchua2@gmail.com', N'testF d5r', N'testL d5r', N'1234', N'98866697', NULL, 48, 3, 4, NULL)
INSERT [dbo].[Employees] ([Id], [Username], [Email], [Firstname], [Lastname], [Password], [PhoneNumber], [DeptRep], [SupervisedById], [EmployeeTypeId], [DepartmentId], [TempDeptHeadTypeId]) VALUES (48, N'd5h', N'darellchua2@gmail.com', N'testF d5h', N'testL d5h', N'1234', N'43956320', NULL, NULL, 4, 4, NULL)
INSERT [dbo].[Employees] ([Id], [Username], [Email], [Firstname], [Lastname], [Password], [PhoneNumber], [DeptRep], [SupervisedById], [EmployeeTypeId], [DepartmentId], [TempDeptHeadTypeId]) VALUES (49, N'd7e', N'darellchua2@gmail.com', N'testF d7e', N'testL d7e', N'1234', N'75936847', NULL, 37, 5, 1, NULL)
INSERT [dbo].[Employees] ([Id], [Username], [Email], [Firstname], [Lastname], [Password], [PhoneNumber], [DeptRep], [SupervisedById], [EmployeeTypeId], [DepartmentId], [TempDeptHeadTypeId]) VALUES (50, N'yanbin', N'darellchua2@gmail.com', N'Yanbin', N'Zhong', N'1234', N'45796700', NULL, 37, 5, 1, NULL)
SET IDENTITY_INSERT [dbo].[Employees] OFF
GO
SET IDENTITY_INSERT [dbo].[EmployeeTypes] ON 

INSERT [dbo].[EmployeeTypes] ([Id], [EmployeeTypeName]) VALUES (1, N'Temporary Department Head')
INSERT [dbo].[EmployeeTypes] ([Id], [EmployeeTypeName]) VALUES (2, N'Warehouse Packer')
INSERT [dbo].[EmployeeTypes] ([Id], [EmployeeTypeName]) VALUES (3, N'Department Representative')
INSERT [dbo].[EmployeeTypes] ([Id], [EmployeeTypeName]) VALUES (4, N'Department Head')
INSERT [dbo].[EmployeeTypes] ([Id], [EmployeeTypeName]) VALUES (5, N'Employee')
INSERT [dbo].[EmployeeTypes] ([Id], [EmployeeTypeName]) VALUES (6, N'Store Manager')
INSERT [dbo].[EmployeeTypes] ([Id], [EmployeeTypeName]) VALUES (7, N'Store Supervisor')
INSERT [dbo].[EmployeeTypes] ([Id], [EmployeeTypeName]) VALUES (8, N'Store Clerk')
SET IDENTITY_INSERT [dbo].[EmployeeTypes] OFF
GO
SET IDENTITY_INSERT [dbo].[InventoryTransactions] ON 

INSERT [dbo].[InventoryTransactions] ([Id], [ProductId1], [ITStatus], [InventoryChangeQuantity], [InventoryTransComments], [InventoryTransDate], [ITCode], [EmployeeId1], [ApprovalComment], [ITApprovalDate], [ITApprovalById], [ProductPrice]) VALUES (1, 1, 0, 50, N'DO/270820/1', CAST(N'2020-08-27T00:00:00.0000000' AS DateTime2), N'IT/270820/1', 27, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, 10)
INSERT [dbo].[InventoryTransactions] ([Id], [ProductId1], [ITStatus], [InventoryChangeQuantity], [InventoryTransComments], [InventoryTransDate], [ITCode], [EmployeeId1], [ApprovalComment], [ITApprovalDate], [ITApprovalById], [ProductPrice]) VALUES (2, 4, 0, 20, N'DO/270820/2', CAST(N'2020-08-27T00:00:00.0000000' AS DateTime2), N'IT/270820/2', 27, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, 3.05)
INSERT [dbo].[InventoryTransactions] ([Id], [ProductId1], [ITStatus], [InventoryChangeQuantity], [InventoryTransComments], [InventoryTransDate], [ITCode], [EmployeeId1], [ApprovalComment], [ITApprovalDate], [ITApprovalById], [ProductPrice]) VALUES (3, 11, 0, -50, N'SR/290820/1', CAST(N'2020-08-29T18:10:45.9833741' AS DateTime2), N'IT/290820/1', 1, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[InventoryTransactions] ([Id], [ProductId1], [ITStatus], [InventoryChangeQuantity], [InventoryTransComments], [InventoryTransDate], [ITCode], [EmployeeId1], [ApprovalComment], [ITApprovalDate], [ITApprovalById], [ProductPrice]) VALUES (4, 78, 0, -20, N'SR/170520/2', CAST(N'2020-08-29T20:03:12.9104143' AS DateTime2), N'IT/290820/2', 1, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[InventoryTransactions] ([Id], [ProductId1], [ITStatus], [InventoryChangeQuantity], [InventoryTransComments], [InventoryTransDate], [ITCode], [EmployeeId1], [ApprovalComment], [ITApprovalDate], [ITApprovalById], [ProductPrice]) VALUES (5, 23, 0, -20, N'SR/290520/2', CAST(N'2020-08-29T21:36:03.8788038' AS DateTime2), N'IT/290820/3', 1, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[InventoryTransactions] ([Id], [ProductId1], [ITStatus], [InventoryChangeQuantity], [InventoryTransComments], [InventoryTransDate], [ITCode], [EmployeeId1], [ApprovalComment], [ITApprovalDate], [ITApprovalById], [ProductPrice]) VALUES (6, 1, 0, -12, N'SR/220620/1', CAST(N'2020-08-29T21:36:37.7123966' AS DateTime2), N'IT/290820/4', 1, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[InventoryTransactions] ([Id], [ProductId1], [ITStatus], [InventoryChangeQuantity], [InventoryTransComments], [InventoryTransDate], [ITCode], [EmployeeId1], [ApprovalComment], [ITApprovalDate], [ITApprovalById], [ProductPrice]) VALUES (7, 5, 0, -12, N'SR/220620/1', CAST(N'2020-08-29T21:36:37.7123966' AS DateTime2), N'IT/290820/5', 1, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[InventoryTransactions] ([Id], [ProductId1], [ITStatus], [InventoryChangeQuantity], [InventoryTransComments], [InventoryTransDate], [ITCode], [EmployeeId1], [ApprovalComment], [ITApprovalDate], [ITApprovalById], [ProductPrice]) VALUES (8, 4, 0, -12, N'SR/220620/1', CAST(N'2020-08-29T21:36:37.7123966' AS DateTime2), N'IT/290820/6', 1, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[InventoryTransactions] ([Id], [ProductId1], [ITStatus], [InventoryChangeQuantity], [InventoryTransComments], [InventoryTransDate], [ITCode], [EmployeeId1], [ApprovalComment], [ITApprovalDate], [ITApprovalById], [ProductPrice]) VALUES (9, 21, 0, -10, N'SR/230620/1', CAST(N'2020-08-29T21:37:01.3279240' AS DateTime2), N'IT/290820/7', 1, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[InventoryTransactions] ([Id], [ProductId1], [ITStatus], [InventoryChangeQuantity], [InventoryTransComments], [InventoryTransDate], [ITCode], [EmployeeId1], [ApprovalComment], [ITApprovalDate], [ITApprovalById], [ProductPrice]) VALUES (10, 4, 0, -5, N'SR/290620/1', CAST(N'2020-08-29T21:37:34.7105902' AS DateTime2), N'IT/290820/8', 1, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[InventoryTransactions] ([Id], [ProductId1], [ITStatus], [InventoryChangeQuantity], [InventoryTransComments], [InventoryTransDate], [ITCode], [EmployeeId1], [ApprovalComment], [ITApprovalDate], [ITApprovalById], [ProductPrice]) VALUES (11, 88, 0, -40, N'SR/110720/1', CAST(N'2020-08-29T21:37:51.9754628' AS DateTime2), N'IT/290820/9', 1, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[InventoryTransactions] ([Id], [ProductId1], [ITStatus], [InventoryChangeQuantity], [InventoryTransComments], [InventoryTransDate], [ITCode], [EmployeeId1], [ApprovalComment], [ITApprovalDate], [ITApprovalById], [ProductPrice]) VALUES (12, 10, 0, -12, N'SR/110720/1', CAST(N'2020-08-29T21:37:51.9754628' AS DateTime2), N'IT/290820/10', 1, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[InventoryTransactions] ([Id], [ProductId1], [ITStatus], [InventoryChangeQuantity], [InventoryTransComments], [InventoryTransDate], [ITCode], [EmployeeId1], [ApprovalComment], [ITApprovalDate], [ITApprovalById], [ProductPrice]) VALUES (13, 3, 0, -5, N'SR/190720/1', CAST(N'2020-08-29T21:38:08.7616010' AS DateTime2), N'IT/290820/11', 1, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[InventoryTransactions] ([Id], [ProductId1], [ITStatus], [InventoryChangeQuantity], [InventoryTransComments], [InventoryTransDate], [ITCode], [EmployeeId1], [ApprovalComment], [ITApprovalDate], [ITApprovalById], [ProductPrice]) VALUES (14, 25, 0, -5, N'SR/300720/1', CAST(N'2020-08-29T21:38:23.4732198' AS DateTime2), N'IT/290820/12', 1, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[InventoryTransactions] ([Id], [ProductId1], [ITStatus], [InventoryChangeQuantity], [InventoryTransComments], [InventoryTransDate], [ITCode], [EmployeeId1], [ApprovalComment], [ITApprovalDate], [ITApprovalById], [ProductPrice]) VALUES (15, 15, 0, -12, N'SR/190820/1', CAST(N'2020-08-29T21:38:40.9867449' AS DateTime2), N'IT/290820/13', 1, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[InventoryTransactions] ([Id], [ProductId1], [ITStatus], [InventoryChangeQuantity], [InventoryTransComments], [InventoryTransDate], [ITCode], [EmployeeId1], [ApprovalComment], [ITApprovalDate], [ITApprovalById], [ProductPrice]) VALUES (16, 72, 0, -40, N'SR/190820/2', CAST(N'2020-08-29T21:38:55.4378433' AS DateTime2), N'IT/290820/14', 1, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[InventoryTransactions] ([Id], [ProductId1], [ITStatus], [InventoryChangeQuantity], [InventoryTransComments], [InventoryTransDate], [ITCode], [EmployeeId1], [ApprovalComment], [ITApprovalDate], [ITApprovalById], [ProductPrice]) VALUES (17, 33, 0, -5, N'SR/190820/2', CAST(N'2020-08-29T21:38:55.4378433' AS DateTime2), N'IT/290820/15', 1, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[InventoryTransactions] ([Id], [ProductId1], [ITStatus], [InventoryChangeQuantity], [InventoryTransComments], [InventoryTransDate], [ITCode], [EmployeeId1], [ApprovalComment], [ITApprovalDate], [ITApprovalById], [ProductPrice]) VALUES (18, 82, 0, -40, N'SR/290820/1', CAST(N'2020-08-29T21:39:24.4557148' AS DateTime2), N'IT/290820/16', 1, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[InventoryTransactions] ([Id], [ProductId1], [ITStatus], [InventoryChangeQuantity], [InventoryTransComments], [InventoryTransDate], [ITCode], [EmployeeId1], [ApprovalComment], [ITApprovalDate], [ITApprovalById], [ProductPrice]) VALUES (19, 81, 0, -12, N'SR/290820/1', CAST(N'2020-08-29T21:39:24.4557148' AS DateTime2), N'IT/290820/17', 1, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[InventoryTransactions] ([Id], [ProductId1], [ITStatus], [InventoryChangeQuantity], [InventoryTransComments], [InventoryTransDate], [ITCode], [EmployeeId1], [ApprovalComment], [ITApprovalDate], [ITApprovalById], [ProductPrice]) VALUES (20, 71, 0, -5, N'SR/290820/1', CAST(N'2020-08-29T21:39:24.4557148' AS DateTime2), N'IT/290820/18', 1, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, 0)
INSERT [dbo].[InventoryTransactions] ([Id], [ProductId1], [ITStatus], [InventoryChangeQuantity], [InventoryTransComments], [InventoryTransDate], [ITCode], [EmployeeId1], [ApprovalComment], [ITApprovalDate], [ITApprovalById], [ProductPrice]) VALUES (21, 43, 0, -5, N'SR/290820/1', CAST(N'2020-08-29T21:39:24.4557148' AS DateTime2), N'IT/290820/19', 1, NULL, CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, 0)
SET IDENTITY_INSERT [dbo].[InventoryTransactions] OFF
GO
SET IDENTITY_INSERT [dbo].[ProductCategories] ON 

INSERT [dbo].[ProductCategories] ([Id], [ProductCategoryName]) VALUES (1, N'Clip')
INSERT [dbo].[ProductCategories] ([Id], [ProductCategoryName]) VALUES (2, N'Tacks')
INSERT [dbo].[ProductCategories] ([Id], [ProductCategoryName]) VALUES (3, N'Stapler')
INSERT [dbo].[ProductCategories] ([Id], [ProductCategoryName]) VALUES (4, N'Shorthand')
INSERT [dbo].[ProductCategories] ([Id], [ProductCategoryName]) VALUES (5, N'Sharpener')
INSERT [dbo].[ProductCategories] ([Id], [ProductCategoryName]) VALUES (6, N'Tape')
INSERT [dbo].[ProductCategories] ([Id], [ProductCategoryName]) VALUES (7, N'Scissors')
INSERT [dbo].[ProductCategories] ([Id], [ProductCategoryName]) VALUES (8, N'Ruler')
INSERT [dbo].[ProductCategories] ([Id], [ProductCategoryName]) VALUES (9, N'Paper')
INSERT [dbo].[ProductCategories] ([Id], [ProductCategoryName]) VALUES (10, N'Pad')
INSERT [dbo].[ProductCategories] ([Id], [ProductCategoryName]) VALUES (11, N'Puncher')
INSERT [dbo].[ProductCategories] ([Id], [ProductCategoryName]) VALUES (12, N'Pen')
INSERT [dbo].[ProductCategories] ([Id], [ProductCategoryName]) VALUES (13, N'File')
INSERT [dbo].[ProductCategories] ([Id], [ProductCategoryName]) VALUES (14, N'Exercise')
INSERT [dbo].[ProductCategories] ([Id], [ProductCategoryName]) VALUES (15, N'Eraser')
INSERT [dbo].[ProductCategories] ([Id], [ProductCategoryName]) VALUES (16, N'Envelope')
INSERT [dbo].[ProductCategories] ([Id], [ProductCategoryName]) VALUES (17, N'Tparency')
INSERT [dbo].[ProductCategories] ([Id], [ProductCategoryName]) VALUES (18, N'Tray')
SET IDENTITY_INSERT [dbo].[ProductCategories] OFF
GO
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (1, N'Exercise Book A4 Hardcover (120 pg)', N'E033', 14, NULL, N'Each', N'product-1.jpg', 138, 100, 50, N'A20', 203, 155)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (2, N'Pencil 4H', N'P044', 12, NULL, N'Dozen', N'product-2.jpg', 150, 100, 50, N'A64', 107, 126)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (3, N'Pencil 2B with Eraser End', N'P043', 12, NULL, N'Dozen', N'product-3.jpg', 145, 100, 50, N'A63', 39, 211)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (4, N'Pencil 2B', N'P042', 12, NULL, N'Dozen', N'product-4.jpg', 133, 100, 50, N'A62', 224, 253)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (5, N'Pen Whiteboard Marker Red', N'P041', 12, NULL, N'Box', N'product-5.jpg', 138, 100, 50, N'A61', 78, 287)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (6, N'Pen Whiteboard Marker Green', N'P040', 12, NULL, N'Box', N'product-6.jpg', 150, 100, 50, N'A60', 174, 87)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (7, N'Pen Whiteboard Marker Blue', N'P039', 12, NULL, N'Box', N'product-7.jpg', 150, 100, 50, N'A59', 173, 182)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (8, N'Pen Whiteboard Marker Black', N'P038', 12, NULL, N'Box', N'product-8.jpg', 150, 100, 50, N'A58', 43, 289)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (9, N'Pen Transparency Soluble', N'P037', 12, NULL, N'Packet', NULL, 150, 100, 50, N'A57', 159, 60)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (10, N'Pencil B', N'P045', 12, NULL, N'Dozen', N'product-10.jpg', 138, 100, 50, N'A65', 207, 115)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (11, N'Pen Transparency Permanent', N'P036', 12, NULL, N'Packet', NULL, 100, 100, 50, N'A56', 282, 169)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (12, N'Pen Felt Tip Blue', N'P034', 12, NULL, N'Dozen', NULL, 150, 100, 50, N'A54', 131, 77)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (13, N'Pen Felt Tip Black', N'P033', 12, NULL, N'Dozen', NULL, 150, 100, 50, N'A53', 258, 291)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (14, N'Pen Ballpoint Red', N'P032', 12, NULL, N'Dozen', NULL, 150, 100, 50, N'A52', 28, 213)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (15, N'Pen Ballpoint Blue', N'P031', 12, NULL, N'Dozen', NULL, 138, 100, 50, N'A51', 86, 75)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (16, N'Pen Ballpoint Black', N'P030', 12, NULL, N'Dozen', N'product-16.jpg', 150, 100, 50, N'A50', 16, 200)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (17, N'Paper Photostat A4', N'P021', 9, NULL, N'Box', N'product-17.jpg', 1000, 500, 500, N'A49', 16, 111)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (18, N'Paper Photostat A3', N'P020', 9, NULL, N'Box', NULL, 1000, 500, 500, N'A48', 242, 131)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (19, N'Pad Postit Memo 3/4"x2"', N'P016', 10, NULL, N'Packet', N'product-19.jpg', 160, 100, 60, N'A47', 213, 120)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (20, N'Pen Felt Tip Red', N'P035', 12, NULL, N'Dozen', NULL, 150, 100, 50, N'A55', 265, 63)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (21, N'Pencil B with Eraser End', N'P046', 12, NULL, N'Dozen', NULL, 140, 100, 50, N'A66', 124, 183)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (22, N'Ruler 12"', N'R002', 8, NULL, N'Dozen', N'product-22.jpg', 70, 50, 20, N'A67', 36, 91)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (23, N'Ruler 6"', N'R001', 8, NULL, N'Dozen', NULL, 50, 50, 20, N'A68', 150, 205)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (24, N'Transparency Reverse Blue', N'T024', 17, NULL, N'Box', NULL, 300, 100, 200, N'A87', 30, 152)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (25, N'Transparency Red', N'T023', 17, NULL, N'Box', NULL, 295, 100, 200, N'A86', 64, 97)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (26, N'Transparency Green', N'T022', 17, NULL, N'Box', NULL, 300, 100, 200, N'A85', 121, 249)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (27, N'Transparency Clear', N'T021', 17, NULL, N'Box', N'product-27.jpg', 900, 500, 400, N'A84', 107, 171)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (28, N'Transparency Blue', N'T020', 17, NULL, N'Box', NULL, 300, 100, 200, N'A83', 79, 149)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (29, N'Thumb Tacks Small', N'T003', 2, NULL, N'Box', NULL, 20, 10, 10, N'A82', 300, 267)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (30, N'Thumb Tacks Medium', N'T002', 2, NULL, N'Box', N'product-30.jpg', 20, 10, 10, N'A81', 161, 228)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (31, N'Thumb Tacks Large', N'T001', 2, NULL, N'Box', NULL, 20, 10, 10, N'A80', 51, 233)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (32, N'Stapler No. 36', N'S023', 3, NULL, N'Box', N'product-32.jpg', 70, 50, 20, N'A79', 76, 263)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (33, N'Stapler No. 28', N'S022', 3, NULL, N'Box', NULL, 65, 50, 20, N'A78', 50, 284)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (34, N'Stapler No. 36', N'S021', 3, NULL, N'Each', NULL, 70, 50, 20, N'A77', 145, 201)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (35, N'Stapler No. 28', N'S020', 3, NULL, N'Each', NULL, 70, 50, 20, N'A76', 98, 233)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (36, N'Shorthand Book (80 pg)', N'S012', 4, NULL, N'Each', N'product-36.jpg', 180, 100, 80, N'A75', 288, 297)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (37, N'Shorthand Book (120 pg)', N'S011', 4, NULL, N'Each', NULL, 180, 100, 80, N'A74', 59, 208)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (38, N'Shorthand Book (100 pg)', N'S010', 4, NULL, N'Each', NULL, 180, 100, 80, N'A73', 149, 261)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (39, N'Sharpener', N'S101', 5, NULL, N'Each', N'product-39.jpg', 70, 50, 20, N'A72', 287, 101)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (40, N'Scotch Tape Dispenser', N'S041', 6, NULL, N'Each', N'product-40.jpg', 70, 50, 20, N'A71', 80, 42)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (41, N'Scotch Tape', N'S040', 6, NULL, N'Each', NULL, 70, 50, 20, N'A70', 127, 294)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (42, N'Scissors', N'S100', 7, NULL, N'Each', N'product-42.jpg', 70, 50, 20, N'A69', 124, 202)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (43, N'Pad Postit Memo 2"x4"', N'P015', 10, NULL, N'Packet', NULL, 155, 100, 60, N'A46', 16, 276)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (44, N'Transparency Cover 3M', N'T025', 17, NULL, N'Box', NULL, 900, 500, 400, N'A88', 35, 60)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (45, N'Pad Postit Memo 2"x4"', N'P014', 10, NULL, N'Packet', NULL, 160, 100, 60, N'A45', 20, 64)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (46, N'Pad Postit Memo 1/2"x2"', N'P012', 10, NULL, N'Packet', NULL, 160, 100, 60, N'A43', 112, 154)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (47, N'Clips Double 2"', N'C002', 1, NULL, N'Dozen', NULL, 80, 50, 30, N'A2', 137, 274)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (48, N'Clips Double 3/4"', N'C003', 1, NULL, N'Dozen', NULL, 80, 50, 30, N'A3', 189, 273)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (49, N'Clips Paper Large', N'C004', 1, NULL, N'Box', NULL, 80, 50, 30, N'A4', 142, 215)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (50, N'Clips Paper Medium', N'C005', 1, NULL, N'Box', NULL, 80, 50, 30, N'A5', 167, 289)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (51, N'Clips Paper Small', N'C006', 1, NULL, N'Box', N'product-51.jpg', 80, 50, 30, N'A6', 165, 75)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (52, N'Envelope Brown (3"x6")', N'E001', 16, NULL, N'Each', NULL, 1000, 600, 400, N'A7', 293, 19)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (53, N'Envelope Brown (3"x6") w/ Window', N'E002', 16, NULL, N'Each', NULL, 1000, 600, 400, N'A8', 58, 223)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (54, N'Envelope Brown (5"x7")', N'E003', 16, NULL, N'Each', N'product-54.jpg', 1000, 600, 400, N'A9', 27, 60)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (55, N'Clips Double 1"', N'C001', 1, NULL, N'Dozen', NULL, 80, 50, 30, N'A1', 54, 75)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (56, N'Envelope Brown (5"x7") w/ Window', N'E004', 16, NULL, N'Each', NULL, 1000, 600, 400, N'A10', 221, 288)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (57, N'Envelope White (3"x6") w/ Window', N'E006', 16, NULL, N'Each', NULL, 1000, 600, 400, N'A12', 93, 188)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (58, N'Envelope White (5"x7")', N'E007', 16, NULL, N'Each', NULL, 1000, 600, 400, N'A13', 221, 212)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (59, N'Envelope White (5"x7") w/ Window', N'E008', 16, NULL, N'Each', NULL, 1000, 600, 400, N'A14', 272, 219)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (60, N'Eraser (hard)', N'E020', 15, NULL, N'Each', NULL, 70, 50, 20, N'A15', 269, 70)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (61, N'Eraser (soft)', N'E021', 15, NULL, N'Each', NULL, 70, 50, 20, N'A16', 37, 278)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (62, N'Exercise Book (100 pg)', N'E030', 14, NULL, N'Each', NULL, 150, 100, 50, N'A17', 273, 128)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (63, N'Exercise Book (120 pg)', N'E031', 14, NULL, N'Each', N'product-63.jpg', 150, 100, 50, N'A18', 273, 222)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (64, N'Exercise Book A4 Hardcover (100 pg)', N'E032', 14, NULL, N'Each', NULL, 150, 100, 50, N'A19', 166, 142)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (65, N'Envelope White (3"x6")', N'E005', 16, NULL, N'Each', NULL, 1000, 600, 400, N'A11', 278, 173)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (66, N'Exercise Book A4 Hardcover (200 pg)', N'E034', 14, NULL, N'Each', NULL, 150, 100, 50, N'A21', 191, 192)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (67, N'Exercise Book Hardcover (100 pg)', N'E035', 14, NULL, N'Each', NULL, 150, 100, 50, N'A22', 234, 232)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (68, N'Exercise Book Hardcover (120 pg)', N'E036', 14, NULL, N'Each', NULL, 150, 100, 50, N'A23', 51, 30)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (69, N'Pad Postit Memo 1/2"x1"', N'P011', 10, NULL, N'Packet', NULL, 160, 100, 60, N'A42', 178, 166)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (70, N'Pad Postit Memo 1"x2"', N'P010', 10, NULL, N'Packet', NULL, 160, 100, 60, N'A41', 61, 142)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (71, N'Hole Puncher Adjustable', N'H033', 11, NULL, N'Each', NULL, 65, 50, 20, N'A40', 193, 123)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (72, N'Hole Puncher 3 holes', N'H032', 11, NULL, N'Each', N'product-72.jpg', 30, 50, 20, N'A39', 74, 18)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (73, N'Hole Puncher 2 holes', N'H031', 11, NULL, N'Each', NULL, 70, 50, 20, N'A38', 51, 284)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (74, N'Highlighter Yellow', N'H014', 12, NULL, N'Box', NULL, 180, 100, 80, N'A37', 103, 285)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (75, N'Highlighter Pink', N'H013', 12, NULL, N'Box', NULL, 180, 100, 80, N'A36', 281, 284)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (76, N'Highlighter Green', N'H012', 12, NULL, N'Box', NULL, 180, 100, 80, N'A35', 87, 156)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (77, N'Highlighter Blue', N'H011', 12, NULL, N'Box', NULL, 180, 100, 80, N'A34', 192, 196)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (78, N'Folder Plastic Yellow', N'F035', 13, NULL, N'Each', NULL, 330, 200, 150, N'A33', 273, 202)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (79, N'Folder Plastic Pink', N'F034', 13, NULL, N'Each', NULL, 350, 200, 150, N'A32', 274, 66)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (80, N'Folder Plastic Green', N'F033', 13, NULL, N'Each', NULL, 350, 200, 150, N'A31', 281, 162)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (81, N'Folder Plastic Clear', N'F032', 13, NULL, N'Each', NULL, 338, 200, 150, N'A30', 15, 55)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (82, N'Folder Plastic Blue', N'F031', 13, NULL, N'Each', NULL, 310, 200, 150, N'A29', 135, 179)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (83, N'File-Brown with Logo', N'F024', 13, NULL, N'Each', NULL, 350, 200, 150, N'A28', 19, 173)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (84, N'File-Brown w/o Logo', N'F023', 13, NULL, N'Each', N'product-84.jpg', 350, 200, 150, N'A27', 216, 110)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (85, N'File-Blue with Logo', N'F022', 13, NULL, N'Each', NULL, 300, 200, 100, N'A26', 171, 96)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (86, N'File-Blue Plain', N'F021', 13, NULL, N'Each', N'product-86.jpg', 300, 200, 100, N'A25', 185, 210)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (87, N'File Separator', N'F020', 13, NULL, N'Set', NULL, 150, 100, 50, N'A24', 32, 227)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (88, N'Pad Postit Memo 2"x3"', N'P013', 10, NULL, N'Packet', NULL, 120, 100, 60, N'A44', 154, 171)
INSERT [dbo].[Products] ([Id], [ProductName], [ProductCode], [ProductCategoryId], [ProductDescription], [Units], [ProductImage], [InventoryQuantity], [ReorderLevel], [ReorderQuantity], [InventoryLocation], [MLReorderQuantity], [MLReorderLevel]) VALUES (89, N'Trays In/Out', N'T100', 18, NULL, N'Set', N'product-89.jpg', 30, 20, 10, N'A89', 231, 139)
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
SET IDENTITY_INSERT [dbo].[PurchaseOrders] ON 

INSERT [dbo].[PurchaseOrders] ([Id], [supplierId], [DeliverTo], [expectedDeliveryDate], [IssuedById], [POStatus], [POComments], [POIssueDate], [POCode]) VALUES (1, 2, N'Logic University - Store', CAST(N'2020-09-13T09:30:20.0000000' AS DateTime2), 18, 1, N'Urgent', CAST(N'2020-07-10T08:40:20.0000000' AS DateTime2), N'PO/100720/2')
INSERT [dbo].[PurchaseOrders] ([Id], [supplierId], [DeliverTo], [expectedDeliveryDate], [IssuedById], [POStatus], [POComments], [POIssueDate], [POCode]) VALUES (2, 2, N'Logic University - Store', CAST(N'2020-09-12T08:30:20.0000000' AS DateTime2), 18, 2, N'Some Items may be out of stock', CAST(N'2020-07-10T08:40:20.0000000' AS DateTime2), N'PO/100720/1')
INSERT [dbo].[PurchaseOrders] ([Id], [supplierId], [DeliverTo], [expectedDeliveryDate], [IssuedById], [POStatus], [POComments], [POIssueDate], [POCode]) VALUES (3, 1, N'Logic University - Store', CAST(N'2020-08-22T08:30:20.0000000' AS DateTime2), 18, 0, N'Order affected by COVID', CAST(N'2020-08-10T08:30:20.0000000' AS DateTime2), N'PO/100820/1')
INSERT [dbo].[PurchaseOrders] ([Id], [supplierId], [DeliverTo], [expectedDeliveryDate], [IssuedById], [POStatus], [POComments], [POIssueDate], [POCode]) VALUES (4, 9, N'Logic University', CAST(N'2020-09-06T06:45:27.9521413' AS DateTime2), 27, 0, N'test', CAST(N'2020-08-27T06:45:27.9525878' AS DateTime2), N'PO/270820/1')
INSERT [dbo].[PurchaseOrders] ([Id], [supplierId], [DeliverTo], [expectedDeliveryDate], [IssuedById], [POStatus], [POComments], [POIssueDate], [POCode]) VALUES (5, 8, N'Logic University', CAST(N'2020-09-06T06:45:52.0845764' AS DateTime2), 27, 2, N'test2', CAST(N'2020-08-27T06:45:52.0845869' AS DateTime2), N'PO/270820/2')
INSERT [dbo].[PurchaseOrders] ([Id], [supplierId], [DeliverTo], [expectedDeliveryDate], [IssuedById], [POStatus], [POComments], [POIssueDate], [POCode]) VALUES (6, 11, N'Logic University', CAST(N'2020-09-06T06:45:52.0961326' AS DateTime2), 27, 1, N'test2', CAST(N'2020-08-27T06:45:52.0961396' AS DateTime2), N'PO/270820/3')
SET IDENTITY_INSERT [dbo].[PurchaseOrders] OFF
GO
SET IDENTITY_INSERT [dbo].[PurchaseOrderSupplierProducts] ON 

INSERT [dbo].[PurchaseOrderSupplierProducts] ([Id], [SupplierProductId], [PurchaseOrderId], [POUnitPrice], [POQuantityRequested]) VALUES (1, 1, 2, 15.05, 3)
INSERT [dbo].[PurchaseOrderSupplierProducts] ([Id], [SupplierProductId], [PurchaseOrderId], [POUnitPrice], [POQuantityRequested]) VALUES (2, 3, 3, 3.01, 5)
INSERT [dbo].[PurchaseOrderSupplierProducts] ([Id], [SupplierProductId], [PurchaseOrderId], [POUnitPrice], [POQuantityRequested]) VALUES (3, 6, 3, 6.1, 15)
INSERT [dbo].[PurchaseOrderSupplierProducts] ([Id], [SupplierProductId], [PurchaseOrderId], [POUnitPrice], [POQuantityRequested]) VALUES (4, 1, 3, 15.05, 10)
INSERT [dbo].[PurchaseOrderSupplierProducts] ([Id], [SupplierProductId], [PurchaseOrderId], [POUnitPrice], [POQuantityRequested]) VALUES (5, 143, 4, 2.05, 50)
INSERT [dbo].[PurchaseOrderSupplierProducts] ([Id], [SupplierProductId], [PurchaseOrderId], [POUnitPrice], [POQuantityRequested]) VALUES (6, 162, 5, 10, 50)
INSERT [dbo].[PurchaseOrderSupplierProducts] ([Id], [SupplierProductId], [PurchaseOrderId], [POUnitPrice], [POQuantityRequested]) VALUES (7, 55, 6, 3.05, 50)
SET IDENTITY_INSERT [dbo].[PurchaseOrderSupplierProducts] OFF
GO
SET IDENTITY_INSERT [dbo].[RequisitionForms] ON 

INSERT [dbo].[RequisitionForms] ([Id], [EmployeeId], [RFCode], [RFStatus], [RFComments], [RFDate], [RFApprovalDate], [RFApprovalById], [RFHeadReply]) VALUES (1, 20, N'RF/SCID/140820/2', 0, NULL, CAST(N'2020-08-14T08:30:20.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL)
INSERT [dbo].[RequisitionForms] ([Id], [EmployeeId], [RFCode], [RFStatus], [RFComments], [RFDate], [RFApprovalDate], [RFApprovalById], [RFHeadReply]) VALUES (2, 20, N'RF/SCID/130820/2', 0, NULL, CAST(N'2020-08-13T08:30:20.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL)
INSERT [dbo].[RequisitionForms] ([Id], [EmployeeId], [RFCode], [RFStatus], [RFComments], [RFDate], [RFApprovalDate], [RFApprovalById], [RFHeadReply]) VALUES (3, 20, N'RF/SCID/140820/1', 6, N'', CAST(N'2020-08-14T08:30:20.0000000' AS DateTime2), CAST(N'2020-08-13T08:30:20.0000000' AS DateTime2), 21, NULL)
INSERT [dbo].[RequisitionForms] ([Id], [EmployeeId], [RFCode], [RFStatus], [RFComments], [RFDate], [RFApprovalDate], [RFApprovalById], [RFHeadReply]) VALUES (4, 20, N'RF/SCID/130820/1', 1, N'', CAST(N'2020-08-13T08:30:20.0000000' AS DateTime2), CAST(N'2020-08-13T08:30:20.0000000' AS DateTime2), 21, NULL)
INSERT [dbo].[RequisitionForms] ([Id], [EmployeeId], [RFCode], [RFStatus], [RFComments], [RFDate], [RFApprovalDate], [RFApprovalById], [RFHeadReply]) VALUES (5, 20, N'RF/SCID/100820/1', 3, NULL, CAST(N'2020-08-10T08:30:20.0000000' AS DateTime2), CAST(N'2020-08-11T08:30:20.0000000' AS DateTime2), 21, NULL)
INSERT [dbo].[RequisitionForms] ([Id], [EmployeeId], [RFCode], [RFStatus], [RFComments], [RFDate], [RFApprovalDate], [RFApprovalById], [RFHeadReply]) VALUES (6, 42, N'RF/SCID/260820/1', 6, NULL, CAST(N'2020-08-26T20:25:16.1142210' AS DateTime2), CAST(N'2020-08-26T20:58:05.4086247' AS DateTime2), 44, N'TEST: approva some but not all')
INSERT [dbo].[RequisitionForms] ([Id], [EmployeeId], [RFCode], [RFStatus], [RFComments], [RFDate], [RFApprovalDate], [RFApprovalById], [RFHeadReply]) VALUES (7, 42, N'RF/SCID/260820/2', 5, NULL, CAST(N'2020-08-26T20:25:53.1304248' AS DateTime2), CAST(N'2020-08-26T20:58:27.4559447' AS DateTime2), 44, N'test: reject')
INSERT [dbo].[RequisitionForms] ([Id], [EmployeeId], [RFCode], [RFStatus], [RFComments], [RFDate], [RFApprovalDate], [RFApprovalById], [RFHeadReply]) VALUES (8, 42, N'RF/SCID/260820/3', 6, NULL, CAST(N'2020-08-26T20:55:29.1385819' AS DateTime2), CAST(N'2020-08-26T20:58:48.9386945' AS DateTime2), 44, N'test : approval all')
INSERT [dbo].[RequisitionForms] ([Id], [EmployeeId], [RFCode], [RFStatus], [RFComments], [RFDate], [RFApprovalDate], [RFApprovalById], [RFHeadReply]) VALUES (9, 3, N'RF/ENGD/270820/1', 6, NULL, CAST(N'2020-08-27T04:41:15.2837350' AS DateTime2), CAST(N'2020-08-27T04:42:33.3017438' AS DateTime2), 5, N'')
INSERT [dbo].[RequisitionForms] ([Id], [EmployeeId], [RFCode], [RFStatus], [RFComments], [RFDate], [RFApprovalDate], [RFApprovalById], [RFHeadReply]) VALUES (10, 3, N'RF/ENGD/270820/2', 6, NULL, CAST(N'2020-08-27T04:41:35.5952405' AS DateTime2), CAST(N'2020-08-27T04:42:40.2578308' AS DateTime2), 5, N'')
INSERT [dbo].[RequisitionForms] ([Id], [EmployeeId], [RFCode], [RFStatus], [RFComments], [RFDate], [RFApprovalDate], [RFApprovalById], [RFHeadReply]) VALUES (11, 3, N'RF/ENGD/270820/3', 6, NULL, CAST(N'2020-08-27T04:41:59.9922801' AS DateTime2), CAST(N'2020-08-27T04:42:44.5686674' AS DateTime2), 5, N'')
INSERT [dbo].[RequisitionForms] ([Id], [EmployeeId], [RFCode], [RFStatus], [RFComments], [RFDate], [RFApprovalDate], [RFApprovalById], [RFHeadReply]) VALUES (12, 4, N'RF/ENGD/270820/4', 0, NULL, CAST(N'2020-08-27T04:43:23.3808455' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL)
INSERT [dbo].[RequisitionForms] ([Id], [EmployeeId], [RFCode], [RFStatus], [RFComments], [RFDate], [RFApprovalDate], [RFApprovalById], [RFHeadReply]) VALUES (13, 49, N'RF/MNGD/270820/1', 0, NULL, CAST(N'2020-08-27T06:21:48.3349205' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL)
INSERT [dbo].[RequisitionForms] ([Id], [EmployeeId], [RFCode], [RFStatus], [RFComments], [RFDate], [RFApprovalDate], [RFApprovalById], [RFHeadReply]) VALUES (14, 49, N'RF/MNGD/270820/2', 0, NULL, CAST(N'2020-08-27T06:22:00.0340464' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL)
INSERT [dbo].[RequisitionForms] ([Id], [EmployeeId], [RFCode], [RFStatus], [RFComments], [RFDate], [RFApprovalDate], [RFApprovalById], [RFHeadReply]) VALUES (15, 49, N'RF/MNGD/270820/3', 0, NULL, CAST(N'2020-08-27T06:22:11.6850636' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL)
INSERT [dbo].[RequisitionForms] ([Id], [EmployeeId], [RFCode], [RFStatus], [RFComments], [RFDate], [RFApprovalDate], [RFApprovalById], [RFHeadReply]) VALUES (16, 49, N'RF/MNGD/270820/4', 0, NULL, CAST(N'2020-08-27T06:22:24.1164719' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL)
INSERT [dbo].[RequisitionForms] ([Id], [EmployeeId], [RFCode], [RFStatus], [RFComments], [RFDate], [RFApprovalDate], [RFApprovalById], [RFHeadReply]) VALUES (17, 38, N'RF/HQDP/270820/1', 1, NULL, CAST(N'2020-08-27T06:22:45.4837751' AS DateTime2), CAST(N'2020-08-27T06:26:58.6065545' AS DateTime2), 36, N'')
INSERT [dbo].[RequisitionForms] ([Id], [EmployeeId], [RFCode], [RFStatus], [RFComments], [RFDate], [RFApprovalDate], [RFApprovalById], [RFHeadReply]) VALUES (18, 38, N'RF/HQDP/270820/2', 0, NULL, CAST(N'2020-08-27T06:22:56.9583431' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL)
INSERT [dbo].[RequisitionForms] ([Id], [EmployeeId], [RFCode], [RFStatus], [RFComments], [RFDate], [RFApprovalDate], [RFApprovalById], [RFHeadReply]) VALUES (19, 38, N'RF/HQDP/270820/3', 0, NULL, CAST(N'2020-08-27T06:23:04.5353444' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL)
INSERT [dbo].[RequisitionForms] ([Id], [EmployeeId], [RFCode], [RFStatus], [RFComments], [RFDate], [RFApprovalDate], [RFApprovalById], [RFHeadReply]) VALUES (20, 38, N'RF/HQDP/270820/4', 0, NULL, CAST(N'2020-08-27T06:23:16.5610106' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL)
INSERT [dbo].[RequisitionForms] ([Id], [EmployeeId], [RFCode], [RFStatus], [RFComments], [RFDate], [RFApprovalDate], [RFApprovalById], [RFHeadReply]) VALUES (21, 38, N'RF/HQDP/270820/5', 4, NULL, CAST(N'2020-08-27T06:23:24.7616480' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL)
INSERT [dbo].[RequisitionForms] ([Id], [EmployeeId], [RFCode], [RFStatus], [RFComments], [RFDate], [RFApprovalDate], [RFApprovalById], [RFHeadReply]) VALUES (22, 34, N'RF/HQDP/270820/6', 0, NULL, CAST(N'2020-08-27T06:28:25.0692995' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL)
INSERT [dbo].[RequisitionForms] ([Id], [EmployeeId], [RFCode], [RFStatus], [RFComments], [RFDate], [RFApprovalDate], [RFApprovalById], [RFHeadReply]) VALUES (23, 34, N'RF/HQDP/270820/7', 0, NULL, CAST(N'2020-08-27T06:28:31.9341647' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL)
INSERT [dbo].[RequisitionForms] ([Id], [EmployeeId], [RFCode], [RFStatus], [RFComments], [RFDate], [RFApprovalDate], [RFApprovalById], [RFHeadReply]) VALUES (24, 11, N'RF/ENDP/270820/1', 0, NULL, CAST(N'2020-08-27T06:31:03.6152703' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL)
INSERT [dbo].[RequisitionForms] ([Id], [EmployeeId], [RFCode], [RFStatus], [RFComments], [RFDate], [RFApprovalDate], [RFApprovalById], [RFHeadReply]) VALUES (25, 42, N'RF/SCID/270820/1', 0, NULL, CAST(N'2020-08-27T06:32:53.6678005' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL)
INSERT [dbo].[RequisitionForms] ([Id], [EmployeeId], [RFCode], [RFStatus], [RFComments], [RFDate], [RFApprovalDate], [RFApprovalById], [RFHeadReply]) VALUES (26, 42, N'RF/SCID/270820/2', 0, NULL, CAST(N'2020-08-27T06:33:01.9579544' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL)
INSERT [dbo].[RequisitionForms] ([Id], [EmployeeId], [RFCode], [RFStatus], [RFComments], [RFDate], [RFApprovalDate], [RFApprovalById], [RFHeadReply]) VALUES (27, 45, N'RF/SCID/270820/3', 0, NULL, CAST(N'2020-08-27T06:33:34.7096751' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), NULL, NULL)
INSERT [dbo].[RequisitionForms] ([Id], [EmployeeId], [RFCode], [RFStatus], [RFComments], [RFDate], [RFApprovalDate], [RFApprovalById], [RFHeadReply]) VALUES (28, 42, N'RF/SCID/110520/1', 3, NULL, CAST(N'2020-05-11T06:32:53.6678005' AS DateTime2), CAST(N'2020-05-15T18:30:19.1541407' AS DateTime2), 44, N'tttest 29')
INSERT [dbo].[RequisitionForms] ([Id], [EmployeeId], [RFCode], [RFStatus], [RFComments], [RFDate], [RFApprovalDate], [RFApprovalById], [RFHeadReply]) VALUES (29, 42, N'RF/SCID/130520/1', 3, NULL, CAST(N'2020-05-13T06:33:01.9579544' AS DateTime2), CAST(N'2020-05-14T18:30:19.1541407' AS DateTime2), 44, NULL)
INSERT [dbo].[RequisitionForms] ([Id], [EmployeeId], [RFCode], [RFStatus], [RFComments], [RFDate], [RFApprovalDate], [RFApprovalById], [RFHeadReply]) VALUES (30, 45, N'RF/SCID/280520/1', 3, NULL, CAST(N'2020-05-28T06:33:34.7096751' AS DateTime2), CAST(N'2020-05-29T18:30:19.1541407' AS DateTime2), 44, NULL)
INSERT [dbo].[RequisitionForms] ([Id], [EmployeeId], [RFCode], [RFStatus], [RFComments], [RFDate], [RFApprovalDate], [RFApprovalById], [RFHeadReply]) VALUES (31, 11, N'RF/ENDP/120620/1', 3, N'', CAST(N'2020-06-12T18:22:34.9183780' AS DateTime2), CAST(N'2020-06-21T18:29:27.1826642' AS DateTime2), 13, NULL)
INSERT [dbo].[RequisitionForms] ([Id], [EmployeeId], [RFCode], [RFStatus], [RFComments], [RFDate], [RFApprovalDate], [RFApprovalById], [RFHeadReply]) VALUES (32, 25, N'RF/ARTD/150620/1', 3, N'', CAST(N'2020-06-15T18:28:58.6080343' AS DateTime2), CAST(N'2020-06-22T18:29:27.1826642' AS DateTime2), 23, N'')
INSERT [dbo].[RequisitionForms] ([Id], [EmployeeId], [RFCode], [RFStatus], [RFComments], [RFDate], [RFApprovalDate], [RFApprovalById], [RFHeadReply]) VALUES (33, 46, N'RF/BIZD/290620/1', 3, N'', CAST(N'2020-06-29T18:30:19.1541407' AS DateTime2), CAST(N'2020-06-30T18:29:27.1826642' AS DateTime2), 48, NULL)
INSERT [dbo].[RequisitionForms] ([Id], [EmployeeId], [RFCode], [RFStatus], [RFComments], [RFDate], [RFApprovalDate], [RFApprovalById], [RFHeadReply]) VALUES (34, 11, N'RF/ENDP/040720/1', 3, N'', CAST(N'2020-07-04T18:22:34.9183780' AS DateTime2), CAST(N'2020-07-09T18:29:27.1826642' AS DateTime2), 13, NULL)
INSERT [dbo].[RequisitionForms] ([Id], [EmployeeId], [RFCode], [RFStatus], [RFComments], [RFDate], [RFApprovalDate], [RFApprovalById], [RFHeadReply]) VALUES (35, 25, N'RF/ARTD/090720/1', 3, N'', CAST(N'2020-07-09T18:28:58.6080343' AS DateTime2), CAST(N'2020-07-11T18:29:27.1826642' AS DateTime2), 23, N'')
INSERT [dbo].[RequisitionForms] ([Id], [EmployeeId], [RFCode], [RFStatus], [RFComments], [RFDate], [RFApprovalDate], [RFApprovalById], [RFHeadReply]) VALUES (36, 46, N'RF/BIZD/210720/1', 3, N'', CAST(N'2020-07-21T18:30:19.1541407' AS DateTime2), CAST(N'2020-07-30T18:29:27.1826642' AS DateTime2), 48, NULL)
INSERT [dbo].[RequisitionForms] ([Id], [EmployeeId], [RFCode], [RFStatus], [RFComments], [RFDate], [RFApprovalDate], [RFApprovalById], [RFHeadReply]) VALUES (37, 7, N'RF/CHSL/040820/1', 3, N'', CAST(N'2020-08-04T18:22:34.9183780' AS DateTime2), CAST(N'2020-08-19T18:29:27.1826642' AS DateTime2), 9, NULL)
INSERT [dbo].[RequisitionForms] ([Id], [EmployeeId], [RFCode], [RFStatus], [RFComments], [RFDate], [RFApprovalDate], [RFApprovalById], [RFHeadReply]) VALUES (38, 7, N'RF/CHSL/090820/1', 3, N'', CAST(N'2020-08-09T18:28:58.6080343' AS DateTime2), CAST(N'2020-08-19T18:29:27.1826642' AS DateTime2), 9, N'')
INSERT [dbo].[RequisitionForms] ([Id], [EmployeeId], [RFCode], [RFStatus], [RFComments], [RFDate], [RFApprovalDate], [RFApprovalById], [RFHeadReply]) VALUES (39, 8, N'RF/CHSL/210820/1', 6, N'', CAST(N'2020-08-21T18:30:19.1541407' AS DateTime2), CAST(N'2020-08-29T18:29:27.1826642' AS DateTime2), 9, NULL)
SET IDENTITY_INSERT [dbo].[RequisitionForms] OFF
GO
SET IDENTITY_INSERT [dbo].[RequisitionFormsProducts] ON 

INSERT [dbo].[RequisitionFormsProducts] ([Id], [RequisitionFormId], [ProductId], [ProductRequested], [ProductApproved], [ProductCollectedTotal]) VALUES (1, 3, 2, 3, 2, 0)
INSERT [dbo].[RequisitionFormsProducts] ([Id], [RequisitionFormId], [ProductId], [ProductRequested], [ProductApproved], [ProductCollectedTotal]) VALUES (2, 3, 1, 5, 5, 0)
INSERT [dbo].[RequisitionFormsProducts] ([Id], [RequisitionFormId], [ProductId], [ProductRequested], [ProductApproved], [ProductCollectedTotal]) VALUES (3, 4, 5, 10, 1, 0)
INSERT [dbo].[RequisitionFormsProducts] ([Id], [RequisitionFormId], [ProductId], [ProductRequested], [ProductApproved], [ProductCollectedTotal]) VALUES (4, 4, 4, 10, 10, 0)
INSERT [dbo].[RequisitionFormsProducts] ([Id], [RequisitionFormId], [ProductId], [ProductRequested], [ProductApproved], [ProductCollectedTotal]) VALUES (5, 4, 2, 10, 10, 0)
INSERT [dbo].[RequisitionFormsProducts] ([Id], [RequisitionFormId], [ProductId], [ProductRequested], [ProductApproved], [ProductCollectedTotal]) VALUES (6, 4, 1, 10, 10, 0)
INSERT [dbo].[RequisitionFormsProducts] ([Id], [RequisitionFormId], [ProductId], [ProductRequested], [ProductApproved], [ProductCollectedTotal]) VALUES (7, 5, 2, 20, 10, 7)
INSERT [dbo].[RequisitionFormsProducts] ([Id], [RequisitionFormId], [ProductId], [ProductRequested], [ProductApproved], [ProductCollectedTotal]) VALUES (8, 5, 1, 10, 10, 9)
INSERT [dbo].[RequisitionFormsProducts] ([Id], [RequisitionFormId], [ProductId], [ProductRequested], [ProductApproved], [ProductCollectedTotal]) VALUES (9, 6, 13, 1, 1, 0)
INSERT [dbo].[RequisitionFormsProducts] ([Id], [RequisitionFormId], [ProductId], [ProductRequested], [ProductApproved], [ProductCollectedTotal]) VALUES (10, 6, 14, 2, 1, 0)
INSERT [dbo].[RequisitionFormsProducts] ([Id], [RequisitionFormId], [ProductId], [ProductRequested], [ProductApproved], [ProductCollectedTotal]) VALUES (11, 6, 15, 3, 1, 0)
INSERT [dbo].[RequisitionFormsProducts] ([Id], [RequisitionFormId], [ProductId], [ProductRequested], [ProductApproved], [ProductCollectedTotal]) VALUES (12, 7, 4, 2, 0, 0)
INSERT [dbo].[RequisitionFormsProducts] ([Id], [RequisitionFormId], [ProductId], [ProductRequested], [ProductApproved], [ProductCollectedTotal]) VALUES (13, 7, 5, 3, 0, 0)
INSERT [dbo].[RequisitionFormsProducts] ([Id], [RequisitionFormId], [ProductId], [ProductRequested], [ProductApproved], [ProductCollectedTotal]) VALUES (14, 7, 6, 4, 0, 0)
INSERT [dbo].[RequisitionFormsProducts] ([Id], [RequisitionFormId], [ProductId], [ProductRequested], [ProductApproved], [ProductCollectedTotal]) VALUES (15, 7, 7, 10, 0, 0)
INSERT [dbo].[RequisitionFormsProducts] ([Id], [RequisitionFormId], [ProductId], [ProductRequested], [ProductApproved], [ProductCollectedTotal]) VALUES (16, 8, 1, 50, 50, 0)
INSERT [dbo].[RequisitionFormsProducts] ([Id], [RequisitionFormId], [ProductId], [ProductRequested], [ProductApproved], [ProductCollectedTotal]) VALUES (17, 9, 27, 50, 50, 0)
INSERT [dbo].[RequisitionFormsProducts] ([Id], [RequisitionFormId], [ProductId], [ProductRequested], [ProductApproved], [ProductCollectedTotal]) VALUES (18, 9, 28, 50, 50, 0)
INSERT [dbo].[RequisitionFormsProducts] ([Id], [RequisitionFormId], [ProductId], [ProductRequested], [ProductApproved], [ProductCollectedTotal]) VALUES (19, 9, 29, 50, 50, 0)
INSERT [dbo].[RequisitionFormsProducts] ([Id], [RequisitionFormId], [ProductId], [ProductRequested], [ProductApproved], [ProductCollectedTotal]) VALUES (20, 9, 30, 50, 50, 0)
INSERT [dbo].[RequisitionFormsProducts] ([Id], [RequisitionFormId], [ProductId], [ProductRequested], [ProductApproved], [ProductCollectedTotal]) VALUES (21, 10, 18, 150, 140, 0)
INSERT [dbo].[RequisitionFormsProducts] ([Id], [RequisitionFormId], [ProductId], [ProductRequested], [ProductApproved], [ProductCollectedTotal]) VALUES (22, 11, 20, 100, 100, 0)
INSERT [dbo].[RequisitionFormsProducts] ([Id], [RequisitionFormId], [ProductId], [ProductRequested], [ProductApproved], [ProductCollectedTotal]) VALUES (23, 12, 3, 15, 0, 0)
INSERT [dbo].[RequisitionFormsProducts] ([Id], [RequisitionFormId], [ProductId], [ProductRequested], [ProductApproved], [ProductCollectedTotal]) VALUES (24, 13, 2, 12, 0, 0)
INSERT [dbo].[RequisitionFormsProducts] ([Id], [RequisitionFormId], [ProductId], [ProductRequested], [ProductApproved], [ProductCollectedTotal]) VALUES (25, 14, 24, 20, 0, 0)
INSERT [dbo].[RequisitionFormsProducts] ([Id], [RequisitionFormId], [ProductId], [ProductRequested], [ProductApproved], [ProductCollectedTotal]) VALUES (26, 15, 27, 30, 0, 0)
INSERT [dbo].[RequisitionFormsProducts] ([Id], [RequisitionFormId], [ProductId], [ProductRequested], [ProductApproved], [ProductCollectedTotal]) VALUES (27, 16, 46, 1, 0, 0)
INSERT [dbo].[RequisitionFormsProducts] ([Id], [RequisitionFormId], [ProductId], [ProductRequested], [ProductApproved], [ProductCollectedTotal]) VALUES (28, 16, 47, 1, 0, 0)
INSERT [dbo].[RequisitionFormsProducts] ([Id], [RequisitionFormId], [ProductId], [ProductRequested], [ProductApproved], [ProductCollectedTotal]) VALUES (29, 16, 48, 1, 0, 0)
INSERT [dbo].[RequisitionFormsProducts] ([Id], [RequisitionFormId], [ProductId], [ProductRequested], [ProductApproved], [ProductCollectedTotal]) VALUES (30, 16, 49, 1, 0, 0)
INSERT [dbo].[RequisitionFormsProducts] ([Id], [RequisitionFormId], [ProductId], [ProductRequested], [ProductApproved], [ProductCollectedTotal]) VALUES (31, 17, 2, 20, 20, 0)
INSERT [dbo].[RequisitionFormsProducts] ([Id], [RequisitionFormId], [ProductId], [ProductRequested], [ProductApproved], [ProductCollectedTotal]) VALUES (32, 17, 3, 20, 20, 0)
INSERT [dbo].[RequisitionFormsProducts] ([Id], [RequisitionFormId], [ProductId], [ProductRequested], [ProductApproved], [ProductCollectedTotal]) VALUES (33, 18, 12, 50, 0, 0)
INSERT [dbo].[RequisitionFormsProducts] ([Id], [RequisitionFormId], [ProductId], [ProductRequested], [ProductApproved], [ProductCollectedTotal]) VALUES (34, 19, 1, 1000, 0, 0)
INSERT [dbo].[RequisitionFormsProducts] ([Id], [RequisitionFormId], [ProductId], [ProductRequested], [ProductApproved], [ProductCollectedTotal]) VALUES (35, 20, 2, 5000, 0, 0)
INSERT [dbo].[RequisitionFormsProducts] ([Id], [RequisitionFormId], [ProductId], [ProductRequested], [ProductApproved], [ProductCollectedTotal]) VALUES (36, 21, 4, 2, 0, 0)
INSERT [dbo].[RequisitionFormsProducts] ([Id], [RequisitionFormId], [ProductId], [ProductRequested], [ProductApproved], [ProductCollectedTotal]) VALUES (37, 22, 1, 1000, 0, 0)
INSERT [dbo].[RequisitionFormsProducts] ([Id], [RequisitionFormId], [ProductId], [ProductRequested], [ProductApproved], [ProductCollectedTotal]) VALUES (38, 23, 2, 111, 0, 0)
INSERT [dbo].[RequisitionFormsProducts] ([Id], [RequisitionFormId], [ProductId], [ProductRequested], [ProductApproved], [ProductCollectedTotal]) VALUES (39, 24, 1, 5, 0, 0)
INSERT [dbo].[RequisitionFormsProducts] ([Id], [RequisitionFormId], [ProductId], [ProductRequested], [ProductApproved], [ProductCollectedTotal]) VALUES (40, 25, 2, 50, 0, 0)
INSERT [dbo].[RequisitionFormsProducts] ([Id], [RequisitionFormId], [ProductId], [ProductRequested], [ProductApproved], [ProductCollectedTotal]) VALUES (41, 26, 88, 20, 0, 0)
INSERT [dbo].[RequisitionFormsProducts] ([Id], [RequisitionFormId], [ProductId], [ProductRequested], [ProductApproved], [ProductCollectedTotal]) VALUES (42, 27, 29, 5000, 0, 0)
INSERT [dbo].[RequisitionFormsProducts] ([Id], [RequisitionFormId], [ProductId], [ProductRequested], [ProductApproved], [ProductCollectedTotal]) VALUES (43, 28, 11, 50, 50, 0)
INSERT [dbo].[RequisitionFormsProducts] ([Id], [RequisitionFormId], [ProductId], [ProductRequested], [ProductApproved], [ProductCollectedTotal]) VALUES (44, 29, 78, 20, 20, 0)
INSERT [dbo].[RequisitionFormsProducts] ([Id], [RequisitionFormId], [ProductId], [ProductRequested], [ProductApproved], [ProductCollectedTotal]) VALUES (45, 30, 23, 5000, 100, 0)
INSERT [dbo].[RequisitionFormsProducts] ([Id], [RequisitionFormId], [ProductId], [ProductRequested], [ProductApproved], [ProductCollectedTotal]) VALUES (46, 31, 1, 12, 12, 0)
INSERT [dbo].[RequisitionFormsProducts] ([Id], [RequisitionFormId], [ProductId], [ProductRequested], [ProductApproved], [ProductCollectedTotal]) VALUES (47, 31, 4, 12, 12, 0)
INSERT [dbo].[RequisitionFormsProducts] ([Id], [RequisitionFormId], [ProductId], [ProductRequested], [ProductApproved], [ProductCollectedTotal]) VALUES (48, 31, 5, 12, 12, 0)
INSERT [dbo].[RequisitionFormsProducts] ([Id], [RequisitionFormId], [ProductId], [ProductRequested], [ProductApproved], [ProductCollectedTotal]) VALUES (49, 32, 21, 50, 40, 0)
INSERT [dbo].[RequisitionFormsProducts] ([Id], [RequisitionFormId], [ProductId], [ProductRequested], [ProductApproved], [ProductCollectedTotal]) VALUES (50, 33, 4, 5, 5, 0)
INSERT [dbo].[RequisitionFormsProducts] ([Id], [RequisitionFormId], [ProductId], [ProductRequested], [ProductApproved], [ProductCollectedTotal]) VALUES (51, 34, 10, 12, 12, 0)
INSERT [dbo].[RequisitionFormsProducts] ([Id], [RequisitionFormId], [ProductId], [ProductRequested], [ProductApproved], [ProductCollectedTotal]) VALUES (52, 34, 88, 50, 40, 0)
INSERT [dbo].[RequisitionFormsProducts] ([Id], [RequisitionFormId], [ProductId], [ProductRequested], [ProductApproved], [ProductCollectedTotal]) VALUES (53, 35, 3, 5, 5, 0)
INSERT [dbo].[RequisitionFormsProducts] ([Id], [RequisitionFormId], [ProductId], [ProductRequested], [ProductApproved], [ProductCollectedTotal]) VALUES (54, 36, 25, 5, 5, 0)
INSERT [dbo].[RequisitionFormsProducts] ([Id], [RequisitionFormId], [ProductId], [ProductRequested], [ProductApproved], [ProductCollectedTotal]) VALUES (55, 37, 15, 12, 12, 0)
INSERT [dbo].[RequisitionFormsProducts] ([Id], [RequisitionFormId], [ProductId], [ProductRequested], [ProductApproved], [ProductCollectedTotal]) VALUES (56, 38, 72, 50, 40, 0)
INSERT [dbo].[RequisitionFormsProducts] ([Id], [RequisitionFormId], [ProductId], [ProductRequested], [ProductApproved], [ProductCollectedTotal]) VALUES (57, 38, 33, 5, 5, 0)
INSERT [dbo].[RequisitionFormsProducts] ([Id], [RequisitionFormId], [ProductId], [ProductRequested], [ProductApproved], [ProductCollectedTotal]) VALUES (58, 39, 81, 12, 12, 0)
INSERT [dbo].[RequisitionFormsProducts] ([Id], [RequisitionFormId], [ProductId], [ProductRequested], [ProductApproved], [ProductCollectedTotal]) VALUES (59, 39, 82, 50, 40, 0)
INSERT [dbo].[RequisitionFormsProducts] ([Id], [RequisitionFormId], [ProductId], [ProductRequested], [ProductApproved], [ProductCollectedTotal]) VALUES (60, 39, 45, 5, 5, 0)
INSERT [dbo].[RequisitionFormsProducts] ([Id], [RequisitionFormId], [ProductId], [ProductRequested], [ProductApproved], [ProductCollectedTotal]) VALUES (61, 39, 71, 5, 5, 0)
SET IDENTITY_INSERT [dbo].[RequisitionFormsProducts] OFF
GO
SET IDENTITY_INSERT [dbo].[StationeryRetrievalProduct] ON 

INSERT [dbo].[StationeryRetrievalProduct] ([Id], [StationeryRetrievalId], [ProductId], [ProductRequestedTotal], [ProductReceivedTotal]) VALUES (1, 1, 2, 10, 0)
INSERT [dbo].[StationeryRetrievalProduct] ([Id], [StationeryRetrievalId], [ProductId], [ProductRequestedTotal], [ProductReceivedTotal]) VALUES (2, 1, 1, 5, 0)
INSERT [dbo].[StationeryRetrievalProduct] ([Id], [StationeryRetrievalId], [ProductId], [ProductRequestedTotal], [ProductReceivedTotal]) VALUES (3, 2, 2, 10, 10)
INSERT [dbo].[StationeryRetrievalProduct] ([Id], [StationeryRetrievalId], [ProductId], [ProductRequestedTotal], [ProductReceivedTotal]) VALUES (4, 2, 1, 5, 4)
INSERT [dbo].[StationeryRetrievalProduct] ([Id], [StationeryRetrievalId], [ProductId], [ProductRequestedTotal], [ProductReceivedTotal]) VALUES (5, 3, 13, 1, 0)
INSERT [dbo].[StationeryRetrievalProduct] ([Id], [StationeryRetrievalId], [ProductId], [ProductRequestedTotal], [ProductReceivedTotal]) VALUES (6, 3, 14, 1, 0)
INSERT [dbo].[StationeryRetrievalProduct] ([Id], [StationeryRetrievalId], [ProductId], [ProductRequestedTotal], [ProductReceivedTotal]) VALUES (7, 3, 15, 1, 0)
INSERT [dbo].[StationeryRetrievalProduct] ([Id], [StationeryRetrievalId], [ProductId], [ProductRequestedTotal], [ProductReceivedTotal]) VALUES (8, 4, 18, 140, 0)
INSERT [dbo].[StationeryRetrievalProduct] ([Id], [StationeryRetrievalId], [ProductId], [ProductRequestedTotal], [ProductReceivedTotal]) VALUES (9, 5, 1, 50, 0)
INSERT [dbo].[StationeryRetrievalProduct] ([Id], [StationeryRetrievalId], [ProductId], [ProductRequestedTotal], [ProductReceivedTotal]) VALUES (10, 5, 20, 100, 0)
INSERT [dbo].[StationeryRetrievalProduct] ([Id], [StationeryRetrievalId], [ProductId], [ProductRequestedTotal], [ProductReceivedTotal]) VALUES (11, 6, 27, 50, 0)
INSERT [dbo].[StationeryRetrievalProduct] ([Id], [StationeryRetrievalId], [ProductId], [ProductRequestedTotal], [ProductReceivedTotal]) VALUES (12, 6, 28, 50, 0)
INSERT [dbo].[StationeryRetrievalProduct] ([Id], [StationeryRetrievalId], [ProductId], [ProductRequestedTotal], [ProductReceivedTotal]) VALUES (13, 6, 29, 50, 0)
INSERT [dbo].[StationeryRetrievalProduct] ([Id], [StationeryRetrievalId], [ProductId], [ProductRequestedTotal], [ProductReceivedTotal]) VALUES (14, 6, 30, 50, 0)
INSERT [dbo].[StationeryRetrievalProduct] ([Id], [StationeryRetrievalId], [ProductId], [ProductRequestedTotal], [ProductReceivedTotal]) VALUES (15, 7, 2, 2, 0)
INSERT [dbo].[StationeryRetrievalProduct] ([Id], [StationeryRetrievalId], [ProductId], [ProductRequestedTotal], [ProductReceivedTotal]) VALUES (16, 7, 1, 5, 0)
INSERT [dbo].[StationeryRetrievalProduct] ([Id], [StationeryRetrievalId], [ProductId], [ProductRequestedTotal], [ProductReceivedTotal]) VALUES (17, 8, 11, 50, 50)
INSERT [dbo].[StationeryRetrievalProduct] ([Id], [StationeryRetrievalId], [ProductId], [ProductRequestedTotal], [ProductReceivedTotal]) VALUES (18, 9, 78, 20, 20)
INSERT [dbo].[StationeryRetrievalProduct] ([Id], [StationeryRetrievalId], [ProductId], [ProductRequestedTotal], [ProductReceivedTotal]) VALUES (19, 10, 23, 200, 20)
INSERT [dbo].[StationeryRetrievalProduct] ([Id], [StationeryRetrievalId], [ProductId], [ProductRequestedTotal], [ProductReceivedTotal]) VALUES (20, 11, 1, 12, 12)
INSERT [dbo].[StationeryRetrievalProduct] ([Id], [StationeryRetrievalId], [ProductId], [ProductRequestedTotal], [ProductReceivedTotal]) VALUES (21, 11, 4, 12, 12)
INSERT [dbo].[StationeryRetrievalProduct] ([Id], [StationeryRetrievalId], [ProductId], [ProductRequestedTotal], [ProductReceivedTotal]) VALUES (22, 11, 5, 12, 12)
INSERT [dbo].[StationeryRetrievalProduct] ([Id], [StationeryRetrievalId], [ProductId], [ProductRequestedTotal], [ProductReceivedTotal]) VALUES (23, 12, 21, 40, 10)
INSERT [dbo].[StationeryRetrievalProduct] ([Id], [StationeryRetrievalId], [ProductId], [ProductRequestedTotal], [ProductReceivedTotal]) VALUES (24, 13, 4, 5, 5)
INSERT [dbo].[StationeryRetrievalProduct] ([Id], [StationeryRetrievalId], [ProductId], [ProductRequestedTotal], [ProductReceivedTotal]) VALUES (25, 14, 10, 12, 12)
INSERT [dbo].[StationeryRetrievalProduct] ([Id], [StationeryRetrievalId], [ProductId], [ProductRequestedTotal], [ProductReceivedTotal]) VALUES (26, 14, 88, 40, 40)
INSERT [dbo].[StationeryRetrievalProduct] ([Id], [StationeryRetrievalId], [ProductId], [ProductRequestedTotal], [ProductReceivedTotal]) VALUES (27, 15, 3, 5, 5)
INSERT [dbo].[StationeryRetrievalProduct] ([Id], [StationeryRetrievalId], [ProductId], [ProductRequestedTotal], [ProductReceivedTotal]) VALUES (28, 16, 25, 5, 5)
INSERT [dbo].[StationeryRetrievalProduct] ([Id], [StationeryRetrievalId], [ProductId], [ProductRequestedTotal], [ProductReceivedTotal]) VALUES (29, 17, 15, 12, 12)
INSERT [dbo].[StationeryRetrievalProduct] ([Id], [StationeryRetrievalId], [ProductId], [ProductRequestedTotal], [ProductReceivedTotal]) VALUES (30, 18, 72, 40, 40)
INSERT [dbo].[StationeryRetrievalProduct] ([Id], [StationeryRetrievalId], [ProductId], [ProductRequestedTotal], [ProductReceivedTotal]) VALUES (31, 18, 33, 5, 5)
INSERT [dbo].[StationeryRetrievalProduct] ([Id], [StationeryRetrievalId], [ProductId], [ProductRequestedTotal], [ProductReceivedTotal]) VALUES (32, 19, 81, 12, 12)
INSERT [dbo].[StationeryRetrievalProduct] ([Id], [StationeryRetrievalId], [ProductId], [ProductRequestedTotal], [ProductReceivedTotal]) VALUES (33, 19, 82, 40, 40)
INSERT [dbo].[StationeryRetrievalProduct] ([Id], [StationeryRetrievalId], [ProductId], [ProductRequestedTotal], [ProductReceivedTotal]) VALUES (34, 19, 43, 5, 5)
INSERT [dbo].[StationeryRetrievalProduct] ([Id], [StationeryRetrievalId], [ProductId], [ProductRequestedTotal], [ProductReceivedTotal]) VALUES (35, 19, 71, 5, 5)
SET IDENTITY_INSERT [dbo].[StationeryRetrievalProduct] OFF
GO
SET IDENTITY_INSERT [dbo].[StationeryRetrievalRequisitionFormProducts] ON 

INSERT [dbo].[StationeryRetrievalRequisitionFormProducts] ([Id], [ProductAssigned], [SRId], [RFPId]) VALUES (1, 0, 2, 1)
INSERT [dbo].[StationeryRetrievalRequisitionFormProducts] ([Id], [ProductAssigned], [SRId], [RFPId]) VALUES (2, 0, 2, 2)
INSERT [dbo].[StationeryRetrievalRequisitionFormProducts] ([Id], [ProductAssigned], [SRId], [RFPId]) VALUES (3, 0, 2, 5)
INSERT [dbo].[StationeryRetrievalRequisitionFormProducts] ([Id], [ProductAssigned], [SRId], [RFPId]) VALUES (4, 0, 2, 6)
INSERT [dbo].[StationeryRetrievalRequisitionFormProducts] ([Id], [ProductAssigned], [SRId], [RFPId]) VALUES (5, 0, 3, 6)
INSERT [dbo].[StationeryRetrievalRequisitionFormProducts] ([Id], [ProductAssigned], [SRId], [RFPId]) VALUES (6, 0, 4, 21)
INSERT [dbo].[StationeryRetrievalRequisitionFormProducts] ([Id], [ProductAssigned], [SRId], [RFPId]) VALUES (7, 0, 5, 16)
INSERT [dbo].[StationeryRetrievalRequisitionFormProducts] ([Id], [ProductAssigned], [SRId], [RFPId]) VALUES (8, 0, 5, 22)
INSERT [dbo].[StationeryRetrievalRequisitionFormProducts] ([Id], [ProductAssigned], [SRId], [RFPId]) VALUES (9, 0, 6, 17)
INSERT [dbo].[StationeryRetrievalRequisitionFormProducts] ([Id], [ProductAssigned], [SRId], [RFPId]) VALUES (10, 0, 6, 18)
INSERT [dbo].[StationeryRetrievalRequisitionFormProducts] ([Id], [ProductAssigned], [SRId], [RFPId]) VALUES (11, 0, 6, 19)
INSERT [dbo].[StationeryRetrievalRequisitionFormProducts] ([Id], [ProductAssigned], [SRId], [RFPId]) VALUES (12, 0, 6, 20)
INSERT [dbo].[StationeryRetrievalRequisitionFormProducts] ([Id], [ProductAssigned], [SRId], [RFPId]) VALUES (13, 0, 7, 1)
INSERT [dbo].[StationeryRetrievalRequisitionFormProducts] ([Id], [ProductAssigned], [SRId], [RFPId]) VALUES (14, 0, 7, 2)
INSERT [dbo].[StationeryRetrievalRequisitionFormProducts] ([Id], [ProductAssigned], [SRId], [RFPId]) VALUES (15, 50, 8, 43)
INSERT [dbo].[StationeryRetrievalRequisitionFormProducts] ([Id], [ProductAssigned], [SRId], [RFPId]) VALUES (16, 20, 9, 44)
INSERT [dbo].[StationeryRetrievalRequisitionFormProducts] ([Id], [ProductAssigned], [SRId], [RFPId]) VALUES (17, 20, 10, 45)
INSERT [dbo].[StationeryRetrievalRequisitionFormProducts] ([Id], [ProductAssigned], [SRId], [RFPId]) VALUES (18, 12, 11, 46)
INSERT [dbo].[StationeryRetrievalRequisitionFormProducts] ([Id], [ProductAssigned], [SRId], [RFPId]) VALUES (19, 12, 11, 47)
INSERT [dbo].[StationeryRetrievalRequisitionFormProducts] ([Id], [ProductAssigned], [SRId], [RFPId]) VALUES (20, 12, 11, 48)
INSERT [dbo].[StationeryRetrievalRequisitionFormProducts] ([Id], [ProductAssigned], [SRId], [RFPId]) VALUES (21, 10, 12, 49)
INSERT [dbo].[StationeryRetrievalRequisitionFormProducts] ([Id], [ProductAssigned], [SRId], [RFPId]) VALUES (22, 5, 13, 50)
INSERT [dbo].[StationeryRetrievalRequisitionFormProducts] ([Id], [ProductAssigned], [SRId], [RFPId]) VALUES (23, 12, 14, 51)
INSERT [dbo].[StationeryRetrievalRequisitionFormProducts] ([Id], [ProductAssigned], [SRId], [RFPId]) VALUES (24, 40, 14, 52)
INSERT [dbo].[StationeryRetrievalRequisitionFormProducts] ([Id], [ProductAssigned], [SRId], [RFPId]) VALUES (25, 5, 15, 53)
INSERT [dbo].[StationeryRetrievalRequisitionFormProducts] ([Id], [ProductAssigned], [SRId], [RFPId]) VALUES (26, 5, 16, 54)
INSERT [dbo].[StationeryRetrievalRequisitionFormProducts] ([Id], [ProductAssigned], [SRId], [RFPId]) VALUES (27, 12, 17, 55)
INSERT [dbo].[StationeryRetrievalRequisitionFormProducts] ([Id], [ProductAssigned], [SRId], [RFPId]) VALUES (28, 40, 18, 56)
INSERT [dbo].[StationeryRetrievalRequisitionFormProducts] ([Id], [ProductAssigned], [SRId], [RFPId]) VALUES (29, 5, 18, 57)
INSERT [dbo].[StationeryRetrievalRequisitionFormProducts] ([Id], [ProductAssigned], [SRId], [RFPId]) VALUES (30, 0, 19, 58)
INSERT [dbo].[StationeryRetrievalRequisitionFormProducts] ([Id], [ProductAssigned], [SRId], [RFPId]) VALUES (31, 0, 19, 59)
INSERT [dbo].[StationeryRetrievalRequisitionFormProducts] ([Id], [ProductAssigned], [SRId], [RFPId]) VALUES (32, 0, 19, 60)
INSERT [dbo].[StationeryRetrievalRequisitionFormProducts] ([Id], [ProductAssigned], [SRId], [RFPId]) VALUES (33, 0, 19, 61)
SET IDENTITY_INSERT [dbo].[StationeryRetrievalRequisitionFormProducts] OFF
GO
SET IDENTITY_INSERT [dbo].[StationeryRetrievalRequisitionForms] ON 

INSERT [dbo].[StationeryRetrievalRequisitionForms] ([Id], [StationeryRetrievalId], [RequisitionFormId], [SRRFStatus]) VALUES (1, 2, 3, 1)
INSERT [dbo].[StationeryRetrievalRequisitionForms] ([Id], [StationeryRetrievalId], [RequisitionFormId], [SRRFStatus]) VALUES (2, 2, 4, 1)
INSERT [dbo].[StationeryRetrievalRequisitionForms] ([Id], [StationeryRetrievalId], [RequisitionFormId], [SRRFStatus]) VALUES (3, 3, 6, 0)
INSERT [dbo].[StationeryRetrievalRequisitionForms] ([Id], [StationeryRetrievalId], [RequisitionFormId], [SRRFStatus]) VALUES (4, 4, 10, 2)
INSERT [dbo].[StationeryRetrievalRequisitionForms] ([Id], [StationeryRetrievalId], [RequisitionFormId], [SRRFStatus]) VALUES (5, 5, 8, 0)
INSERT [dbo].[StationeryRetrievalRequisitionForms] ([Id], [StationeryRetrievalId], [RequisitionFormId], [SRRFStatus]) VALUES (6, 5, 11, 0)
INSERT [dbo].[StationeryRetrievalRequisitionForms] ([Id], [StationeryRetrievalId], [RequisitionFormId], [SRRFStatus]) VALUES (7, 6, 9, 3)
INSERT [dbo].[StationeryRetrievalRequisitionForms] ([Id], [StationeryRetrievalId], [RequisitionFormId], [SRRFStatus]) VALUES (8, 7, 3, 0)
INSERT [dbo].[StationeryRetrievalRequisitionForms] ([Id], [StationeryRetrievalId], [RequisitionFormId], [SRRFStatus]) VALUES (9, 8, 28, 2)
INSERT [dbo].[StationeryRetrievalRequisitionForms] ([Id], [StationeryRetrievalId], [RequisitionFormId], [SRRFStatus]) VALUES (10, 9, 29, 2)
INSERT [dbo].[StationeryRetrievalRequisitionForms] ([Id], [StationeryRetrievalId], [RequisitionFormId], [SRRFStatus]) VALUES (11, 10, 30, 2)
INSERT [dbo].[StationeryRetrievalRequisitionForms] ([Id], [StationeryRetrievalId], [RequisitionFormId], [SRRFStatus]) VALUES (12, 11, 31, 2)
INSERT [dbo].[StationeryRetrievalRequisitionForms] ([Id], [StationeryRetrievalId], [RequisitionFormId], [SRRFStatus]) VALUES (13, 12, 32, 2)
INSERT [dbo].[StationeryRetrievalRequisitionForms] ([Id], [StationeryRetrievalId], [RequisitionFormId], [SRRFStatus]) VALUES (14, 13, 33, 2)
INSERT [dbo].[StationeryRetrievalRequisitionForms] ([Id], [StationeryRetrievalId], [RequisitionFormId], [SRRFStatus]) VALUES (15, 14, 34, 2)
INSERT [dbo].[StationeryRetrievalRequisitionForms] ([Id], [StationeryRetrievalId], [RequisitionFormId], [SRRFStatus]) VALUES (16, 15, 35, 2)
INSERT [dbo].[StationeryRetrievalRequisitionForms] ([Id], [StationeryRetrievalId], [RequisitionFormId], [SRRFStatus]) VALUES (17, 16, 36, 2)
INSERT [dbo].[StationeryRetrievalRequisitionForms] ([Id], [StationeryRetrievalId], [RequisitionFormId], [SRRFStatus]) VALUES (18, 17, 37, 2)
INSERT [dbo].[StationeryRetrievalRequisitionForms] ([Id], [StationeryRetrievalId], [RequisitionFormId], [SRRFStatus]) VALUES (19, 18, 38, 2)
INSERT [dbo].[StationeryRetrievalRequisitionForms] ([Id], [StationeryRetrievalId], [RequisitionFormId], [SRRFStatus]) VALUES (20, 19, 39, 1)
SET IDENTITY_INSERT [dbo].[StationeryRetrievalRequisitionForms] OFF
GO
SET IDENTITY_INSERT [dbo].[StationeryRetrievals] ON 

INSERT [dbo].[StationeryRetrievals] ([Id], [StoreClerkId], [WarehousePackerId], [SRCode], [SRStatus], [SRComments], [SRDate], [SRAssignedDate], [SRRetrievalDate]) VALUES (1, 20, 18, N'SR/130820/2', 2, N'Assigned', CAST(N'2020-08-13T08:30:20.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[StationeryRetrievals] ([Id], [StoreClerkId], [WarehousePackerId], [SRCode], [SRStatus], [SRComments], [SRDate], [SRAssignedDate], [SRRetrievalDate]) VALUES (2, 20, 18, N'SR/130820/1', 1, N'Pending', CAST(N'2020-08-13T08:30:20.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[StationeryRetrievals] ([Id], [StoreClerkId], [WarehousePackerId], [SRCode], [SRStatus], [SRComments], [SRDate], [SRAssignedDate], [SRRetrievalDate]) VALUES (3, 1, 29, N'SR/260820/1', 1, N'test create', CAST(N'2020-08-26T21:06:00.4180440' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'2020-08-26T21:09:11.4232470' AS DateTime2))
INSERT [dbo].[StationeryRetrievals] ([Id], [StoreClerkId], [WarehousePackerId], [SRCode], [SRStatus], [SRComments], [SRDate], [SRAssignedDate], [SRRetrievalDate]) VALUES (4, 27, 29, N'SR/270820/1', 2, N'test creat', CAST(N'2020-08-27T04:49:19.2927538' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'2020-08-27T05:35:53.4152661' AS DateTime2))
INSERT [dbo].[StationeryRetrievals] ([Id], [StoreClerkId], [WarehousePackerId], [SRCode], [SRStatus], [SRComments], [SRDate], [SRAssignedDate], [SRRetrievalDate]) VALUES (5, 46, 25, N'SR/270820/2', 1, N'test multi-creat ', CAST(N'2020-08-27T04:50:48.8847620' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'2020-08-27T05:55:55.3736091' AS DateTime2))
INSERT [dbo].[StationeryRetrievals] ([Id], [StoreClerkId], [WarehousePackerId], [SRCode], [SRStatus], [SRComments], [SRDate], [SRAssignedDate], [SRRetrievalDate]) VALUES (6, 28, 27, N'SR/270820/3', 2, N'test for not enough from warehouse', CAST(N'2020-08-27T05:31:08.6540141' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'2020-08-27T05:51:49.3312276' AS DateTime2))
INSERT [dbo].[StationeryRetrievals] ([Id], [StoreClerkId], [WarehousePackerId], [SRCode], [SRStatus], [SRComments], [SRDate], [SRAssignedDate], [SRRetrievalDate]) VALUES (7, 28, NULL, N'SR/270820/3', 0, N'test', CAST(N'2020-08-27T06:35:57.1078352' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[StationeryRetrievals] ([Id], [StoreClerkId], [WarehousePackerId], [SRCode], [SRStatus], [SRComments], [SRDate], [SRAssignedDate], [SRRetrievalDate]) VALUES (8, 1, 29, N'SR/170520/1', 2, N'test 29', CAST(N'2020-05-17T18:09:33.5591545' AS DateTime2), CAST(N'2020-08-29T18:11:05.2222524' AS DateTime2), CAST(N'2020-08-29T18:10:45.9833741' AS DateTime2))
INSERT [dbo].[StationeryRetrievals] ([Id], [StoreClerkId], [WarehousePackerId], [SRCode], [SRStatus], [SRComments], [SRDate], [SRAssignedDate], [SRRetrievalDate]) VALUES (9, 1, 18, N'SR/170520/2', 2, N'', CAST(N'2020-05-17T19:33:43.4939211' AS DateTime2), CAST(N'2020-08-29T21:39:46.7256361' AS DateTime2), CAST(N'2020-08-29T20:03:12.9104143' AS DateTime2))
INSERT [dbo].[StationeryRetrievals] ([Id], [StoreClerkId], [WarehousePackerId], [SRCode], [SRStatus], [SRComments], [SRDate], [SRAssignedDate], [SRRetrievalDate]) VALUES (10, 1, 18, N'SR/290520/1', 2, N'', CAST(N'2020-05-29T19:34:06.6459253' AS DateTime2), CAST(N'2020-08-29T21:53:19.0521925' AS DateTime2), CAST(N'2020-08-29T21:36:03.8788038' AS DateTime2))
INSERT [dbo].[StationeryRetrievals] ([Id], [StoreClerkId], [WarehousePackerId], [SRCode], [SRStatus], [SRComments], [SRDate], [SRAssignedDate], [SRRetrievalDate]) VALUES (11, 1, 18, N'SR/220620/1', 2, N'', CAST(N'2020-06-22T19:34:14.1514226' AS DateTime2), CAST(N'2020-08-29T21:53:39.0013943' AS DateTime2), CAST(N'2020-08-29T21:36:37.7123966' AS DateTime2))
INSERT [dbo].[StationeryRetrievals] ([Id], [StoreClerkId], [WarehousePackerId], [SRCode], [SRStatus], [SRComments], [SRDate], [SRAssignedDate], [SRRetrievalDate]) VALUES (12, 1, 18, N'SR/230620/1', 2, N'', CAST(N'2020-06-23T19:34:22.9752927' AS DateTime2), CAST(N'2020-08-29T21:53:45.8592660' AS DateTime2), CAST(N'2020-08-29T21:37:01.3279240' AS DateTime2))
INSERT [dbo].[StationeryRetrievals] ([Id], [StoreClerkId], [WarehousePackerId], [SRCode], [SRStatus], [SRComments], [SRDate], [SRAssignedDate], [SRRetrievalDate]) VALUES (13, 1, 29, N'SR/290620/1', 2, N'', CAST(N'2020-06-29T19:34:30.2601128' AS DateTime2), CAST(N'2020-08-29T21:53:49.8116473' AS DateTime2), CAST(N'2020-08-29T21:37:34.7105902' AS DateTime2))
INSERT [dbo].[StationeryRetrievals] ([Id], [StoreClerkId], [WarehousePackerId], [SRCode], [SRStatus], [SRComments], [SRDate], [SRAssignedDate], [SRRetrievalDate]) VALUES (14, 1, 18, N'SR/110720/1', 2, N'', CAST(N'2020-07-11T19:34:37.6468077' AS DateTime2), CAST(N'2020-08-29T21:53:55.0779026' AS DateTime2), CAST(N'2020-08-29T21:37:51.9754628' AS DateTime2))
INSERT [dbo].[StationeryRetrievals] ([Id], [StoreClerkId], [WarehousePackerId], [SRCode], [SRStatus], [SRComments], [SRDate], [SRAssignedDate], [SRRetrievalDate]) VALUES (15, 1, 29, N'SR/190720/1', 2, N'', CAST(N'2020-07-19T19:34:44.9772703' AS DateTime2), CAST(N'2020-08-29T21:54:00.6828338' AS DateTime2), CAST(N'2020-08-29T21:38:08.7616010' AS DateTime2))
INSERT [dbo].[StationeryRetrievals] ([Id], [StoreClerkId], [WarehousePackerId], [SRCode], [SRStatus], [SRComments], [SRDate], [SRAssignedDate], [SRRetrievalDate]) VALUES (16, 1, 29, N'SR/300720/1', 2, N'', CAST(N'2020-07-30T19:34:51.8068216' AS DateTime2), CAST(N'2020-08-29T21:54:04.2857448' AS DateTime2), CAST(N'2020-08-29T21:38:23.4732198' AS DateTime2))
INSERT [dbo].[StationeryRetrievals] ([Id], [StoreClerkId], [WarehousePackerId], [SRCode], [SRStatus], [SRComments], [SRDate], [SRAssignedDate], [SRRetrievalDate]) VALUES (17, 1, 29, N'SR/190820/1', 2, N'', CAST(N'2020-08-19T19:34:57.6097596' AS DateTime2), CAST(N'2020-08-29T21:54:55.0944302' AS DateTime2), CAST(N'2020-08-29T21:38:40.9867449' AS DateTime2))
INSERT [dbo].[StationeryRetrievals] ([Id], [StoreClerkId], [WarehousePackerId], [SRCode], [SRStatus], [SRComments], [SRDate], [SRAssignedDate], [SRRetrievalDate]) VALUES (18, 1, 29, N'SR/190820/2', 2, N'', CAST(N'2020-08-19T19:35:03.1617386' AS DateTime2), CAST(N'2020-08-29T21:55:02.3304536' AS DateTime2), CAST(N'2020-08-29T21:38:55.4378433' AS DateTime2))
INSERT [dbo].[StationeryRetrievals] ([Id], [StoreClerkId], [WarehousePackerId], [SRCode], [SRStatus], [SRComments], [SRDate], [SRAssignedDate], [SRRetrievalDate]) VALUES (19, 1, 29, N'SR/290820/1', 1, N'', CAST(N'2020-08-29T19:35:12.1266074' AS DateTime2), CAST(N'0001-01-01T00:00:00.0000000' AS DateTime2), CAST(N'2020-08-29T21:39:24.4557148' AS DateTime2))
SET IDENTITY_INSERT [dbo].[StationeryRetrievals] OFF
GO
SET IDENTITY_INSERT [dbo].[SupplierProducts] ON 

INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (1, 7, 45, 15.05, 0, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (2, 11, 30, 3, 1, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (3, 4, 30, 3.01, 0, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (4, 4, 88, 6.02, 1, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (5, 5, 88, 6, 1, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (6, 6, 88, 6.1, 1, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (7, 9, 20, 6.09, 1, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (8, 8, 20, 6.02, 1, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (9, 6, 20, 6.05, 1, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (10, 10, 22, 6.02, 1, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (11, 7, 22, 6, 0, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (12, 2, 22, 6.03, 0, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (13, 8, 33, 6.08, 0, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (14, 5, 33, 6, 0, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (15, 11, 33, 6.08, 1, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (16, 5, 58, 6.08, 0, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (17, 6, 58, 6.08, 0, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (18, 3, 58, 6.03, 0, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (19, 5, 87, 6.02, 0, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (20, 5, 87, 6.1, 0, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (21, 8, 87, 6.05, 0, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (22, 6, 76, 6.09, 1, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (23, 7, 76, 6.02, 1, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (24, 7, 76, 6.01, 0, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (25, 3, 28, 6.05, 0, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (26, 9, 28, 6.01, 0, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (27, 10, 28, 6.09, 0, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (28, 9, 43, 6.05, 0, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (29, 9, 43, 6.03, 1, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (30, 2, 43, 6.08, 1, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (31, 2, 30, 3, 0, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (32, 9, 78, 3.1, 1, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (33, 11, 78, 3.04, 1, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (34, 9, 78, 3.07, 1, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (35, 11, 39, 3.08, 0, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (36, 9, 39, 3.05, 1, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (37, 4, 39, 3.04, 0, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (38, 5, 63, 3.09, 0, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (39, 3, 63, 3.06, 0, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (40, 2, 63, 3.04, 1, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (41, 11, 82, 3.08, 0, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (42, 11, 82, 3.07, 0, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (43, 4, 82, 3.1, 1, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (44, 4, 71, 3.08, 0, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (45, 4, 71, 3.01, 1, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (46, 10, 71, 3.08, 0, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (47, 9, 44, 3.02, 1, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (48, 4, 44, 3.05, 1, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (49, 10, 9, 7.05, 1, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (50, 5, 44, 3.08, 1, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (51, 5, 15, 3.02, 0, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (52, 7, 15, 3.08, 1, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (53, 5, 4, 3.04, 1, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (54, 11, 4, 3.01, 1, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (55, 11, 4, 3.05, 0, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (56, 10, 37, 3.06, 1, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (57, 2, 37, 3.03, 1, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (58, 5, 37, 3.02, 1, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (59, 5, 65, 3.09, 1, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (60, 2, 65, 3.08, 0, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (61, 8, 65, 3.04, 1, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (62, 7, 67, 3.04, 1, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (63, 4, 67, 3.06, 0, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (64, 11, 67, 3.09, 0, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (65, 11, 15, 3.05, 1, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (66, 11, 6, 16.01, 1, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (67, 11, 9, 7, 1, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (68, 2, 42, 7.05, 1, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (69, 2, 24, 7.1, 0, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (70, 9, 16, 7.02, 1, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (71, 9, 16, 7.03, 0, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (72, 2, 16, 7.09, 0, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (73, 2, 5, 7.1, 1, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (74, 5, 5, 7.08, 0, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (75, 11, 5, 7.03, 1, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (76, 11, 38, 8.05, 1, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (77, 11, 38, 8.09, 1, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (78, 10, 38, 8.02, 1, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (79, 2, 64, 8.07, 0, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (80, 10, 64, 8.04, 1, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (81, 11, 64, 8.03, 1, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (82, 6, 81, 8.08, 0, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (83, 4, 81, 8.06, 1, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (84, 10, 81, 8.02, 0, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (85, 3, 70, 8.06, 0, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (86, 2, 70, 8.1, 1, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (87, 8, 70, 8.06, 1, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (88, 4, 89, 8.1, 1, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (89, 2, 89, 8.01, 1, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (90, 5, 89, 8.1, 0, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (91, 5, 14, 8.01, 0, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (92, 11, 14, 8.06, 0, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (93, 7, 14, 8.01, 1, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (94, 10, 3, 8.08, 1, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (95, 3, 3, 8.1, 1, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (96, 3, 3, 8.09, 0, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (97, 10, 36, 8.07, 1, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (98, 7, 24, 7.09, 0, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (99, 11, 24, 7, 1, 3)
GO
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (100, 2, 72, 7.05, 0, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (101, 3, 72, 7, 0, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (102, 10, 42, 7.02, 1, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (103, 9, 42, 7.08, 1, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (104, 7, 60, 7, 1, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (105, 4, 60, 7.04, 1, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (106, 7, 60, 7.01, 0, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (107, 11, 85, 7.09, 0, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (108, 6, 85, 7, 0, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (109, 7, 85, 7.03, 0, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (110, 3, 74, 7.09, 0, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (111, 2, 74, 7, 1, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (112, 10, 74, 7.05, 1, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (113, 5, 26, 7.06, 1, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (114, 5, 26, 7.01, 1, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (115, 5, 26, 7.05, 1, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (116, 3, 9, 7.01, 0, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (117, 3, 18, 7.08, 1, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (118, 3, 18, 7.1, 1, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (119, 4, 7, 7.09, 0, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (120, 11, 7, 7.07, 0, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (121, 7, 7, 7.08, 0, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (122, 11, 40, 7.05, 1, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (123, 9, 40, 7.02, 1, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (124, 10, 40, 7.03, 1, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (125, 8, 62, 7, 1, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (126, 5, 62, 7.06, 0, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (127, 10, 62, 7.04, 1, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (128, 4, 83, 7.04, 0, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (129, 8, 83, 7.01, 1, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (130, 9, 83, 7, 0, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (131, 5, 72, 7.02, 0, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (132, 4, 18, 7, 0, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (133, 2, 36, 8.07, 1, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (134, 8, 6, 16.01, 0, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (135, 11, 17, 16.05, 1, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (136, 2, 56, 2.03, 0, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (137, 2, 56, 2.06, 0, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (138, 6, 56, 2.01, 1, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (139, 10, 35, 2.05, 1, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (140, 6, 35, 2.07, 0, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (141, 2, 35, 2, 0, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (142, 11, 2, 2.1, 1, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (143, 9, 2, 2.05, 0, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (144, 2, 2, 2.06, 1, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (145, 4, 10, 2.02, 0, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (146, 8, 10, 2.09, 0, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (147, 2, 10, 2.04, 0, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (148, 10, 13, 2.03, 1, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (149, 2, 13, 2.07, 0, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (150, 3, 13, 2.07, 1, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (151, 2, 69, 2.01, 1, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (152, 8, 69, 2, 0, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (153, 7, 69, 2.01, 1, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (154, 9, 32, 2.02, 1, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (155, 10, 32, 2.08, 1, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (156, 4, 32, 2.07, 1, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (157, 10, 80, 10.05, 0, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (158, 10, 80, 10, 1, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (159, 4, 80, 10.03, 0, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (160, 8, 1, 10.08, 1, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (161, 7, 1, 10.06, 0, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (162, 8, 1, 10, 0, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (163, 5, 54, 10.09, 0, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (164, 7, 54, 10.07, 0, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (165, 3, 66, 2.09, 1, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (166, 4, 66, 2.09, 0, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (167, 8, 66, 2, 0, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (168, 7, 79, 2.07, 1, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (169, 9, 45, 15.05, 0, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (170, 9, 29, 15.04, 0, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (171, 2, 29, 15.04, 1, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (172, 11, 29, 15.08, 0, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (173, 5, 77, 15.09, 1, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (174, 7, 77, 15, 0, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (175, 6, 77, 15.06, 1, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (176, 9, 68, 15.08, 1, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (177, 10, 68, 15.07, 0, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (178, 7, 68, 15.1, 1, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (179, 2, 57, 15.02, 0, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (180, 2, 57, 15.06, 0, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (181, 2, 57, 15.05, 0, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (182, 11, 34, 15.07, 0, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (183, 7, 54, 10.01, 1, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (184, 3, 34, 15.07, 1, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (185, 2, 21, 2.05, 1, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (186, 9, 21, 2.01, 1, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (187, 5, 21, 2.09, 1, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (188, 11, 12, 2.06, 0, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (189, 11, 12, 2, 0, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (190, 9, 12, 2.03, 0, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (191, 11, 46, 2.09, 0, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (192, 4, 46, 2.1, 1, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (193, 10, 46, 2.02, 1, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (194, 6, 31, 2.06, 1, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (195, 9, 31, 2.02, 1, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (196, 7, 31, 2.05, 1, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (197, 4, 79, 2.05, 0, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (198, 11, 79, 2.03, 1, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (199, 9, 34, 15.08, 0, 3)
GO
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (200, 9, 6, 16.01, 1, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (201, 8, 53, 10.06, 1, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (202, 11, 53, 10.05, 1, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (203, 8, 75, 16.06, 0, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (204, 3, 75, 16.06, 1, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (205, 10, 75, 16.01, 0, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (206, 4, 27, 16.1, 1, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (207, 4, 27, 16.1, 1, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (208, 4, 27, 16.02, 0, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (209, 3, 19, 16.07, 0, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (210, 2, 19, 16.08, 1, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (211, 10, 19, 16.04, 0, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (212, 7, 8, 16.1, 1, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (213, 6, 8, 16.04, 0, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (214, 3, 8, 16.02, 1, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (215, 8, 41, 16.07, 1, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (216, 11, 41, 16.01, 0, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (217, 10, 41, 16, 1, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (218, 5, 61, 16.1, 0, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (219, 3, 61, 16.01, 0, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (220, 8, 61, 16.01, 1, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (221, 5, 84, 16.01, 0, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (222, 6, 84, 16.04, 1, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (223, 7, 84, 16.09, 1, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (224, 11, 73, 16.03, 0, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (225, 8, 73, 16, 0, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (226, 3, 73, 16, 0, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (227, 9, 25, 16.09, 0, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (228, 11, 25, 16.08, 1, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (229, 9, 25, 16, 0, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (230, 4, 17, 16.07, 0, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (231, 8, 17, 16.06, 1, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (232, 6, 86, 16.02, 1, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (233, 6, 86, 16.09, 1, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (234, 9, 86, 16.01, 0, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (235, 5, 59, 15.07, 1, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (236, 11, 52, 10.09, 1, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (237, 5, 52, 10.02, 1, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (238, 9, 52, 10.05, 1, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (239, 6, 51, 5.05, 0, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (240, 3, 51, 5.07, 0, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (241, 3, 51, 5.1, 0, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (242, 5, 50, 5, 1, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (243, 11, 50, 5.07, 0, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (244, 8, 50, 5.01, 0, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (245, 5, 49, 5.03, 0, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (246, 4, 49, 5.03, 0, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (247, 4, 49, 5.04, 1, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (248, 10, 48, 5.07, 1, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (249, 7, 48, 5, 1, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (250, 6, 53, 10.01, 1, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (251, 2, 48, 5.08, 1, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (252, 10, 47, 4.07, 1, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (253, 9, 47, 4.08, 1, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (254, 9, 55, 4.04, 1, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (255, 8, 55, 4.08, 1, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (256, 4, 55, 4.04, 0, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (257, 8, 45, 15.09, 1, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (258, 2, 11, 15.09, 0, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (259, 6, 11, 15, 1, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (260, 5, 11, 15.07, 0, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (261, 2, 23, 15.01, 1, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (262, 4, 23, 15.09, 0, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (263, 2, 23, 15.03, 1, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (264, 8, 59, 15.05, 1, 2)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (265, 8, 59, 15.01, 0, 3)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (266, 11, 47, 4.05, 0, 1)
INSERT [dbo].[SupplierProducts] ([Id], [SupplierId], [ProductId], [ProductPrice], [SPAvailStatus], [PriorityLevel]) VALUES (267, 6, 36, 8.01, 0, 1)
SET IDENTITY_INSERT [dbo].[SupplierProducts] OFF
GO
SET IDENTITY_INSERT [dbo].[Suppliers] ON 

INSERT [dbo].[Suppliers] ([Id], [SupplierCode], [SupplierName], [ContactName], [PhoneNumber], [FaxNumber], [Address], [GSTregistrationNumber]) VALUES (1, N'S18', N'Supplier 18', N'Supplier Contact 18', N'60810627', N'63919198', N'Address 18', N'GST Registration No. 18')
INSERT [dbo].[Suppliers] ([Id], [SupplierCode], [SupplierName], [ContactName], [PhoneNumber], [FaxNumber], [Address], [GSTregistrationNumber]) VALUES (2, N'S1', N'Supplier 1', N'Supplier Contact 1', N'68263607', N'62898538', N'Address 1', N'GST Registration No. 1')
INSERT [dbo].[Suppliers] ([Id], [SupplierCode], [SupplierName], [ContactName], [PhoneNumber], [FaxNumber], [Address], [GSTregistrationNumber]) VALUES (3, N'S2', N'Supplier 2', N'Supplier Contact 2', N'62939291', N'61699147', N'Address 2', N'GST Registration No. 2')
INSERT [dbo].[Suppliers] ([Id], [SupplierCode], [SupplierName], [ContactName], [PhoneNumber], [FaxNumber], [Address], [GSTregistrationNumber]) VALUES (4, N'S3', N'Supplier 3', N'Supplier Contact 3', N'66205440', N'65141515', N'Address 3', N'GST Registration No. 3')
INSERT [dbo].[Suppliers] ([Id], [SupplierCode], [SupplierName], [ContactName], [PhoneNumber], [FaxNumber], [Address], [GSTregistrationNumber]) VALUES (5, N'S4', N'Supplier 4', N'Supplier Contact 4', N'61037609', N'65314798', N'Address 4', N'GST Registration No. 4')
INSERT [dbo].[Suppliers] ([Id], [SupplierCode], [SupplierName], [ContactName], [PhoneNumber], [FaxNumber], [Address], [GSTregistrationNumber]) VALUES (6, N'S5', N'Supplier 5', N'Supplier Contact 5', N'69428721', N'67050167', N'Address 5', N'GST Registration No. 5')
INSERT [dbo].[Suppliers] ([Id], [SupplierCode], [SupplierName], [ContactName], [PhoneNumber], [FaxNumber], [Address], [GSTregistrationNumber]) VALUES (7, N'S6', N'Supplier 6', N'Supplier Contact 6', N'60161516', N'62965089', N'Address 6', N'GST Registration No. 6')
INSERT [dbo].[Suppliers] ([Id], [SupplierCode], [SupplierName], [ContactName], [PhoneNumber], [FaxNumber], [Address], [GSTregistrationNumber]) VALUES (8, N'S7', N'Supplier 7', N'Supplier Contact 7', N'63298032', N'68246654', N'Address 7', N'GST Registration No. 7')
INSERT [dbo].[Suppliers] ([Id], [SupplierCode], [SupplierName], [ContactName], [PhoneNumber], [FaxNumber], [Address], [GSTregistrationNumber]) VALUES (9, N'S8', N'Supplier 8', N'Supplier Contact 8', N'69716220', N'65684660', N'Address 8', N'GST Registration No. 8')
INSERT [dbo].[Suppliers] ([Id], [SupplierCode], [SupplierName], [ContactName], [PhoneNumber], [FaxNumber], [Address], [GSTregistrationNumber]) VALUES (10, N'S9', N'Supplier 9', N'Supplier Contact 9', N'67167712', N'66413586', N'Address 9', N'GST Registration No. 9')
INSERT [dbo].[Suppliers] ([Id], [SupplierCode], [SupplierName], [ContactName], [PhoneNumber], [FaxNumber], [Address], [GSTregistrationNumber]) VALUES (11, N'S10', N'Supplier 10', N'Supplier Contact 10', N'66021946', N'67551753', N'Address 10', N'GST Registration No. 10')
INSERT [dbo].[Suppliers] ([Id], [SupplierCode], [SupplierName], [ContactName], [PhoneNumber], [FaxNumber], [Address], [GSTregistrationNumber]) VALUES (12, N'S11', N'Supplier 11', N'Supplier Contact 11', N'68693500', N'62616595', N'Address 11', N'GST Registration No. 11')
INSERT [dbo].[Suppliers] ([Id], [SupplierCode], [SupplierName], [ContactName], [PhoneNumber], [FaxNumber], [Address], [GSTregistrationNumber]) VALUES (13, N'S12', N'Supplier 12', N'Supplier Contact 12', N'68015976', N'62002728', N'Address 12', N'GST Registration No. 12')
INSERT [dbo].[Suppliers] ([Id], [SupplierCode], [SupplierName], [ContactName], [PhoneNumber], [FaxNumber], [Address], [GSTregistrationNumber]) VALUES (14, N'S13', N'Supplier 13', N'Supplier Contact 13', N'62368439', N'68510624', N'Address 13', N'GST Registration No. 13')
INSERT [dbo].[Suppliers] ([Id], [SupplierCode], [SupplierName], [ContactName], [PhoneNumber], [FaxNumber], [Address], [GSTregistrationNumber]) VALUES (15, N'S14', N'Supplier 14', N'Supplier Contact 14', N'64830422', N'65362725', N'Address 14', N'GST Registration No. 14')
INSERT [dbo].[Suppliers] ([Id], [SupplierCode], [SupplierName], [ContactName], [PhoneNumber], [FaxNumber], [Address], [GSTregistrationNumber]) VALUES (16, N'S15', N'Supplier 15', N'Supplier Contact 15', N'60256632', N'60045309', N'Address 15', N'GST Registration No. 15')
INSERT [dbo].[Suppliers] ([Id], [SupplierCode], [SupplierName], [ContactName], [PhoneNumber], [FaxNumber], [Address], [GSTregistrationNumber]) VALUES (17, N'S16', N'Supplier 16', N'Supplier Contact 16', N'66045843', N'66618473', N'Address 16', N'GST Registration No. 16')
INSERT [dbo].[Suppliers] ([Id], [SupplierCode], [SupplierName], [ContactName], [PhoneNumber], [FaxNumber], [Address], [GSTregistrationNumber]) VALUES (18, N'S17', N'Supplier 17', N'Supplier Contact 17', N'68126246', N'65867551', N'Address 17', N'GST Registration No. 17')
INSERT [dbo].[Suppliers] ([Id], [SupplierCode], [SupplierName], [ContactName], [PhoneNumber], [FaxNumber], [Address], [GSTregistrationNumber]) VALUES (19, N'S19', N'Supplier 19', N'Supplier Contact 19', N'60597127', N'66272502', N'Address 19', N'GST Registration No. 19')
INSERT [dbo].[Suppliers] ([Id], [SupplierCode], [SupplierName], [ContactName], [PhoneNumber], [FaxNumber], [Address], [GSTregistrationNumber]) VALUES (20, N'S20', N'Supplier 20', N'Supplier Contact 20', N'62876421', N'67456668', N'Address 20', N'GST Registration No. 20')
SET IDENTITY_INSERT [dbo].[Suppliers] OFF
GO
/****** Object:  Index [IX_DelegationForms_DelegateeId]    Script Date: 2020/8/31 7:53:11 ******/
CREATE NONCLUSTERED INDEX [IX_DelegationForms_DelegateeId] ON [dbo].[DelegationForms]
(
	[DelegateeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_DeliveryOrders_PurchaseOrderId]    Script Date: 2020/8/31 7:53:11 ******/
CREATE NONCLUSTERED INDEX [IX_DeliveryOrders_PurchaseOrderId] ON [dbo].[DeliveryOrders]
(
	[PurchaseOrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_DeliveryOrders_ReceivedById]    Script Date: 2020/8/31 7:53:11 ******/
CREATE NONCLUSTERED INDEX [IX_DeliveryOrders_ReceivedById] ON [dbo].[DeliveryOrders]
(
	[ReceivedById] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_DeliveryOrders_SupplierId]    Script Date: 2020/8/31 7:53:11 ******/
CREATE NONCLUSTERED INDEX [IX_DeliveryOrders_SupplierId] ON [dbo].[DeliveryOrders]
(
	[SupplierId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_DeliveryOrderSupplierProducts_DeliveryOrderId]    Script Date: 2020/8/31 7:53:11 ******/
CREATE NONCLUSTERED INDEX [IX_DeliveryOrderSupplierProducts_DeliveryOrderId] ON [dbo].[DeliveryOrderSupplierProducts]
(
	[DeliveryOrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_DeliveryOrderSupplierProducts_PurchaseOrderSupplierProductId]    Script Date: 2020/8/31 7:53:11 ******/
CREATE NONCLUSTERED INDEX [IX_DeliveryOrderSupplierProducts_PurchaseOrderSupplierProductId] ON [dbo].[DeliveryOrderSupplierProducts]
(
	[PurchaseOrderSupplierProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Departments_CollectionPointId]    Script Date: 2020/8/31 7:53:11 ******/
CREATE NONCLUSTERED INDEX [IX_Departments_CollectionPointId] ON [dbo].[Departments]
(
	[CollectionPointId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_DisbursementFormProducts_DisbursementFormId]    Script Date: 2020/8/31 7:53:11 ******/
CREATE NONCLUSTERED INDEX [IX_DisbursementFormProducts_DisbursementFormId] ON [dbo].[DisbursementFormProducts]
(
	[DisbursementFormId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_DisbursementFormProducts_ProductId]    Script Date: 2020/8/31 7:53:11 ******/
CREATE NONCLUSTERED INDEX [IX_DisbursementFormProducts_ProductId] ON [dbo].[DisbursementFormProducts]
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_DisbursementFormRequisitionFormProducts_DisbursementFormId]    Script Date: 2020/8/31 7:53:11 ******/
CREATE NONCLUSTERED INDEX [IX_DisbursementFormRequisitionFormProducts_DisbursementFormId] ON [dbo].[DisbursementFormRequisitionFormProducts]
(
	[DisbursementFormId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_DisbursementFormRequisitionFormProducts_RequisitionFormsProductId]    Script Date: 2020/8/31 7:53:11 ******/
CREATE NONCLUSTERED INDEX [IX_DisbursementFormRequisitionFormProducts_RequisitionFormsProductId] ON [dbo].[DisbursementFormRequisitionFormProducts]
(
	[RequisitionFormsProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_DisbursementFormRequisitionForms_DisbursementFormId]    Script Date: 2020/8/31 7:53:11 ******/
CREATE NONCLUSTERED INDEX [IX_DisbursementFormRequisitionForms_DisbursementFormId] ON [dbo].[DisbursementFormRequisitionForms]
(
	[DisbursementFormId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_DisbursementFormRequisitionForms_RequisitionFormId]    Script Date: 2020/8/31 7:53:11 ******/
CREATE NONCLUSTERED INDEX [IX_DisbursementFormRequisitionForms_RequisitionFormId] ON [dbo].[DisbursementFormRequisitionForms]
(
	[RequisitionFormId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_DisbursementForms_CollectionPointId]    Script Date: 2020/8/31 7:53:11 ******/
CREATE NONCLUSTERED INDEX [IX_DisbursementForms_CollectionPointId] ON [dbo].[DisbursementForms]
(
	[CollectionPointId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_DisbursementForms_DeptRepId]    Script Date: 2020/8/31 7:53:11 ******/
CREATE NONCLUSTERED INDEX [IX_DisbursementForms_DeptRepId] ON [dbo].[DisbursementForms]
(
	[DeptRepId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_DisbursementForms_StoreClerkId]    Script Date: 2020/8/31 7:53:11 ******/
CREATE NONCLUSTERED INDEX [IX_DisbursementForms_StoreClerkId] ON [dbo].[DisbursementForms]
(
	[StoreClerkId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Employees_DepartmentId]    Script Date: 2020/8/31 7:53:11 ******/
CREATE NONCLUSTERED INDEX [IX_Employees_DepartmentId] ON [dbo].[Employees]
(
	[DepartmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Employees_EmployeeTypeId]    Script Date: 2020/8/31 7:53:11 ******/
CREATE NONCLUSTERED INDEX [IX_Employees_EmployeeTypeId] ON [dbo].[Employees]
(
	[EmployeeTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Employees_SupervisedById]    Script Date: 2020/8/31 7:53:11 ******/
CREATE NONCLUSTERED INDEX [IX_Employees_SupervisedById] ON [dbo].[Employees]
(
	[SupervisedById] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Employees_TempDeptHeadTypeId]    Script Date: 2020/8/31 7:53:11 ******/
CREATE NONCLUSTERED INDEX [IX_Employees_TempDeptHeadTypeId] ON [dbo].[Employees]
(
	[TempDeptHeadTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_InventoryTransactions_EmployeeId1]    Script Date: 2020/8/31 7:53:11 ******/
CREATE NONCLUSTERED INDEX [IX_InventoryTransactions_EmployeeId1] ON [dbo].[InventoryTransactions]
(
	[EmployeeId1] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_InventoryTransactions_ITApprovalById]    Script Date: 2020/8/31 7:53:11 ******/
CREATE NONCLUSTERED INDEX [IX_InventoryTransactions_ITApprovalById] ON [dbo].[InventoryTransactions]
(
	[ITApprovalById] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_InventoryTransactions_ITCode]    Script Date: 2020/8/31 7:53:11 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_InventoryTransactions_ITCode] ON [dbo].[InventoryTransactions]
(
	[ITCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_InventoryTransactions_ProductId1]    Script Date: 2020/8/31 7:53:11 ******/
CREATE NONCLUSTERED INDEX [IX_InventoryTransactions_ProductId1] ON [dbo].[InventoryTransactions]
(
	[ProductId1] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Products_ProductCategoryId]    Script Date: 2020/8/31 7:53:11 ******/
CREATE NONCLUSTERED INDEX [IX_Products_ProductCategoryId] ON [dbo].[Products]
(
	[ProductCategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_PurchaseOrders_IssuedById]    Script Date: 2020/8/31 7:53:11 ******/
CREATE NONCLUSTERED INDEX [IX_PurchaseOrders_IssuedById] ON [dbo].[PurchaseOrders]
(
	[IssuedById] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_PurchaseOrders_supplierId]    Script Date: 2020/8/31 7:53:11 ******/
CREATE NONCLUSTERED INDEX [IX_PurchaseOrders_supplierId] ON [dbo].[PurchaseOrders]
(
	[supplierId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_PurchaseOrderSupplierProducts_PurchaseOrderId]    Script Date: 2020/8/31 7:53:11 ******/
CREATE NONCLUSTERED INDEX [IX_PurchaseOrderSupplierProducts_PurchaseOrderId] ON [dbo].[PurchaseOrderSupplierProducts]
(
	[PurchaseOrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_PurchaseOrderSupplierProducts_SupplierProductId]    Script Date: 2020/8/31 7:53:11 ******/
CREATE NONCLUSTERED INDEX [IX_PurchaseOrderSupplierProducts_SupplierProductId] ON [dbo].[PurchaseOrderSupplierProducts]
(
	[SupplierProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_RequisitionForms_EmployeeId]    Script Date: 2020/8/31 7:53:11 ******/
CREATE NONCLUSTERED INDEX [IX_RequisitionForms_EmployeeId] ON [dbo].[RequisitionForms]
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_RequisitionForms_RFApprovalById]    Script Date: 2020/8/31 7:53:11 ******/
CREATE NONCLUSTERED INDEX [IX_RequisitionForms_RFApprovalById] ON [dbo].[RequisitionForms]
(
	[RFApprovalById] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_RequisitionFormsProducts_ProductId]    Script Date: 2020/8/31 7:53:11 ******/
CREATE NONCLUSTERED INDEX [IX_RequisitionFormsProducts_ProductId] ON [dbo].[RequisitionFormsProducts]
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_RequisitionFormsProducts_RequisitionFormId]    Script Date: 2020/8/31 7:53:11 ******/
CREATE NONCLUSTERED INDEX [IX_RequisitionFormsProducts_RequisitionFormId] ON [dbo].[RequisitionFormsProducts]
(
	[RequisitionFormId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_StationeryRetrievalProduct_ProductId]    Script Date: 2020/8/31 7:53:11 ******/
CREATE NONCLUSTERED INDEX [IX_StationeryRetrievalProduct_ProductId] ON [dbo].[StationeryRetrievalProduct]
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_StationeryRetrievalProduct_StationeryRetrievalId]    Script Date: 2020/8/31 7:53:11 ******/
CREATE NONCLUSTERED INDEX [IX_StationeryRetrievalProduct_StationeryRetrievalId] ON [dbo].[StationeryRetrievalProduct]
(
	[StationeryRetrievalId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_StationeryRetrievalRequisitionFormProducts_RFPId]    Script Date: 2020/8/31 7:53:11 ******/
CREATE NONCLUSTERED INDEX [IX_StationeryRetrievalRequisitionFormProducts_RFPId] ON [dbo].[StationeryRetrievalRequisitionFormProducts]
(
	[RFPId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_StationeryRetrievalRequisitionFormProducts_SRId]    Script Date: 2020/8/31 7:53:11 ******/
CREATE NONCLUSTERED INDEX [IX_StationeryRetrievalRequisitionFormProducts_SRId] ON [dbo].[StationeryRetrievalRequisitionFormProducts]
(
	[SRId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_StationeryRetrievalRequisitionForms_RequisitionFormId]    Script Date: 2020/8/31 7:53:11 ******/
CREATE NONCLUSTERED INDEX [IX_StationeryRetrievalRequisitionForms_RequisitionFormId] ON [dbo].[StationeryRetrievalRequisitionForms]
(
	[RequisitionFormId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_StationeryRetrievalRequisitionForms_StationeryRetrievalId]    Script Date: 2020/8/31 7:53:11 ******/
CREATE NONCLUSTERED INDEX [IX_StationeryRetrievalRequisitionForms_StationeryRetrievalId] ON [dbo].[StationeryRetrievalRequisitionForms]
(
	[StationeryRetrievalId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_StationeryRetrievals_StoreClerkId]    Script Date: 2020/8/31 7:53:11 ******/
CREATE NONCLUSTERED INDEX [IX_StationeryRetrievals_StoreClerkId] ON [dbo].[StationeryRetrievals]
(
	[StoreClerkId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_StationeryRetrievals_WarehousePackerId]    Script Date: 2020/8/31 7:53:11 ******/
CREATE NONCLUSTERED INDEX [IX_StationeryRetrievals_WarehousePackerId] ON [dbo].[StationeryRetrievals]
(
	[WarehousePackerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_SupplierProducts_ProductId]    Script Date: 2020/8/31 7:53:11 ******/
CREATE NONCLUSTERED INDEX [IX_SupplierProducts_ProductId] ON [dbo].[SupplierProducts]
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_SupplierProducts_SupplierId]    Script Date: 2020/8/31 7:53:11 ******/
CREATE NONCLUSTERED INDEX [IX_SupplierProducts_SupplierId] ON [dbo].[SupplierProducts]
(
	[SupplierId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[DelegationForms]  WITH CHECK ADD  CONSTRAINT [FK_DelegationForms_Employees_DelegateeId] FOREIGN KEY([DelegateeId])
REFERENCES [dbo].[Employees] ([Id])
GO
ALTER TABLE [dbo].[DelegationForms] CHECK CONSTRAINT [FK_DelegationForms_Employees_DelegateeId]
GO
ALTER TABLE [dbo].[DelegationForms]  WITH CHECK ADD  CONSTRAINT [FK_DelegationForms_Employees_DepartmentHeadId] FOREIGN KEY([DepartmentHeadId])
REFERENCES [dbo].[Employees] ([Id])
GO
ALTER TABLE [dbo].[DelegationForms] CHECK CONSTRAINT [FK_DelegationForms_Employees_DepartmentHeadId]
GO
ALTER TABLE [dbo].[DelegationForms]  WITH CHECK ADD  CONSTRAINT [FK_DelegationForms_EmployeeTypes_DelegatedTypeId] FOREIGN KEY([DelegatedTypeId])
REFERENCES [dbo].[EmployeeTypes] ([Id])
GO
ALTER TABLE [dbo].[DelegationForms] CHECK CONSTRAINT [FK_DelegationForms_EmployeeTypes_DelegatedTypeId]
GO
ALTER TABLE [dbo].[DeliveryOrders]  WITH CHECK ADD  CONSTRAINT [FK_DeliveryOrders_Employees_ReceivedById] FOREIGN KEY([ReceivedById])
REFERENCES [dbo].[Employees] ([Id])
GO
ALTER TABLE [dbo].[DeliveryOrders] CHECK CONSTRAINT [FK_DeliveryOrders_Employees_ReceivedById]
GO
ALTER TABLE [dbo].[DeliveryOrders]  WITH CHECK ADD  CONSTRAINT [FK_DeliveryOrders_PurchaseOrders_PurchaseOrderId] FOREIGN KEY([PurchaseOrderId])
REFERENCES [dbo].[PurchaseOrders] ([Id])
GO
ALTER TABLE [dbo].[DeliveryOrders] CHECK CONSTRAINT [FK_DeliveryOrders_PurchaseOrders_PurchaseOrderId]
GO
ALTER TABLE [dbo].[DeliveryOrders]  WITH CHECK ADD  CONSTRAINT [FK_DeliveryOrders_Suppliers_SupplierId] FOREIGN KEY([SupplierId])
REFERENCES [dbo].[Suppliers] ([Id])
GO
ALTER TABLE [dbo].[DeliveryOrders] CHECK CONSTRAINT [FK_DeliveryOrders_Suppliers_SupplierId]
GO
ALTER TABLE [dbo].[DeliveryOrderSupplierProducts]  WITH CHECK ADD  CONSTRAINT [FK_DeliveryOrderSupplierProducts_DeliveryOrders_DeliveryOrderId] FOREIGN KEY([DeliveryOrderId])
REFERENCES [dbo].[DeliveryOrders] ([Id])
GO
ALTER TABLE [dbo].[DeliveryOrderSupplierProducts] CHECK CONSTRAINT [FK_DeliveryOrderSupplierProducts_DeliveryOrders_DeliveryOrderId]
GO
ALTER TABLE [dbo].[DeliveryOrderSupplierProducts]  WITH CHECK ADD  CONSTRAINT [FK_DeliveryOrderSupplierProducts_PurchaseOrderSupplierProducts_PurchaseOrderSupplierProductId] FOREIGN KEY([PurchaseOrderSupplierProductId])
REFERENCES [dbo].[PurchaseOrderSupplierProducts] ([Id])
GO
ALTER TABLE [dbo].[DeliveryOrderSupplierProducts] CHECK CONSTRAINT [FK_DeliveryOrderSupplierProducts_PurchaseOrderSupplierProducts_PurchaseOrderSupplierProductId]
GO
ALTER TABLE [dbo].[Departments]  WITH CHECK ADD  CONSTRAINT [FK_Departments_CollectionPoints_CollectionPointId] FOREIGN KEY([CollectionPointId])
REFERENCES [dbo].[CollectionPoints] ([Id])
GO
ALTER TABLE [dbo].[Departments] CHECK CONSTRAINT [FK_Departments_CollectionPoints_CollectionPointId]
GO
ALTER TABLE [dbo].[DisbursementFormProducts]  WITH CHECK ADD  CONSTRAINT [FK_DisbursementFormProducts_DisbursementForms_DisbursementFormId] FOREIGN KEY([DisbursementFormId])
REFERENCES [dbo].[DisbursementForms] ([Id])
GO
ALTER TABLE [dbo].[DisbursementFormProducts] CHECK CONSTRAINT [FK_DisbursementFormProducts_DisbursementForms_DisbursementFormId]
GO
ALTER TABLE [dbo].[DisbursementFormProducts]  WITH CHECK ADD  CONSTRAINT [FK_DisbursementFormProducts_Products_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
GO
ALTER TABLE [dbo].[DisbursementFormProducts] CHECK CONSTRAINT [FK_DisbursementFormProducts_Products_ProductId]
GO
ALTER TABLE [dbo].[DisbursementFormRequisitionFormProducts]  WITH CHECK ADD  CONSTRAINT [FK_DisbursementFormRequisitionFormProducts_DisbursementForms_DisbursementFormId] FOREIGN KEY([DisbursementFormId])
REFERENCES [dbo].[DisbursementForms] ([Id])
GO
ALTER TABLE [dbo].[DisbursementFormRequisitionFormProducts] CHECK CONSTRAINT [FK_DisbursementFormRequisitionFormProducts_DisbursementForms_DisbursementFormId]
GO
ALTER TABLE [dbo].[DisbursementFormRequisitionFormProducts]  WITH CHECK ADD  CONSTRAINT [FK_DisbursementFormRequisitionFormProducts_RequisitionFormsProducts_RequisitionFormsProductId] FOREIGN KEY([RequisitionFormsProductId])
REFERENCES [dbo].[RequisitionFormsProducts] ([Id])
GO
ALTER TABLE [dbo].[DisbursementFormRequisitionFormProducts] CHECK CONSTRAINT [FK_DisbursementFormRequisitionFormProducts_RequisitionFormsProducts_RequisitionFormsProductId]
GO
ALTER TABLE [dbo].[DisbursementFormRequisitionForms]  WITH CHECK ADD  CONSTRAINT [FK_DisbursementFormRequisitionForms_DisbursementForms_DisbursementFormId] FOREIGN KEY([DisbursementFormId])
REFERENCES [dbo].[DisbursementForms] ([Id])
GO
ALTER TABLE [dbo].[DisbursementFormRequisitionForms] CHECK CONSTRAINT [FK_DisbursementFormRequisitionForms_DisbursementForms_DisbursementFormId]
GO
ALTER TABLE [dbo].[DisbursementFormRequisitionForms]  WITH CHECK ADD  CONSTRAINT [FK_DisbursementFormRequisitionForms_RequisitionForms_RequisitionFormId] FOREIGN KEY([RequisitionFormId])
REFERENCES [dbo].[RequisitionForms] ([Id])
GO
ALTER TABLE [dbo].[DisbursementFormRequisitionForms] CHECK CONSTRAINT [FK_DisbursementFormRequisitionForms_RequisitionForms_RequisitionFormId]
GO
ALTER TABLE [dbo].[DisbursementForms]  WITH CHECK ADD  CONSTRAINT [FK_DisbursementForms_CollectionPoints_CollectionPointId] FOREIGN KEY([CollectionPointId])
REFERENCES [dbo].[CollectionPoints] ([Id])
GO
ALTER TABLE [dbo].[DisbursementForms] CHECK CONSTRAINT [FK_DisbursementForms_CollectionPoints_CollectionPointId]
GO
ALTER TABLE [dbo].[DisbursementForms]  WITH CHECK ADD  CONSTRAINT [FK_DisbursementForms_Employees_DeptRepId] FOREIGN KEY([DeptRepId])
REFERENCES [dbo].[Employees] ([Id])
GO
ALTER TABLE [dbo].[DisbursementForms] CHECK CONSTRAINT [FK_DisbursementForms_Employees_DeptRepId]
GO
ALTER TABLE [dbo].[DisbursementForms]  WITH CHECK ADD  CONSTRAINT [FK_DisbursementForms_Employees_StoreClerkId] FOREIGN KEY([StoreClerkId])
REFERENCES [dbo].[Employees] ([Id])
GO
ALTER TABLE [dbo].[DisbursementForms] CHECK CONSTRAINT [FK_DisbursementForms_Employees_StoreClerkId]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_Departments_DepartmentId] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Departments] ([Id])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_Departments_DepartmentId]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_Employees_SupervisedById] FOREIGN KEY([SupervisedById])
REFERENCES [dbo].[Employees] ([Id])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_Employees_SupervisedById]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_EmployeeTypes_EmployeeTypeId] FOREIGN KEY([EmployeeTypeId])
REFERENCES [dbo].[EmployeeTypes] ([Id])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_EmployeeTypes_EmployeeTypeId]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_EmployeeTypes_TempDeptHeadTypeId] FOREIGN KEY([TempDeptHeadTypeId])
REFERENCES [dbo].[EmployeeTypes] ([Id])
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_EmployeeTypes_TempDeptHeadTypeId]
GO
ALTER TABLE [dbo].[InventoryTransactions]  WITH CHECK ADD  CONSTRAINT [FK_InventoryTransactions_Employees_EmployeeId1] FOREIGN KEY([EmployeeId1])
REFERENCES [dbo].[Employees] ([Id])
GO
ALTER TABLE [dbo].[InventoryTransactions] CHECK CONSTRAINT [FK_InventoryTransactions_Employees_EmployeeId1]
GO
ALTER TABLE [dbo].[InventoryTransactions]  WITH CHECK ADD  CONSTRAINT [FK_InventoryTransactions_Employees_ITApprovalById] FOREIGN KEY([ITApprovalById])
REFERENCES [dbo].[Employees] ([Id])
GO
ALTER TABLE [dbo].[InventoryTransactions] CHECK CONSTRAINT [FK_InventoryTransactions_Employees_ITApprovalById]
GO
ALTER TABLE [dbo].[InventoryTransactions]  WITH CHECK ADD  CONSTRAINT [FK_InventoryTransactions_Products_ProductId1] FOREIGN KEY([ProductId1])
REFERENCES [dbo].[Products] ([Id])
GO
ALTER TABLE [dbo].[InventoryTransactions] CHECK CONSTRAINT [FK_InventoryTransactions_Products_ProductId1]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_ProductCategories_ProductCategoryId] FOREIGN KEY([ProductCategoryId])
REFERENCES [dbo].[ProductCategories] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_ProductCategories_ProductCategoryId]
GO
ALTER TABLE [dbo].[PurchaseOrders]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseOrders_Employees_IssuedById] FOREIGN KEY([IssuedById])
REFERENCES [dbo].[Employees] ([Id])
GO
ALTER TABLE [dbo].[PurchaseOrders] CHECK CONSTRAINT [FK_PurchaseOrders_Employees_IssuedById]
GO
ALTER TABLE [dbo].[PurchaseOrders]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseOrders_Suppliers_supplierId] FOREIGN KEY([supplierId])
REFERENCES [dbo].[Suppliers] ([Id])
GO
ALTER TABLE [dbo].[PurchaseOrders] CHECK CONSTRAINT [FK_PurchaseOrders_Suppliers_supplierId]
GO
ALTER TABLE [dbo].[PurchaseOrderSupplierProducts]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseOrderSupplierProducts_PurchaseOrders_PurchaseOrderId] FOREIGN KEY([PurchaseOrderId])
REFERENCES [dbo].[PurchaseOrders] ([Id])
GO
ALTER TABLE [dbo].[PurchaseOrderSupplierProducts] CHECK CONSTRAINT [FK_PurchaseOrderSupplierProducts_PurchaseOrders_PurchaseOrderId]
GO
ALTER TABLE [dbo].[PurchaseOrderSupplierProducts]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseOrderSupplierProducts_SupplierProducts_SupplierProductId] FOREIGN KEY([SupplierProductId])
REFERENCES [dbo].[SupplierProducts] ([Id])
GO
ALTER TABLE [dbo].[PurchaseOrderSupplierProducts] CHECK CONSTRAINT [FK_PurchaseOrderSupplierProducts_SupplierProducts_SupplierProductId]
GO
ALTER TABLE [dbo].[RequisitionForms]  WITH CHECK ADD  CONSTRAINT [FK_RequisitionForms_Employees_EmployeeId] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employees] ([Id])
GO
ALTER TABLE [dbo].[RequisitionForms] CHECK CONSTRAINT [FK_RequisitionForms_Employees_EmployeeId]
GO
ALTER TABLE [dbo].[RequisitionForms]  WITH CHECK ADD  CONSTRAINT [FK_RequisitionForms_Employees_RFApprovalById] FOREIGN KEY([RFApprovalById])
REFERENCES [dbo].[Employees] ([Id])
GO
ALTER TABLE [dbo].[RequisitionForms] CHECK CONSTRAINT [FK_RequisitionForms_Employees_RFApprovalById]
GO
ALTER TABLE [dbo].[RequisitionFormsProducts]  WITH CHECK ADD  CONSTRAINT [FK_RequisitionFormsProducts_Products_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RequisitionFormsProducts] CHECK CONSTRAINT [FK_RequisitionFormsProducts_Products_ProductId]
GO
ALTER TABLE [dbo].[RequisitionFormsProducts]  WITH CHECK ADD  CONSTRAINT [FK_RequisitionFormsProducts_RequisitionForms_RequisitionFormId] FOREIGN KEY([RequisitionFormId])
REFERENCES [dbo].[RequisitionForms] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RequisitionFormsProducts] CHECK CONSTRAINT [FK_RequisitionFormsProducts_RequisitionForms_RequisitionFormId]
GO
ALTER TABLE [dbo].[StationeryRetrievalProduct]  WITH CHECK ADD  CONSTRAINT [FK_StationeryRetrievalProduct_Products_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
GO
ALTER TABLE [dbo].[StationeryRetrievalProduct] CHECK CONSTRAINT [FK_StationeryRetrievalProduct_Products_ProductId]
GO
ALTER TABLE [dbo].[StationeryRetrievalProduct]  WITH CHECK ADD  CONSTRAINT [FK_StationeryRetrievalProduct_StationeryRetrievals_StationeryRetrievalId] FOREIGN KEY([StationeryRetrievalId])
REFERENCES [dbo].[StationeryRetrievals] ([Id])
GO
ALTER TABLE [dbo].[StationeryRetrievalProduct] CHECK CONSTRAINT [FK_StationeryRetrievalProduct_StationeryRetrievals_StationeryRetrievalId]
GO
ALTER TABLE [dbo].[StationeryRetrievalRequisitionFormProducts]  WITH CHECK ADD  CONSTRAINT [FK_StationeryRetrievalRequisitionFormProducts_RequisitionFormsProducts_RFPId] FOREIGN KEY([RFPId])
REFERENCES [dbo].[RequisitionFormsProducts] ([Id])
GO
ALTER TABLE [dbo].[StationeryRetrievalRequisitionFormProducts] CHECK CONSTRAINT [FK_StationeryRetrievalRequisitionFormProducts_RequisitionFormsProducts_RFPId]
GO
ALTER TABLE [dbo].[StationeryRetrievalRequisitionFormProducts]  WITH CHECK ADD  CONSTRAINT [FK_StationeryRetrievalRequisitionFormProducts_StationeryRetrievals_SRId] FOREIGN KEY([SRId])
REFERENCES [dbo].[StationeryRetrievals] ([Id])
GO
ALTER TABLE [dbo].[StationeryRetrievalRequisitionFormProducts] CHECK CONSTRAINT [FK_StationeryRetrievalRequisitionFormProducts_StationeryRetrievals_SRId]
GO
ALTER TABLE [dbo].[StationeryRetrievalRequisitionForms]  WITH CHECK ADD  CONSTRAINT [FK_StationeryRetrievalRequisitionForms_RequisitionForms_RequisitionFormId] FOREIGN KEY([RequisitionFormId])
REFERENCES [dbo].[RequisitionForms] ([Id])
GO
ALTER TABLE [dbo].[StationeryRetrievalRequisitionForms] CHECK CONSTRAINT [FK_StationeryRetrievalRequisitionForms_RequisitionForms_RequisitionFormId]
GO
ALTER TABLE [dbo].[StationeryRetrievalRequisitionForms]  WITH CHECK ADD  CONSTRAINT [FK_StationeryRetrievalRequisitionForms_StationeryRetrievals_StationeryRetrievalId] FOREIGN KEY([StationeryRetrievalId])
REFERENCES [dbo].[StationeryRetrievals] ([Id])
GO
ALTER TABLE [dbo].[StationeryRetrievalRequisitionForms] CHECK CONSTRAINT [FK_StationeryRetrievalRequisitionForms_StationeryRetrievals_StationeryRetrievalId]
GO
ALTER TABLE [dbo].[StationeryRetrievals]  WITH CHECK ADD  CONSTRAINT [FK_StationeryRetrievals_Employees_StoreClerkId] FOREIGN KEY([StoreClerkId])
REFERENCES [dbo].[Employees] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[StationeryRetrievals] CHECK CONSTRAINT [FK_StationeryRetrievals_Employees_StoreClerkId]
GO
ALTER TABLE [dbo].[StationeryRetrievals]  WITH CHECK ADD  CONSTRAINT [FK_StationeryRetrievals_Employees_WarehousePackerId] FOREIGN KEY([WarehousePackerId])
REFERENCES [dbo].[Employees] ([Id])
GO
ALTER TABLE [dbo].[StationeryRetrievals] CHECK CONSTRAINT [FK_StationeryRetrievals_Employees_WarehousePackerId]
GO
ALTER TABLE [dbo].[SupplierProducts]  WITH CHECK ADD  CONSTRAINT [FK_SupplierProducts_Products_ProductId] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([Id])
GO
ALTER TABLE [dbo].[SupplierProducts] CHECK CONSTRAINT [FK_SupplierProducts_Products_ProductId]
GO
ALTER TABLE [dbo].[SupplierProducts]  WITH CHECK ADD  CONSTRAINT [FK_SupplierProducts_Suppliers_SupplierId] FOREIGN KEY([SupplierId])
REFERENCES [dbo].[Suppliers] ([Id])
GO
ALTER TABLE [dbo].[SupplierProducts] CHECK CONSTRAINT [FK_SupplierProducts_Suppliers_SupplierId]
GO
USE [master]
GO
ALTER DATABASE [InventoryManagementSystem3] SET  READ_WRITE 
GO
