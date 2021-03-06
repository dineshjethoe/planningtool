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
PRINT N'Rename refactoring operation with key 3f8ea120-8db1-4317-94f6-edbc34ca9f50 is skipped, element [Task].[Tasks].[TaskTime] (SqlSimpleColumn) will not be renamed to EstimatedTime';


GO
PRINT N'Creating [Employee].[Employees]...';


GO
CREATE TABLE [Employee].[Employees] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [FirstName]       NVARCHAR (100) NOT NULL,
    [LastName]        NVARCHAR (100) NULL,
    [DateTimeCreated] DATETIME       NOT NULL,
    [IsDeleted]       BIT            NOT NULL,
    [RowVersion]      ROWVERSION     NOT NULL,
    CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [Task].[Tasks]...';


GO
CREATE TABLE [Task].[Tasks] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [TaskDescription] NVARCHAR (100) NOT NULL,
    [TaskDate]        DATE           NOT NULL,
    [EstimatedTime]   TIME (7)       NOT NULL,
    [DateTimeCreated] DATETIME       NOT NULL,
    [IsDeleted]       BIT            NOT NULL,
    [RowVersion]      ROWVERSION     NOT NULL,
    CONSTRAINT [PK_Tasks] PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating [Employee].[DF_Employee_DateTimeCreated]...';


GO
ALTER TABLE [Employee].[Employees]
    ADD CONSTRAINT [DF_Employee_DateTimeCreated] DEFAULT (getdate()) FOR [DateTimeCreated];


GO
PRINT N'Creating [Employee].[DF_Employee_IsDeleted]...';


GO
ALTER TABLE [Employee].[Employees]
    ADD CONSTRAINT [DF_Employee_IsDeleted] DEFAULT ((0)) FOR [IsDeleted];


GO
PRINT N'Creating [Task].[DF_Task_DateTimeCreated]...';


GO
ALTER TABLE [Task].[Tasks]
    ADD CONSTRAINT [DF_Task_DateTimeCreated] DEFAULT (getdate()) FOR [DateTimeCreated];


GO
PRINT N'Creating [Task].[DF_Task_IsDeleted]...';


GO
ALTER TABLE [Task].[Tasks]
    ADD CONSTRAINT [DF_Task_IsDeleted] DEFAULT ((0)) FOR [IsDeleted];


GO
PRINT N'Creating [Employee].[FK_AssignedTasks_Employees]...';


GO
ALTER TABLE [Employee].[AssignedTasks] WITH NOCHECK
    ADD CONSTRAINT [FK_AssignedTasks_Employees] FOREIGN KEY ([EmployeeId]) REFERENCES [Employee].[Employees] ([Id]);


GO
PRINT N'Creating [Employee].[FK_AssignedTasks_Tasks]...';


GO
ALTER TABLE [Employee].[AssignedTasks] WITH NOCHECK
    ADD CONSTRAINT [FK_AssignedTasks_Tasks] FOREIGN KEY ([TaskId]) REFERENCES [Task].[Tasks] ([Id]);


GO
-- Refactoring step to update target server with deployed transaction logs

IF OBJECT_ID(N'dbo.__RefactorLog') IS NULL
BEGIN
    CREATE TABLE [dbo].[__RefactorLog] (OperationKey UNIQUEIDENTIFIER NOT NULL PRIMARY KEY)
    EXEC sp_addextendedproperty N'microsoft_database_tools_support', N'refactoring log', N'schema', N'dbo', N'table', N'__RefactorLog'
END
GO
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '3f8ea120-8db1-4317-94f6-edbc34ca9f50')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('3f8ea120-8db1-4317-94f6-edbc34ca9f50')

GO

GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [Employee].[AssignedTasks] WITH CHECK CHECK CONSTRAINT [FK_AssignedTasks_Employees];

ALTER TABLE [Employee].[AssignedTasks] WITH CHECK CHECK CONSTRAINT [FK_AssignedTasks_Tasks];


GO
PRINT N'Update complete.';


GO
