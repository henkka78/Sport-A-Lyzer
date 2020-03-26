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
/*
The type for column FoulTypeID in table [dbo].[Fouls] is currently  UNIQUEIDENTIFIER NOT NULL but is being changed to  INT NOT NULL. There is no implicit or explicit conversion.
*/

IF EXISTS (select top 1 1 from [dbo].[Fouls])
    RAISERROR (N'Rows were detected. The schema update is terminating because data loss might occur.', 16, 127) WITH NOWAIT

GO
/*
The type for column ID in table [dbo].[FoulTypes] is currently  UNIQUEIDENTIFIER NOT NULL but is being changed to  INT IDENTITY (1, 1) NOT NULL. There is no implicit or explicit conversion.
*/

IF EXISTS (select top 1 1 from [dbo].[FoulTypes])
    RAISERROR (N'Rows were detected. The schema update is terminating because data loss might occur.', 16, 127) WITH NOWAIT

GO
PRINT N'Dropping [dbo].[FK_Fouls_FoulTypes]...';


GO
ALTER TABLE [dbo].[Fouls] DROP CONSTRAINT [FK_Fouls_FoulTypes];


GO
PRINT N'Dropping [dbo].[FK_Fouls_Players]...';


GO
ALTER TABLE [dbo].[Fouls] DROP CONSTRAINT [FK_Fouls_Players];


GO
PRINT N'Dropping [dbo].[FK_Fouls_Teams]...';


GO
ALTER TABLE [dbo].[Fouls] DROP CONSTRAINT [FK_Fouls_Teams];


GO
PRINT N'Starting rebuilding table [dbo].[Fouls]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Fouls] (
    [ID]          INT              IDENTITY (1, 1) NOT NULL,
    [FoulTypeID]  INT              NOT NULL,
    [PlayerID]    UNIQUEIDENTIFIER NOT NULL,
    [TeamID]      UNIQUEIDENTIFIER NULL,
    [Description] NVARCHAR (255)   NULL,
    [TimeStamp]   DATETIME         NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Fouls])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Fouls] ON;
        INSERT INTO [dbo].[tmp_ms_xx_Fouls] ([ID], [FoulTypeID], [PlayerID], [TeamID], [Description], [TimeStamp])
        SELECT   [ID],
                 [FoulTypeID],
                 [PlayerID],
                 [TeamID],
                 [Description],
                 [TimeStamp]
        FROM     [dbo].[Fouls]
        ORDER BY [ID] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Fouls] OFF;
    END

DROP TABLE [dbo].[Fouls];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Fouls]', N'Fouls';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Starting rebuilding table [dbo].[FoulTypes]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_FoulTypes] (
    [ID]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (255) NOT NULL,
    [Description] NVARCHAR (255) NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[FoulTypes])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_FoulTypes] ON;
        INSERT INTO [dbo].[tmp_ms_xx_FoulTypes] ([ID], [Name], [Description])
        SELECT   [ID],
                 [Name],
                 [Description]
        FROM     [dbo].[FoulTypes]
        ORDER BY [ID] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_FoulTypes] OFF;
    END

DROP TABLE [dbo].[FoulTypes];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_FoulTypes]', N'FoulTypes';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating [dbo].[FK_Fouls_FoulTypes]...';


GO
ALTER TABLE [dbo].[Fouls] WITH NOCHECK
    ADD CONSTRAINT [FK_Fouls_FoulTypes] FOREIGN KEY ([FoulTypeID]) REFERENCES [dbo].[FoulTypes] ([ID]);


GO
PRINT N'Creating [dbo].[FK_Fouls_Players]...';


GO
ALTER TABLE [dbo].[Fouls] WITH NOCHECK
    ADD CONSTRAINT [FK_Fouls_Players] FOREIGN KEY ([PlayerID]) REFERENCES [dbo].[Players] ([ID]);


GO
PRINT N'Creating [dbo].[FK_Fouls_Teams]...';


GO
ALTER TABLE [dbo].[Fouls] WITH NOCHECK
    ADD CONSTRAINT [FK_Fouls_Teams] FOREIGN KEY ([TeamID]) REFERENCES [dbo].[Teams] ([ID]);


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
ALTER TABLE [dbo].[Fouls] WITH CHECK CHECK CONSTRAINT [FK_Fouls_FoulTypes];

ALTER TABLE [dbo].[Fouls] WITH CHECK CHECK CONSTRAINT [FK_Fouls_Players];

ALTER TABLE [dbo].[Fouls] WITH CHECK CHECK CONSTRAINT [FK_Fouls_Teams];


GO
PRINT N'Update complete.';


GO
