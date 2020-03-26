﻿CREATE TABLE [dbo].[Games]
(
	[ID] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [HomeTeamID] UNIQUEIDENTIFIER NOT NULL, 
    [AwayTeamID] UNIQUEIDENTIFIER NOT NULL, 
    [TournamentID] UNIQUEIDENTIFIER NULL, 
    [MinutesPlayed] INT NULL, 
    [IsClockTicking] BIT NULL, 
    [GameEnded] BIT NULL, 
    [StartTime] DATETIME NULL, 
    CONSTRAINT [FK_Games_Tournaments] FOREIGN KEY ([TournamentID]) REFERENCES [Tournaments]([ID]), 
    CONSTRAINT [FK_Games_Teams_Home] FOREIGN KEY ([HomeTeamID]) REFERENCES [Teams]([ID]), 
    CONSTRAINT [FK_Games_Teams_Away] FOREIGN KEY ([AwayTeamID]) REFERENCES [Teams]([ID])
)
