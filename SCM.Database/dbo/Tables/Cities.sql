CREATE TABLE [dbo].[Cities]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(50) NOT NULL
)

GO

CREATE UNIQUE INDEX [IX_Cities_Name] ON [dbo].[Cities] ([Name])
