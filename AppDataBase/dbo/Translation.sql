CREATE TABLE [dbo].[Translation]
(
	[ID] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [SportID] INT NOT NULL, 
    [UIKey] NVARCHAR(20) NOT NULL, 
    [Translation] NVARCHAR(255) NOT NULL, 
    CONSTRAINT [FK_Translations_Sports] FOREIGN KEY ([SportID]) REFERENCES [Sport]([ID])
)

GO

CREATE INDEX [IX_Translations_SportID_UIKey] ON [dbo].[Translation] ([SportID], [UIKey])
