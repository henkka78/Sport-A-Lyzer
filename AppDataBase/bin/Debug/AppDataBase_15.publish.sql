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
USE [master];


GO

IF (DB_ID(N'$(DatabaseName)') IS NOT NULL) 
BEGIN
    ALTER DATABASE [$(DatabaseName)]
    SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [$(DatabaseName)];
END

GO
PRINT N'Creating $(DatabaseName)...'
GO
CREATE DATABASE [$(DatabaseName)]
    ON 
    PRIMARY(NAME = [$(DatabaseName)], FILENAME = N'$(DefaultDataPath)$(DefaultFilePrefix)_Primary.mdf')
    LOG ON (NAME = [$(DatabaseName)_log], FILENAME = N'$(DefaultLogPath)$(DefaultFilePrefix)_Primary.ldf') COLLATE SQL_Latin1_General_CP1_CI_AS
GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CLOSE OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
USE [$(DatabaseName)];


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ANSI_NULLS ON,
                ANSI_PADDING ON,
                ANSI_WARNINGS ON,
                ARITHABORT ON,
                CONCAT_NULL_YIELDS_NULL ON,
                NUMERIC_ROUNDABORT OFF,
                QUOTED_IDENTIFIER ON,
                ANSI_NULL_DEFAULT ON,
                CURSOR_DEFAULT LOCAL,
                CURSOR_CLOSE_ON_COMMIT OFF,
                AUTO_CREATE_STATISTICS ON,
                AUTO_SHRINK OFF,
                AUTO_UPDATE_STATISTICS ON,
                RECURSIVE_TRIGGERS OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET ALLOW_SNAPSHOT_ISOLATION OFF;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET READ_COMMITTED_SNAPSHOT OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_UPDATE_STATISTICS_ASYNC OFF,
                PAGE_VERIFY NONE,
                DATE_CORRELATION_OPTIMIZATION OFF,
                DISABLE_BROKER,
                PARAMETERIZATION SIMPLE,
                SUPPLEMENTAL_LOGGING OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET TRUSTWORTHY OFF,
        DB_CHAINING OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'The database settings cannot be modified. You must be a SysAdmin to apply these settings.';
    END


GO
IF IS_SRVROLEMEMBER(N'sysadmin') = 1
    BEGIN
        IF EXISTS (SELECT 1
                   FROM   [master].[dbo].[sysdatabases]
                   WHERE  [name] = N'$(DatabaseName)')
            BEGIN
                EXECUTE sp_executesql N'ALTER DATABASE [$(DatabaseName)]
    SET HONOR_BROKER_PRIORITY OFF 
    WITH ROLLBACK IMMEDIATE';
            END
    END
ELSE
    BEGIN
        PRINT N'The database settings cannot be modified. You must be a SysAdmin to apply these settings.';
    END


GO
ALTER DATABASE [$(DatabaseName)]
    SET TARGET_RECOVERY_TIME = 0 SECONDS 
    WITH ROLLBACK IMMEDIATE;


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET FILESTREAM(NON_TRANSACTED_ACCESS = OFF),
                CONTAINMENT = NONE 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET AUTO_CREATE_STATISTICS ON(INCREMENTAL = OFF),
                MEMORY_OPTIMIZED_ELEVATE_TO_SNAPSHOT = OFF,
                DELAYED_DURABILITY = DISABLED 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET QUERY_STORE (QUERY_CAPTURE_MODE = ALL, DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_PLANS_PER_QUERY = 200, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 367), MAX_STORAGE_SIZE_MB = 100) 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET QUERY_STORE = OFF 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
        ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
        ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
        ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
        ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
    END


GO
IF EXISTS (SELECT 1
           FROM   [master].[dbo].[sysdatabases]
           WHERE  [name] = N'$(DatabaseName)')
    BEGIN
        ALTER DATABASE [$(DatabaseName)]
            SET TEMPORAL_HISTORY_RETENTION ON 
            WITH ROLLBACK IMMEDIATE;
    END


GO
IF fulltextserviceproperty(N'IsFulltextInstalled') = 1
    EXECUTE sp_fulltext_database 'enable';


GO
PRINT N'Creating [dbo].[Fouls]...';


GO
CREATE TABLE [dbo].[Fouls] (
    [ID]          INT              IDENTITY (1, 1) NOT NULL,
    [FoulTypeID]  INT              NOT NULL,
    [PlayerID]    UNIQUEIDENTIFIER NOT NULL,
    [TeamID]      UNIQUEIDENTIFIER NULL,
    [Description] NVARCHAR (255)   NULL,
    [TimeStamp]   DATETIME         NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
PRINT N'Creating [dbo].[FoulTypes]...';


GO
CREATE TABLE [dbo].[FoulTypes] (
    [ID]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (255) NOT NULL,
    [Description] NVARCHAR (255) NULL,
    [UIKey]       NVARCHAR (20)  NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
PRINT N'Creating [dbo].[GameEvents]...';


GO
CREATE TABLE [dbo].[GameEvents] (
    [ID]          UNIQUEIDENTIFIER NOT NULL,
    [EventTypeID] INT              NOT NULL,
    [Description] NVARCHAR (500)   NULL,
    [PlayerID]    UNIQUEIDENTIFIER NOT NULL,
    [TeamID]      UNIQUEIDENTIFIER NULL,
    [TimeStamp]   DATETIME         NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
PRINT N'Creating [dbo].[GameEventTypes]...';


GO
CREATE TABLE [dbo].[GameEventTypes] (
    [ID]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (255) NOT NULL,
    [Description] NVARCHAR (255) NULL,
    [UIKey]       NVARCHAR (20)  NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
PRINT N'Creating [dbo].[Games]...';


GO
CREATE TABLE [dbo].[Games] (
    [ID]           UNIQUEIDENTIFIER NOT NULL,
    [HomeTeamID]   UNIQUEIDENTIFIER NOT NULL,
    [AwayTeamID]   UNIQUEIDENTIFIER NOT NULL,
    [TournamentID] UNIQUEIDENTIFIER NULL,
    [StartTime]    DATETIME         NULL,
    [EndTime]      DATETIME         NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
PRINT N'Creating [dbo].[Goals]...';


GO
CREATE TABLE [dbo].[Goals] (
    [ID]         UNIQUEIDENTIFIER NOT NULL,
    [TeamID]     UNIQUEIDENTIFIER NOT NULL,
    [GameID]     UNIQUEIDENTIFIER NOT NULL,
    [TimeStamp]  DATETIME         NOT NULL,
    [PlayerID]   UNIQUEIDENTIFIER NOT NULL,
    [GoalTypeID] INT              NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
PRINT N'Creating [dbo].[GoalsEvents]...';


GO
CREATE TABLE [dbo].[GoalsEvents] (
    [GoalID]      UNIQUEIDENTIFIER NOT NULL,
    [GameEventID] UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY CLUSTERED ([GameEventID] ASC, [GoalID] ASC)
);


GO
PRINT N'Creating [dbo].[GoalTypes]...';


GO
CREATE TABLE [dbo].[GoalTypes] (
    [ID]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (255) NOT NULL,
    [Description] NVARCHAR (255) NULL,
    [PointAmount] INT            NOT NULL,
    [SportID]     INT            NOT NULL,
    [UIKey]       NVARCHAR (20)  NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
PRINT N'Creating [dbo].[Players]...';


GO
CREATE TABLE [dbo].[Players] (
    [ID]        UNIQUEIDENTIFIER NOT NULL,
    [LastName]  NVARCHAR (100)   NOT NULL,
    [FirstName] NVARCHAR (100)   NULL,
    [Number]    INT              NOT NULL,
    [TeamID]    UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
PRINT N'Creating [dbo].[Sports]...';


GO
CREATE TABLE [dbo].[Sports] (
    [ID]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (255) NOT NULL,
    [Description] NVARCHAR (255) NULL,
    [UIKey]       NVARCHAR (20)  NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
PRINT N'Creating [dbo].[Teams]...';


GO
CREATE TABLE [dbo].[Teams] (
    [ID]   UNIQUEIDENTIFIER NOT NULL,
    [Name] NVARCHAR (255)   NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);


GO
PRINT N'Creating [dbo].[Tournaments]...';


GO
CREATE TABLE [dbo].[Tournaments] (
    [ID]        UNIQUEIDENTIFIER NOT NULL,
    [Name]      NCHAR (255)      NOT NULL,
    [StartTime] DATETIME         NOT NULL,
    [EndTime]   DATETIME         NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);


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
PRINT N'Creating unnamed constraint on [dbo].[Players]...';


GO
ALTER TABLE [dbo].[Players]
    ADD DEFAULT '' FOR [FirstName];


GO
PRINT N'Creating [dbo].[FK_Fouls_FoulTypes]...';


GO
ALTER TABLE [dbo].[Fouls]
    ADD CONSTRAINT [FK_Fouls_FoulTypes] FOREIGN KEY ([FoulTypeID]) REFERENCES [dbo].[FoulTypes] ([ID]);


GO
PRINT N'Creating [dbo].[FK_Fouls_Players]...';


GO
ALTER TABLE [dbo].[Fouls]
    ADD CONSTRAINT [FK_Fouls_Players] FOREIGN KEY ([PlayerID]) REFERENCES [dbo].[Players] ([ID]);


GO
PRINT N'Creating [dbo].[FK_Fouls_Teams]...';


GO
ALTER TABLE [dbo].[Fouls]
    ADD CONSTRAINT [FK_Fouls_Teams] FOREIGN KEY ([TeamID]) REFERENCES [dbo].[Teams] ([ID]);


GO
PRINT N'Creating [dbo].[FK_GameEvents_GameEventTypes]...';


GO
ALTER TABLE [dbo].[GameEvents]
    ADD CONSTRAINT [FK_GameEvents_GameEventTypes] FOREIGN KEY ([EventTypeID]) REFERENCES [dbo].[GameEventTypes] ([ID]);


GO
PRINT N'Creating [dbo].[FK_GameEvents_Players]...';


GO
ALTER TABLE [dbo].[GameEvents]
    ADD CONSTRAINT [FK_GameEvents_Players] FOREIGN KEY ([PlayerID]) REFERENCES [dbo].[Players] ([ID]);


GO
PRINT N'Creating [dbo].[FK_GameEvents_Teams]...';


GO
ALTER TABLE [dbo].[GameEvents]
    ADD CONSTRAINT [FK_GameEvents_Teams] FOREIGN KEY ([TeamID]) REFERENCES [dbo].[Teams] ([ID]);


GO
PRINT N'Creating [dbo].[FK_Games_Tournaments]...';


GO
ALTER TABLE [dbo].[Games]
    ADD CONSTRAINT [FK_Games_Tournaments] FOREIGN KEY ([TournamentID]) REFERENCES [dbo].[Tournaments] ([ID]);


GO
PRINT N'Creating [dbo].[FK_Games_Teams_Home]...';


GO
ALTER TABLE [dbo].[Games]
    ADD CONSTRAINT [FK_Games_Teams_Home] FOREIGN KEY ([HomeTeamID]) REFERENCES [dbo].[Teams] ([ID]);


GO
PRINT N'Creating [dbo].[FK_Games_Teams_Away]...';


GO
ALTER TABLE [dbo].[Games]
    ADD CONSTRAINT [FK_Games_Teams_Away] FOREIGN KEY ([AwayTeamID]) REFERENCES [dbo].[Teams] ([ID]);


GO
PRINT N'Creating [dbo].[FK_Goals_Teams]...';


GO
ALTER TABLE [dbo].[Goals]
    ADD CONSTRAINT [FK_Goals_Teams] FOREIGN KEY ([TeamID]) REFERENCES [dbo].[Teams] ([ID]);


GO
PRINT N'Creating [dbo].[FK_Goals_Games]...';


GO
ALTER TABLE [dbo].[Goals]
    ADD CONSTRAINT [FK_Goals_Games] FOREIGN KEY ([GameID]) REFERENCES [dbo].[Games] ([ID]);


GO
PRINT N'Creating [dbo].[FK_Goals_Players]...';


GO
ALTER TABLE [dbo].[Goals]
    ADD CONSTRAINT [FK_Goals_Players] FOREIGN KEY ([PlayerID]) REFERENCES [dbo].[Players] ([ID]);


GO
PRINT N'Creating [dbo].[FK_Goals_GoalTypes]...';


GO
ALTER TABLE [dbo].[Goals]
    ADD CONSTRAINT [FK_Goals_GoalTypes] FOREIGN KEY ([GoalTypeID]) REFERENCES [dbo].[GoalTypes] ([ID]);


GO
PRINT N'Creating [dbo].[FK_GoalsEvents_Goals]...';


GO
ALTER TABLE [dbo].[GoalsEvents]
    ADD CONSTRAINT [FK_GoalsEvents_Goals] FOREIGN KEY ([GoalID]) REFERENCES [dbo].[Goals] ([ID]);


GO
PRINT N'Creating [dbo].[FK_GoalsEvents_GameEvents]...';


GO
ALTER TABLE [dbo].[GoalsEvents]
    ADD CONSTRAINT [FK_GoalsEvents_GameEvents] FOREIGN KEY ([GameEventID]) REFERENCES [dbo].[GameEvents] ([ID]);


GO
PRINT N'Creating [dbo].[FK_GoalTypes_Sports]...';


GO
ALTER TABLE [dbo].[GoalTypes]
    ADD CONSTRAINT [FK_GoalTypes_Sports] FOREIGN KEY ([SportID]) REFERENCES [dbo].[Sports] ([ID]);


GO
PRINT N'Creating [dbo].[FK_Players_Teams]...';


GO
ALTER TABLE [dbo].[Players]
    ADD CONSTRAINT [FK_Players_Teams] FOREIGN KEY ([TeamID]) REFERENCES [dbo].[Teams] ([ID]);


GO
PRINT N'Creating [dbo].[FK_Translations_Sports]...';


GO
ALTER TABLE [dbo].[Translations]
    ADD CONSTRAINT [FK_Translations_Sports] FOREIGN KEY ([SportID]) REFERENCES [dbo].[Sports] ([ID]);


GO
-- Refactoring step to update target server with deployed transaction logs

IF OBJECT_ID(N'dbo.__RefactorLog') IS NULL
BEGIN
    CREATE TABLE [dbo].[__RefactorLog] (OperationKey UNIQUEIDENTIFIER NOT NULL PRIMARY KEY)
    EXEC sp_addextendedproperty N'microsoft_database_tools_support', N'refactoring log', N'schema', N'dbo', N'table', N'__RefactorLog'
END
GO
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '67158c10-47f1-419c-a2fa-1d641e7280c4')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('67158c10-47f1-419c-a2fa-1d641e7280c4')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = 'bb936d7b-74ae-42ec-b796-27839a9fc076')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('bb936d7b-74ae-42ec-b796-27839a9fc076')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '1bbeee76-f251-422a-9b5d-f148e7069701')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('1bbeee76-f251-422a-9b5d-f148e7069701')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = 'fa5f35f2-aae4-49c3-9a01-8eda15792bf1')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('fa5f35f2-aae4-49c3-9a01-8eda15792bf1')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '653d3f65-427e-41c6-b600-6a3894fe7409')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('653d3f65-427e-41c6-b600-6a3894fe7409')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '948b16f8-253c-4393-bec2-d78de3764763')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('948b16f8-253c-4393-bec2-d78de3764763')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = 'fb2df483-c4a1-4d2b-bb8c-fb1d25c995d9')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('fb2df483-c4a1-4d2b-bb8c-fb1d25c995d9')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '27824663-9fa9-463b-a7a6-4f62d5e4cb32')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('27824663-9fa9-463b-a7a6-4f62d5e4cb32')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '4b1449bc-e883-47c6-ada7-1c3c81ebaed8')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('4b1449bc-e883-47c6-ada7-1c3c81ebaed8')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = 'b5c55400-3f41-4d9b-849e-050f4ccac3a3')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('b5c55400-3f41-4d9b-849e-050f4ccac3a3')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '56173106-0ffb-4df0-a0cf-8d662d73a240')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('56173106-0ffb-4df0-a0cf-8d662d73a240')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '800e56cd-c7f3-4b7f-be0b-bab81da0da7e')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('800e56cd-c7f3-4b7f-be0b-bab81da0da7e')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '887da0ba-2880-482e-8ed3-793a4f4ad1e2')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('887da0ba-2880-482e-8ed3-793a4f4ad1e2')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '05927f8f-0899-44f3-9ea5-675bb3c8ee0f')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('05927f8f-0899-44f3-9ea5-675bb3c8ee0f')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '6f2d1944-8fc8-4f48-b63d-c28515a1fdaa')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('6f2d1944-8fc8-4f48-b63d-c28515a1fdaa')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '37cfc250-be1d-4eeb-87ba-bde504db3b7a')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('37cfc250-be1d-4eeb-87ba-bde504db3b7a')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '2252e552-e155-4f31-82a7-476880b8678f')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('2252e552-e155-4f31-82a7-476880b8678f')
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
DECLARE @VarDecimalSupported AS BIT;

SELECT @VarDecimalSupported = 0;

IF ((ServerProperty(N'EngineEdition') = 3)
    AND (((@@microsoftversion / power(2, 24) = 9)
          AND (@@microsoftversion & 0xffff >= 3024))
         OR ((@@microsoftversion / power(2, 24) = 10)
             AND (@@microsoftversion & 0xffff >= 1600))))
    SELECT @VarDecimalSupported = 1;

IF (@VarDecimalSupported > 0)
    BEGIN
        EXECUTE sp_db_vardecimal_storage_format N'$(DatabaseName)', 'ON';
    END


GO
PRINT N'Update complete.';


GO
