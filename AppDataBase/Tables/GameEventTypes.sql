CREATE TABLE [dbo].[GameEventTypes]
(
	[ID] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(255) NOT NULL, 
    [Description] NVARCHAR(255) NULL, 
    [UIKey] NVARCHAR(20) NOT NULL DEFAULT '', 
    [SportId] INT NOT NULL, 
    CONSTRAINT [FK_GameEventTypes_Sports] FOREIGN KEY ([SportId]) REFERENCES [Sports]([ID])
)
