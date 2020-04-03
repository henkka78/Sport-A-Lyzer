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



:r .\Scripts\StaticData\Sports.sql
:r .\Scripts\StaticData\GoalTypes.sql
:r .\Scripts\StaticData\GameEventType.sql
:r .\Scripts\StaticData\Town.sql
:r .\Scripts\StaticData\Role.sql
:r .\Scripts\StaticData\User.sql

GO
