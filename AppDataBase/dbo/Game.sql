CREATE TABLE [dbo].[Game]
(
	[ID] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [HomeTeamID] UNIQUEIDENTIFIER NOT NULL, 
    [AwayTeamID] UNIQUEIDENTIFIER NOT NULL, 
    [TournamentID] UNIQUEIDENTIFIER NULL, 
    [StartTime] DATETIME NULL, 
    [GameDay] DATE NULL, 
    [ActualStartTime] DATETIME NULL, 
    [ActualEndTime] DATETIME NULL, 
    [PlannedLength] INT NULL, 
    [Description] NVARCHAR(255) NULL, 
    [PitchName] NVARCHAR(50) NULL, 
    CONSTRAINT [FK_Games_Tournament] FOREIGN KEY ([TournamentID]) REFERENCES [Tournament]([ID]), 
    CONSTRAINT [FK_Games_Team_Home] FOREIGN KEY ([HomeTeamID]) REFERENCES [Team]([ID]), 
    CONSTRAINT [FK_Games_Team_Away] FOREIGN KEY ([AwayTeamID]) REFERENCES [Team]([ID])
)
