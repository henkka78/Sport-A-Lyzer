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