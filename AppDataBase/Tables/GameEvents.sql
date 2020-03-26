CREATE TABLE [dbo].[GameEvents]
(
	[ID] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [EventTypeID] INT NOT NULL, 
    [Description] NVARCHAR(500) NULL, 
    [PlayerID] UNIQUEIDENTIFIER NOT NULL, 
    [TeamID] UNIQUEIDENTIFIER NULL, 
    [TimeStamp] DATETIME NOT NULL, 
    [GameID] UNIQUEIDENTIFIER NOT NULL, 
    CONSTRAINT [FK_GameEvents_GameEventTypes] FOREIGN KEY ([EventTypeID]) REFERENCES [GameEventTypes]([ID]), 
    CONSTRAINT [FK_GameEvents_Players] FOREIGN KEY ([PlayerID]) REFERENCES [Players]([ID]), 
    CONSTRAINT [FK_GameEvents_Teams] FOREIGN KEY ([TeamID]) REFERENCES [Teams]([ID]), 
    CONSTRAINT [FK_GameEvents_Games] FOREIGN KEY ([GameID]) REFERENCES [Games]([ID])
)
