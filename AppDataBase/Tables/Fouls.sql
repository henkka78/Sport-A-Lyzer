CREATE TABLE [dbo].[Fouls]
(
	[ID] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [FoulTypeID] INT NOT NULL, 
    [PlayerID] UNIQUEIDENTIFIER NOT NULL, 
    [TeamID] UNIQUEIDENTIFIER NULL, 
    [Description] NVARCHAR(255) NULL, 
    [TimeStamp] DATETIME NOT NULL, 
    CONSTRAINT [FK_Fouls_FoulTypes] FOREIGN KEY ([FoulTypeID]) REFERENCES [FoulTypes]([ID]), 
    CONSTRAINT [FK_Fouls_Players] FOREIGN KEY ([PlayerID]) REFERENCES [Players]([ID]), 
    CONSTRAINT [FK_Fouls_Teams] FOREIGN KEY ([TeamID]) REFERENCES [Teams]([ID])
)
