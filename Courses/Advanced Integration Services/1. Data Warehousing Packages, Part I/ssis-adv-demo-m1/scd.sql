USE [AdventureWorks2012]
GO

INSERT INTO [Production].[Product]
           ([Name]
           ,[ProductNumber]
           ,[MakeFlag]
           ,[FinishedGoodsFlag]
           ,[Color]
           ,[SafetyStockLevel]
           ,[ReorderPoint]
           ,[StandardCost]
           ,[ListPrice]
           ,[Size]
           ,[SizeUnitMeasureCode]
           ,[WeightUnitMeasureCode]
           ,[Weight]
           ,[DaysToManufacture]
           ,[ProductLine]
           ,[Class]
           ,[Style]
           ,[ProductSubcategoryID]
           ,[ProductModelID]
           ,[SellStartDate]
           ,[SellEndDate]
           ,[DiscontinuedDate]
           ,[rowguid]
           ,[ModifiedDate])
     VALUES
           ('Adjustable Race II'
           ,'AR-5382'
           ,0
           ,0
           ,'NA'
           ,1000
           ,750
           ,0
           ,0
           ,NULL
           ,NULL
           ,NULL
           ,NULL
           ,0
           ,NULL
           ,NULL
           ,NULL
           ,NULL
           ,NULL
           ,'7/1/2013'
           ,NULL
           ,NULL
           ,newid()
           ,getdate())
GO

UPDATE Production.Product
SET ListPrice = 1.00
WHERE ProductID = 317

GO