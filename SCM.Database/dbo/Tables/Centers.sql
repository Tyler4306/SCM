CREATE TABLE [dbo].[Centers]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Code] NVARCHAR(20) NULL
)

GO

CREATE UNIQUE INDEX [IX_Centers_Name] ON [dbo].[Centers] (Name)
