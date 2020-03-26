CREATE TABLE [dbo].[GoalsEvents]
(
	[GoalID] UNIQUEIDENTIFIER NOT NULL , 
    [GameEventID] UNIQUEIDENTIFIER NOT NULL, 
    PRIMARY KEY ([GameEventID], [GoalID]), 
    CONSTRAINT [FK_GoalsEvents_Goals] FOREIGN KEY ([GoalID]) REFERENCES [Goals]([ID]), 
    CONSTRAINT [FK_GoalsEvents_GameEvents] FOREIGN KEY ([GameEventID]) REFERENCES [GameEvents]([ID])
)
