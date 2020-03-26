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
