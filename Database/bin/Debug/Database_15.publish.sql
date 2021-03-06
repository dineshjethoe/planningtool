﻿/*
Deployment script for KlmPlanningToolDb

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "KlmPlanningToolDb"
:setvar DefaultFilePrefix "KlmPlanningToolDb"
:setvar DefaultDataPath "C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\"
:setvar DefaultLogPath "C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
/*
The column [Employee].[AssignedTasks].[EndDate] is being dropped, data loss could occur.

The column [Employee].[AssignedTasks].[IsAllDay] is being dropped, data loss could occur.
*/

IF EXISTS (select top 1 1 from [Employee].[AssignedTasks])
    RAISERROR (N'Rows were detected. The schema update is terminating because data loss might occur.', 16, 127) WITH NOWAIT

GO
PRINT N'The following operation was generated from a refactoring log file 0a1e201c-a6e9-4eab-a07c-c02bb18b5251';

PRINT N'Rename [Employee].[AssignedTasks].[StartDate] to AssignmentDate';


GO
EXECUTE sp_rename @objname = N'[Employee].[AssignedTasks].[StartDate]', @newname = N'AssignmentDate', @objtype = N'COLUMN';


GO
PRINT N'Dropping unnamed constraint on [Employee].[AssignedTasks]...';


GO
ALTER TABLE [Employee].[AssignedTasks] DROP CONSTRAINT [DF__AssignedT__IsAll__286302EC];


GO
PRINT N'Altering [Employee].[AssignedTasks]...';


GO
ALTER TABLE [Employee].[AssignedTasks] DROP COLUMN [EndDate], COLUMN [IsAllDay];


GO
-- Refactoring step to update target server with deployed transaction logs
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '0a1e201c-a6e9-4eab-a07c-c02bb18b5251')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('0a1e201c-a6e9-4eab-a07c-c02bb18b5251')

GO

GO
PRINT N'Update complete.';


GO
