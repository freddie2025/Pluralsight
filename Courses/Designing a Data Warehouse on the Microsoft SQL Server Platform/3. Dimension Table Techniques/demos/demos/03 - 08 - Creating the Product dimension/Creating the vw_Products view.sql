USE [HappyScoopers_DW]
GO

DROP VIEW IF EXISTS [dbo].[vw_Products]
GO

CREATE VIEW [dbo].[vw_Products]
AS
SELECT 
	 'HSD|' + CONVERT(NVARCHAR, prod.[ProductID])	AS [_SourceKey]
	,prod.[ProductName]								AS [Product Name]
	,prod.[ProductCode]								AS [Product Code]
	,prod.[ProductDescription]						AS [Product Description]
	,subcat.[SubcategoryName]						AS [Subcategory]
	,cat.[CategoryName]								AS [Category]
	,dep.[Name]										AS [Department]
	,um.[UnitMeasureCode]							AS [Unit of measure Code]
	,um.[Name]										AS [Unit of measure Name]
	,prod.[UnitPrice]								AS [Unit Price]
	,CASE prod.[Discontinued]
		WHEN 1 THEN 'Yes'
		WHEN 0 THEN 'No'
		ELSE 'N/A'
	 END											AS [Discontinued] 
	,prod.[ModifiedDate]							AS [Modified Date]
FROM [HappyScoopers_Demo].[dbo].[Products] prod
LEFT JOIN [HappyScoopers_Demo].[dbo].[ProductSubcategories] subcat ON prod.SubcategoryID = subcat.ProductSubcategoryID
LEFT JOIN [HappyScoopers_Demo].[dbo].[ProductCategories] cat ON subcat.ProductCategoryID = cat.CategoryID
LEFT JOIN [HappyScoopers_Demo].[dbo].[ProductDepartments] dep ON cat.DepartmentID = dep.DepartmentID
LEFT JOIN [HappyScoopers_Demo].[dbo].[UnitsOfMeasure] um ON prod.UnitOfMeasureID = um.UnitOfMeasureID
GO







