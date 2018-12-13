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
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ANSI_NULLS ON,
                ANSI_PADDING ON,
                ANSI_WARNINGS ON,
                ARITHABORT ON,
                CONCAT_NULL_YIELDS_NULL ON,
                QUOTED_IDENTIFIER ON,
                ANSI_NULL_DEFAULT ON,
                CURSOR_DEFAULT LOCAL 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET PAGE_VERIFY NONE 
            WITH ROLLBACK IMMEDIATE;
    END


GO
ALTER DATABASE [$(DatabaseName)]
    SET TARGET_RECOVERY_TIME = 0 SECONDS 
    WITH ROLLBACK IMMEDIATE;


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET QUERY_STORE (CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 367)) 
            WITH ROLLBACK IMMEDIATE;
    END


GO
PRINT N'Dropping [Employee].[DF_Details_DateTimeCreated]...';


GO
ALTER TABLE [Employee].[Details] DROP CONSTRAINT [DF_Details_DateTimeCreated];


GO
PRINT N'Dropping [Task].[DF_Details_DateTimeCreated]...';


GO
ALTER TABLE [Task].[Details] DROP CONSTRAINT [DF_Details_DateTimeCreated];


GO
PRINT N'Dropping [Employee].[PK_AssignedTasks]...';


GO
ALTER TABLE [Employee].[AssignedTasks] DROP CONSTRAINT [PK_AssignedTasks];


GO
PRINT N'Starting rebuilding table [Employee].[Details]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [Employee].[tmp_ms_xx_Details] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [FirstName]       NVARCHAR (100) NOT NULL,
    [LastName]        NVARCHAR (100) NULL,
    [DateTimeCreated] DATETIME       CONSTRAINT [DF_Employee_DateTimeCreated] DEFAULT (getdate()) NOT NULL,
    [IsDeleted]       BIT            CONSTRAINT [DF_Employee_IsDeleted] DEFAULT ((0)) NOT NULL,
    [RowVersion]      ROWVERSION     NOT NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_Employee1] PRIMARY KEY CLUSTERED ([Id] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [Employee].[Details])
    BEGIN
        SET IDENTITY_INSERT [Employee].[tmp_ms_xx_Details] ON;
        INSERT INTO [Employee].[tmp_ms_xx_Details] ([Id], [FirstName], [LastName], [DateTimeCreated])
        SELECT   [Id],
                 [FirstName],
                 [LastName],
                 [DateTimeCreated]
        FROM     [Employee].[Details]
        ORDER BY [Id] ASC;
        SET IDENTITY_INSERT [Employee].[tmp_ms_xx_Details] OFF;
    END

DROP TABLE [Employee].[Details];

EXECUTE sp_rename N'[Employee].[tmp_ms_xx_Details]', N'Details';

EXECUTE sp_rename N'[Employee].[tmp_ms_xx_constraint_PK_Employee1]', N'PK_Employee', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Starting rebuilding table [Task].[Details]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [Task].[tmp_ms_xx_Details] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [TaskDescription] NVARCHAR (100) NOT NULL,
    [TaskDate]        DATE           NOT NULL,
    [TaskTime]        TIME (7)       NOT NULL,
    [DateTimeCreated] DATETIME       CONSTRAINT [DF_Task_DateTimeCreated] DEFAULT (getdate()) NOT NULL,
    [IsDeleted]       BIT            CONSTRAINT [DF_Task_IsDeleted] DEFAULT ((0)) NOT NULL,
    [RowVersion]      ROWVERSION     NOT NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_Tasks1] PRIMARY KEY CLUSTERED ([Id] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [Task].[Details])
    BEGIN
        SET IDENTITY_INSERT [Task].[tmp_ms_xx_Details] ON;
        INSERT INTO [Task].[tmp_ms_xx_Details] ([Id], [TaskDescription], [TaskDate], [TaskTime], [DateTimeCreated])
        SELECT   [Id],
                 [TaskDescription],
                 [TaskDate],
                 [TaskTime],
                 [DateTimeCreated]
        FROM     [Task].[Details]
        ORDER BY [Id] ASC;
        SET IDENTITY_INSERT [Task].[tmp_ms_xx_Details] OFF;
    END

DROP TABLE [Task].[Details];

EXECUTE sp_rename N'[Task].[tmp_ms_xx_Details]', N'Details';

EXECUTE sp_rename N'[Task].[tmp_ms_xx_constraint_PK_Tasks1]', N'PK_Tasks', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating [Employee].[FK_AssignedTasks_Employees]...';


GO
ALTER TABLE [Employee].[AssignedTasks] WITH NOCHECK
    ADD CONSTRAINT [FK_AssignedTasks_Employees] FOREIGN KEY ([TaskId]) REFERENCES [Employee].[Details] ([Id]);


GO
PRINT N'Creating [Employee].[FK_AssignedTasks_Tasks]...';


GO
ALTER TABLE [Employee].[AssignedTasks] WITH NOCHECK
    ADD CONSTRAINT [FK_AssignedTasks_Tasks] FOREIGN KEY ([TaskId]) REFERENCES [Task].[Details] ([Id]);


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
