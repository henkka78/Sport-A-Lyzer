CREATE TABLE [dbo].[Organization]
(
	[ID] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(255) NOT NULL, 
    [HometownID] NVARCHAR(10) NULL, 
    [Description] NVARCHAR(255) NULL, 
    CONSTRAINT [FK_Organizantion_Town] FOREIGN KEY ([HometownID]) REFERENCES [Town]([ID])
)
