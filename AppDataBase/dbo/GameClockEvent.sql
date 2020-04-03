CREATE TABLE [dbo].[GameClockEvent]
(
	[ID] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [GameID] UNIQUEIDENTIFIER NOT NULL, 
    [TimeStamp] DATETIME NOT NULL, 
    [IsClockRunning] BIT NOT NULL, 
    [SecondsSinceStart] INT NOT NULL, 
    CONSTRAINT [FK_GameClockEvents_Games] FOREIGN KEY ([GameID]) REFERENCES [Game]([ID])
)
