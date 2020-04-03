

MERGE INTO [dbo].[GameEventType] AS Target
USING (VALUES
  ('10159085-2f0b-4faa-8ac7-a9f00ecafe1b','Assist',NULL,'assist',1)
 ,('0ca1d4ee-1a58-4c59-8b84-e8a9053471fb','Goal',NULL,'goal',1)
 ,('5f067d7d-4e13-4fdc-bfa9-7a9e3cae59d6', 'Corner Kick',NULL,'corner',1)
 ,('88ccd067-31a6-43d0-9db3-5ac18ed6f69a','Offside',NULL,'offside',1)
 ,('bb08392f-465f-442f-a68a-0988d6e3c2a2','Throw',NULL,'throw',1)
 ,('9b8f96bc-5e01-4b71-b4a9-9a1c60aaf9cc','Penalty',NULL,'penalty',1)
 ,('317b748c-c8c4-4516-9665-1a6d25a90c9f','Free Kick',NULL,'freekick',1)
) AS Source ([ID],[Name],[Description],[UIKey],[SportId])
ON (Target.[ID] = Source.[ID])
WHEN MATCHED AND (
	NULLIF(Source.[Name], Target.[Name]) IS NOT NULL OR NULLIF(Target.[Name], Source.[Name]) IS NOT NULL OR 
	NULLIF(Source.[Description], Target.[Description]) IS NOT NULL OR NULLIF(Target.[Description], Source.[Description]) IS NOT NULL OR 
	NULLIF(Source.[UIKey], Target.[UIKey]) IS NOT NULL OR NULLIF(Target.[UIKey], Source.[UIKey]) IS NOT NULL OR 
	NULLIF(Source.[SportId], Target.[SportId]) IS NOT NULL OR NULLIF(Target.[SportId], Source.[SportId]) IS NOT NULL) THEN
 UPDATE SET
 [Name] = Source.[Name], 
[Description] = Source.[Description], 
[UIKey] = Source.[UIKey], 
[SportId] = Source.[SportId]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([ID],[Name],[Description],[UIKey],[SportId])
 VALUES(Source.[ID],Source.[Name],Source.[Description],Source.[UIKey],Source.[SportId])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE;

GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [dbo].[GameEventTypes]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[dbo].[GameEventTypes] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

