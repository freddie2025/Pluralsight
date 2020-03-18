USE master;

-- Script assumes database backup is located in c:\temp
-- Also assumes a folder called c:\temp\StockSystemDB is present
-- This is where the database files will be restored to
-- To try restoring to the default location, comment out the two MOVE lines 
--	(this might not work on your computer)
-- You will need at least SQL Server Express installed for this to work, 
--	along with SSMS (SQL Server Management Studio).

-- Download SQL Server from:
-- https://www.microsoft.com/en-us/sql-server/sql-server-downloads
-- Developer Edition is full-featured and is free (best one to go for)
-- Download SSMS from:
-- https://docs.microsoft.com/en-gb/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-2017
-- You cannot manage a SQL Server instance graphically without SSMS

-- Good luck!

RESTORE DATABASE StockSystem 
FROM DISK = 'c:\temp\ExcelVba_StockSystem.bak'
WITH REPLACE,
MOVE 'StockSystem' TO 'c:\temp\StockSystemDB\StockSystem.mdf',
MOVE 'StockSystem_Log' TO 'c:\temp\StockSystemDB\StockSystem_log.ldf'