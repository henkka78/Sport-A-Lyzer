CREATE TABLE [dbo].[User]
(
	[ID] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [FirstName] NVARCHAR(50) NOT NULL, 
    [LastName] NVARCHAR(50) NOT NULL, 
    [UserName] NVARCHAR(50) NOT NULL, 
    [Password] NVARCHAR(50) NOT NULL, 
    [RoleID] UNIQUEIDENTIFIER NOT NULL, 
    [OrganizationID] UNIQUEIDENTIFIER NULL, 
    CONSTRAINT [FK_User_Organization] FOREIGN KEY ([OrganizationID]) REFERENCES [Organization]([ID])
)
