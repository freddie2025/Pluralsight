USE [HappyScoopers_DW]
GO

DECLARE	@return_value int

EXEC	@return_value = [dbo].[Load_DimDate]
		@StartDate = '2008-01-01',
		@EndDate = '2025-12-31'

SELECT	'Return Value' = @return_value

GO

SELECT * FROM Dim_Date