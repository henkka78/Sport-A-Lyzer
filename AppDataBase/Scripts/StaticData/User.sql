SET IDENTITY_INSERT [dbo].[User] ON

MERGE INTO [dbo].[User] AS Target
USING (VALUES
  (1,'Henri','Leppä','henkka','FuJF2EbvCGre2ugqokq4+Q+MJwJQKhLbLjetl6LnCKFXJVl+','92ABAD75-08DA-4AE9-B335-D5FB9E2D1DB5                                                                                                                                                                                                                           ')
) AS Source ([ID],[FirstName],[LastName],[UserName],[Password],[RoleID])
ON (Target.[ID] = Source.[ID])
WHEN MATCHED AND (
	NULLIF(Source.[FirstName], Target.[FirstName]) IS NOT NULL OR NULLIF(Target.[FirstName], Source.[FirstName]) IS NOT NULL OR 
	NULLIF(Source.[LastName], Target.[LastName]) IS NOT NULL OR NULLIF(Target.[LastName], Source.[LastName]) IS NOT NULL OR 
	NULLIF(Source.[UserName], Target.[UserName]) IS NOT NULL OR NULLIF(Target.[UserName], Source.[UserName]) IS NOT NULL OR 
	NULLIF(Source.[Password], Target.[Password]) IS NOT NULL OR NULLIF(Target.[Password], Source.[Password]) IS NOT NULL OR 
	NULLIF(Source.[RoleID], Target.[RoleID]) IS NOT NULL OR NULLIF(Target.[RoleID], Source.[RoleID]) IS NOT NULL) THEN
 UPDATE SET
 [FirstName] = Source.[FirstName], 
[LastName] = Source.[LastName], 
[UserName] = Source.[UserName], 
[Password] = Source.[Password],
[RoleID] = Source.[RoleID]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([ID],[FirstName],[LastName],[UserName],[Password],[RoleID])
 VALUES(Source.[ID],Source.[FirstName],Source.[LastName],Source.[UserName],Source.[Password],Source.[RoleID])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE;

GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [dbo].[User]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[dbo].[User] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET IDENTITY_INSERT [dbo].[User] OFF
GO