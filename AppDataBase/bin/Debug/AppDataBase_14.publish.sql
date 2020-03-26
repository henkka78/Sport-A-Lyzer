﻿/*
Deployment script for SportALyzerAppDb

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "SportALyzerAppDb"
:setvar DefaultFilePrefix "SportALyzerAppDb"
:setvar DefaultDataPath "C:\Users\henri.leppa\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\"
:setvar DefaultLogPath "C:\Users\henri.leppa\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB\"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
PRINT N'Rename refactoring operation with key a65af679-70af-408b-8fb9-f0fcdfb6cb6b is skipped, element [dbo].[Translations].[Id] (SqlSimpleColumn) will not be renamed to ID';


GO
PRINT N'Dropping [dbo].[FK_GameEvents_GameEventTypes]...';


GO
ALTER TABLE [dbo].[GameEvents] DROP CONSTRAINT [FK_GameEvents_GameEventTypes];


GO
PRINT N'Dropping [dbo].[FK_GameEvents_Players]...';


GO
ALTER TABLE [dbo].[GameEvents] DROP CONSTRAINT [FK_GameEvents_Players];


GO
PRINT N'Dropping [dbo].[FK_GameEvents_Teams]...';


GO
ALTER TABLE [dbo].[GameEvents] DROP CONSTRAINT [FK_GameEvents_Teams];


GO
PRINT N'Dropping [dbo].[FK_GoalsEvents_GameEvents]...';


GO
ALTER TABLE [dbo].[GoalsEvents] DROP CONSTRAINT [FK_GoalsEvents_GameEvents];


GO
PRINT N'Altering [dbo].[FoulTypes]...';


GO
ALTER TABLE [dbo].[FoulTypes]
    ADD [UIKey] NVARCHAR (20) NOT NULL;


GO
/*
The type for column EventTypeID in table [dbo].[GameEvents] is currently  UNIQUEIDENTIFIER NOT NULL but is being changed to  INT NOT NULL. There is no implicit or explicit conversion.
*/
GO
PRINT N'Starting rebuilding table [dbo].[GameEvents]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_GameEvents] (
    [ID]          UNIQUEIDENTIFIER NOT NULL,
    [EventTypeID] INT              NOT NULL,
    [Description] NVARCHAR (500)   NULL,
    [PlayerID]    UNIQUEIDENTIFIER NOT NULL,
    [TeamID]      UNIQUEIDENTIFIER NULL,
    [TimeStamp]   DATETIME         NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[GameEvents])
    BEGIN
        INSERT INTO [dbo].[tmp_ms_xx_GameEvents] ([ID], [EventTypeID], [Description], [PlayerID], [TeamID], [TimeStamp])
        SELECT   [ID],
                 [EventTypeID],
                 [Description],
                 [PlayerID],
                 [TeamID],
                 [TimeStamp]
        FROM     [dbo].[GameEvents]
        ORDER BY [ID] ASC;
    END

DROP TABLE [dbo].[GameEvents];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_GameEvents]', N'GameEvents';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
/*
The column [dbo].[GameEventTypes].[UIKey] on table [dbo].[GameEventTypes] must be added, but the column has no default value and does not allow NULL values. If the table contains data, the ALTER script will not work. To avoid this issue you must either: add a default value to the column, mark it as allowing NULL values, or enable the generation of smart-defaults as a deployment option.

The type for column ID in table [dbo].[GameEventTypes] is currently  UNIQUEIDENTIFIER NOT NULL but is being changed to  INT IDENTITY (1, 1) NOT NULL. There is no implicit or explicit conversion.
*/
GO
PRINT N'Starting rebuilding table [dbo].[GameEventTypes]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_GameEventTypes] (
    [ID]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (255) NOT NULL,
    [Description] NVARCHAR (255) NULL,
    [UIKey]       NVARCHAR (20)  NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[GameEventTypes])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_GameEventTypes] ON;
        INSERT INTO [dbo].[tmp_ms_xx_GameEventTypes] ([ID], [Name], [Description])
        SELECT   [ID],
                 [Name],
                 [Description]
        FROM     [dbo].[GameEventTypes]
        ORDER BY [ID] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_GameEventTypes] OFF;
    END

DROP TABLE [dbo].[GameEventTypes];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_GameEventTypes]', N'GameEventTypes';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Altering [dbo].[GoalTypes]...';


GO
ALTER TABLE [dbo].[GoalTypes]
    ADD [UIKey] NVARCHAR (20) NOT NULL;


GO
PRINT N'Altering [dbo].[Sports]...';


GO
ALTER TABLE [dbo].[Sports]
    ADD [UIKey] NVARCHAR (20) NOT NULL;


GO
PRINT N'Creating [dbo].[Translations]...';


GO
CREATE TABLE [dbo].[Translations] (
    [ID]          INT            IDENTITY (1, 1) NOT NULL,
    [SportID]     INT            NOT NULL,
    [UIKey]       NVARCHAR (20)  NOT NULL,
    [Translation] NVARCHAR (255) NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
PRINT N'Creating [dbo].[Translations].[IX_Translations_SportID_UIKey]...';


GO
CREATE NONCLUSTERED INDEX [IX_Translations_SportID_UIKey]
    ON [dbo].[Translations]([SportID] ASC, [UIKey] ASC);


GO
PRINT N'Creating [dbo].[FK_GameEvents_GameEventTypes]...';


GO
ALTER TABLE [dbo].[GameEvents] WITH NOCHECK
    ADD CONSTRAINT [FK_GameEvents_GameEventTypes] FOREIGN KEY ([EventTypeID]) REFERENCES [dbo].[GameEventTypes] ([ID]);


GO
PRINT N'Creating [dbo].[FK_GameEvents_Players]...';


GO
ALTER TABLE [dbo].[GameEvents] WITH NOCHECK
    ADD CONSTRAINT [FK_GameEvents_Players] FOREIGN KEY ([PlayerID]) REFERENCES [dbo].[Players] ([ID]);


GO
PRINT N'Creating [dbo].[FK_GameEvents_Teams]...';


GO
ALTER TABLE [dbo].[GameEvents] WITH NOCHECK
    ADD CONSTRAINT [FK_GameEvents_Teams] FOREIGN KEY ([TeamID]) REFERENCES [dbo].[Teams] ([ID]);


GO
PRINT N'Creating [dbo].[FK_GoalsEvents_GameEvents]...';


GO
ALTER TABLE [dbo].[GoalsEvents] WITH NOCHECK
    ADD CONSTRAINT [FK_GoalsEvents_GameEvents] FOREIGN KEY ([GameEventID]) REFERENCES [dbo].[GameEvents] ([ID]);


GO
PRINT N'Creating [dbo].[FK_Translations_Sports]...';


GO
ALTER TABLE [dbo].[Translations] WITH NOCHECK
    ADD CONSTRAINT [FK_Translations_Sports] FOREIGN KEY ([SportID]) REFERENCES [dbo].[Sports] ([ID]);


GO
-- Refactoring step to update target server with deployed transaction logs
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = 'a65af679-70af-408b-8fb9-f0fcdfb6cb6b')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('a65af679-70af-408b-8fb9-f0fcdfb6cb6b')

GO

GO
/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

SET NOCOUNT ON



SET IDENTITY_INSERT dbo.Sports ON;

GO

MERGE INTO [dbo].[Sports] AS Target
USING (VALUES
  (1,'Football','Trump people call it soccer')
 ,(2,'Basketball',NULL)
 ,(3,'Volleyball',NULL)
) AS Source ([ID],[Name],[Description])
ON (Target.[ID] = Source.[ID])
WHEN MATCHED AND (
	NULLIF(Source.[Name], Target.[Name]) IS NOT NULL OR NULLIF(Target.[Name], Source.[Name]) IS NOT NULL OR 
	NULLIF(Source.[Description], Target.[Description]) IS NOT NULL OR NULLIF(Target.[Description], Source.[Description]) IS NOT NULL) THEN
 UPDATE SET
 [Name] = Source.[Name], 
[Description] = Source.[Description]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([ID],[Name],[Description])
 VALUES(Source.[ID],Source.[Name],Source.[Description])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE;

GO

SET IDENTITY_INSERT dbo.Sports OFF;

GO

SET IDENTITY_INSERT [dbo].[GoalTypes] ON

MERGE INTO [dbo].[GoalTypes] AS Target
USING (VALUES
  (1,'Maali',NULL,1,1)
 ,(2,'Kahden pisteen heitto',NULL,2,2)
 ,(3,'Kolmen pisteen heitto',NULL,3,2)
) AS Source ([ID],[Name],[Description],[PointAmount],[SportID])
ON (Target.[ID] = Source.[ID])
WHEN MATCHED AND (
	NULLIF(Source.[Name], Target.[Name]) IS NOT NULL OR NULLIF(Target.[Name], Source.[Name]) IS NOT NULL OR 
	NULLIF(Source.[Description], Target.[Description]) IS NOT NULL OR NULLIF(Target.[Description], Source.[Description]) IS NOT NULL OR 
	NULLIF(Source.[PointAmount], Target.[PointAmount]) IS NOT NULL OR NULLIF(Target.[PointAmount], Source.[PointAmount]) IS NOT NULL OR 
	NULLIF(Source.[SportID], Target.[SportID]) IS NOT NULL OR NULLIF(Target.[SportID], Source.[SportID]) IS NOT NULL) THEN
 UPDATE SET
 [Name] = Source.[Name], 
[Description] = Source.[Description], 
[PointAmount] = Source.[PointAmount], 
[SportID] = Source.[SportID]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([ID],[Name],[Description],[PointAmount],[SportID])
 VALUES(Source.[ID],Source.[Name],Source.[Description],Source.[PointAmount],Source.[SportID])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE;

GO

SET IDENTITY_INSERT [dbo].[GoalTypes] OFF
GO

GO

GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[GameEvents] WITH CHECK CHECK CONSTRAINT [FK_GameEvents_GameEventTypes];

ALTER TABLE [dbo].[GameEvents] WITH CHECK CHECK CONSTRAINT [FK_GameEvents_Players];

ALTER TABLE [dbo].[GameEvents] WITH CHECK CHECK CONSTRAINT [FK_GameEvents_Teams];

ALTER TABLE [dbo].[GoalsEvents] WITH CHECK CHECK CONSTRAINT [FK_GoalsEvents_GameEvents];

ALTER TABLE [dbo].[Translations] WITH CHECK CHECK CONSTRAINT [FK_Translations_Sports];


GO
PRINT N'Update complete.';


GO