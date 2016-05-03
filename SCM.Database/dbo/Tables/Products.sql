CREATE TABLE [dbo].[Products]
(
	[Id] NVARCHAR(3) NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [IsActive] BIT NOT NULL DEFAULT 1 
)

GO

CREATE UNIQUE INDEX [IX_Products_Name] ON [dbo].[Products] ([Name])
