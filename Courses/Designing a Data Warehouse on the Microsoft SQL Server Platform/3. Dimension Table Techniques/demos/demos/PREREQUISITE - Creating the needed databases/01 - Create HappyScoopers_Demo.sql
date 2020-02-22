USE [master]
GO


IF  EXISTS (SELECT name FROM sys.databases WHERE name = N'HappyScoopers_Demo')
DROP DATABASE [HappyScoopers_Demo]
GO
CREATE DATABASE [HappyScoopers_Demo]
 CONTAINMENT = NONE
 ON  PRIMARY 
 /***********
ATTENTION: 
Replace the string {LOCAL_PATH} with an existing path on your machine, where you want to create the database
***********/
( NAME = N'HappyScoopers_Demo', FILENAME = N'{LOCAL_PATH}\HappyScoopers_Demo.mdf' , SIZE = 270336KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'HappyScoopers_Demo_log', FILENAME = N'{LOCAL_PATH}\HappyScoopers_Demo_log.ldf' , SIZE = 401408KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [HappyScoopers_Demo].[dbo].[sp_fulltext_database] @action = 'enable'
end
EXEC sys.sp_db_vardecimal_storage_format N'HappyScoopers_Demo', N'ON'
GO

USE [HappyScoopers_Demo]
GO

CREATE TABLE [dbo].[Addresses](
	[AddressID] [int] IDENTITY(1,1) NOT NULL,
	[AddressLine1] [nvarchar](60) NOT NULL,
	[AddressLine2] [nvarchar](60) NULL,
	[CityID] [int] NOT NULL,
	[PostalCode] [nvarchar](15) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Addresses] PRIMARY KEY CLUSTERED ([AddressID] ASC)
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Cities](
	[CityID] [int] IDENTITY(1,1) NOT NULL,
	[CityName] [nvarchar](50) NOT NULL,
	[ProvinceID] [int] NOT NULL,
	[Population] [bigint] NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Cities] PRIMARY KEY CLUSTERED (	[CityID] ASC)
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Countries](
	[CountryID] [int] IDENTITY(1,1) NOT NULL,
	[CountryName] [nvarchar](60) NOT NULL,
	[FormalName] [nvarchar](60) NOT NULL,
	[CountryCode] [nvarchar](3) NULL,
	[Population] [bigint] NULL,
	[Continent] [nvarchar](30) NOT NULL,
	[Region] [nvarchar](30) NOT NULL,
	[Subregion] [nvarchar](30) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Countries] PRIMARY KEY CLUSTERED ([CountryID] ASC)
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Customers](
	[CustomerID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](100) NOT NULL,
	[FullName]  AS (([FirstName]+' ')+[LastName]),
	[Title] [nvarchar](30) NOT NULL,
	[DeliveryAddressID] [int] NOT NULL,
	[BillingAddressID] [int] NOT NULL,
	[PhoneNumber] [nvarchar](24) NOT NULL,
	[Email] [nvarchar](100) NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED ([CustomerID] ASC)
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Employees](
	[EmployeeID] [int] IDENTITY(1,1) NOT NULL,
	[LastName] [nvarchar](100) NOT NULL,
	[FirstName] [nvarchar](100) NOT NULL,
	[Title] [nvarchar](25) NOT NULL,
	[BirthDate] [datetime] NOT NULL,
	[Gender] [nchar](10) NOT NULL,
	[HireDate] [datetime] NOT NULL,
	[JobTitle] [nvarchar](100) NOT NULL,
	[AddressID] [int] NOT NULL,
	[ManagerID] [int] NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED ([EmployeeID] ASC)
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Ingredients](
	[IngredientID] [int] IDENTITY(1,1) NOT NULL,
	[IngredientName] [nvarchar](200) NULL,
	[UnitOfMeasureID] [int] NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Ingredients] PRIMARY KEY CLUSTERED ([IngredientID] ASC)
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[InventoryItems](
	[InventoryItemID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NULL,
	[IngredientID] [int] NULL,
	[PackageTypeID] [int] NOT NULL,
	[UnitOfMeasureID] [int] NOT NULL,
	[Quantity] [decimal](18, 2) NOT NULL,
	[Barcode] [nvarchar](50) NULL,
	[VATRate] [decimal](18, 3) NOT NULL,
	[UnitPrice] [decimal](18, 2) NOT NULL,
	[RecommendedRetailPrice] [decimal](18, 2) NULL,
	[TypicalWeightPerUnit] [decimal](18, 3) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_InventoryItems] PRIMARY KEY CLUSTERED ([InventoryItemID] ASC)
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[InventoryTransactions](
	[InventoryTransactionID] [int] IDENTITY(1,1) NOT NULL,
	[InventoryItemID] [int] NOT NULL,
	[CustomerID] [int] NULL,
	[OrderID] [int] NULL,
	[TransactionDate] [datetime2](7) NOT NULL,
	[Quantity] [decimal](18, 3) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_InventoryTransactions] PRIMARY KEY CLUSTERED ([InventoryTransactionID] ASC)
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[OrderLines](
	[OrderLineID] [int] IDENTITY(1,1) NOT NULL,
	[OrderID] [int] NOT NULL,
	[ProductID] [int] NOT NULL,
	[PackageTypeID] [int] NOT NULL,
	[PromotionID] [int] NULL,
	[InventoryItemID] [int] NULL,
	[UnitPrice] [decimal](18, 2) NOT NULL,
	[Description] [nvarchar](200) NOT NULL,
	[Quantity] [int] NOT NULL,
	[Discount] [decimal](18, 2) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[LineNumber] [nvarchar](10) NULL,
	[VATRate] [decimal](18, 2) NULL
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Orders](
	[OrderID] [int] IDENTITY(1,1) NOT NULL,
	[CustomerID] [int] NULL,
	[EmployeeID] [int] NOT NULL,
	[DeliveryAddressID] [int] NOT NULL,
	[PaymentTypeID] [int] NOT NULL,
	[OrderDate] [datetime] NOT NULL,
	[DeliveryDate] [datetime] NOT NULL,
	[Comments] [nvarchar](max) NULL,
	[Status] [int] NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED ([OrderID] ASC)
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [dbo].[PackageTypes](
	[PackageTypeID] [int] IDENTITY(1,1) NOT NULL,
	[PackageTypeName] [nvarchar](50) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_PackageTypes] PRIMARY KEY CLUSTERED ([PackageTypeID] ASC)
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[PaymentTypes](
	[PaymentTypeID] [int] IDENTITY(1,1) NOT NULL,
	[PaymentTypeName] [nvarchar](50) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_PaymentTypes] PRIMARY KEY CLUSTERED ([PaymentTypeID] ASC)
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ProductCategories](
	[CategoryID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](15) NOT NULL,
	[CategoryDescription] [nvarchar](200) NOT NULL,
	[DepartmentID] [int] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED ([CategoryID] ASC)
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ProductDepartments](
	[DepartmentID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](200) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ProductDepartments] PRIMARY KEY CLUSTERED ([DepartmentID] ASC)
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Products](
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [nvarchar](40) NOT NULL,
	[ProductCode] [nvarchar](10) NOT NULL,
	[ProductDescription] [nvarchar](200) NOT NULL,
	[SubcategoryID] [int] NOT NULL,
	[UnitOfMeasureID] [int] NOT NULL,
	[UnitPrice] [decimal](18, 2) NOT NULL,
	[Discontinued] [bit] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Products2] PRIMARY KEY CLUSTERED ([ProductID] ASC)
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ProductSubcategories](
	[ProductSubcategoryID] [int] IDENTITY(1,1) NOT NULL,
	[ProductCategoryID] [int] NOT NULL,
	[SubcategoryName] [nvarchar](200) NOT NULL,
	[SubcategoryDescription] [nvarchar](200) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_ProductSubcategories] PRIMARY KEY CLUSTERED ([ProductSubcategoryID] ASC)
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Promotions](
	[PromotionID] [int] IDENTITY(1,1) NOT NULL,
	[DealDescription] [nvarchar](30) NOT NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NOT NULL,
	[DiscountAmount] [decimal](18, 2) NULL,
	[DiscountPercentage] [decimal](18, 3) NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Promotions] PRIMARY KEY CLUSTERED ([PromotionID] ASC)
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Provinces](
	[ProvinceID] [int] IDENTITY(1,1) NOT NULL,
	[ProvinceCode] [nvarchar](5) NOT NULL,
	[ProvinceName] [nvarchar](200) NOT NULL,
	[CountryID] [int] NOT NULL,
	[Population] [bigint] NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Provinces] PRIMARY KEY CLUSTERED ([ProvinceID] ASC)
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Recipes](
	[RecipeID] [int] IDENTITY(1,1) NOT NULL,
	[ProductID] [int] NOT NULL,
	[IngredientID] [int] NOT NULL,
	[Quantity] [decimal](18, 2) NOT NULL,
	[Comments] [nvarchar](2000) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[ValidFrom] [datetime2](0) NOT NULL,
	[ValidTo] [datetime2](0) NOT NULL,
 CONSTRAINT [PK_Recipes] PRIMARY KEY CLUSTERED ([RecipeID] ASC)
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Stores](
	[StoreID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[ManagerID] [int] NULL,
	[AddressID] [int] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Stores] PRIMARY KEY CLUSTERED ([StoreID] ASC)
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UnitsOfMeasure](
	[UnitOfMeasureID] [int] IDENTITY(1,1) NOT NULL,
	[UnitMeasureCode] [nchar](3) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_UnitsOfMeasure] PRIMARY KEY CLUSTERED ([UnitOfMeasureID] ASC)
) ON [PRIMARY]
GO


ALTER TABLE [dbo].[Addresses] ADD  CONSTRAINT [DF_Addresses_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[Cities] ADD  CONSTRAINT [DF_Cities_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[Countries] ADD  CONSTRAINT [DF_Countries_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[Customers] ADD  CONSTRAINT [DF_Customers_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[Employees] ADD  CONSTRAINT [DF_Employees_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[Ingredients] ADD  CONSTRAINT [DF_Ingredients_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[InventoryItems] ADD  CONSTRAINT [DF_InventoryItems_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[InventoryTransactions] ADD  CONSTRAINT [DF_InventoryTransactions_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[OrderLines] ADD  CONSTRAINT [DF_OrderLines_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[Orders] ADD  CONSTRAINT [DF_Orders_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[PackageTypes] ADD  CONSTRAINT [DF_PackageTypes_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[PaymentTypes] ADD  CONSTRAINT [DF_PaymentTypes_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[ProductCategories] ADD  CONSTRAINT [DF_ProductCategories_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[ProductDepartments] ADD  CONSTRAINT [DF_ProductDepartments_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[Products] ADD  CONSTRAINT [DF_Products2_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[ProductSubcategories] ADD  CONSTRAINT [DF_ProductSubcategories_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[Promotions] ADD  CONSTRAINT [DF_Promotions_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[Provinces] ADD  CONSTRAINT [DF_Provinces_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[Recipes] ADD  CONSTRAINT [DF_Recipes_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[Stores] ADD  CONSTRAINT [DF_Stores_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO
ALTER TABLE [dbo].[UnitsOfMeasure] ADD  CONSTRAINT [DF_UnitsOfMeasure_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO

-- Create or modify the procedure for loading data into the staging table
create PROCEDURE [dbo].[Load_StagingCustomer]
@LastLoadDate datetime,
@NewLoadDate datetime
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

--SELECT @LastLoadDate, @NewLoadDate

SELECT 
	 'HSD|' + CONVERT(NVARCHAR, cus.[CustomerID])						AS [_SourceKey],
	CONVERT(nvarchar(100),ISNULL(cus.[FirstName], 'N/A'))				AS [First Name],
	CONVERT(nvarchar(100),ISNULL(cus.[LastName], 'N/A'))				AS [Last Name],
	CONVERT(nvarchar(200),ISNULL(cus.[FullName], 'N/A'))				AS [Full Name],
	CONVERT(nvarchar(30), ISNULL(cus.[Title], 'N/A'))					AS [Title], 
	CONCAT_WS('|', 'HSD', 
		CONVERT(nvarchar(5), ISNULL(dcou.CountryID, 0)),
		CONVERT(nvarchar(5), ISNULL(dprv.ProvinceID, 0)),
		CONVERT(nvarchar(5), ISNULL(dcit.CityID, 0)), 
		CONVERT(nvarchar(5), ISNULL(dadr.AddressID, 0)))				AS [Delivery Location Key],
	CONCAT_WS('|', 'HSD', 
		CONVERT(nvarchar(5), ISNULL(bcou.CountryID, 0)),
		CONVERT(nvarchar(5), ISNULL(bprv.ProvinceID, 0)),
		CONVERT(nvarchar(5), ISNULL(bcit.CityID, 0)), 
		CONVERT(nvarchar(5), ISNULL(badr.AddressID, 0)))				AS [Billing Location Key],
	CONVERT(nvarchar(24), ISNULL(cus.[PhoneNumber], 'N/A'))				AS [Phone Number], 
	CONVERT(nvarchar(100),ISNULL(cus.[Email], 'N/A'))					AS [Email],
		CONVERT(datetime, ISNULL([cus].ModifiedDate, '1753-01-01'))		AS [Customer Modified Date],
		CONVERT(datetime, ISNULL([dadr].ModifiedDate, '1753-01-01'))	AS [Delivery Addr Modified Date],
		CONVERT(datetime, ISNULL([badr].ModifiedDate, '1753-01-01'))	AS [Billing Addr Modified Date],
	(SELECT MAX(t) FROM
                             (VALUES
                               ([cus].ModifiedDate)
                             , ([badr].ModifiedDate)
                             , ([dadr].ModifiedDate)
                             ) AS [maxModifiedDate](t)
                           )											AS [Valid From],
	CONVERT(datetime, '9999-12-31')										AS [Valid To]
FROM	
	[HappyScoopers_Demo].[dbo].[Customers] cus
	LEFT JOIN [HappyScoopers_Demo].[dbo].[Addresses] badr on cus.BillingAddressID = badr.AddressID
	LEFT JOIN [HappyScoopers_Demo].[dbo].[Cities] bcit on badr.CityID = bcit.CityID
	LEFT JOIN [HappyScoopers_Demo].[dbo].[Provinces] bprv on bcit.ProvinceID = bprv.ProvinceID
	LEFT JOIN [HappyScoopers_Demo].[dbo].[Countries] bcou on bprv.CountryID = bcou.CountryID
	LEFT JOIN [HappyScoopers_Demo].[dbo].[Addresses] dadr on cus.DeliveryAddressID = dadr.AddressID
	LEFT JOIN [HappyScoopers_Demo].[dbo].[Cities] dcit on dadr.CityID = dcit.CityID
	LEFT JOIN [HappyScoopers_Demo].[dbo].[Provinces] dprv on dcit.ProvinceID = dprv.ProvinceID
	LEFT JOIN [HappyScoopers_Demo].[dbo].[Countries] dcou on dprv.CountryID = dcou.CountryID

WHERE 
	([cus].ModifiedDate > @LastLoadDate AND [cus].ModifiedDate <= @NewLoadDate) OR
	([badr].ModifiedDate > @LastLoadDate AND [badr].ModifiedDate <= @NewLoadDate) OR
	([dadr].ModifiedDate > @LastLoadDate AND [dadr].ModifiedDate <= @NewLoadDate) 


    RETURN 0;
END;
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Create or modify the procedure for loading data into the staging table
create   PROCEDURE [dbo].[Load_StagingEmployee]
@LastLoadDate datetime,
@NewLoadDate datetime
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

--SELECT @LastLoadDate, @NewLoadDate

SELECT 
	 'HSD|' + CONVERT(NVARCHAR, emp.[EmployeeID])				AS [_SourceKey]
	,	CONCAT_WS('|', 'HSD', 
		CONVERT(nvarchar(5), ISNULL(cou.CountryID, 0)),
		CONVERT(nvarchar(5), ISNULL(prv.ProvinceID, 0)),
		CONVERT(nvarchar(5), ISNULL(cit.CityID, 0)), 
		CONVERT(nvarchar(5), ISNULL(adr.AddressID, 0)))			AS [Location Key]
	,CONVERT(nvarchar(100),emp.LastName)						AS [Last Name]
	,CONVERT(nvarchar(100),emp.FirstName)						AS [First Name]
	,CONVERT(nvarchar(25),emp.Title)							AS [Title]
	,CONVERT(date,emp.BirthDate)								AS [Birth Date]
	,CONVERT(nvarchar(10),emp.Gender)							AS [Gender]
	,CONVERT(date,emp.HireDate)									AS [Hire Date]
	,CONVERT(nvarchar(100),emp.JobTitle)						AS [Job Title]
	,CONVERT(nvarchar(100),adr.AddressLine1)					AS [Address Line]
	,CONVERT(nvarchar(100),cit.CityName)						AS [City]
	,CONVERT(nvarchar(100),cou.CountryName)						AS [Country]
	,'HSD|' + CONVERT(NVARCHAR, emp.ManagerID)					AS [Manager Key]
	,CONVERT(datetime, ISNULL(emp.ModifiedDate, '1753-01-01'))	AS [Employee Modified Date]
	,CONVERT(datetime, ISNULL(adr.ModifiedDate, '1753-01-01'))	AS [Address Modified Date]
	,(SELECT MAX(t) FROM
                             (VALUES
                               ([emp].ModifiedDate)
                             , ([adr].ModifiedDate)
                             ) AS [maxModifiedDate](t)
                           )								AS [ValidFrom]
	,CONVERT(datetime, '9999-12-31')						AS [ValidTo]
FROM [HappyScoopers_Demo].[dbo].[Employees] [emp]
LEFT JOIN [HappyScoopers_Demo].[dbo].[Addresses] [adr] ON emp.AddressID = adr.AddressID
LEFT JOIN [HappyScoopers_Demo].[dbo].Cities [cit] ON adr.CityID = cit.CityID
LEFT JOIN [HappyScoopers_Demo].[dbo].Provinces [prv] ON cit.ProvinceID = prv.ProvinceID
LEFT JOIN [HappyScoopers_Demo].[dbo].Countries [cou] ON prv.CountryID = cou.CountryID
WHERE 
	([emp].ModifiedDate > @LastLoadDate AND [emp].ModifiedDate <= @NewLoadDate) OR
	([adr].ModifiedDate > @LastLoadDate AND [adr].ModifiedDate <= @NewLoadDate) 

    RETURN 0;
END;
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Create or modify the procedure for loading data into the staging table
create  PROCEDURE [dbo].[Load_StagingLocation]
@LastLoadDate datetime,
@NewLoadDate datetime
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

--SELECT @LastLoadDate, @NewLoadDate

SELECT 
	CONCAT_WS('|', 'HSD', 
		CONVERT(nvarchar(5), ISNULL(cou.CountryID, 0)),
		CONVERT(nvarchar(5), ISNULL(prv.ProvinceID, 0)),
		CONVERT(nvarchar(5), ISNULL(cit.CityID, 0)), 
		CONVERT(nvarchar(5), ISNULL(adr.AddressID, 0)))						AS [_SourceKey],
	CONVERT(nvarchar(200),ISNULL(cou.Continent, 'N/A'))						AS [Continent],
	CONVERT(nvarchar(200),ISNULL(cou.Region, 'N/A'))						AS [Region],
	CONVERT(nvarchar(200),ISNULL(cou.Subregion, 'N/A'))						AS [Subregion],
	CONVERT(nvarchar(200), ISNULL(cou.CountryCode, 'N/A'))					AS [Country Code], 
	CONVERT(nvarchar(200), ISNULL(cou.CountryName, 'N/A'))					AS [Country], 
	CONVERT(nvarchar(200),ISNULL(cou.FormalName, 'N/A'))					AS [Country Formal Name],
	ISNULL(CONVERT(bigint,cou.Population), -1)								AS [Country Population],
	CONVERT(nvarchar(200),ISNULL(prv.ProvinceCode, 'N/A'))					AS [Province Code],
	CONVERT(nvarchar(200),ISNULL(prv.ProvinceName, 'N/A'))					AS [Province],
	ISNULL(CONVERT(bigint,prv.Population), -1)								AS [Province Population],
	CONVERT(nvarchar(200),ISNULL(cit.CityName, 'N/A'))						AS [City],
	ISNULL(CONVERT(bigint,cit.Population), -1)								AS [City Population],
	CONVERT(nvarchar(200),ISNULL(adr.PostalCode, 'N/A'))					AS [Postal Code],
	CONVERT(nvarchar(200),ISNULL(adr.AddressLine1, 'N/A'))					AS [Address Line 1],
	CONVERT(nvarchar(200),ISNULL(adr.AddressLine2, 'N/A'))					AS [Address Line 2],
	CONVERT(datetime, ISNULL([adr].ModifiedDate, '1753-01-01'))				AS [Address Modified Date],
	CONVERT(datetime, ISNULL([cit].ModifiedDate, '1753-01-01'))				AS [City Modified Date],
	CONVERT(datetime, ISNULL([prv].ModifiedDate, '1753-01-01'))				AS [Province Modified Date],
	CONVERT(datetime, ISNULL([cou].ModifiedDate, '1753-01-01'))				AS [Country Modified Date],
	(SELECT MAX(t) FROM
                             (VALUES
                               ([adr].ModifiedDate)
                             , ([cit].ModifiedDate)
                             , ([prv].ModifiedDate)
                             , ([cou].ModifiedDate)
                             ) AS [maxModifiedDate](t)
                           )												AS [ValidFrom],
	CONVERT(datetime, '9999-12-31')											AS [ValidTo]
FROM	
	[HappyScoopers_Demo].[dbo].[Addresses] adr 
	FULL JOIN [HappyScoopers_Demo].[dbo].[Cities] cit on adr.CityID = cit.CityID
	FULL JOIN [HappyScoopers_Demo].[dbo].[Provinces] prv on cit.ProvinceID = prv.ProvinceID
	FULL JOIN [HappyScoopers_Demo].[dbo].[Countries] cou on prv.CountryID = cou.CountryID
WHERE 
	([adr].ModifiedDate > @LastLoadDate AND [adr].ModifiedDate <= @NewLoadDate) OR
	([cit].ModifiedDate > @LastLoadDate AND [cit].ModifiedDate <= @NewLoadDate) OR
	([prv].ModifiedDate > @LastLoadDate AND [prv].ModifiedDate <= @NewLoadDate) OR
	([cou].ModifiedDate > @LastLoadDate AND [cou].ModifiedDate <= @NewLoadDate) 


    RETURN 0;
END;
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Create or modify the procedure for loading data into the staging table
create PROCEDURE [dbo].[Load_StagingPaymentType]
@LastLoadDate datetime,
@NewLoadDate datetime
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

--SELECT @LastLoadDate, @NewLoadDate

SELECT 
	 'HSD|' + CONVERT(NVARCHAR, pay.[PaymentTypeID])				AS [_SourceKey],
	CONVERT(nvarchar(100),ISNULL(pay.[PaymentTypeName], 'N/A'))		AS [Payment Type Name],
	CONVERT(datetime, ISNULL(pay.[ModifiedDate], '1753-01-01'))		AS [ValidFrom],
	CONVERT(datetime, '9999-12-31')									AS [ValidTo]
FROM	
	[HappyScoopers_Demo].[dbo].[PaymentTypes] pay

WHERE 
	([pay].ModifiedDate > @LastLoadDate AND [pay].ModifiedDate <= @NewLoadDate) 

    RETURN 0;
END;
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Create or modify the procedure for loading data into the staging table
CREATE PROCEDURE [dbo].[Load_StagingProduct]
@LastLoadDate datetime,
@NewLoadDate datetime
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

--SELECT @LastLoadDate, @NewLoadDate

SELECT 
	 'HSD|' + CONVERT(NVARCHAR, prod.[ProductID])			AS [_SourceKey]
	,CONVERT(nvarchar(200), prod.[ProductName])				AS [Product Name]
	,CONVERT(nvarchar(50), prod.[ProductCode])				AS [Product Code]
	,CONVERT(nvarchar(200), prod.[ProductDescription])		AS [Product Description]
	,CONVERT(nvarchar(200), subcat.[SubcategoryName])		AS [Subcategory]
	,CONVERT(nvarchar(200), cat.[CategoryName])				AS [Category]
	,CONVERT(nvarchar(200), dep.[Name])						AS [Department]
	,CONVERT(nvarchar(10), um.[UnitMeasureCode])			AS [Unit of measure Code]
	,CONVERT(nvarchar(50), um.[Name])						AS [Unit of measure Name]
	,CONVERT(decimal(18,2), prod.[UnitPrice])				AS [Unit Price]
	,CONVERT(nvarchar(10), CASE prod.[Discontinued]
		WHEN 1 THEN 'Yes'
		ELSE 'No'
	 END)													AS [Discontinued] 
	,CONVERT(datetime, ISNULL([prod].ModifiedDate, '1753-01-01'))	AS [Product Modified Date]
	,CONVERT(datetime, ISNULL([subcat].ModifiedDate, '1753-01-01'))	AS [Subcategory Modified Date]
	,CONVERT(datetime, ISNULL([cat].ModifiedDate, '1753-01-01'))	AS [Category Modified Date]
	,CONVERT(datetime, ISNULL([dep].ModifiedDate, '1753-01-01'))	AS [Department Modified Date]
	,CONVERT(datetime, ISNULL([um].ModifiedDate, '1753-01-01'))		AS [UM Modified Date]
	,(SELECT MAX(t) FROM
                             (VALUES
                               ([prod].ModifiedDate)
                             , ([subcat].ModifiedDate)
                             , ([cat].ModifiedDate)
                             , ([dep].ModifiedDate)
                             , ([um].ModifiedDate)
                             ) AS [maxModifiedDate](t)
                           )								AS [ValidFrom]
	,CONVERT(datetime, '9999-12-31')						AS [ValidTo]

FROM [HappyScoopers_Demo].[dbo].[Products] prod
LEFT JOIN [HappyScoopers_Demo].[dbo].[ProductSubcategories] subcat ON prod.SubcategoryID = subcat.ProductSubcategoryID
LEFT JOIN [HappyScoopers_Demo].[dbo].[ProductCategories] cat ON subcat.ProductCategoryID = cat.CategoryID
LEFT JOIN [HappyScoopers_Demo].[dbo].[ProductDepartments] dep ON cat.DepartmentID = dep.DepartmentID
LEFT JOIN [HappyScoopers_Demo].[dbo].[UnitsOfMeasure] um ON prod.UnitOfMeasureID = um.UnitOfMeasureID
WHERE 
	([prod].ModifiedDate > @LastLoadDate AND [prod].ModifiedDate <= @NewLoadDate) OR
	([subcat].ModifiedDate > @LastLoadDate AND [subcat].ModifiedDate <= @NewLoadDate) OR
	([cat].ModifiedDate > @LastLoadDate AND [cat].ModifiedDate <= @NewLoadDate) OR
	([dep].ModifiedDate > @LastLoadDate AND [dep].ModifiedDate <= @NewLoadDate) OR
	([um].ModifiedDate > @LastLoadDate AND [um].ModifiedDate <= @NewLoadDate)

    RETURN 0;
END;
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Create or modify the procedure for loading data into the staging table
create  PROCEDURE [dbo].[Load_StagingPromotion]
@LastLoadDate datetime,
@NewLoadDate datetime
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

--SELECT @LastLoadDate, @NewLoadDate

SELECT 
	 'HSD|' + CONVERT(NVARCHAR, pro.[PromotionID])					AS [_SourceKey],
	CONVERT(nvarchar(100),ISNULL(pro.[DealDescription], 'N/A'))		AS [Deal Description],
	CONVERT(date,ISNULL(pro.[StartDate], '1753-01-01'))				AS [Start Date],
	CONVERT(date,ISNULL(pro.[EndDate], '1753-01-01'))				AS [End Date],
	CONVERT(decimal(18,2), ISNULL(pro.[DiscountAmount], 0))			AS [Discount Amount], 
	CONVERT(decimal(18,3), ISNULL(pro.[DiscountPercentage], 0))		AS [Discount Percentage], 
	CONVERT(datetime, ISNULL(pro.[ModifiedDate], '1753-01-01'))		AS [Promotion Modified Date],
	CONVERT(datetime, ISNULL(pro.[ModifiedDate], '1753-01-01'))		AS [ValidFrom],
	CONVERT(datetime, '9999-12-31')									AS [ValidTo]
FROM	
	[HappyScoopers_Demo].[dbo].[Promotions] pro

WHERE 
	([pro].ModifiedDate > @LastLoadDate AND [pro].ModifiedDate <= @NewLoadDate) 

    RETURN 0;
END;
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- Create or modify the procedure for loading data into the staging table
CREATE PROCEDURE [dbo].[Load_StagingSales]
@LastLoadDate datetime,
@NewLoadDate datetime
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    SELECT CAST(ord.OrderDate AS date)								AS [_SourceOrderDateKey],
           CAST(ord.DeliveryDate AS date)							AS [_SourceDeliveryDateKey],
           ord.OrderID												AS [_SourceOrder],
		   ISNULL(orl.OrderLineID, 0)								AS [_SourceOrderLine],
		   cus.CustomerID											AS [_SourceCustomerKey],
		   emp.EmployeeID											AS [_SourceEmployeeKey],
		   prd.ProductID											AS [_SourceProductKey],
		   pmt.PaymentTypeID										AS [_SourcePaymentTypeKey],
		   cou.CountryID											AS [_SourceDeliveryCountryKey],
		   prv.ProvinceID											AS [_SourceDeliveryProvinceKey],
		   cit.CityID												AS [_SourceDeliveryCityKey],
		   adr.AddressID											AS [_SourceDeliveryAddressKey],
		   CONCAT_WS('|', cou.CountryID, 
			prv.ProvinceID,
			cit.CityID,
			adr.AddressID)											AS [_SourceDeliveryLocationKey],
		   pro.PromotionID											AS [_SourcePromotionKey],
		   ISNULL(orl.Description, 'N/A)')							AS [Description],
           ISNULL(pck.PackageTypeName, 'N/A')						AS [Package],
           orl.Quantity												AS [Quantity],
           orl.UnitPrice											AS [Unit Price],
           ISNULL(orl.VATRate, 0.20)								AS [VAT Rate],
           orl.Quantity * orl.UnitPrice								AS [Total Excluding VAT],
		   orl.Quantity * orl.UnitPrice * ISNULL(orl.VATRate, 0.20) AS [VAT Amount],
		   orl.Quantity*orl.UnitPrice*(1+ ISNULL(orl.VATRate, 0.20)) AS [Total Including VAT],
           CASE 
			WHEN orl.ModifiedDate > ord.ModifiedDate 
				THEN orl.ModifiedDate 
			ELSE ord.ModifiedDate END								AS [ModifiedDate]
    FROM 
		[HappyScoopers_Demo].[dbo].[Orders] ord
		LEFT JOIN [HappyScoopers_Demo].[dbo].[OrderLines] orl ON ord.OrderID = orl.OrderID
	    LEFT JOIN [HappyScoopers_Demo].[dbo].[Customers] cus ON ord.CustomerID = cus.CustomerID
		LEFT JOIN [HappyScoopers_Demo].[dbo].[Addresses] adr ON ord.DeliveryAddressID = adr.AddressID
		LEFT JOIN [HappyScoopers_Demo].[dbo].[Cities] cit ON adr.CityID = cit.CityID
		LEFT JOIN [HappyScoopers_Demo].[dbo].[Provinces] prv ON cit.ProvinceID = prv.ProvinceID
		LEFT JOIN [HappyScoopers_Demo].[dbo].[Countries] cou ON prv.CountryID = cou.CountryID
	    LEFT JOIN [HappyScoopers_Demo].[dbo].[Employees] emp ON ord.EmployeeID = emp.EmployeeID
        LEFT JOIN [HappyScoopers_Demo].[dbo].[PaymentTypes] pmt ON ord.PaymentTypeID = pmt.PaymentTypeID
		LEFT JOIN [HappyScoopers_Demo].[dbo].[Products] prd ON orl.ProductID = prd.ProductID
		LEFT JOIN [HappyScoopers_Demo].[dbo].[PackageTypes] pck ON orl.PackageTypeID = pck.PackageTypeID 
		LEFT JOIN [HappyScoopers_Demo].[dbo].[Promotions] pro ON orl.PromotionID = pro.PromotionID
WHERE 
	([ord].ModifiedDate > @LastLoadDate AND [ord].ModifiedDate <= @NewLoadDate) OR
	([orl].ModifiedDate > @LastLoadDate AND [orl].ModifiedDate <= @NewLoadDate) 


    RETURN 0;
END;
GO
ALTER DATABASE [HappyScoopers_Demo] SET READ_WRITE 
GO
