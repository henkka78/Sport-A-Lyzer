MERGE INTO [dbo].[Role] AS Target
USING (VALUES
  ('A3AEA76A-A7E7-41E8-B752-7189062D5BBC','Admin')
 ,('77F6551C-97B6-43BE-86BE-8050EB3F8CE6','Regular')
 ,('92ABAD75-08DA-4AE9-B335-D5FB9E2D1DB5','Super')
) AS Source ([ID],[Name])
ON (Target.[ID] = Source.[ID])
WHEN MATCHED AND (
	NULLIF(Source.[Name], Target.[Name]) IS NOT NULL OR NULLIF(Target.[Name], Source.[Name]) IS NOT NULL) THEN
 UPDATE SET
 [Name] = Source.[Name]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([ID],[Name])
 VALUES(Source.[ID],Source.[Name])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE;

GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [dbo].[Role]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[dbo].[Role] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO