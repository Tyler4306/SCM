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

INSERT INTO dbo.Centers VALUES (1, N'Sons of Zouheir Ghreiwati', N'SY0005050012S');

INSERT INTO dbo.Departments VALUES (N'Home Appliances');
INSERT INTO dbo.Departments VALUES (N'Electronics');
INSERT INTO dbo.Departments VALUES (N'Air Conditions');

SET IDENTITY_INSERT dbo.Tags ON;
INSERT INTO DBO.Tags (Id, TagType, Name, Format) VALUES (1, N'C', N'General End User', N'label-default');
INSERT INTO DBO.Tags (Id, TagType, Name, Format) VALUES (2, N'C', N'Dealer', N'label-info');
INSERT INTO DBO.Tags (Id, TagType, Name, Format) VALUES (3, N'C', N'VIP', N'label-success');
INSERT INTO DBO.Tags (Id, TagType, Name, Format) VALUES (4, N'R', N'In-Home Service', N'label-default');
INSERT INTO DBO.Tags (Id, TagType, Name, Format) VALUES (5, N'R', N'Carry-In Service', N'label-primary');
INSERT INTO DBO.Tags (Id, TagType, Name, Format) VALUES (6, N'R', N'Installation', N'label-default');
INSERT INTO DBO.Tags (Id, TagType, Name, Format) VALUES (7, N'R', N'HM', N'label-default');
INSERT INTO DBO.Tags (Id, TagType, Name, Format) VALUES (8, N'R', N'In Warranty', N'label-primary');
INSERT INTO DBO.Tags (Id, TagType, Name, Format) VALUES (9, N'R', N'Out of Warranty', N'label-warning');
INSERT INTO DBO.Tags (Id, TagType, Name, Format) VALUES (10, N'R', N'Extended Card', N'label-warning');
SET IDENTITY_INSERT dbo.Tags OFF;

