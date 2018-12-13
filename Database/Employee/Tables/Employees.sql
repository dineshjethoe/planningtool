CREATE TABLE [Employee].[Employees] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [FirstName]       NVARCHAR (100) NOT NULL,
    [LastName]        NVARCHAR (100) NULL,
    [DateTimeCreated] DATETIME2       CONSTRAINT [DF_Employee_DateTimeCreated] DEFAULT (getdate()) NOT NULL,
    [IsDeleted]       BIT			 CONSTRAINT [DF_Employee_IsDeleted] DEFAULT ((0)) NOT NULL,
	[RowVersion]      ROWVERSION     NOT NULL,
    CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED ([Id] ASC)
);

