CREATE TABLE [dbo].[Customers]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(75) NOT NULL, 
    [Phone] NVARCHAR(20) NULL, 
    [Mobile] NVARCHAR(20) NULL, 
    [CityId] INT NULL, 
    [RegionId] INT NULL, 
    [Address] NVARCHAR(200) NULL, 
    [IsBlackListed] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT [CK_Customers_Phone_Rquired] CHECK ((Phone Is Not Null) OR (Mobile Is Not Null)), 
    CONSTRAINT [FK_Customers_To_Cities] FOREIGN KEY (CityId) REFERENCES Cities(Id), 
    CONSTRAINT [FK_Customers_To_Regions] FOREIGN KEY (RegionId) REFERENCES Regions(Id) 
)

GO

CREATE INDEX [IX_Customers_Name] ON [dbo].[Customers] ([Name])

GO

CREATE INDEX [IX_Customers_Phone] ON [dbo].[Customers] ([Phone], [Mobile])

GO

CREATE INDEX [IX_Customers_Address] ON [dbo].[Customers] ([CityId], [RegionId], [Address])
