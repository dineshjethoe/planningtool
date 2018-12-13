CREATE TABLE [Task].[Tasks] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [TaskDescription] NVARCHAR (100) NOT NULL,
    [TaskDate]        DATE           NOT NULL,
    [DateTimeCreated] DATETIME2       CONSTRAINT [DF_Task_DateTimeCreated] DEFAULT (getdate()) NOT NULL,
	[IsDeleted]       BIT			 CONSTRAINT [DF_Task_IsDeleted] DEFAULT ((0)) NOT NULL,
    [RowVersion]      ROWVERSION     NOT NULL,
    CONSTRAINT [PK_Tasks] PRIMARY KEY CLUSTERED ([Id] ASC)
);

