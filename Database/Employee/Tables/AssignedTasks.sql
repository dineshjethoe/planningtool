CREATE TABLE [Employee].[AssignedTasks] (
    [Id]              INT      IDENTITY (1, 1) NOT NULL,
    [TaskId]          INT      NOT NULL,
    [EmployeeId]      INT      NOT NULL,
    [AssignmentDate]  DATE NOT NULL, 
    [StartTime]       TIME NOT NULL, 
    [EndTime]         TIME NOT NULL, 
	[IsDeleted]       BIT      CONSTRAINT [DF_AssignedTasks_IsDeleted] DEFAULT ((0)) NOT NULL,
    [DateTimeCreated] DATETIME2 CONSTRAINT [DF_AssignedTasks_DateTimeCreated] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [FK_AssignedTasks_Tasks] FOREIGN KEY ([TaskId]) REFERENCES [Task].[Tasks]([Id]),
    CONSTRAINT [FK_AssignedTasks_Employees] FOREIGN KEY ([EmployeeId]) REFERENCES [Employee].[Employees]([Id])

);

