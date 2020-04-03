CREATE TABLE [dbo].[Game]
(
	[ID] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [HomeTeamID] UNIQUEIDENTIFIER NOT NULL, 
    [AwayTeamID] UNIQUEIDENTIFIER NOT NULL, 
    [TournamentID] UNIQUEIDENTIFIER NULL, 
    [MinutesPlayed] INT NULL, 
    [IsClockTicking] BIT NULL, 
    [GameEnded] BIT NULL, 
    [StartTime] DATETIME NULL, 
    CONSTRAINT [FK_Games_Tournament] FOREIGN KEY ([TournamentID]) REFERENCES [Tournament]([ID]), 
    CONSTRAINT [FK_Games_Team_Home] FOREIGN KEY ([HomeTeamID]) REFERENCES [Team]([ID]), 
    CONSTRAINT [FK_Games_Team_Away] FOREIGN KEY ([AwayTeamID]) REFERENCES [Team]([ID])
)
