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

CREATE TABLE [stageProduct_Module_03] (
    [Name] varchar(50),
    [ProductNumber] nvarchar(25),
    [MakeFlag] bit,
    [FinishedGoodsFlag] bit,
    [Color] nvarchar(15),
    [SafetyStockLevel] smallint,
    [ReorderPoint] smallint,
    [StandardCost] money,
    [ListPrice] money,
    [ProductId] int
)



CREATE TABLE [dbo].[stageSalesDetails_Module_03](
	[SalesOrderID] [int] NULL,
	[SalesOrderDetailID] [int] NULL,
	[CarrierTrackingNumber] [nvarchar](25) NULL,
	[OrderQty] [smallint] NULL,
	[ProductID] [int] NULL,
	[SpecialOfferID] [int] NULL,
	[UnitPrice] [money] NULL,
	[UnitPriceDiscount] [money] NULL,
	[LineTotal] [numeric](38, 6) NULL,
	[rowguid] [uniqueidentifier] NULL,
	[ModifiedDate] [datetime] NULL,
	[CarrierTrackingNumberstr] [varchar](25) NULL
) ON [PRIMARY]

GO




CREATE TABLE [dbo].[stageSalesDetailsUpdate_Module_03](
	[SalesOrderID] [int] NULL,
	[SalesOrderDetailID] [int] NULL,
	[CarrierTrackingNumber] [nvarchar](25) NULL,
	[OrderQty] [smallint] NULL,
	[ProductID] [int] NULL,
	[SpecialOfferID] [int] NULL,
	[UnitPrice] [money] NULL,
	[UnitPriceDiscount] [money] NULL,
	[LineTotal] [numeric](38, 6) NULL,
	[StandardCost] [money] NULL,
	[rowguid] [uniqueidentifier] NULL,
	[ModifiedDate] [datetime] NULL
) ON [PRIMARY]

GO
CREATE TABLE [stageSalesHeader_Module_03] (
    [rowguid] uniqueidentifier,
    [ModifiedDate] datetime,
    [TerritoryID] int,
    [SalesOrderID] int,
    [RevisionNumber] tinyint,
    [OrderDate] datetime,
    [DueDate] datetime,
    [ShipDate] datetime,
    [Status] tinyint,
    [OnlineOrderFlag] bit,
    [SalesOrderNumber] nvarchar(25),
    [PurchaseOrderNumber] nvarchar(25),
    [AccountNumber] nvarchar(15),
    [CustomerID] int,
    [SalesPersonID] int,
    [BillToAddressID] int,
    [ShipToAddressID] int,
    [ShipMethodID] int,
    [CreditCardID] int,
    [CreditCardApprovalCode] varchar(15),
    [CurrencyRateID] int,
    [SubTotal] money,
    [TaxAmt] money,
    [Freight] money,
    [TotalDue] money,
    [Comment] nvarchar(128),
    [StoreID] int
)


CREATE TABLE [stageSalesDetail_Module_03] (
    [rowguid] uniqueidentifier,
    [ModifiedDate] datetime,
    [SalesOrderID] int,
    [SalesOrderDetailID] int,
    [CarrierTrackingNumber] nvarchar(25),
    [OrderQty] smallint,
    [ProductID] int,
    [SpecialOfferID] int,
    [UnitPrice] money,
    [UnitPriceDiscount] money,
    [LineTotal] numeric(38,6)
)
