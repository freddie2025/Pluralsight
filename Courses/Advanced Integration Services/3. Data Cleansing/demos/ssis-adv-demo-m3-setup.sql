USE [master]
GO

IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'AdventureWorksDW_stage_demo')
DROP DATABASE [AdventureWorksDW_stage_demo]

CREATE DATABASE [AdventureWorksDW_stage_demo]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'AdventureWorksDW_stage_demo', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\AdventureWorksDW_stage_demo.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'AdventureWorksDW_stage_demo_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\AdventureWorksDW_stage_demo_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO

ALTER DATABASE [AdventureWorksDW_stage_demo] SET COMPATIBILITY_LEVEL = 110
GO


ALTER DATABASE [AdventureWorksDW_stage_demo] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [AdventureWorksDW_stage_demo] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [AdventureWorksDW_stage_demo] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [AdventureWorksDW_stage_demo] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [AdventureWorksDW_stage_demo] SET ARITHABORT OFF 
GO

ALTER DATABASE [AdventureWorksDW_stage_demo] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [AdventureWorksDW_stage_demo] SET AUTO_CREATE_STATISTICS ON 
GO

ALTER DATABASE [AdventureWorksDW_stage_demo] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [AdventureWorksDW_stage_demo] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [AdventureWorksDW_stage_demo] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [AdventureWorksDW_stage_demo] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [AdventureWorksDW_stage_demo] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [AdventureWorksDW_stage_demo] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [AdventureWorksDW_stage_demo] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [AdventureWorksDW_stage_demo] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [AdventureWorksDW_stage_demo] SET  DISABLE_BROKER 
GO

ALTER DATABASE [AdventureWorksDW_stage_demo] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [AdventureWorksDW_stage_demo] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [AdventureWorksDW_stage_demo] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [AdventureWorksDW_stage_demo] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [AdventureWorksDW_stage_demo] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [AdventureWorksDW_stage_demo] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [AdventureWorksDW_stage_demo] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [AdventureWorksDW_stage_demo] SET RECOVERY FULL 
GO

ALTER DATABASE [AdventureWorksDW_stage_demo] SET  MULTI_USER 
GO

ALTER DATABASE [AdventureWorksDW_stage_demo] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [AdventureWorksDW_stage_demo] SET DB_CHAINING OFF 
GO

ALTER DATABASE [AdventureWorksDW_stage_demo] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [AdventureWorksDW_stage_demo] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO

ALTER DATABASE [AdventureWorksDW_stage_demo] SET  READ_WRITE 
GO

use [AdventureWorksDW_stage_demo]
go



SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[stageCountry](
	[Name] [nvarchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[CountryRegionCode] [nvarchar](3) NULL
) ON [PRIMARY]

GO
USE [AdventureWorksDW_stage_demo]
GO


CREATE TABLE [dbo].[stageResellerSalesNoMatch](
	[TerritoryID] [int] NULL,
	[OrderDate] [date] NULL,
	[DueDate] [date] NULL,
	[ShipDate] [date] NULL,
	[SalesOrderNumber] [nvarchar](25) NULL,
	[PurchaseOrderNumber] [nvarchar](25) NULL,
	[AccountNumber] [nvarchar](15) NULL,
	[CustomerID] [int] NULL,
	[SalesPersonID] [int] NULL,
	[SubTotal] [money] NULL,
	[TaxAmt] [money] NULL,
	[Freight] [money] NULL,
	[TotalDue] [money] NULL,
	[RevisionNumber] [tinyint] NULL,
	[SalesOrderDetailID] [int] NULL,
	[CarrierTrackingNumber] [nvarchar](25) NULL,
	[OrderQty] [smallint] NULL,
	[ProductID] [nvarchar](25) NULL,
	[UnitPrice] [money] NULL,
	[UnitPriceDiscount] [money] NULL,
	[SalesOrderID] [int] NULL,
	[StoreID] [int] NULL,
	[AuditKey] [int] NULL,
	[StoreIDstr] [nvarchar](15) NULL,
	[ProductKey] [int] NULL,
	[StandardCost] [money] NULL,
	[SalesTerritoryKey] [int] NULL,
	[OrderDateKey] [int] NULL,
	[DueDateKey] [int] NULL,
	[ShipDateKey] [int] NULL,
	[EmployeeKey] [int] NULL,
	[ResellerKey] [int] NULL
) ON [PRIMARY]

GO



CREATE TABLE [dbo].[stageSalesTerritoryError](
	[TerritoryID] [int] NULL,
	[Name] [nvarchar](1000) NULL,
	[CountryRegionCode] [nvarchar](1000) NULL,
	[Group] [nvarchar](1000) NULL,
	[SalesYTD] [int] NULL,
	[SalesLastYear] [int] NULL,
	[CostYTD] [int] NULL,
	[CostLastYear] [int] NULL,
	[rowguid] [uniqueidentifier] NULL,
	[ModifiedDate] [int] NULL,
	[ErrorCode] [int] NULL,
	[ErrorColumn] [int] NULL
) ON [PRIMARY]

GO 
CREATE TABLE [stageReseller_FuzzyGrouping] (
    [_key_in] int,
    [_key_out] int,
    [_score] real,
    [AnnualSales] money,
    [AnnualRevenue] money,
    [BankName] nvarchar(50),
    [BusinessType] nvarchar(5),
    [YearOpened] int,
    [NumberEmployees] int,
    [FirstOrderYear] int,
    [LastOrderYear] int,
    [ResellerKey] int,
    [ResellerAlternateKey] nvarchar(15),
    [ResellerName] nvarchar(50),
    [ProductLine] nvarchar(50),
    [AddressLine1] nvarchar(60),
    [AddressLine2] nvarchar(60),
    [City] nvarchar(30),
    [StateProvinceCode] nvarchar(3),
    [PostalCode] nvarchar(15),
    [Current] bit,
    [Inferred] bit,
    [ResellerName_clean] nvarchar(50),
    [AddressLine1_clean] nvarchar(60),
    [City_clean] nvarchar(30),
    [StateProvinceCode_clean] nvarchar(3),
    [PostalCode_clean] nvarchar(15),
    [_Similarity_ResellerName] real,
    [_Similarity_AddressLine1] real,
    [_Similarity_City] real,
    [_Similarity_StateProvinceCode] real,
    [_Similarity_PostalCode] real
)
CREATE TABLE [dbo].[stageReseller_Fuzzy](
	[AnnualSales] [money] NULL,
	[AnnualRevenue] [money] NULL,
	[BankName] [nvarchar](50) NULL,
	[BusinessType] [nvarchar](5) NULL,
	[YearOpened] [int] NULL,
	[NumberEmployees] [int] NULL,
	[FirstOrderYear] [int] NULL,
	[LastOrderYear] [int] NULL,
	[ResellerAlternateKey] [nvarchar](15) NULL,
	[ResellerName] [nvarchar](50) NULL,
	[ProductLine] [nvarchar](50) NULL,
	[AddressLine1] [nvarchar](60) NULL,
	[AddressLine2] [nvarchar](60) NULL,
	[City] [nvarchar](30) NULL,
	[StateProvinceCode] [nvarchar](3) NULL,
	[PostalCode] [nvarchar](15) NULL,
	[Current] [bit] NULL,
	[Inferred] [bit] NULL,
	[ResellerKey] [int] NULL,
	[ResellerKeyFinal] [int] NULL,
	[_Similarity] [real] NULL,
	[_Confidence] [real] NULL,
	[_Similarity_ResellerName] [real] NULL,
	[_Similarity_AddressLine1] [real] NULL,
	[_Similarity_AddressLine2] [real] NULL,
	[_Similarity_City] [real] NULL,
	[_Similarity_StateProvinceCode] [real] NULL,
	[_Similarity_PostalCode] [real] NULL
) ON [PRIMARY]

GO


CREATE TABLE [dbo].[stageReseller_Invalid](
	[Name] [nvarchar](50) NULL,
	[BusinessEntityID] [int] NULL,
	[AnnualSales] [money] NULL,
	[AnnualRevenue] [money] NULL,
	[BankName] [nvarchar](50) NULL,
	[BusinessType] [nvarchar](5) NULL,
	[YearOpened] [int] NULL,
	[Specialty] [nvarchar](50) NULL,
	[SquareFeet] [int] NULL,
	[Brands] [nvarchar](30) NULL,
	[Internet] [nvarchar](30) NULL,
	[NumberEmployees] [int] NULL,
	[AddressLine1] [nvarchar](60) NULL,
	[AddressLine2] [nvarchar](60) NULL,
	[City] [nvarchar](30) NULL,
	[StateProvinceCode] [nvarchar](3) NULL,
	[PostalCode] [nvarchar](15) NULL,
	[FirstOrderYear] [int] NULL,
	[LastOrderYear] [int] NULL,
	[AuditKey] [int] NULL,
	[ResellerID] [nvarchar](15) NULL,
	[BusinessTypestr] [varchar](20) NULL,
	[ErrorMsg] [nvarchar](22) NULL
) ON [PRIMARY]

GO





CREATE TABLE [stageAddress] (
    [BusinessEntityID] int,
    [AddressID] int,
    [AddressLine1] nvarchar(60),
    [AddressLine2] nvarchar(60),
    [City] nvarchar(30),
    [StateProvinceCode] nvarchar(3),
    [PostalCode] nvarchar(15)
)
CREATE TABLE [dbo].[stageProduct](
	[ProductID] [int] NULL,
	[Name] [nvarchar](50) NULL,
	[ProductNumber] [nvarchar](25) NULL,
	[MakeFlag] [bit] NULL,
	[FinishedGoodsFlag] [bit] NULL,
	[Color] [nvarchar](15) NULL,
	[SafetyStockLevel] [smallint] NULL,
	[ReorderPoint] [smallint] NULL,
	[StandardCost] [money] NULL,
	[ListPrice] [money] NULL,
	[Size] [nvarchar](5) NULL,
	[SizeUnitMeasureCode] [nvarchar](3) NULL,
	[WeightUnitMeasureCode] [nvarchar](3) NULL,
	[Weight] [numeric](8, 2) NULL,
	[DaysToManufacture] [int] NULL,
	[ProductLine] [nvarchar](2) NULL,
	[Class] [nvarchar](2) NULL,
	[Style] [nvarchar](2) NULL,
	[ProductSubcategoryID] [int] NULL,
	[ProductModelID] [int] NULL,
	[SellStartDate] [datetime] NULL,
	[SellEndDate] [datetime] NULL,
	[DiscontinuedDate] [datetime] NULL,
	[rowguid] [uniqueidentifier] NULL,
	[ModifiedDate] [datetime] NULL
) ON [PRIMARY]

GO

CREATE TABLE [stageReseller] (
    [BusinessEntityID] int,
    [Name] nvarchar(50),
    [AnnualSales] money,
    [AnnualRevenue] money,
    [BankName] nvarchar(50),
    [BusinessType] nvarchar(5),
    [YearOpened] int,
    [Specialty] nvarchar(50),
    [SquareFeet] int,
    [Brands] nvarchar(30),
    [Internet] nvarchar(30),
    [NumberEmployees] int,
    [FirstOrderYear] int,
    [LastOrderYear] int,
    [StoreID] int
)
CREATE TABLE [dbo].[stageProductCategory](
	[Name] [nvarchar](50) NULL,
	[rowguid] [uniqueidentifier] NULL,
	[ModifiedDate] [datetime] NULL,
	[ProductCategoryID] [int] NULL
) ON [PRIMARY]

GO
CREATE TABLE [dbo].[stageProductSubcategory](
	[Name] [nvarchar](50) NULL,
	[ProductSubcategoryID] [int] NULL,
	[rowguid] [uniqueidentifier] NULL,
	[ModifiedDate] [datetime] NULL,
	[ProductCategoryID] [int] NULL
) ON [PRIMARY]

GO
CREATE TABLE [dbo].[stageSalesDetail](
	[rowguid] [uniqueidentifier] NULL,
	[ModifiedDate] [datetime] NULL,
	[SalesOrderID] [int] NULL,
	[SalesOrderDetailID] [int] NULL,
	[CarrierTrackingNumber] [nvarchar](25) NULL,
	[OrderQty] [smallint] NULL,
	[ProductID] [int] NULL,
	[SpecialOfferID] [int] NULL,
	[UnitPrice] [money] NULL,
	[UnitPriceDiscount] [money] NULL,
	[LineTotal] [numeric](38, 6) NULL
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[stageSalesHeader](
	[rowguid] [uniqueidentifier] NULL,
	[ModifiedDate] [datetime] NULL,
	[TerritoryID] [int] NULL,
	[SalesOrderID] [int] NULL,
	[RevisionNumber] [tinyint] NULL,
	[OrderDate] [datetime] NULL,
	[DueDate] [datetime] NULL,
	[ShipDate] [datetime] NULL,
	[Status] [tinyint] NULL,
	[OnlineOrderFlag] [bit] NULL,
	[SalesOrderNumber] [nvarchar](25) NULL,
	[PurchaseOrderNumber] [nvarchar](25) NULL,
	[AccountNumber] [nvarchar](15) NULL,
	[CustomerID] [int] NULL,
	[SalesPersonID] [int] NULL,
	[BillToAddressID] [int] NULL,
	[ShipToAddressID] [int] NULL,
	[ShipMethodID] [int] NULL,
	[CreditCardID] [int] NULL,
	[CreditCardApprovalCode] [varchar](15) NULL,
	[CurrencyRateID] [int] NULL,
	[SubTotal] [money] NULL,
	[TaxAmt] [money] NULL,
	[Freight] [money] NULL,
	[TotalDue] [money] NULL,
	[Comment] [nvarchar](128) NULL,
	[StoreID] int NULL
) ON [PRIMARY]

GO
CREATE TABLE [dbo].[stageSalesTerritory](
	[Name] [nvarchar](50) NULL,
	[rowguid] [uniqueidentifier] NULL,
	[ModifiedDate] [datetime] NULL,
	[TerritoryID] [int] NULL,
	[CountryRegionCode] [nvarchar](3) NULL,
	[Group] [nvarchar](50) NULL,
	[SalesYTD] [money] NULL,
	[SalesLastYear] [money] NULL,
	[CostYTD] [money] NULL,
	[CostLastYear] [money] NULL
) ON [PRIMARY]

GO

CREATE TABLE [stageEmployee] (
    [BusinessEntityID] int,
    [NationalIDNumber] nvarchar(15),
    [ManagerNationalIDNumber] nvarchar(15),
    [FirstName] nvarchar(50),
    [MiddleName] nvarchar(50),
    [LastName] nvarchar(50),
    [JobTitle] nvarchar(50),
    [PhoneNumber] nvarchar(25),
    [EmailAddress] nvarchar(50),
    [City] nvarchar(30),
    [StateProvinceName] nvarchar(50),
    [CountryRegionName] nvarchar(50),
    [TerritoryName] nvarchar(50),
    [TerritoryGroup] nvarchar(50),
    [BirthDate] date,
    [MaritalStatus] nvarchar(1),
    [Gender] nvarchar(1),
    [HireDate] date,
    [SalariedFlag] bit,
    [VacationHours] smallint,
    [SickLeaveHours] smallint,
    [CurrentFlag] bit,
    [DepartmentName] nvarchar(50),
    [PayFrequency] tinyint,
    [Rate] money
)
use [master]
go

IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'AdventureWorksDW_demo')
DROP DATABASE [AdventureWorksDW_demo]

CREATE DATABASE [AdventureWorksDW_demo]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'AdventureWorksDW_demo', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\AdventureWorksDW_demo.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'AdventureWorksDW_demo_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\AdventureWorksDW_demo_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO

ALTER DATABASE [AdventureWorksDW_demo] SET COMPATIBILITY_LEVEL = 110
GO


ALTER DATABASE [AdventureWorksDW_demo] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [AdventureWorksDW_demo] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [AdventureWorksDW_demo] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [AdventureWorksDW_demo] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [AdventureWorksDW_demo] SET ARITHABORT OFF 
GO

ALTER DATABASE [AdventureWorksDW_demo] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [AdventureWorksDW_demo] SET AUTO_CREATE_STATISTICS ON 
GO

ALTER DATABASE [AdventureWorksDW_demo] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [AdventureWorksDW_demo] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [AdventureWorksDW_demo] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [AdventureWorksDW_demo] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [AdventureWorksDW_demo] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [AdventureWorksDW_demo] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [AdventureWorksDW_demo] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [AdventureWorksDW_demo] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [AdventureWorksDW_demo] SET  DISABLE_BROKER 
GO

ALTER DATABASE [AdventureWorksDW_demo] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [AdventureWorksDW_demo] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [AdventureWorksDW_demo] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [AdventureWorksDW_demo] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [AdventureWorksDW_demo] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [AdventureWorksDW_demo] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [AdventureWorksDW_demo] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [AdventureWorksDW_demo] SET RECOVERY FULL 
GO

ALTER DATABASE [AdventureWorksDW_demo] SET  MULTI_USER 
GO

ALTER DATABASE [AdventureWorksDW_demo] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [AdventureWorksDW_demo] SET DB_CHAINING OFF 
GO

ALTER DATABASE [AdventureWorksDW_demo] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [AdventureWorksDW_demo] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO

ALTER DATABASE [AdventureWorksDW_demo] SET  READ_WRITE 
GO

use [AdventureWorksDW_demo]
go

create schema dw
go


SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dw].[DimDate](
	[DateKey] [int] NOT NULL,
	[FullDateAlternateKey] [date] NOT NULL,
	[DayNumberOfWeek] [tinyint] NOT NULL,
	[EnglishDayNameOfWeek] [nvarchar](10) NOT NULL,
	[DayNumberOfMonth] [tinyint] NOT NULL,
	[DayNumberOfYear] [smallint] NOT NULL,
	[WeekNumberOfYear] [tinyint] NOT NULL,
	[EnglishMonthName] [nvarchar](10) NOT NULL,
	[MonthNumberOfYear] [tinyint] NOT NULL,
	[CalendarQuarter] [tinyint] NOT NULL,
	[CalendarYear] [smallint] NOT NULL,
	[CalendarSemester] [tinyint] NOT NULL,
	[AuditKey] int NOT NULL,
	
 CONSTRAINT [PK_DimDate_DateKey] PRIMARY KEY CLUSTERED 
(
	[DateKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_DimDate_FullDateAlternateKey] UNIQUE NONCLUSTERED 
(
	[FullDateAlternateKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
CREATE TABLE [dw].[DimAudit](
	[AuditKey] [int] IDENTITY(1,1) NOT NULL,
	[ParentAuditKey] [int] NOT NULL,
	[TableName] [varchar](50) NOT NULL,
	[PkgName] [varchar](50) NOT NULL,
	[PkgGUID] [uniqueidentifier] NULL,
	[PkgVersionGUID] [uniqueidentifier] NULL,
	[PkgVersionMajor] [smallint] NULL,
	[PkgVersionMinor] [smallint] NULL,
	[ExecStartDT] [datetime] NOT NULL,
	[ExecStopDT] [datetime] NULL,
	[ExecutionInstanceGUID] [uniqueidentifier] NULL,
	[ExtractRowCnt] [bigint] NULL,
	[InsertRowCnt] [bigint] NULL,
	[UpdateRowCnt] [bigint] NULL,
	[ErrorRowCnt] [bigint] NULL,
	[TableInitialRowCnt] [bigint] NULL,
	[TableFinalRowCnt] [bigint] NULL,
	[TableMaxDateTime] [datetime] NULL,
	[SuccessfulProcessingInd] [char](1) NOT NULL,
 CONSTRAINT [PK_dbo.DimAudit] PRIMARY KEY CLUSTERED 
(
	[AuditKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
SET IDENTITY_INSERT [dw].[DimAudit] ON 

INSERT [dw].[DimAudit] ([AuditKey], [ParentAuditKey], [TableName], [PkgName], [PkgGUID], [PkgVersionGUID], [PkgVersionMajor], [PkgVersionMinor], [ExecStartDT], [ExecStopDT], [ExecutionInstanceGUID], [ExtractRowCnt], [InsertRowCnt], [UpdateRowCnt], [ErrorRowCnt], [TableInitialRowCnt], [TableFinalRowCnt], [TableMaxDateTime], [SuccessfulProcessingInd]) VALUES (-1, -1, N'Not Applicable', N'Initial Load', NULL, NULL, 0, 0, getdate(), getdate(), NULL, 0, 0, 0, 0, 0, 0, getdate(), N' ')
SET IDENTITY_INSERT [dw].[DimAudit] OFF
ALTER TABLE [dw].[DimAudit] ADD  DEFAULT ('Unknown') FOR [TableName]
GO
ALTER TABLE [dw].[DimAudit] ADD  DEFAULT ('Unknown') FOR [PkgName]
GO
ALTER TABLE [dw].[DimAudit] ADD  DEFAULT (getdate()) FOR [ExecStartDT]
GO
ALTER TABLE [dw].[DimAudit] ADD  DEFAULT ('N') FOR [SuccessfulProcessingInd]
GO

ALTER TABLE [dw].[DimAudit]  WITH CHECK ADD  CONSTRAINT [FK_dbo_DimAudit_ParentAuditKey] FOREIGN KEY([ParentAuditKey])
REFERENCES [dw].[DimAudit] ([AuditKey])
GO
ALTER TABLE [dw].[DimAudit] CHECK CONSTRAINT [FK_dbo_DimAudit_ParentAuditKey]
GO

CREATE TABLE [dw].[DimProduct](
	[ProductKey] [int] IDENTITY(1,1) NOT NULL,
	[ProductAlternateKey] [nvarchar](25) NULL,
	[ProductSubcategoryKey] [int] NULL,
	[WeightUnitMeasure] [nvarchar](50) NULL,
	[SizeUnitMeasure] [nvarchar](50) NULL,
	[EnglishProductName] [nvarchar](50) NOT NULL,
	
	[StandardCost] [money] NULL,
	[FinishedGoodsFlag] [bit] NOT NULL,
	[Color] [nvarchar](15) NOT NULL,
	[SafetyStockLevel] [smallint] NULL,
	[ReorderPoint] [smallint] NULL,
	[ListPrice] [money] NULL,
	[Size] [nvarchar](50) NULL,
	[SizeRange] [nvarchar](50) NULL,
	[Weight] [float] NULL,
	[DaysToManufacture] [int] NULL,
	[ProductLine] [nchar](2) NULL,
	[DealerPrice] [money] NULL,
	[Class] [nchar](2) NULL,
	[Style] [nchar](2) NULL,
	[ModelName] [nvarchar](50) NULL,
	[EnglishDescription] [nvarchar](400) NULL,
	
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[Status] [nvarchar](7) NULL,
	[AuditKey] int NOT NULL,
 CONSTRAINT [PK_DimProduct_ProductKey] PRIMARY KEY CLUSTERED 
(
	[ProductKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_DimProduct_ProductAlternateKey_StartDate] UNIQUE NONCLUSTERED 
(
	[ProductAlternateKey] ASC,
	[StartDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) 

GO

CREATE TABLE [dw].[DimProductCategory](
	[ProductCategoryKey] [int] IDENTITY(1,1) NOT NULL,
	[ProductCategoryAlternateKey] [int] NULL,
	[EnglishProductCategoryName] [nvarchar](50) NOT NULL,
	[AuditKey] int NOT NULL,
 CONSTRAINT [PK_DimProductCategory_ProductCategoryKey] PRIMARY KEY CLUSTERED 
(
	[ProductCategoryKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_DimProductCategory_ProductCategoryAlternateKey] UNIQUE NONCLUSTERED 
(
	[ProductCategoryAlternateKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


CREATE TABLE [dw].[DimProductSubcategory](
	[ProductSubcategoryKey] [int] IDENTITY(1,1) NOT NULL,
	[ProductSubcategoryAlternateKey] [int] NULL,
	[EnglishProductSubcategoryName] [nvarchar](50) NOT NULL,
	
	[ProductCategoryKey] [int] NULL,
	[AuditKey] int NOT NULL,
 CONSTRAINT [PK_DimProductSubcategory_ProductSubcategoryKey] PRIMARY KEY CLUSTERED 
(
	[ProductSubcategoryKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_DimProductSubcategory_ProductSubcategoryAlternateKey] UNIQUE NONCLUSTERED 
(
	[ProductSubcategoryAlternateKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dw].[DimSalesTerritory](
	[SalesTerritoryKey] [int] IDENTITY(1,1) NOT NULL,
	[SalesTerritoryAlternateKey] [int] NULL,
	[SalesTerritoryRegion] [nvarchar](50) NOT NULL,
	[SalesTerritoryCountry] [nvarchar](50) NOT NULL,
	[SalesTerritoryGroup] [nvarchar](50) NULL,
	[AuditKey] int NOT NULL,
 CONSTRAINT [PK_DimSalesTerritory_SalesTerritoryKey] PRIMARY KEY CLUSTERED 
(
	[SalesTerritoryKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_DimSalesTerritory_SalesTerritoryAlternateKey] UNIQUE NONCLUSTERED 
(
	[SalesTerritoryAlternateKey] ASC
))

GO



CREATE TABLE [dw].[DimReseller](
	[ResellerKey] [int] IDENTITY(1,1) NOT FOR REPLICATION NOT NULL,

	[ResellerAlternateKey] [nvarchar](15) NULL,

	[BusinessType] [varchar](20) NOT NULL,
	[ResellerName] [nvarchar](50) NOT NULL,
	[NumberEmployees] [int] NULL,
	
	[FirstOrderYear] [int] NULL,
	[LastOrderYear] [int] NULL,
	[ProductLine] [nvarchar](50) NULL,
	[AddressLine1] [nvarchar](60) NULL,
	[AddressLine2] [nvarchar](60) NULL,
	[City] [nvarchar](30) NULL,
    [StateProvinceCode] [nchar](3) NULL,
    [PostalCode] [nvarchar](15) NULL,
	[AnnualSales] [money] NULL,
	[BankName] [nvarchar](50) NULL,

	[AnnualRevenue] [money] NULL,
	[YearOpened] [int] NULL,
	[Current] [bit], 
	[Inferred] [bit],
 CONSTRAINT [PK_DimReseller_ResellerKey] PRIMARY KEY CLUSTERED 
(
	[ResellerKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_DimReseller_ResellerAlternateKey] UNIQUE NONCLUSTERED 
(
	[ResellerAlternateKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO





CREATE TABLE [dw].[DimEmployee](
	[EmployeeKey] [int] IDENTITY(1,1) NOT NULL,
	[ParentEmployeeKey] [int] NULL,
	[EmployeeNationalIDAlternateKey] [nvarchar](15) NULL,
	[ParentEmployeeNationalIDAlternateKey] [nvarchar](15) NULL,
	[SalesTerritoryKey] [int] NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[MiddleName] [nvarchar](50) NULL,

	[Title] [nvarchar](50) NULL,
	[HireDate] [date] NULL,
	[BirthDate] [date] NULL,
	
	[EmailAddress] [nvarchar](50) NULL,
	[Phone] [nvarchar](25) NULL,
	[MaritalStatus] [nchar](1) NULL,
	[SalesPersonID] [int] NULL,
	[SalariedFlag] [bit] NULL,
	[Gender] [nchar](1) NULL,
	[PayFrequency] [tinyint] NULL,
	[BaseRate] [money] NULL,
	[VacationHours] [smallint] NULL,
	[SickLeaveHours] [smallint] NULL,
	[CurrentFlag] [bit] NOT NULL,
	[SalesPersonFlag] [bit] NOT NULL,
	[DepartmentName] [nvarchar](50) NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[Status] [nvarchar](50) NULL,

 CONSTRAINT [PK_DimEmployee_EmployeeKey] PRIMARY KEY CLUSTERED 
(
	[EmployeeKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] 

GO



ALTER TABLE [dw].[DimEmployee] ADD  CONSTRAINT [DF_DimEmployee_CurrentFlag]  DEFAULT ((1)) FOR [CurrentFlag]
GO

ALTER TABLE [dw].[DimEmployee] ADD  CONSTRAINT [DF_DimEmployee_Status]  DEFAULT (N'Current') FOR [Status]
GO

ALTER TABLE [dw].[DimEmployee]  WITH CHECK ADD  CONSTRAINT [FK_DimEmployee_DimEmployee] FOREIGN KEY([ParentEmployeeKey])
REFERENCES [dw].[DimEmployee] ([EmployeeKey])
GO

ALTER TABLE [dw].[DimEmployee] CHECK CONSTRAINT [FK_DimEmployee_DimEmployee]
GO

ALTER TABLE [dw].[DimEmployee]  WITH CHECK ADD  CONSTRAINT [FK_DimEmployee_DimSalesTerritory] FOREIGN KEY([SalesTerritoryKey])
REFERENCES [dw].[DimSalesTerritory] ([SalesTerritoryKey])
GO

ALTER TABLE [dw].[DimEmployee] CHECK CONSTRAINT [FK_DimEmployee_DimSalesTerritory]
GO



CREATE TABLE [dw].[FactResellerSales](
	[ProductKey] [int] NOT NULL,
	[OrderDateKey] [int] NOT NULL,
	[DueDateKey] [int] NOT NULL,
	[ShipDateKey] [int] NOT NULL,
	[EmployeeKey] [int] NOT NULL,
	[SalesTerritoryKey] [int] NOT NULL,
	[ResellerKey] [int] NOT NULL,
	[SalesOrderNumber] [nvarchar](25) NOT NULL,
	[SalesOrderLineNumber] [tinyint] NOT NULL,
	[RevisionNumber] [tinyint] NULL,
	[OrderQuantity] [smallint] NULL,
	[UnitPrice] [money] NULL,
	[ExtendedAmount] [money] NULL,
	[UnitPriceDiscountPct] [float] NULL,
	
	[ProductStandardCost] [money] NULL,
	
	[SalesAmount] [money] NULL,
	[TaxAmt] [money] NULL,
	[Freight] [money] NULL,
	[CarrierTrackingNumber] [nvarchar](25) NULL,
	[CustomerPONumber] [nvarchar](25) NULL,
	
	[AuditKey] int NOT NULL,
 CONSTRAINT [PK_FactResellerSales_SalesOrderNumber_SalesOrderLineNumber] PRIMARY KEY CLUSTERED 
(
	[SalesOrderNumber] ASC,
	[SalesOrderLineNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dw].[FactResellerSales]  WITH CHECK ADD  CONSTRAINT [FK_FactResellerSales_DimReseller] FOREIGN KEY([ResellerKey])
REFERENCES [dw].[DimReseller] ([ResellerKey])
GO

ALTER TABLE [dw].[FactResellerSales] CHECK CONSTRAINT [FK_FactResellerSales_DimReseller]
GO
ALTER TABLE [dw].[FactResellerSales]  WITH CHECK ADD  CONSTRAINT [FK_FactResellerSales_DimDate] FOREIGN KEY([OrderDateKey])
REFERENCES [dw].[DimDate] ([DateKey])
GO

ALTER TABLE [dw].[FactResellerSales] CHECK CONSTRAINT [FK_FactResellerSales_DimDate]
GO

ALTER TABLE [dw].[FactResellerSales]  WITH CHECK ADD  CONSTRAINT [FK_FactResellerSales_DimDate1] FOREIGN KEY([DueDateKey])
REFERENCES [dw].[DimDate] ([DateKey])
GO

ALTER TABLE [dw].[FactResellerSales] CHECK CONSTRAINT [FK_FactResellerSales_DimDate1]
GO

ALTER TABLE [dw].[FactResellerSales]  WITH CHECK ADD  CONSTRAINT [FK_FactResellerSales_DimDate2] FOREIGN KEY([ShipDateKey])
REFERENCES [dw].[DimDate] ([DateKey])
GO

ALTER TABLE [dw].[FactResellerSales] CHECK CONSTRAINT [FK_FactResellerSales_DimDate2]
GO

ALTER TABLE [dw].[FactResellerSales]  WITH CHECK ADD  CONSTRAINT [FK_FactResellerSales_DimProduct] FOREIGN KEY([ProductKey])
REFERENCES [dw].[DimProduct] ([ProductKey])
GO

ALTER TABLE [dw].[FactResellerSales] CHECK CONSTRAINT [FK_FactResellerSales_DimProduct]
GO

ALTER TABLE [dw].[FactResellerSales]  WITH CHECK ADD  CONSTRAINT [FK_FactResellerSales_DimEmployee] FOREIGN KEY([EmployeeKey])
REFERENCES [dw].[DimEmployee] ([EmployeeKey])
GO

ALTER TABLE [dw].[FactResellerSales] CHECK CONSTRAINT [FK_FactResellerSales_DimEmployee]
GO

ALTER TABLE [dw].[FactResellerSales]  WITH CHECK ADD  CONSTRAINT [FK_FactResellerSales_DimSalesTerritory] FOREIGN KEY([SalesTerritoryKey])
REFERENCES [dw].[DimSalesTerritory] ([SalesTerritoryKey])
GO

ALTER TABLE [dw].[FactResellerSales] CHECK CONSTRAINT [FK_FactResellerSales_DimSalesTerritory]
GO

ALTER TABLE [dw].[FactResellerSales]  WITH CHECK ADD  CONSTRAINT [FK_FactResellerSales_FactResellerSales] FOREIGN KEY([SalesOrderNumber], [SalesOrderLineNumber])
REFERENCES [dw].[FactResellerSales] ([SalesOrderNumber], [SalesOrderLineNumber])
GO

ALTER TABLE [dw].[FactResellerSales] CHECK CONSTRAINT [FK_FactResellerSales_FactResellerSales]
GO

ALTER TABLE [dw].[FactResellerSales]  WITH CHECK ADD  CONSTRAINT [FK_FactResellerSales_DimAudit] FOREIGN KEY([AuditKey])
REFERENCES [dw].[DimAudit] ([AuditKey])
GO

ALTER TABLE [dw].[FactResellerSales] CHECK CONSTRAINT [FK_FactResellerSales_DimAudit]
GO
ALTER TABLE [dw].[DimProduct]  WITH CHECK ADD  CONSTRAINT [FK_DimProduct_DimProductSubcategory] FOREIGN KEY([ProductSubcategoryKey])
REFERENCES [dw].[DimProductSubcategory] ([ProductSubcategoryKey])
GO
ALTER TABLE [dw].[DimProduct] CHECK CONSTRAINT [FK_DimProduct_DimProductSubcategory]
GO
ALTER TABLE [dw].[DimProduct]  WITH CHECK ADD  CONSTRAINT [FK_DimProduct_DimAudit] FOREIGN KEY([AuditKey])
REFERENCES [dw].[DimAudit] ([AuditKey])
GO

ALTER TABLE [dw].[DimProduct] CHECK CONSTRAINT [FK_DimProduct_DimAudit]
ALTER TABLE [dw].[DimProductSubcategory]  WITH CHECK ADD  CONSTRAINT [FK_DimProductSubcategory_DimProductCategory] FOREIGN KEY([ProductCategoryKey])
REFERENCES [dw].[DimProductCategory] ([ProductCategoryKey])
GO
ALTER TABLE [dw].[DimProductSubcategory] CHECK CONSTRAINT [FK_DimProductSubcategory_DimProductCategory]
GO

ALTER TABLE [dw].[DimProductSubcategory]  WITH CHECK ADD  CONSTRAINT [FK_DimProductSubcategory_DimAudit] FOREIGN KEY([AuditKey])
REFERENCES [dw].[DimAudit] ([AuditKey])
GO

ALTER TABLE [dw].[DimProductSubcategory] CHECK CONSTRAINT [FK_DimProductSubcategory_DimAudit]
ALTER TABLE [dw].[DimProductCategory]  WITH CHECK ADD  CONSTRAINT [FK_DimProductCategory_DimAudit] FOREIGN KEY([AuditKey])
REFERENCES [dw].[DimAudit] ([AuditKey])
GO

ALTER TABLE [dw].[DimProductCategory] CHECK CONSTRAINT [FK_DimProductCategory_DimAudit]
ALTER TABLE [dw].[DimDate]  WITH CHECK ADD  CONSTRAINT [FK_DimDate_DimAudit] FOREIGN KEY([AuditKey])
REFERENCES [dw].[DimAudit] ([AuditKey])
GO

ALTER TABLE [dw].[DimDate] CHECK CONSTRAINT [FK_DimDate_DimAudit]

ALTER TABLE [dw].[DimSalesTerritory]  WITH CHECK ADD  CONSTRAINT [FK_DimSalesTerritory_DimAudit] FOREIGN KEY([AuditKey])
REFERENCES [dw].[DimAudit] ([AuditKey])
GO

ALTER TABLE [dw].[DimSalesTerritory] CHECK CONSTRAINT [FK_DimSalesTerritory_DimAudit]
GO

CREATE PROCEDURE Generate_ResellerKey
  @Store NVARCHAR(15) -- Business key
AS 
SET NOCOUNT ON

/* Prevent race conditions */ 
SET TRANSACTION ISOLATION LEVEL SERIALIZABLE

/* Ensure key does not exist */
DECLARE @ResellerKey INT 
SELECT @ResellerKey = ResellerKEy
FROM dw.DimReseller 
WHERE ResellerAlternateKey = @Store

/* Generate new surrogate key for inferred member */ 
IF @ResellerKey IS NULL BEGIN 
  

  INSERT [dw].[DimReseller]
           ([ResellerAlternateKey]
           ,[BusinessType]
           ,[ResellerName]
           ,[NumberEmployees]
           ,[FirstOrderYear]
           ,[LastOrderYear]
           ,[ProductLine]
           ,[AddressLine1]
           ,[AddressLine2]
           ,[City]
           ,[StateProvinceCode]
           ,[PostalCode]
           ,[AnnualSales]
           ,[BankName]
           ,[AnnualRevenue]
           ,[YearOpened]
           ,[Current]
		   ,[Inferred])
     VALUES
           (@Store
           ,'Unknown'
           ,'Unknown'
           ,null
           ,null
           ,null
           ,null
           ,null
           ,null
           ,null
           ,null
           ,null
           ,null
           ,null
           ,null
           ,null
           ,1
		   ,1)
  SET @ResellerKey = SCOPE_IDENTITY() 
END

/* Return surrogate and business key */ 
SELECT @ResellerKey AS ResellerKey, @Store AS ResellerAlternateKey

