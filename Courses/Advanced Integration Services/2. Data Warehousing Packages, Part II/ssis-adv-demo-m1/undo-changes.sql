USE [AdventureWorks2012]
GO

delete from production.product where ProductNumber = 'AR-5382'

UPDATE Production.Product
SET ListPrice =  0
WHERE ProductID = 317