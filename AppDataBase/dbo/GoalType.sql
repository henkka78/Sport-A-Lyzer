CREATE TABLE [dbo].[GoalType]
(
	[ID] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(255) NOT NULL, 
    [Description] NVARCHAR(255) NULL, 
    [PointAmount] INT NOT NULL, 
    [SportID] INT NOT NULL, 
    [UIKey] NVARCHAR(20) NOT NULL DEFAULT '', 
    CONSTRAINT [FK_GoalTypes_Sports] FOREIGN KEY ([SportID]) REFERENCES [Sport]([ID])
)
