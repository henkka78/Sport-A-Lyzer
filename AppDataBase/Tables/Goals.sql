﻿CREATE TABLE [dbo].[Goals]
(
	[ID] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [TeamID] UNIQUEIDENTIFIER NOT NULL, 
    [GameID] UNIQUEIDENTIFIER NOT NULL, 
    [TimeStamp] DATETIME NOT NULL, 
    [PlayerID] UNIQUEIDENTIFIER NOT NULL, 
    [GoalTypeID] INT NOT NULL, 
    [MinuteOfGame] INT NOT NULL, 
    CONSTRAINT [FK_Goals_Teams] FOREIGN KEY ([TeamID]) REFERENCES [Teams]([ID]), 
    CONSTRAINT [FK_Goals_Games] FOREIGN KEY ([GameID]) REFERENCES [Games]([ID]), 
    CONSTRAINT [FK_Goals_Players] FOREIGN KEY ([PlayerID]) REFERENCES [Players]([ID]), 
    CONSTRAINT [FK_Goals_GoalTypes] FOREIGN KEY ([GoalTypeID]) REFERENCES [GoalTypes]([ID]) 
)
