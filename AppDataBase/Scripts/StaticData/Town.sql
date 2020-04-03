﻿SET NOCOUNT ON

MERGE INTO [dbo].[Town] AS Target
USING (VALUES
  ('005','Alajärvi',NULL)
 ,('009','Alavieska',NULL)
 ,('010','Alavus',NULL)
 ,('016','Asikkala',NULL)
 ,('018','Askola',NULL)
 ,('019','Aura',NULL)
 ,('020','Akaa',NULL)
 ,('035','Brändö',NULL)
 ,('043','Eckerö',NULL)
 ,('046','Enonkoski',NULL)
 ,('047','Enontekiö',NULL)
 ,('049','Espoo',NULL)
 ,('050','Eura',NULL)
 ,('051','Eurajoki',NULL)
 ,('052','Evijärvi',NULL)
 ,('060','Finström',NULL)
 ,('061','Forssa',NULL)
 ,('062','Föglö',NULL)
 ,('065','Geta',NULL)
 ,('069','Haapajärvi',NULL)
 ,('071','Haapavesi',NULL)
 ,('072','Hailuoto',NULL)
 ,('074','Halsua',NULL)
 ,('075','Hamina',NULL)
 ,('076','Hammarland',NULL)
 ,('077','Hankasalmi',NULL)
 ,('078','Hanko',NULL)
 ,('079','Harjavalta',NULL)
 ,('081','Hartola',NULL)
 ,('082','Hattula',NULL)
 ,('086','Hausjärvi',NULL)
 ,('090','Heinävesi',NULL)
 ,('091','Helsinki',NULL)
 ,('092','Vantaa',NULL)
 ,('097','Hirvensalmi',NULL)
 ,('098','Hollola',NULL)
 ,('099','Honkajoki',NULL)
 ,('102','Huittinen',NULL)
 ,('103','Humppila',NULL)
 ,('105','Hyrynsalmi',NULL)
 ,('106','Hyvinkää',NULL)
 ,('108','Hämeenkyrö',NULL)
 ,('109','Hämeenlinna',NULL)
 ,('111','Heinola',NULL)
 ,('139','Ii',NULL)
 ,('140','Iisalmi',NULL)
 ,('142','Iitti',NULL)
 ,('143','Ikaalinen',NULL)
 ,('145','Ilmajoki',NULL)
 ,('146','Ilomantsi',NULL)
 ,('148','Inari',NULL)
 ,('149','Inkoo',NULL)
 ,('151','Isojoki',NULL)
 ,('152','Isokyrö',NULL)
 ,('153','Imatra',NULL)
 ,('165','Janakkala',NULL)
 ,('167','Joensuu',NULL)
 ,('169','Jokioinen',NULL)
 ,('170','Jomala',NULL)
 ,('171','Joroinen',NULL)
 ,('172','Joutsa',NULL)
 ,('176','Juuka',NULL)
 ,('177','Juupajoki',NULL)
 ,('178','Juva',NULL)
 ,('179','Jyväskylä',NULL)
 ,('181','Jämijärvi',NULL)
 ,('182','Jämsä',NULL)
 ,('186','Järvenpää',NULL)
 ,('202','Kaarina',NULL)
 ,('204','Kaavi',NULL)
 ,('205','Kajaani',NULL)
 ,('208','Kalajoki',NULL)
 ,('211','Kangasala',NULL)
 ,('213','Kangasniemi',NULL)
 ,('214','Kankaanpää',NULL)
 ,('216','Kannonkoski',NULL)
 ,('217','Kannus',NULL)
 ,('218','Karijoki',NULL)
 ,('224','Karkkila',NULL)
 ,('226','Karstula',NULL)
 ,('230','Karvia',NULL)
 ,('231','Kaskinen',NULL)
 ,('232','Kauhajoki',NULL)
 ,('233','Kauhava',NULL)
 ,('235','Kauniainen',NULL)
 ,('236','Kaustinen',NULL)
 ,('239','Keitele',NULL)
 ,('240','Kemi',NULL)
 ,('241','Keminmaa',NULL)
 ,('244','Kempele',NULL)
 ,('245','Kerava',NULL)
 ,('249','Keuruu',NULL)
 ,('250','Kihniö',NULL)
 ,('256','Kinnula',NULL)
 ,('257','Kirkkonummi',NULL)
 ,('260','Kitee',NULL)
 ,('261','Kittilä',NULL)
 ,('263','Kiuruvesi',NULL)
 ,('265','Kivijärvi',NULL)
 ,('271','Kokemäki',NULL)
 ,('272','Kokkola',NULL)
 ,('273','Kolari',NULL)
 ,('275','Konnevesi',NULL)
 ,('276','Kontiolahti',NULL)
 ,('280','Korsnäs',NULL)
 ,('284','Koski',NULL)
 ,('285','Kotka',NULL)
 ,('286','Kouvola',NULL)
 ,('287','Kristiinankaupunki',NULL)
 ,('288','Kruunupyy',NULL)
 ,('290','Kuhmo',NULL)
 ,('291','Kuhmoinen',NULL)
 ,('295','Kumlinge',NULL)
 ,('297','Kuopio',NULL)
 ,('300','Kuortane',NULL)
 ,('301','Kurikka',NULL)
 ,('304','Kustavi',NULL)
 ,('305','Kuusamo',NULL)
 ,('309','Outokumpu',NULL)
 ,('312','Kyyjärvi',NULL)
 ,('316','Kärkölä',NULL)
 ,('317','Kärsämäki',NULL)
 ,('318','Kökar',NULL)
 ,('320','Kemijärvi',NULL)
 ,('322','Kemiönsaari',NULL)
 ,('398','Lahti',NULL)
 ,('399','Laihia',NULL)
 ,('400','Laitila',NULL)
 ,('402','Lapinlahti',NULL)
 ,('403','Lappajärvi',NULL)
 ,('405','Lappeenranta',NULL)
 ,('407','Lapinjärvi',NULL)
 ,('408','Lapua',NULL)
 ,('410','Laukaa',NULL)
 ,('416','Lemi',NULL)
 ,('417','Lemland',NULL)
 ,('418','Lempäälä',NULL)
 ,('420','Leppävirta',NULL)
 ,('421','Lestijärvi',NULL)
 ,('422','Lieksa',NULL)
 ,('423','Lieto',NULL)
 ,('425','Liminka',NULL)
 ,('426','Liperi',NULL)
 ,('430','Loimaa',NULL)
 ,('433','Loppi',NULL)
 ,('434','Loviisa',NULL)
 ,('435','Luhanka',NULL)
 ,('436','Lumijoki',NULL)
 ,('438','Lumparland',NULL)
 ,('440','Luoto',NULL)
 ,('441','Luumäki',NULL)
 ,('444','Lohja',NULL)
 ,('445','Parainen',NULL)
 ,('475','Maalahti',NULL)
 ,('478','Maarianhamina',NULL)
 ,('480','Marttila',NULL)
 ,('481','Masku',NULL)
 ,('483','Merijärvi',NULL)
 ,('484','Merikarvia',NULL)
 ,('489','Miehikkälä',NULL)
 ,('491','Mikkeli',NULL)
 ,('494','Muhos',NULL)
 ,('495','Multia',NULL)
 ,('498','Muonio',NULL)
 ,('499','Mustasaari',NULL)
 ,('500','Muurame',NULL)
 ,('503','Mynämäki',NULL)
 ,('504','Myrskylä',NULL)
 ,('505','Mäntsälä',NULL)
 ,('507','Mäntyharju',NULL)
 ,('508','Mänttä-Vilppula',NULL)
 ,('529','Naantali',NULL)
 ,('531','Nakkila',NULL)
 ,('535','Nivala',NULL)
 ,('536','Nokia',NULL)
 ,('538','Nousiainen',NULL)
 ,('541','Nurmes',NULL)
 ,('543','Nurmijärvi',NULL)
 ,('545','Närpiö',NULL)
 ,('560','Orimattila',NULL)
 ,('561','Oripää',NULL)
 ,('562','Orivesi',NULL)
 ,('563','Oulainen',NULL)
 ,('564','Oulu',NULL)
 ,('576','Padasjoki',NULL)
 ,('577','Paimio',NULL)
 ,('578','Paltamo',NULL)
 ,('580','Parikkala',NULL)
 ,('581','Parkano',NULL)
 ,('583','Pelkosenniemi',NULL)
 ,('584','Perho',NULL)
 ,('588','Pertunmaa',NULL)
 ,('592','Petäjävesi',NULL)
 ,('593','Pieksämäki',NULL)
 ,('595','Pielavesi',NULL)
 ,('598','Pietarsaari',NULL)
 ,('599','Pedersören',NULL)
 ,('601','Pihtipudas',NULL)
 ,('604','Pirkkala',NULL)
 ,('607','Polvijärvi',NULL)
 ,('608','Pomarkku',NULL)
 ,('609','Pori',NULL)
 ,('611','Pornainen',NULL)
 ,('614','Posio',NULL)
 ,('615','Pudasjärvi',NULL)
 ,('616','Pukkila',NULL)
 ,('619','Punkalaidun',NULL)
 ,('620','Puolanka',NULL)
 ,('623','Puumala',NULL)
 ,('624','Pyhtää',NULL)
 ,('625','Pyhäjoki',NULL)
 ,('626','Pyhäjärvi',NULL)
 ,('630','Pyhäntä',NULL)
 ,('631','Pyhäranta',NULL)
 ,('635','Pälkäne',NULL)
 ,('636','Pöytyä',NULL)
 ,('638','Porvoo',NULL)
 ,('678','Raahe',NULL)
 ,('680','Raisio',NULL)
 ,('681','Rantasalmi',NULL)
 ,('683','Ranua',NULL)
 ,('684','Rauma',NULL)
 ,('686','Rautalampi',NULL)
 ,('687','Rautavaara',NULL)
 ,('689','Rautjärvi',NULL)
 ,('691','Reisjärvi',NULL)
 ,('694','Riihimäki',NULL)
 ,('697','Ristijärvi',NULL)
 ,('698','Rovaniemi',NULL)
 ,('700','Ruokolahti',NULL)
 ,('702','Ruovesi',NULL)
 ,('704','Rusko',NULL)
 ,('707','Rääkkylä',NULL)
 ,('710','Raasepori',NULL)
 ,('729','Saarijärvi',NULL)
 ,('732','Salla',NULL)
 ,('734','Salo',NULL)
 ,('736','Saltvik',NULL)
 ,('738','Sauvo',NULL)
 ,('739','Savitaipale',NULL)
 ,('740','Savonlinna',NULL)
 ,('742','Savukoski',NULL)
 ,('743','Seinäjoki',NULL)
 ,('746','Sievi',NULL)
 ,('747','Siikainen',NULL)
 ,('748','Siikajoki',NULL)
 ,('749','Siilinjärvi',NULL)
 ,('751','Simo',NULL)
 ,('753','Sipoo',NULL)
 ,('755','Siuntio',NULL)
 ,('758','Sodankylä',NULL)
 ,('759','Soini',NULL)
 ,('761','Somero',NULL)
 ,('762','Sonkajärvi',NULL)
 ,('765','Sotkamo',NULL)
 ,('766','Sottunga',NULL)
 ,('768','Sulkava',NULL)
 ,('771','Sund',NULL)
 ,('777','Suomussalmi',NULL)
 ,('778','Suonenjoki',NULL)
 ,('781','Sysmä',NULL)
 ,('783','Säkylä',NULL)
 ,('785','Vaala',NULL)
 ,('790','Sastamala',NULL)
 ,('791','Siikalatva',NULL)
 ,('831','Taipalsaari',NULL)
 ,('832','Taivalkoski',NULL)
 ,('833','Taivassalo',NULL)
 ,('834','Tammela',NULL)
 ,('837','Tampere',NULL)
 ,('844','Tervo',NULL)
 ,('845','Tervola',NULL)
 ,('846','Teuva',NULL)
 ,('848','Tohmajärvi',NULL)
 ,('849','Toholampi',NULL)
 ,('850','Toivakka',NULL)
 ,('851','Tornio',NULL)
 ,('853','Turku',NULL)
 ,('854','Pello',NULL)
 ,('857','Tuusniemi',NULL)
 ,('858','Tuusula',NULL)
 ,('859','Tyrnävä',NULL)
 ,('886','Ulvila',NULL)
 ,('887','Urjala',NULL)
 ,('889','Utajärvi',NULL)
 ,('890','Utsjoki',NULL)
 ,('892','Uurainen',NULL)
 ,('893','Uusikaarlepyy',NULL)
 ,('895','Uusikaupunki',NULL)
 ,('905','Vaasa',NULL)
 ,('908','Valkeakoski',NULL)
 ,('911','Valtimo',NULL)
 ,('915','Varkaus',NULL)
 ,('918','Vehmaa',NULL)
 ,('921','Vesanto',NULL)
 ,('922','Vesilahti',NULL)
 ,('924','Veteli',NULL)
 ,('925','Vieremä',NULL)
 ,('927','Vihti',NULL)
 ,('931','Viitasaari',NULL)
 ,('934','Vimpeli',NULL)
 ,('935','Virolahti',NULL)
 ,('936','Virrat',NULL)
 ,('941','Vårdö',NULL)
 ,('946','Vöyri',NULL)
 ,('976','Ylitornio',NULL)
 ,('977','Ylivieska',NULL)
 ,('980','Ylöjärvi',NULL)
 ,('981','Ypäjä',NULL)
 ,('989','Ähtäri',NULL)
 ,('992','Äänekoski',NULL)
) AS Source ([ID],[Name],[PostalCode])
ON (Target.[ID] = Source.[ID])
WHEN MATCHED AND (
	NULLIF(Source.[Name], Target.[Name]) IS NOT NULL OR NULLIF(Target.[Name], Source.[Name]) IS NOT NULL OR 
	NULLIF(Source.[PostalCode], Target.[PostalCode]) IS NOT NULL OR NULLIF(Target.[PostalCode], Source.[PostalCode]) IS NOT NULL) THEN
 UPDATE SET
 [Name] = Source.[Name], 
[PostalCode] = Source.[PostalCode]
WHEN NOT MATCHED BY TARGET THEN
 INSERT([ID],[Name],[PostalCode])
 VALUES(Source.[ID],Source.[Name],Source.[PostalCode])
WHEN NOT MATCHED BY SOURCE THEN 
 DELETE;

GO
DECLARE @mergeError int
 , @mergeCount int
SELECT @mergeError = @@ERROR, @mergeCount = @@ROWCOUNT
IF @mergeError != 0
 BEGIN
 PRINT 'ERROR OCCURRED IN MERGE FOR [dbo].[Town]. Rows affected: ' + CAST(@mergeCount AS VARCHAR(100)); -- SQL should always return zero rows affected
 END
ELSE
 BEGIN
 PRINT '[dbo].[Town] rows affected by MERGE: ' + CAST(@mergeCount AS VARCHAR(100));
 END
GO

SET NOCOUNT OFF
GO