CREATE TABLE [dbo].[Regions]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CityId] INT NOT NULL, 
    [Name] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [FK_Regions_To_Cities] FOREIGN KEY (CityId) REFERENCES Cities(Id)
)

GO

CREATE UNIQUE INDEX [IX_Regions_Name] ON [dbo].[Regions] ([CityId], [Name])
