USE [master]
GO
DROP DATABASE IF EXISTS [HappyScoopers_DW]
GO
CREATE DATABASE [HappyScoopers_DW]
 CONTAINMENT = NONE
 ON  PRIMARY 
 /***********
ATTENTION!
	Before you execute the script, replace the string {LOCAL_PATH} with an existing path on your machine
	This path is the location where you want to create the database
***********/
( NAME = N'HappyScoopers_DW', FILENAME = N'{LOCAL_PATH}\HappyScoopers_DW.mdf' , SIZE = 1581056KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'HappyScoopers_DW_log', FILENAME = N'{LOCAL_PATH}\HappyScoopers_DW_log.ldf' , SIZE = 7610368KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [HappyScoopers_DW] SET COMPATIBILITY_LEVEL = 140
GO

USE [HappyScoopers_DW]
GO

CREATE SCHEMA [int]
GO

CREATE VIEW [dbo].[vw_Employees]
AS

SELECT 
	 CONVERT(nvarchar(200),'')								AS [_SourceKey]
	,CONVERT(nvarchar(50),'')							    AS [Location Key]
	,CONVERT(nvarchar(100),'N/A')							AS [Last Name]
	,CONVERT(nvarchar(100),'N/A')							AS [First Name]
	,CONVERT(nvarchar(25),'N/A')							AS [Title]
	,CONVERT(date,'1753-01-01')							    AS [Birth Date]
	,CONVERT(nvarchar(10),'N/A')							AS [Gender]
	,CONVERT(date,'1753-01-01')							    AS [Hire Date]
	,CONVERT(nvarchar(100),'N/A')							AS [Job Title]
	,CONVERT(nvarchar(100),'N/A')							AS [Address Line]
	,CONVERT(nvarchar(100),'N/A')							AS [City]
	,CONVERT(nvarchar(100),'N/A')							AS [Country]
	,CONVERT(nvarchar(50),'')						        AS [Manager Key]
	,CONVERT(datetime, '1753-01-01')						AS [Valid From]
	,CONVERT(datetime, '9999-12-31')						AS [Valid To]
	,CONVERT(int, -1)										AS [Lineage Key]


UNION ALL

SELECT 
	 'HSD|' + CONVERT(NVARCHAR, emp.[EmployeeID])			AS [_SourceKey]
	,	CONCAT_WS('|', 'HSD', 
		CONVERT(nvarchar(5), ISNULL(cou.CountryID, 0)),
		CONVERT(nvarchar(5), ISNULL(prv.ProvinceID, 0)),
		CONVERT(nvarchar(5), ISNULL(cit.CityID, 0)), 
		CONVERT(nvarchar(5), ISNULL(adr.AddressID, 0)))		AS [Location Key]
	,CONVERT(nvarchar(100),emp.LastName)					AS [Last Name]
	,CONVERT(nvarchar(100),emp.FirstName)					AS [First Name]
	,CONVERT(nvarchar(25),emp.Title)						AS [Title]
	,CONVERT(date,emp.BirthDate)						    AS [Birth Date]
	,CONVERT(nvarchar(10),emp.Gender)						AS [Gender]
	,CONVERT(date,emp.HireDate)							    AS [Hire Date]
	,CONVERT(nvarchar(100),emp.JobTitle)					AS [Job Title]
	,CONVERT(nvarchar(100),adr.AddressLine1)				AS [Address Line]
	,CONVERT(nvarchar(100),cit.CityName)					AS [City]
	,CONVERT(nvarchar(100),cou.CountryName)					AS [Country]
	,'HSD|' + CONVERT(NVARCHAR, emp.ManagerID)		        AS [Manager Key]
	,CONVERT(datetime, '1753-01-01')						AS [Valid From]
	,CONVERT(datetime, '9999-12-31')						AS [Valid To]
	,CONVERT(int, -1)										AS [Lineage Key]
FROM [HappyScoopers_Demo].[dbo].[Employees] emp
LEFT JOIN [HappyScoopers_Demo].[dbo].[Addresses] adr ON emp.AddressID = adr.AddressID
LEFT JOIN [HappyScoopers_Demo].[dbo].Cities cit ON adr.CityID = cit.CityID
LEFT JOIN [HappyScoopers_Demo].[dbo].Provinces prv ON cit.ProvinceID = prv.ProvinceID
LEFT JOIN [HappyScoopers_Demo].[dbo].Countries cou ON prv.CountryID = cou.CountryID
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   VIEW [dbo].[vw_Location]
AS
SELECT 
	CONCAT_WS('|', 'HSD', 
		CONVERT(nvarchar(5), ISNULL(cou.CountryID, 0)),
		CONVERT(nvarchar(5), ISNULL(prv.ProvinceID, 0)),
		CONVERT(nvarchar(5), ISNULL(cit.CityID, 0)), 
		CONVERT(nvarchar(5), ISNULL(adr.AddressID, 0)))						AS [_Source Key],
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
	CONVERT(datetime, cou.ModifiedDate)										AS [Valid From],
	CONVERT(datetime, '9999-12-31')											AS [Valid To],
	CONVERT(int, -1)														AS [Lineage Key]
FROM	
	[HappyScoopers_Demo].[dbo].[Addresses] adr 
	FULL JOIN [HappyScoopers_Demo].[dbo].[Cities] cit on adr.CityID = cit.CityID
	FULL JOIN [HappyScoopers_Demo].[dbo].[Provinces] prv on cit.ProvinceID = prv.ProvinceID
	FULL JOIN [HappyScoopers_Demo].[dbo].[Countries] cou on prv.CountryID = cou.CountryID
  
  
UNION ALL
  SELECT
		'CSV|1|0|0|1'														AS [_SourceKey]
      ,'Europe'																AS [Continent]
      ,'Europe'																AS [Region]
      ,'Southern Europe'													AS [Subregion]
      ,'VAT'																AS [Country Code]
      ,'Vatican'															AS [Country]
      ,'Vatican City'														AS [Country Formal Name]
      ,1000																	AS [Country Population]
      ,'N/A'																AS [Province Code]
      ,'N/A'																AS [Province]
      ,-1																	AS [Province Population]
      ,'N/A'																AS [City]
      ,-1																	AS [City Population]
      ,'00120'																AS [Postal Code]
      ,'Sistine Chapel'														AS [Address Line 1]
      ,'00120 Città del Vaticano'											AS [Address Line 2]
      ,'2019-05-17'															AS [ValidFrom]
      ,'9999-12-31'															AS [ValidTo]
      ,-1																	AS [LineageKey]
UNION ALL
  SELECT
		'CSV|1|0|0|2'														AS [_SourceKey]
      ,'Europe'																AS [Continent]
      ,'Europe'																AS [Region]
      ,'Southern Europe'													AS [Subregion]
      ,'VAT'																AS [Country Code]
      ,'Vatican'															AS [Country]
      ,'Vatican City'														AS [Country Formal Name]
      ,1000																	AS [Country Population]
      ,'N/A'																AS [Province Code]
      ,'N/A'																AS [Province]
      ,-1																	AS [Province Population]
      ,'N/A'																AS [City]
      ,-1																	AS [City Population]
      ,'00165'																AS [Postal Code]
      ,'Porta Pertusa'														AS [Address Line 1]			
      ,'Viale Vaticano, 37, 00165 Roma RM, Italy'							AS [Address Line 2]
      ,'2019-05-17'															AS [ValidFrom]
      ,'9999-12-31'															AS [ValidTo]
      ,-1																	AS [LineageKey]
UNION ALL
  SELECT
		'CSV|1|0|0|3'
      ,'Europe'																AS [Continent]
      ,'Europe'																AS [Region]
      ,'Southern Europe'													AS [Subregion]
      ,'VAT'																AS [Country Code]
      ,'Vatican'															AS [Country]
      ,'Vatican City'														AS [Country Formal Name]
      ,1000																	AS [Country Population]
      ,'N/A'																AS [Province Code]
      ,'N/A'																AS [Province]
      ,-1																	AS [Province Population]
      ,'N/A'																AS [City]
      ,-1																	AS [City Population]
      ,'00165'																AS [Postal Code]
      ,'Cancello Petriano'													AS [Address Line 1]	
      ,'Via Paolo VI 31 00193 Roma Italy, 00120 Città del Vaticano'			AS [Address Line 2]
      ,'2019-05-17'															AS [ValidFrom]
      ,'9999-12-31'															AS [ValidTo]
      ,-1																	AS [LineageKey]
UNION ALL
  SELECT
		'CSV|1|0|0|4'
      ,'Europe'																AS [Continent]
      ,'Europe'																AS [Region]
      ,'Southern Europe'													AS [Subregion]
      ,'VAT'																AS [Country Code]
      ,'Vatican'															AS [Country]
      ,'Vatican City'														AS [Country Formal Name]
      ,1000																	AS [Country Population]
      ,'N/A'																AS [Province Code]
      ,'N/A'																AS [Province]
      ,-1																	AS [Province Population]
      ,'N/A'																AS [City]
      ,-1																	AS [City Population]
      ,'00120'																AS [Postal Code]
      ,'Paul VI Audience Hall'												AS [Address Line 1]	
      ,'Via di Porta Cavalleggeri, 7891, 00165 Roma RM, Italy'				AS [Address Line 2]
      ,'2019-05-17'															AS [ValidFrom]
      ,'9999-12-31'															AS [ValidTo]
      ,-1																	AS [LineageKey]

UNION ALL
  SELECT
		'CSV|152|333|48922|1'
      ,'Asia'
      ,'Asia'
      ,'South-Eastern Asia'
      ,'SGP'
      ,'Singapore'
      ,'Republic of Singapore'
      ,4657542
      ,'CEN'
      ,'Singapore'
      ,-1
      ,'Singapore'
      ,-1
      ,'578775'
      ,'TreeTop Walk'
      ,'601 Island Club Rd, Singapore 578775'
      ,'2019-05-17'
      ,'9999-12-31'
      ,-1
UNION ALL
  SELECT
		'CSV|152|333|48922|2'
      ,'Asia'
      ,'Asia'
      ,'South-Eastern Asia'
      ,'SGP'
      ,'Singapore'
      ,'Republic of Singapore'
      ,4657542
      ,'CEN'
      ,'Singapore'
      ,-1
      ,'Singapore'
      ,-1
      ,'729826'
      ,'River Safari'
      ,'80 Mandai Lake Rd, Singapore 729826'
      ,'2019-05-17'
      ,'9999-12-31'
      ,-1
UNION ALL
  SELECT
		'CSV|152|333|48922|3'
      ,'Asia'
      ,'Asia'
      ,'South-Eastern Asia'
      ,'SGP'
      ,'Singapore'
      ,'Republic of Singapore'
      ,4657542
      ,'CEN'
      ,'Singapore'
      ,-1
      ,'Singapore'
      ,-1
      ,'018956'
      ,'Marina Bay Sands'
      ,'10 Bayfront Ave, Singapore 018956'
      ,'2019-05-17'
      ,'9999-12-31'
      ,-1
UNION ALL
  SELECT
		'CSV|152|333|48922|4'
      ,'Asia'
      ,'Asia'
      ,'South-Eastern Asia'
      ,'SGP'
      ,'Singapore'
      ,'Republic of Singapore'
      ,4657542
      ,'CEN'
      ,'Singapore'
      ,-1
      ,'Singapore'
      ,-1
      ,'538768'
      ,'Punggol Park'
      ,'Hougang Ave 10, Singapore 538768'
      ,'2019-05-17'
      ,'9999-12-31'
      ,-1

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[vw_Products]
AS

SELECT 
	 CONVERT(nvarchar(200),'')								AS [_SourceKey]
	,CONVERT(nvarchar(200),'N/A')							AS [Product Name]
	,CONVERT(nvarchar(200),'N/A')							AS [Product Code]
	,CONVERT(nvarchar(200),'N/A')							AS [Product Description]
	,CONVERT(nvarchar(200),'N/A')							AS [Subcategory]
	,CONVERT(nvarchar(200),'N/A')							AS [Category]
	,CONVERT(nvarchar(200),'N/A')							AS [Department]
	,CONVERT(nvarchar(200),'N/A')							AS [Unit of measure Code]
	,CONVERT(nvarchar(200),'N/A')							AS [Unit of measure Name]
	,CONVERT(decimal(18,2),-1)								AS [Unit Price]
	,CONVERT(nvarchar(200),'N/A')							AS [Discontinued] 
	,CONVERT(datetime, '1753-01-01')						AS [ValidFrom]
	,CONVERT(datetime, '9999-12-31')						AS [ValidTo]
	,CONVERT(int, -1)										AS [LineageKey]


UNION ALL

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
	,CONVERT(datetime, prod.ModifiedDate)					AS [ValidFrom]
	,CONVERT(datetime, '9999-12-31')						AS [ValidTo]
	,CONVERT(int, -1)										AS [LineageKey]

FROM [HappyScoopers_Demo].[dbo].[Products] prod
LEFT JOIN [HappyScoopers_Demo].[dbo].[ProductSubcategories] subcat ON prod.SubcategoryID = subcat.ProductSubcategoryID
LEFT JOIN [HappyScoopers_Demo].[dbo].[ProductCategories] cat ON subcat.ProductCategoryID = cat.CategoryID
LEFT JOIN [HappyScoopers_Demo].[dbo].[ProductDepartments] dep ON cat.DepartmentID = dep.DepartmentID
LEFT JOIN [HappyScoopers_Demo].[dbo].[UnitsOfMeasure] um ON prod.UnitOfMeasureID = um.UnitOfMeasureID
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dim_Customer](
	[Customer Key] [int] IDENTITY(1,1) NOT NULL,
	[_Source Key] [nvarchar](50) NOT NULL,
	[First Name] [nvarchar](100) NOT NULL,
	[Last Name] [nvarchar](100) NOT NULL,
	[Full Name]  AS (([First Name]+' ')+[Last Name]),
	[Title] [nvarchar](30) NOT NULL,
	[Delivery Location Key] [nvarchar](50) NOT NULL,
	[Billing Location Key] [nvarchar](50) NOT NULL,
	[Phone Number] [nvarchar](24) NOT NULL,
	[Email] [nvarchar](100) NULL,
	[Valid From] [datetime] NOT NULL,
	[Valid To] [datetime] NOT NULL,
	[Lineage Key] [int] NOT NULL,
 CONSTRAINT [PK_Dim_Customer] PRIMARY KEY CLUSTERED 
(
	[Customer Key] ASC
)
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dim_Date](
	[Date Key] [int] NOT NULL,
	[Date] [date] NOT NULL,
	[Day] [tinyint] NOT NULL,
	[Day Suffix] [char](2) NOT NULL,
	[Weekday] [tinyint] NOT NULL,
	[Weekday Name] [varchar](10) NOT NULL,
	[Weekday Name Short] [char](3) NOT NULL,
	[Weekday Name FirstLetter] [char](1) NOT NULL,
	[Day Of Year] [smallint] NOT NULL,
	[Week Of Month] [tinyint] NOT NULL,
	[Week Of Year] [tinyint] NOT NULL,
	[Month] [tinyint] NOT NULL,
	[Month Name] [varchar](10) NOT NULL,
	[Month Name Short] [char](3) NOT NULL,
	[Month Name FirstLetter] [char](1) NOT NULL,
	[Quarter] [tinyint] NOT NULL,
	[Quarter Name] [varchar](6) NOT NULL,
	[Year] [int] NOT NULL,
	[MMYYYY] [char](6) NOT NULL,
	[Month Year] [char](7) NOT NULL,
	[Is Weekend] [bit] NOT NULL,
	[Is Holiday] [bit] NOT NULL,
	[Holiday Name] [varchar](20) NOT NULL,
	[Special Day] [varchar](20) NOT NULL,
	[First Date Of Year] [date] NULL,
	[Last Date Of Year] [date] NULL,
	[First Date Of Quater] [date] NULL,
	[Last Date Of Quater] [date] NULL,
	[First Date Of Month] [date] NULL,
	[Last Date Of Month] [date] NULL,
	[First Date Of Week] [date] NULL,
	[Last Date Of Week] [date] NULL,
	[Lineage Key] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Date Key] ASC
)
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dim_Employee](
	[Employee Key] [int] IDENTITY(1,1) NOT NULL,
	[_Source Key] [nvarchar](50) NOT NULL,
	[Location Key] [nvarchar](50) NOT NULL,
	[Last Name] [nvarchar](100) NOT NULL,
	[First Name] [nvarchar](100) NOT NULL,
	[Title] [nvarchar](30) NOT NULL,
	[Birth Date] [datetime] NOT NULL,
	[Gender] [nchar](10) NOT NULL,
	[Hire Date] [datetime] NOT NULL,
	[Job Title] [nvarchar](100) NOT NULL,
	[Address Line] [nvarchar](100) NULL,
	[City] [nvarchar](100) NULL,
	[Country] [nvarchar](100) NULL,
	[Manager Key] [nvarchar](50) NULL,
	[Valid From] [datetime] NOT NULL,
	[Valid To] [datetime] NOT NULL,
	[Lineage Key] [int] NOT NULL,
 CONSTRAINT [PK_Dim_Employee] PRIMARY KEY CLUSTERED 
(
	[Employee Key] ASC
)
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dim_Location](
	[Location Key] [int] IDENTITY(1,1) NOT NULL,
	[_Source Key] [nvarchar](200) NOT NULL,
	[Continent] [nvarchar](200) NOT NULL,
	[Region] [nvarchar](200) NOT NULL,
	[Subregion] [nvarchar](200) NOT NULL,
	[Country Code] [nvarchar](200) NULL,
	[Country] [nvarchar](200) NOT NULL,
	[Country Formal Name] [nvarchar](200) NOT NULL,
	[Country Population] [bigint] NULL,
	[Province Code] [nvarchar](200) NOT NULL,
	[Province] [nvarchar](200) NOT NULL,
	[Province Population] [bigint] NULL,
	[City] [nvarchar](200) NOT NULL,
	[City Population] [bigint] NULL,
	[Address Line 1] [nvarchar](200) NOT NULL,
	[Address Line 2] [nvarchar](200) NULL,
	[Postal Code] [nvarchar](200) NOT NULL,
	[Valid From] [datetime] NOT NULL,
	[Valid To] [datetime] NOT NULL,
	[Lineage Key] [int] NOT NULL,
 CONSTRAINT [PK_Dim_Location] PRIMARY KEY CLUSTERED 
(
	[Location Key] ASC
)
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dim_PaymentType](
	[Payment Type Key] [int] IDENTITY(1,1) NOT NULL,
	[_Source Key] [nvarchar](50) NOT NULL,
	[Payment Type Name] [nvarchar](50) NOT NULL,
	[Valid From] [datetime] NOT NULL,
	[Valid To] [datetime] NOT NULL,
	[Lineage Key] [int] NOT NULL,
 CONSTRAINT [PK_Dim_PaymentType] PRIMARY KEY CLUSTERED 
(
	[Payment Type Key] ASC
)
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dim_Product](
	[Product Key] [int] IDENTITY(1,1) NOT NULL,
	[_Source Key] [nvarchar](50) NOT NULL,
	[Product Name] [nvarchar](200) NOT NULL,
	[Product Code] [nvarchar](50) NOT NULL,
	[Product Description] [nvarchar](200) NOT NULL,
	[Product Subcategory] [nvarchar](200) NOT NULL,
	[Product Category] [nvarchar](200) NOT NULL,
	[Product Department] [nvarchar](200) NOT NULL,
	[Unit Of Measure Code] [nvarchar](10) NOT NULL,
	[Unit Of Measure Name] [nvarchar](50) NOT NULL,
	[Unit Price] [decimal](18, 2) NOT NULL,
	[Discontinued] [nvarchar](10) NOT NULL,
	[Valid From] [datetime] NOT NULL,
	[Valid To] [datetime] NOT NULL,
	[Lineage Key] [int] NOT NULL,
 CONSTRAINT [PK_Dim_Product] PRIMARY KEY CLUSTERED 
(
	[Product Key] ASC
)
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dim_Promotion](
	[Promotion Key] [int] IDENTITY(1,1) NOT NULL,
	[_Source Key] [nvarchar](50) NOT NULL,
	[Deal Description] [nvarchar](30) NOT NULL,
	[Start Date] [date] NOT NULL,
	[End Date] [date] NOT NULL,
	[Discount Amount] [decimal](18, 2) NULL,
	[Discount Percentage] [decimal](18, 3) NULL,
	[Valid From] [datetime] NOT NULL,
	[Valid To] [datetime] NOT NULL,
	[Lineage Key] [int] NOT NULL,
 CONSTRAINT [PK_Dim_Promotion] PRIMARY KEY CLUSTERED 
(
	[Promotion Key] ASC
)
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Fact_Sales](
	[Sale Key] [bigint] IDENTITY(1,1) NOT NULL,
	[Customer Key] [int] NOT NULL,
	[Employee Key] [int] NOT NULL,
	[Product Key] [int] NOT NULL,
	[Payment Type Key] [int] NOT NULL,
	[Order Date Key] [int] NOT NULL,
	[Delivery Date Key] [int] NULL,
	[Delivery Location Key] [int] NULL,
	[Promotion Key] [int] NOT NULL,
	[Description] [nvarchar](100) NOT NULL,
	[Package] [nvarchar](50) NOT NULL,
	[Quantity] [int] NULL,
	[Unit Price] [decimal](18, 2) NULL,
	[VAT Rate] [decimal](18, 3) NULL,
	[Total Excluding VAT] [decimal](18, 2) NULL,
	[VAT Amount] [decimal](18, 2) NULL,
	[Total Including VAT] [decimal](18, 2) NULL,
	[_SourceOrder] [nvarchar](50) NOT NULL,
	[_SourceOrderLine] [nvarchar](50) NOT NULL,
	[Lineage Key] [int] NOT NULL
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Staging_Customer](
	[Customer Key] [int] IDENTITY(1,1) NOT NULL,
	[_Source Key] [nvarchar](50) NOT NULL,
	[First Name] [nvarchar](100) NOT NULL,
	[Last Name] [nvarchar](100) NOT NULL,
	[Full Name] [nvarchar](50) NOT NULL,
	[Title] [nvarchar](30) NOT NULL,
	[Delivery Location Key] [nvarchar](50) NOT NULL,
	[Billing Location Key] [nvarchar](50) NOT NULL,
	[Phone Number] [nvarchar](24) NOT NULL,
	[Email] [nvarchar](100) NULL,
	[Customer Modified Date] [datetime] NOT NULL,
	[Delivery Addr Modified Date] [datetime] NOT NULL,
	[Billing Addr Modified Date] [datetime] NOT NULL,
	[Valid From] [datetime] NOT NULL,
	[Valid To] [datetime] NOT NULL
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Staging_Employee](
	[Employee Key] [int] IDENTITY(1,1) NOT NULL,
	[_Source Key] [nvarchar](50) NOT NULL,
	[Location Key] [nvarchar](50) NOT NULL,
	[Last Name] [nvarchar](100) NOT NULL,
	[First Name] [nvarchar](100) NOT NULL,
	[Title] [nvarchar](30) NOT NULL,
	[Birth Date] [datetime] NOT NULL,
	[Gender] [nchar](10) NOT NULL,
	[Hire Date] [datetime] NOT NULL,
	[Job Title] [nvarchar](100) NOT NULL,
	[Address Line] [nvarchar](100) NULL,
	[City] [nvarchar](100) NULL,
	[Country] [nvarchar](100) NULL,
	[Manager Key] [nvarchar](50) NULL,
	[Employee Modified Date] [datetime] NOT NULL,
	[Addresses Modified Date] [datetime] NOT NULL,
	[Valid From] [datetime] NOT NULL,
	[Valid To] [datetime] NOT NULL
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Staging_Location](
	[Location Key] [int] IDENTITY(1,1) NOT NULL,
	[_Source Key] [nvarchar](200) NOT NULL,
	[Continent] [nvarchar](200) NOT NULL,
	[Region] [nvarchar](200) NOT NULL,
	[Subregion] [nvarchar](200) NOT NULL,
	[Country Code] [nvarchar](200) NULL,
	[Country] [nvarchar](200) NOT NULL,
	[Country Formal Name] [nvarchar](200) NOT NULL,
	[Country Population] [bigint] NULL,
	[Province Code] [nvarchar](200) NOT NULL,
	[Province] [nvarchar](200) NOT NULL,
	[Province Population] [bigint] NULL,
	[City] [nvarchar](200) NOT NULL,
	[City Population] [bigint] NULL,
	[Address Line 1] [nvarchar](200) NOT NULL,
	[Address Line 2] [nvarchar](200) NULL,
	[Postal Code] [nvarchar](200) NOT NULL,
	[Address Modified Date] [datetime] NOT NULL,
	[City Modified Date] [datetime] NOT NULL,
	[Province Modified Date] [datetime] NOT NULL,
	[Country Modified Date] [datetime] NOT NULL,
	[Valid From] [datetime] NOT NULL,
	[Valid To] [datetime] NOT NULL
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Staging_PaymentType](
	[Payment Type Key] [int] IDENTITY(1,1) NOT NULL,
	[_Source Key] [nvarchar](50) NOT NULL,
	[Payment Type Name] [nvarchar](50) NOT NULL,
	[Valid From] [datetime] NOT NULL,
	[Valid To] [datetime] NOT NULL
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Staging_Product](
	[Product Key] [int] IDENTITY(1,1) NOT NULL,
	[_Source Key] [nvarchar](50) NOT NULL,
	[Product Name] [nvarchar](200) NOT NULL,
	[Product Code] [nvarchar](50) NOT NULL,
	[Product Description] [nvarchar](200) NOT NULL,
	[Product Subcategory] [nvarchar](200) NOT NULL,
	[Product Category] [nvarchar](200) NOT NULL,
	[Product Department] [nvarchar](200) NOT NULL,
	[Unit Of Measure Code] [nvarchar](10) NOT NULL,
	[Unit Of Measure Name] [nvarchar](50) NOT NULL,
	[Unit Price] [decimal](18, 2) NOT NULL,
	[Discontinued] [nvarchar](10) NOT NULL,
	[Product Modified Date] [datetime] NOT NULL,
	[Subcategory Modified Date] [datetime] NOT NULL,
	[Category Modified Date] [datetime] NOT NULL,
	[Department Modified Date] [datetime] NOT NULL,
	[UM Modified Date] [datetime] NOT NULL,
	[Valid From] [datetime] NOT NULL,
	[Valid To] [datetime] NOT NULL
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Staging_Promotion](
	[Promotion Key] [int] IDENTITY(1,1) NOT NULL,
	[_Source Key] [nvarchar](50) NOT NULL,
	[Deal Description] [nvarchar](30) NOT NULL,
	[Start Date] [date] NOT NULL,
	[End Date] [date] NOT NULL,
	[Discount Amount] [decimal](18, 2) NULL,
	[Discount Percentage] [decimal](18, 3) NULL,
	[Modified Date] [datetime] NOT NULL,
	[Valid From] [datetime] NOT NULL,
	[Valid To] [datetime] NOT NULL
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Staging_Sales](
	[Staging Sale Key] [bigint] IDENTITY(1,1) NOT NULL,
	[Customer Key] [int] NULL,
	[Employee Key] [int] NULL,
	[Product Key] [int] NULL,
	[Payment Type Key] [int] NULL,
	[Order Date Key] [int] NULL,
	[Delivery Date Key] [int] NULL,
	[Delivery Location Key] [int] NULL,
	[Promotion Key] [int] NULL,
	[Description] [nvarchar](100) NULL,
	[Package] [nvarchar](50) NULL,
	[Quantity] [int] NULL,
	[Unit Price] [decimal](18, 2) NULL,
	[VAT Rate] [decimal](18, 3) NULL,
	[Total Excluding VAT] [decimal](18, 2) NULL,
	[VAT Amount] [decimal](18, 2) NULL,
	[Total Including VAT] [decimal](18, 2) NULL,
	[ModifiedDate] [datetime] NULL,
	[_SourceOrder] [nvarchar](50) NULL,
	[_SourceOrderLine] [nvarchar](50) NULL,
	[_SourceCustomerKey] [int] NULL,
	[_SourceEmployeeKey] [int] NULL,
	[_SourceProductKey] [int] NULL,
	[_SourcePaymentTypeKey] [int] NULL,
	[_SourceOrderDateKey] [date] NULL,
	[_SourceDeliveryDateKey] [date] NULL,
	[_SourceDeliveryCountryKey] [int] NULL,
	[_SourceDeliveryProvinceKey] [int] NULL,
	[_SourceDeliveryCityKey] [int] NULL,
	[_SourceDeliveryAddressKey] [int] NULL,
	[_SourceDeliveryLocationKey] [nvarchar](50) NULL,
	[_SourcePromotionKey] [int] NULL
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [int].[IncrementalLoads](
	[LoadDateKey] [int] IDENTITY(1,1) NOT NULL,
	[TableName] [nvarchar](100) NOT NULL,
	[LoadDate] [datetime] NOT NULL,
 CONSTRAINT [PK_LoadDates] PRIMARY KEY CLUSTERED 
(
	[LoadDateKey] ASC
)
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [int].[Lineage](
	[LineageKey] [int] IDENTITY(1,1) NOT NULL,
	[TableName] [nvarchar](200) NOT NULL,
	[StartLoad] [datetime] NOT NULL,
	[FinishLoad] [datetime] NULL,
	[LastLoadedDate] [datetime] NOT NULL,
	[Status] [nvarchar](1) NOT NULL,
	[Type] [nvarchar](1) NOT NULL,
 CONSTRAINT [PK_Integration_Lineage] PRIMARY KEY CLUSTERED 
(
	[LineageKey] ASC
)
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Dim_Date] ADD  DEFAULT ((0)) FOR [Is Weekend]
GO
ALTER TABLE [dbo].[Dim_Date] ADD  DEFAULT ((0)) FOR [Is Holiday]
GO
ALTER TABLE [dbo].[Dim_Date] ADD  DEFAULT ('') FOR [Holiday Name]
GO
ALTER TABLE [dbo].[Dim_Date] ADD  DEFAULT ('') FOR [Special Day]
GO
ALTER TABLE [int].[Lineage] ADD  CONSTRAINT [DF_Lineage_Status]  DEFAULT (N'P') FOR [Status]
GO
ALTER TABLE [int].[Lineage] ADD  CONSTRAINT [DF_Lineage_Type]  DEFAULT (N'F') FOR [Type]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[Load_DimCustomer]
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @EndOfTime datetime =  '9999-12-31';
	DECLARE @LastDateLoaded datetime;

    BEGIN TRAN;

    -- Get the lineage of the current load of Dim_Customer
	DECLARE @LineageKey int = (SELECT TOP(1) [LineageKey]
                               FROM int.Lineage
                               WHERE [TableName] = N'Dim_Customer'
                               AND [FinishLoad] IS NULL
                               ORDER BY [LineageKey] DESC);

	-- Update the validity date of modified customers in Dim_Customer. 
	-- The rows will not be active anymore, because the staging table holds newer versions
    UPDATE initial
    SET initial.[Valid To] = modif.[Valid From]
    FROM 
		Dim_Customer AS initial INNER JOIN 
		Staging_Customer AS modif ON initial.[_Source Key] = modif.[_Source Key]
    WHERE initial.[Valid To] = @EndOfTime

    IF NOT EXISTS (SELECT 1 FROM Dim_Customer WHERE [_Source Key] = '')
		INSERT Dim_Customer
           ([_Source Key]
           ,[First Name]
           ,[Last Name]
           ,[Title]
           ,[Delivery Location Key]
           ,[Billing Location Key]
           ,[Phone Number]
           ,[Email]
           ,[Valid From]
           ,[Valid To]
           ,[Lineage Key])
	VALUES ('', 'N/A', 'N/A', 'N/A', '', '', 'N/A', 'N/A', '1753-01-01', '9999-12-31', -1)
	
	-- Insert new rows for the modified Customers
	INSERT Dim_Customer
           ([_Source Key]
           ,[First Name]
           ,[Last Name]
           ,[Title]
           ,[Delivery Location Key]
           ,[Billing Location Key]
           ,[Phone Number]
           ,[Email]
           ,[Valid From]
           ,[Valid To]
           ,[Lineage Key])
    
	SELECT  [_Source Key]
           ,[First Name]
           ,[Last Name]
           ,[Title]
           ,[Delivery Location Key]
           ,[Billing Location Key]
           ,[Phone Number]
           ,[Email]
           ,[Valid From]
           ,[Valid To]
           ,@LineageKey
    FROM Staging_Customer;

    
	-- Update the lineage table for the most current Dim_Customer load with the finish date and 
	-- 'S' in the Status column, meaning that the load finished successfully
	UPDATE [int].Lineage
        SET 
			FinishLoad = SYSDATETIME(),
            Status = 'S',
			@LastDateLoaded = LastLoadedDate
    WHERE [LineageKey] = @LineageKey;
	 
	
	-- Update the LoadDates table with the most current load date for Dim_Customer
	UPDATE [int].[IncrementalLoads]
        SET [LoadDate] = @LastDateLoaded
    WHERE [TableName] = N'Dim_Customer';

    -- All these tasks happen together or don't happen at all. 
	COMMIT;

    RETURN 0;
END;
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[Load_DimDate]
@StartDate DATE = '2000-01-01',
@EndDate DATE = '2025-12-31'

AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

	TRUNCATE TABLE [Dim_Date];


    DECLARE @EndOfTime datetime =  '9999-12-31';
	DECLARE @LastDateLoaded datetime;

    BEGIN TRAN;

	INSERT INTO [int].[Lineage](
		 [TableName]
		,[StartLoad]
		,[FinishLoad]
		,[Status]
		,[Type]
		,[LastLoadedDate]
		)
	VALUES (
		 'Dim_Date'
		,GETDATE()
		,NULL
		,'P'
		,'F'
		,GETDATE()
		);
-- Get the lineage of the current load of Dim_Date
DECLARE @LineageKey int = (SELECT TOP(1) [LineageKey]
                               FROM int.Lineage
                               WHERE [TableName] = N'Dim_Date'
                               AND [FinishLoad] IS NULL
                               ORDER BY [LineageKey] DESC);

IF NOT EXISTS (SELECT 1 FROM Dim_Date WHERE [Date Key] = 0)
INSERT INTO [dbo].[Dim_Date]
           ([Date Key]
           ,[Date]
           ,[Day]
           ,[Day Suffix]
           ,[Weekday]
           ,[Weekday Name]
           ,[Weekday Name Short]
           ,[Weekday Name FirstLetter]
           ,[Day Of Year]
           ,[Week Of Month]
           ,[Week Of Year]
           ,[Month]
           ,[Month Name]
           ,[Month Name Short]
           ,[Month Name FirstLetter]
           ,[Quarter]
           ,[Quarter Name]
           ,[Year]
           ,[MMYYYY]
           ,[Month Year]
           ,[Is Weekend]
           ,[Is Holiday]
           ,[Holiday Name]
           ,[Special Day]
           ,[First Date Of Year]
           ,[Last Date Of Year]
           ,[First Date Of Quater]
           ,[Last Date Of Quater]
           ,[First Date Of Month]
           ,[Last Date Of Month]
           ,[First Date Of Week]
           ,[Last Date Of Week]
           ,[Lineage Key])
     VALUES
           (0,'1753-01-01',0,'',0,'N/A','N/A','',0,0,0,0,'N/A','N/A','',0,'N/A',0,'N/A','N/A',0,0,'N/A','N/A','1753-01-01','1753-01-01','1753-01-01','1753-01-01','1753-01-01','1753-01-01','1753-01-01','1753-01-01',-1)


WHILE @StartDate < @EndDate
BEGIN
   INSERT INTO [dbo].[Dim_Date] (
			[Date Key]
           ,[Date]
           ,[Day]
           ,[Day Suffix]
           ,[Weekday]
           ,[Weekday Name]
           ,[Weekday Name Short]
           ,[Weekday Name FirstLetter]
           ,[Day Of Year]
           ,[Week Of Month]
           ,[Week Of Year]
           ,[Month]
           ,[Month Name]
           ,[Month Name Short]
           ,[Month Name FirstLetter]
           ,[Quarter]
           ,[Quarter Name]
           ,[Year]
           ,[MMYYYY]
           ,[Month Year]
           ,[Is Weekend]
           ,[Is Holiday]
           ,[Holiday Name]
           ,[Special Day]
           ,[First Date Of Year]
           ,[Last Date Of Year]
           ,[First Date Of Quater]
           ,[Last Date Of Quater]
           ,[First Date Of Month]
           ,[Last Date Of Month]
           ,[First Date Of Week]
           ,[Last Date Of Week]
		   ,[Lineage Key]
      )
   SELECT DateKey = YEAR(@StartDate) * 10000 + MONTH(@StartDate) * 100 + DAY(@StartDate),
      DATE = @StartDate,
      Day = DAY(@StartDate),
      [DaySuffix] = CASE 
         WHEN DAY(@StartDate) = 1
            OR DAY(@StartDate) = 21
            OR DAY(@StartDate) = 31
            THEN 'st'
         WHEN DAY(@StartDate) = 2
            OR DAY(@StartDate) = 22
            THEN 'nd'
         WHEN DAY(@StartDate) = 3
            OR DAY(@StartDate) = 23
            THEN 'rd'
         ELSE 'th'
         END,
      WEEKDAY = DATEPART(dw, @StartDate),
      WeekDayName = DATENAME(dw, @StartDate),
      WeekDayName_Short = UPPER(LEFT(DATENAME(dw, @StartDate), 3)),
      WeekDayName_FirstLetter = LEFT(DATENAME(dw, @StartDate), 1),
      [DayOfYear] = DATENAME(dy, @StartDate),
      [WeekOfMonth] = DATEPART(WEEK, @StartDate) - DATEPART(WEEK, DATEADD(MM, DATEDIFF(MM, 0, @StartDate), 0)) + 1,
      [WeekOfYear] = DATEPART(wk, @StartDate),
      [Month] = MONTH(@StartDate),
      [MonthName] = DATENAME(mm, @StartDate),
      [MonthName_Short] = UPPER(LEFT(DATENAME(mm, @StartDate), 3)),
      [MonthName_FirstLetter] = LEFT(DATENAME(mm, @StartDate), 1),
      [Quarter] = DATEPART(q, @StartDate),
      [QuarterName] = CASE 
         WHEN DATENAME(qq, @StartDate) = 1
            THEN 'First'
         WHEN DATENAME(qq, @StartDate) = 2
            THEN 'second'
         WHEN DATENAME(qq, @StartDate) = 3
            THEN 'third'
         WHEN DATENAME(qq, @StartDate) = 4
            THEN 'fourth'
         END,
      [Year] = YEAR(@StartDate),
      [MMYYYY] = RIGHT('0' + CAST(MONTH(@StartDate) AS VARCHAR(2)), 2) + CAST(YEAR(@StartDate) AS VARCHAR(4)),
      [MonthYear] = CAST(YEAR(@StartDate) AS VARCHAR(4)) + UPPER(LEFT(DATENAME(mm, @StartDate), 3)),
      [IsWeekend] = CASE 
         WHEN DATENAME(dw, @StartDate) = 'Sunday'
            OR DATENAME(dw, @StartDate) = 'Saturday'
            THEN 1
         ELSE 0
         END,
      [IsHoliday] = 0,
[HolidayName] =	CONVERT(varchar(20), ''),
[SpecialDays] =	CONVERT(varchar(20), ''),
[FirstDateofYear]   = CAST(CAST(YEAR(@StartDate) AS VARCHAR(4)) + '-01-01' AS DATE),
[LastDateofYear]    = CAST(CAST(YEAR(@StartDate) AS VARCHAR(4)) + '-12-31' AS DATE),
[FirstDateofQuater] = DATEADD(qq, DATEDIFF(qq, 0, GETDATE()), 0),
[LastDateofQuater]  = DATEADD(dd, - 1, DATEADD(qq, DATEDIFF(qq, 0, GETDATE()) + 1, 0)),
[FirstDateofMonth]  = CAST(CAST(YEAR(@StartDate) AS VARCHAR(4)) + '-' + CAST(MONTH(@StartDate) AS VARCHAR(2)) + '-01' AS DATE),
[LastDateofMonth]   = EOMONTH(@StartDate),
[FirstDateofWeek]   = DATEADD(dd, - (DATEPART(dw, @StartDate) - 1), @StartDate),
[LastDateofWeek] = DATEADD(dd, 7 - (DATEPART(dw, @StartDate)), @StartDate),
@LineageKey


   SET @StartDate = DATEADD(DD, 1, @StartDate)
END

--Update Holiday information
UPDATE Dim_Date
SET [Is Holiday] = 1,
   [Holiday Name] = 'Christmas'
WHERE [Month] = 12
   AND [Day] = 25

UPDATE Dim_Date
SET [Special Day] = 'Valentines Day'
WHERE [Month] = 2
   AND [Day] = 14


SELECT * FROM Dim_Date


    
	-- Update the lineage table for the most current Dim_Date load with the finish date and 
	-- 'S' in the Status column, meaning that the load finished successfully
	UPDATE [int].Lineage
        SET 
			FinishLoad = SYSDATETIME(),
            Status = 'S',
			@LastDateLoaded = LastLoadedDate
    WHERE [LineageKey] = @LineageKey;
	 
	
	-- Update the LoadDates table with the most current load date for Dim_Date
	UPDATE [int].[IncrementalLoads]
        SET [LoadDate] = @LastDateLoaded
    WHERE [TableName] = N'Dim_Date';

    -- All these tasks happen together or don't happen at all. 
	COMMIT;

    RETURN 0;
END;
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[Load_DimEmployee]
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @EndOfTime datetime =  '9999-12-31';
	DECLARE @LastDateLoaded datetime;

    BEGIN TRAN;

    -- Get the lineage of the current load of Dim_Product
	DECLARE @LineageKey int = (SELECT TOP(1) [LineageKey]
                               FROM int.Lineage
                               WHERE [TableName] = N'Dim_Employee'
                               AND [FinishLoad] IS NULL
                               ORDER BY [LineageKey] DESC);

    IF NOT EXISTS (SELECT * FROM Dim_Employee WHERE [_Source Key] = '')
		INSERT INTO [dbo].[Dim_Employee]
				   ([_Source Key]
				   ,[Location Key]
				   ,[Last Name]
				   ,[First Name]
				   ,[Title]
				   ,[Birth Date]
				   ,[Gender]
				   ,[Hire Date]
				   ,[Job Title]
				   ,[Address Line]
				   ,[City]
				   ,[Country]
				   ,[Manager Key]
				   ,[Valid From]
				   ,[Valid To]
				   ,[Lineage Key])
			 VALUES
				   ('', '', 'N/A', 'N/A', 'N/A', '1753-01-01', 'N/A', '1753-01-01', 'N/A', 'N/A', 'N/A', 'N/A', '', '1753-01-01', '9999-12-31', -1)

	-- Update the validity date of modified products in Dim_Product. 
	-- The rows will not be active anymore, because the staging table holds newer versions
    UPDATE emp
    SET emp.[Valid To] = mod_emp.[Valid From]
    FROM 
		Dim_Employee AS emp INNER JOIN 
		Staging_Employee AS mod_emp ON emp.[_Source Key] = mod_emp.[_Source Key]
    WHERE emp.[Valid To] = @EndOfTime

    -- Insert new rows for the modified products
	INSERT Dim_Employee
		   ([_Source Key]
           ,[Location Key]
           ,[Last Name]
           ,[First Name]
           ,[Title]
           ,[Birth Date]
           ,[Gender]
           ,[Hire Date]
           ,[Job Title]
           ,[Address Line]
           ,[City]
           ,[Country]
           ,[Manager Key]
           ,[Valid From]
           ,[Valid To]
           ,[Lineage Key])
    SELECT [_Source Key]
           ,[Location Key]
           ,[Last Name]
           ,[First Name]
           ,[Title]
           ,[Birth Date]
           ,[Gender]
           ,[Hire Date]
           ,[Job Title]
           ,[Address Line]
           ,[City]
           ,[Country]
           ,[Manager Key]
           ,[Valid From]
           ,[Valid To]
           ,@LineageKey
    FROM Staging_Employee;

    
	-- Update the lineage table for the most current Dim_Product load with the finish date and 
	-- 'S' in the Status column, meaning that the load finished successfully
	UPDATE [int].Lineage
        SET 
			FinishLoad = SYSDATETIME(),
            Status = 'S',
			@LastDateLoaded = LastLoadedDate
    WHERE [LineageKey] = @LineageKey;
	 
	
	-- Update the LoadDates table with the most current load date for Dim_Product
	UPDATE [int].[IncrementalLoads]
        SET [LoadDate] = @LastDateLoaded
    WHERE [TableName] = N'Dim_Employee';

    -- All these tasks happen together or don't happen at all. 
	COMMIT;

    RETURN 0;
END;
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE  PROCEDURE [dbo].[Load_DimLocation]
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @EndOfTime datetime =  '9999-12-31';
	DECLARE @LastDateLoaded datetime;

    BEGIN TRAN;

    -- Get the lineage of the current load of Dim_Product
	DECLARE @LineageKey int = (SELECT TOP(1) [LineageKey]
                               FROM int.Lineage
                               WHERE [TableName] = N'Dim_Location'
                               AND [FinishLoad] IS NULL
                               ORDER BY [LineageKey] DESC);

	IF NOT EXISTS (SELECT * FROM Dim_Location WHERE [_Source Key] = '')
INSERT INTO [dbo].[Dim_Location]
           ([_Source Key]
           ,[Continent]
           ,[Region]
           ,[Subregion]
           ,[Country Code]
           ,[Country]
           ,[Country Formal Name]
           ,[Country Population]
           ,[Province Code]
           ,[Province]
           ,[Province Population]
           ,[City]
           ,[City Population]
           ,[Address Line 1]
           ,[Address Line 2]
           ,[Postal Code]
           ,[Valid From]
           ,[Valid To]
           ,[Lineage Key])
     VALUES
           ('', 'N/A', 'N/A','N/A','N/A','N/A','N/A', -1, 'N/A','N/A', -1, 'N/A', -1, 'N/A', 'N/A', 'N/A', '1753-01-01', '9999-12-31', -1)

	-- Update the validity date of modified products in Dim_Location. 
	-- The rows will not be active anymore, because the staging table holds newer versions
    UPDATE initial
    SET initial.[Valid To] = modif.[Valid From]
    FROM 
		Dim_Location AS initial INNER JOIN 
		Staging_Location AS modif ON initial.[_Source Key] = modif.[_Source Key]
    WHERE initial.[Valid To] = @EndOfTime

    -- Insert new rows for the modified products
	INSERT Dim_Location
           ([_Source Key]
           ,[Continent]
           ,[Region]
           ,[Subregion]
           ,[Country Code]
           ,[Country]
           ,[Country Formal Name]
           ,[Country Population]
           ,[Province Code]
           ,[Province]
           ,[Province Population]
           ,[City]
           ,[City Population]
           ,[Address Line 1]
           ,[Address Line 2]
           ,[Postal Code]
           ,[Valid From]
           ,[Valid To]
           ,[Lineage Key])
    
	SELECT  [_Source Key]
           ,[Continent]
           ,[Region]
           ,[Subregion]
           ,[Country Code]
           ,[Country]
           ,[Country Formal Name]
           ,[Country Population]
           ,[Province Code]
           ,[Province]
           ,[Province Population]
           ,[City]
           ,[City Population]
           ,[Address Line 1]
           ,[Address Line 2]
           ,[Postal Code]
           ,[Valid From]
           ,[Valid To]
           ,@LineageKey
    FROM Staging_Location;

    
	-- Update the lineage table for the most current Dim_Location load with the finish date and 
	-- 'S' in the Status column, meaning that the load finished successfully
	UPDATE [int].Lineage
        SET 
			FinishLoad = SYSDATETIME(),
            Status = 'S',
			@LastDateLoaded = LastLoadedDate
    WHERE [LineageKey] = @LineageKey;
	 
	
	-- Update the LoadDates table with the most current load date for Dim_Product
	UPDATE [int].[IncrementalLoads]
        SET [LoadDate] = @LastDateLoaded
    WHERE [TableName] = N'Dim_Location';

    -- All these tasks happen together or don't happen at all. 
	COMMIT;

    RETURN 0;
END;
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE    PROCEDURE [dbo].[Load_DimPaymentType]
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @EndOfTime datetime =  '9999-12-31';
	DECLARE @LastDateLoaded datetime;

    BEGIN TRAN;

    -- Get the lineage of the current load of Dim_PaymentType
	DECLARE @LineageKey int = (SELECT TOP(1) [LineageKey]
                               FROM int.Lineage
                               WHERE [TableName] = N'Dim_PaymentType'
                               AND [FinishLoad] IS NULL
                               ORDER BY [LineageKey] DESC);

	    IF NOT EXISTS (SELECT * FROM Dim_PaymentType WHERE [_Source Key] = '')
			INSERT INTO [dbo].[Dim_PaymentType]
				   ([_Source Key]
				   ,[Payment Type Name]
				   ,[Valid From]
				   ,[Valid To]
				   ,[Lineage Key])
			 VALUES
				   ('', 'N/A', '1753-01-01', '9999-12-31', -1)

	
	-- Update the validity date of modified PaymentTypes in Dim_PaymentType. 
	-- The rows will not be active anymore, because the staging table holds newer versions
    UPDATE initial
    SET initial.[Valid To] = modif.[Valid From]
    FROM 
		Dim_PaymentType AS initial INNER JOIN 
		Staging_PaymentType AS modif ON initial.[_Source Key] = modif.[_Source Key]
    WHERE initial.[Valid To] = @EndOfTime

    -- Insert new rows for the modified PaymentTypes
	INSERT Dim_PaymentType
           ([_Source Key]
           ,[Payment Type Name]
           ,[Valid From]
           ,[Valid To]
           ,[Lineage Key])
    
	SELECT  [_Source Key]
           ,[Payment Type Name]
           ,[Valid From]
           ,[Valid To]
           ,@LineageKey
    FROM Staging_PaymentType;

    
	-- Update the lineage table for the most current Dim_PaymentType load with the finish date and 
	-- 'S' in the Status column, meaning that the load finished successfully
	UPDATE [int].Lineage
        SET 
			FinishLoad = SYSDATETIME(),
            Status = 'S',
			@LastDateLoaded = LastLoadedDate
    WHERE [LineageKey] = @LineageKey;
	 
	
	-- Update the LoadDates table with the most current load date for Dim_PaymentType
	UPDATE [int].[IncrementalLoads]
        SET [LoadDate] = @LastDateLoaded
    WHERE [TableName] = N'Dim_PaymentType';

    -- All these tasks happen together or don't happen at all. 
	COMMIT;

    RETURN 0;
END;
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Load_DimProduct]
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @EndOfTime datetime =  '9999-12-31';
	DECLARE @LastDateLoaded datetime;

    BEGIN TRAN;

    -- Get the lineage of the current load of Dim_Product
	DECLARE @LineageKey int = (SELECT TOP(1) [LineageKey]
                               FROM int.Lineage
                               WHERE [TableName] = N'Dim_Product'
                               AND [FinishLoad] IS NULL
                               ORDER BY [LineageKey] DESC);


	IF NOT EXISTS (SELECT * FROM Dim_Product WHERE [_Source Key] = '')
INSERT INTO [dbo].[Dim_Product]
           ([_Source Key]
           ,[Product Name]
           ,[Product Code]
           ,[Product Description]
           ,[Product Subcategory]
           ,[Product Category]
           ,[Product Department]
           ,[Unit Of Measure Code]
           ,[Unit Of Measure Name]
           ,[Unit Price]
           ,[Discontinued]
           ,[Valid From]
           ,[Valid To]
           ,[Lineage Key])
     VALUES
           ('', 'N/A', 'N/A','N/A','N/A','N/A','N/A','N/A','N/A', -1, 'N/A', '1753-01-01', '9999-12-31', -1)

	-- Update the validity date of modified products in Dim_Product. 
	-- The rows will not be active anymore, because the staging table holds newer versions
    UPDATE prod
    SET prod.[Valid To] = mprod.[Valid From]
    FROM 
		Dim_Product AS prod INNER JOIN 
		Staging_Product AS mprod ON prod.[_Source Key] = mprod.[_Source Key]
    WHERE prod.[Valid To] = @EndOfTime

    -- Insert new rows for the modified products
	INSERT Dim_Product
		    ([_Source Key]
           ,[Product Name]
           ,[Product Code]
           ,[Product Description]
           ,[Product Subcategory]
           ,[Product Category]
           ,[Product Department]
           ,[Unit Of Measure Code]
           ,[Unit Of Measure Name]
           ,[Unit Price]
           ,[Discontinued]
           ,[Valid From]
           ,[Valid To]
           ,[Lineage Key])
    SELECT [_Source Key]
           ,[Product Name]
           ,[Product Code]
           ,[Product Description]
           ,[Product Subcategory]
           ,[Product Category]
           ,[Product Department]
           ,[Unit Of Measure Code]
           ,[Unit Of Measure Name]
           ,[Unit Price]
           ,[Discontinued]
           ,[Valid From]
           ,[Valid To]
           ,@LineageKey
    FROM Staging_Product;

    
	-- Update the lineage table for the most current Dim_Product load with the finish date and 
	-- 'S' in the Status column, meaning that the load finished successfully
	UPDATE [int].Lineage
        SET 
			FinishLoad = SYSDATETIME(),
            Status = 'S',
			@LastDateLoaded = LastLoadedDate
    WHERE [LineageKey] = @LineageKey;
	 
	
	-- Update the LoadDates table with the most current load date for Dim_Product
	UPDATE [int].[IncrementalLoads]
        SET [LoadDate] = @LastDateLoaded
    WHERE [TableName] = N'Dim_Product';

    -- All these tasks happen together or don't happen at all. 
	COMMIT;

    RETURN 0;
END;
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [dbo].[Load_DimPromotion]
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

    DECLARE @EndOfTime datetime =  '9999-12-31';
	DECLARE @LastDateLoaded datetime;

    BEGIN TRAN;

    -- Get the lineage of the current load of Dim_Promotion
	DECLARE @LineageKey int = (SELECT TOP(1) [LineageKey]
                               FROM int.Lineage
                               WHERE [TableName] = N'Dim_Promotion'
                               AND [FinishLoad] IS NULL
                               ORDER BY [LineageKey] DESC);

	IF NOT EXISTS (SELECT * FROM [Dim_Promotion] WHERE [_Source Key] = '')
		INSERT INTO [dbo].[Dim_Promotion]
				   ([_Source Key]
				   ,[Deal Description]
				   ,[Start Date]
				   ,[End Date]
				   ,[Discount Amount]
				   ,[Discount Percentage]
				   ,[Valid From]
				   ,[Valid To]
				   ,[Lineage Key])
		 VALUES
			   ('', 'N/A', '1753-01-01', '1753-01-01', -1, -1, '1753-01-01', '9999-12-31', -1)


	-- Update the validity date of modified Promotions in Dim_Promotion. 
	-- The rows will not be active anymore, because the staging table holds newer versions
    UPDATE initial
    SET initial.[Valid To] = modif.[Valid From]
    FROM 
		Dim_Promotion AS initial INNER JOIN 
		Staging_Promotion AS modif ON initial.[_Source Key] = modif.[_Source Key]
    WHERE initial.[Valid To] = @EndOfTime

    -- Insert new rows for the modified Promotions
	INSERT Dim_Promotion
           ([_Source Key]
           ,[Deal Description]
           ,[Start Date]
           ,[End Date]
           ,[Discount Amount]
           ,[Discount Percentage]
           ,[Valid From]
           ,[Valid To]
           ,[Lineage Key])
    
	SELECT  [_Source Key]
           ,[Deal Description]
           ,[Start Date]
           ,[End Date]
           ,[Discount Amount]
           ,[Discount Percentage]
           ,[Valid From]
           ,[Valid To]
           ,@LineageKey
    FROM Staging_Promotion;

    
	-- Update the lineage table for the most current Dim_Promotion load with the finish date and 
	-- 'S' in the Status column, meaning that the load finished successfully
	UPDATE [int].Lineage
        SET 
			FinishLoad = SYSDATETIME(),
            Status = 'S',
			@LastDateLoaded = LastLoadedDate
    WHERE [LineageKey] = @LineageKey;
	 
	
	-- Update the LoadDates table with the most current load date for Dim_Promotion
	UPDATE [int].[IncrementalLoads]
        SET [LoadDate] = @LastDateLoaded
    WHERE [TableName] = N'Dim_Promotion';

    -- All these tasks happen together or don't happen at all. 
	COMMIT;

    RETURN 0;
END;
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Create the procedure that populates the Fact_Sales table
CREATE   PROCEDURE [dbo].[Load_FactSales]
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

	DECLARE @EndOfTime datetime =  '9999-12-31';
	DECLARE @LastDateLoaded datetime;


    BEGIN TRAN;

    -- Select the lineage, for logging purposes. This will be used in the ETL load
	DECLARE @LineageKey int = ISNULL((SELECT TOP(1) [LineageKey]
                               FROM int.Lineage
                               WHERE [TableName] = N'Fact_Sales'
                               AND [FinishLoad] IS NULL
                               ORDER BY [LineageKey] DESC), -1);


    -- Update the surrogate key columns in the staging table
    UPDATE s
	SET  s.[Customer Key] = COALESCE((
								SELECT TOP (1) c.[Customer Key]
								FROM Dim_Customer AS c
								WHERE REPLACE(c.[_Source Key], 'HSD|', '') = s.[_SourceCustomerKey]
									--AND c.[Valid To] = '9999-12-31'
									AND s.[ModifiedDate] >= c.[Valid From]
									AND s.[ModifiedDate] < c.[Valid To]
								ORDER BY c.[Valid From]
							), (
								SELECT TOP (1) c.[Customer Key]
								FROM Dim_Customer AS c
								WHERE c.[_Source Key] = ''
							), 0),

		s.[Employee Key] = COALESCE((
								SELECT TOP(1) e.[Employee Key]
                                FROM Dim_Employee AS e
                                WHERE REPLACE(e.[_Source Key], 'HSD|', '') = s.[_SourceEmployeeKey]
                               	--AND e.[Valid To] = '9999-12-31'
							     AND s.[ModifiedDate] >= e.[Valid From]
								 AND s.[ModifiedDate] < e.[Valid To]
								ORDER BY e.[Valid From]
								), (
								SELECT TOP (1) e.[Employee Key]
								FROM Dim_Employee AS e
								WHERE e.[_Source Key] = ''
							), 0),
		s.[Product Key] = COALESCE((
								SELECT TOP(1) p.[Product Key]
                                FROM Dim_Product AS p
                                WHERE REPLACE(p.[_Source Key], 'HSD|', '') = s.[_SourceProductKey]
                                    --AND p.[Valid To] = '9999-12-31'
									AND s.[ModifiedDate] >= p.[Valid From]
                                    AND s.[ModifiedDate] < p.[Valid To]
								ORDER BY p.[Valid From]
								), (
								SELECT TOP (1) p.[Product Key]
								FROM Dim_Product AS p
								WHERE p.[_Source Key] = ''
							), 0),
		s.[Payment Type Key] = COALESCE((
								SELECT TOP(1) pm.[Payment Type Key]
                                FROM Dim_PaymentType AS pm
                                WHERE REPLACE(pm.[_Source Key], 'HSD|', '') = s.[_SourcePaymentTypeKey]
                                    --AND pm.[Valid To] = '9999-12-31'
									AND s.[ModifiedDate] >= pm.[Valid From]
                                    AND s.[ModifiedDate] < pm.[Valid To]
								ORDER BY pm.[Valid From]
								), (
								SELECT TOP (1) pm.[Payment Type Key]
								FROM Dim_PaymentType AS pm
								WHERE pm.[_Source Key] = ''
							), 0),

		s.[Delivery Location Key] = COALESCE((
								SELECT TOP(1) l.[Location Key]
                                FROM Dim_Location AS l
                                WHERE REPLACE(l.[_Source Key], 'HSD|', '') = s.[_SourceDeliveryLocationKey]
                                    --AND l.[Valid To] = '9999-12-31'
									AND s.[ModifiedDate] >= l.[Valid From]
                                    AND s.[ModifiedDate] < l.[Valid To]
								ORDER BY l.[Valid From]
								), (
								SELECT TOP (1) l.[Location Key]
								FROM Dim_Location AS l
								WHERE l.[_Source Key] = ''
							), 0),
		s.[Promotion Key] = COALESCE((
							SELECT TOP(1) p.[Promotion Key]
                            FROM Dim_Promotion AS p
                            WHERE REPLACE(p.[_Source Key], 'HSD|', '') = s.[_SourcePromotionKey]
                                 --AND p.[Valid To] = '9999-12-31'
								 AND s.[ModifiedDate] >= p.[Valid From]
                                 AND s.[ModifiedDate] < p.[Valid To]
							ORDER BY p.[Valid From]
							), (
							SELECT TOP (1) p.[Promotion Key]
							FROM Dim_Promotion AS p
							WHERE p.[_Source Key] = ''
							), 0),
		s.[Order Date Key] = COALESCE((SELECT TOP(1) d.[Date Key]
                                           FROM Dim_Date AS d
                                           WHERE d.[Date] = s.[_SourceOrderDateKey]
									       ), 0),
		s.[Delivery Date Key] = COALESCE((SELECT TOP(1) d.[Date Key]
                                           FROM Dim_Date AS d
                                           WHERE d.[Date] = s.[_SourceDeliveryDateKey]
									       ), 0)
    FROM [dbo].[Staging_Sales] AS s;

    -- Delete data from the fact table that is present now in the staging table 
    DELETE s
    FROM [dbo].[Fact_Sales] AS s
    WHERE s._SourceOrder IN (SELECT [_SourceOrder] FROM [dbo].[Staging_Sales]);

-- Perform a simple insert from staging to the fact
INSERT INTO [dbo].[Fact_Sales]
           ([Customer Key]
           ,[Employee Key]
           ,[Product Key]
           ,[Payment Type Key]
           ,[Order Date Key]
           ,[Delivery Date Key]
           ,[Delivery Location Key]
           ,[Promotion Key]
           ,[Description]
           ,[Package]
           ,[Quantity]
           ,[Unit Price]
           ,[VAT Rate]
           ,[Total Excluding VAT]
           ,[VAT Amount]
           ,[Total Including VAT]
           ,[_SourceOrder]
           ,[_SourceOrderLine]
           ,[Lineage Key])
SELECT 
			[Customer Key]
           ,[Employee Key]
           ,[Product Key]
           ,[Payment Type Key]
           ,[Order Date Key]
           ,[Delivery Date Key]
           ,[Delivery Location Key]
           ,[Promotion Key]
           ,[Description]
           ,[Package]
           ,[Quantity]
           ,[Unit Price]
           ,[VAT Rate]
           ,[Total Excluding VAT]
           ,[VAT Amount]
           ,[Total Including VAT]
           ,[_SourceOrder]
           ,[_SourceOrderLine]
		   ,@LineageKey
	FROM [dbo].[Staging_Sales]

	-- Update the lineage table for the most current Dim_Customer load with the finish date and 
	-- 'S' in the Status column, meaning that the load finished successfully
	UPDATE [int].Lineage
        SET 
			FinishLoad = SYSDATETIME(),
            Status = 'S',
			@LastDateLoaded = LastLoadedDate
    WHERE [LineageKey] = @LineageKey;
	 
	
	-- Update the LoadDates table with the most current load date for Dim_Customer
	UPDATE [int].[IncrementalLoads]
        SET [LoadDate] = ISNULL(@LastDateLoaded, GETDATE())
    WHERE [TableName] = N'Fact_Sales';

    -- All these tasks happen together or don't happen at all. 
	COMMIT;

    RETURN 0;
END;
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE   PROCEDURE [dbo].[Test_LoadDimension]
@LoadType nvarchar(1) = 'F',
@TableName nvarchar(100) = 'Dim_Product',
@LastLoadedDate datetime
AS
BEGIN
-- Declaration of the variables needed in this script
DECLARE @Prev_LastLoadedDate datetime;
DECLARE @LineageKey int;

DECLARE @lineage TABLE (lineage int)
DECLARE @lastload TABLE (load_date datetime)

DECLARE @StagingTableName nvarchar(100) = REPLACE(REPLACE(@TableName, 'Dim_', 'Staging_'), 'Fact_', 'Staging_');
DECLARE @StagingSP nvarchar(50) = 'Load_Staging' + REPLACE(REPLACE(@TableName, 'Dim_', ''), 'Fact_', '');
DECLARE @DimSP nvarchar(50) = 'Load_Dim' + REPLACE(REPLACE(@TableName, 'Dim_', ''), 'Fact_', '');

DECLARE @SQL nvarchar(max);

--STEP 1: If it's an initial load, change the last loaded date from the IncrementalLoads table to be something in the past
IF (@LoadType = 'F')
	UPDATE [int].[IncrementalLoads]
	SET LoadDate = '1753-01-01'
	WHERE TableName = @TableName

--STEP 2: Insert a new row into the lineage table, to keep track of the new Dim_Product load that just started
--STEP 3: Store the key of the new row in the @LineageKey variable, for future usage
INSERT INTO @lineage EXEC [int].[Get_LineageKey] @LoadType, @TableName, @LastLoadedDate
SELECT TOP 1 @LineageKey = lineage FROM @lineage

-- Take a look at the current lineage number
SELECT @LineageKey AS [Current lineage]


--STEP 4: Make sure that the Staging_Product table is empty before loading new information in it
SET @SQL = 'TRUNCATE TABLE ' + @StagingTableName
PRINT @SQL
EXEC sp_executesql @SQL


--STEP 5: Retrieve the date when Dim_Product was last loaded
--STEP 6: Store this date into the @Prev_LastLoadedDate variable
INSERT INTO @lastload EXEC [int].[Get_LastLoadedDate] @TableName
SELECT TOP 1 @Prev_LastLoadedDate = load_date FROM @lastload
SELECT @Prev_LastLoadedDate AS [Date of the previous load]


--STEP 7: Insert into the staging table new products or products that were modified after the last Dim_Product load finished 
SET @SQL = 'INSERT INTO ' + @StagingTableName + ' EXEC [HappyScoopers_Demo].[dbo].' + @StagingSP + ' ''' + CONVERT(nvarchar(20), @Prev_LastLoadedDate, 23) + ''',''' + convert(nvarchar(20), @LastLoadedDate, 23) + ''''
PRINT @SQL
EXEC sp_executesql @SQL

-- For an initial load, truncate also the dimension table
SET @SQL = 'TRUNCATE TABLE ' + @TableName
PRINT @SQL
EXEC sp_executesql @SQL


--STEP 8: Transfer information from the staging table to the actual dimension table: Dim_Product
SET @SQL = 'EXEC ' + @DimSP
PRINT @SQL
EXEC sp_executesql @SQL

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE   PROCEDURE [dbo].[Test_LoadFact]
@LoadType nvarchar(1) = 'F',
@TableName nvarchar(100) = 'Fact_Sales',
@LastLoadedDate datetime
AS
BEGIN
-- Declaration of the variables needed in this script
DECLARE @Prev_LastLoadedDate datetime;
DECLARE @LineageKey int;

DECLARE @lineage TABLE (lineage int)
DECLARE @lastload TABLE (load_date datetime)

--STEP 1: If it's an initial load, change the last loaded date from the IncrementalLoads table to be something in the past
IF (@LoadType = 'F')
	UPDATE [int].[IncrementalLoads]
	SET LoadDate = '1753-01-01'
	WHERE TableName = @TableName

--STEP 2: Insert a new row into the lineage table, to keep track of the new Dim_Product load that just started
--STEP 3: Store the key of the new row in the @LineageKey variable, for future usage
INSERT INTO @lineage EXEC [int].[Get_LineageKey] @LoadType, @TableName, @LastLoadedDate
SELECT TOP 1 @LineageKey = lineage FROM @lineage

-- Take a look at the current lineage number
SELECT @LineageKey AS [Current lineage]


TRUNCATE TABLE Staging_Sales

INSERT INTO @lastload EXEC [int].[Get_LastLoadedDate] @TableName
SELECT TOP 1 @Prev_LastLoadedDate = load_date FROM @lastload
SELECT @Prev_LastLoadedDate AS [Date of the previous load]


--STEP 7: Insert into the staging table new products or products that were modified after the last Dim_Product load finished 
	INSERT INTO [dbo].[Staging_Sales]
		([_SourceOrderDateKey],
		[_SourceDeliveryDateKey],
		[_SourceOrder],
		[_SourceOrderLine],
		[_SourceCustomerKey],
		[_SourceEmployeeKey],
		[_SourceProductKey],
		[_SourcePaymentTypeKey],
		[_SourceDeliveryCountryKey],
		[_SourceDeliveryProvinceKey],
		[_SourceDeliveryCityKey],
		[_SourceDeliveryAddressKey],
		[_SourceDeliveryLocationKey],
		[_SourcePromotionKey],
		[Description],
		[Package],
		[Quantity],
		[Unit Price],
		[VAT Rate],
		[Total Excluding VAT],
		[VAT Amount],
		[Total Including VAT],
		[ModifiedDate]
		)
	EXEC [HappyScoopers_Demo].[dbo].[Load_StagingSales] @Prev_LastLoadedDate, @LastLoadedDate


TRUNCATE TABLE Fact_Sales

-- Populate the fact table
EXEC [dbo].[Load_FactSales]

END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [int].[Get_LastLoadedDate]
@TableName nvarchar(100)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

	-- If the procedure is executed with a wrong table name, throw an error.
	IF NOT EXISTS(SELECT 1 FROM sys.tables WHERE name = @TableName AND Type = N'U')
	BEGIN
        PRINT N'The table does not exist in the data warehouse.';
        THROW 51000, N'The table does not exist in the data warehouse.', 1;
        RETURN -1;
	END
	
    -- If the table exists, but was never loaded before, there won't be a record for it in the table
	-- A record is created for the @TableName, with the minimum possible date in the LoadDate column
	IF NOT EXISTS (SELECT 1 FROM [int].[IncrementalLoads] WHERE TableName = @TableName)
		INSERT INTO [int].[IncrementalLoads]
		SELECT @TableName, '1753-01-01'

    -- Select the LoadDate for the @TableName
	SELECT 
		[LoadDate] AS [LoadDate]
    FROM [int].[IncrementalLoads]
    WHERE 
		[TableName] = @TableName;



    RETURN 0;
END;
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE   PROCEDURE [int].[Get_LineageKey]
@LoadType nvarchar(1),
@TableName nvarchar(100),
@LastLoadedDate datetime
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;

-- The load for @TableName starts now 
DECLARE @StartLoad datetime = SYSDATETIME();

/* 
A new row is inserted into the Lineage table, with the table name that will be loaded,
the starting date of the load, load type and load status.
Possible values for Type:
- F = Full load
- I = Incremental load

Possible values for Status:
- P = In progress
- E = Error
- S = Success
*/
INSERT INTO [int].[Lineage](
	 [TableName]
	,[StartLoad]
	,[FinishLoad]
	,[Status]
	,[Type]
	,[LastLoadedDate]
	)
VALUES (
	 @TableName
	,@StartLoad
	,NULL
	,'P'
	,@LoadType
	,@LastLoadedDate
	);

-- If we're doing an initial load, remove the date of the most recent load for this table
IF (@LoadType = 'F')
	BEGIN
		UPDATE [int].[IncrementalLoads]
		SET LoadDate = '1753-01-01'
		WHERE TableName = @TableName

		EXEC('TRUNCATE TABLE ' + @TableName)
	END;

-- Select the key of the previously inserted row
SELECT MAX([LineageKey]) AS LineageKey
FROM [int].[Lineage]
WHERE 
	[TableName] = @TableName
	AND [StartLoad] = @StartLoad

RETURN 0;
END;
GO
USE [master]
GO
ALTER DATABASE [HappyScoopers_DW] SET  READ_WRITE 
GO
