CREATE TABLE [dbo].[Tags]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [TagType] NVARCHAR NOT NULL, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Format] NVARCHAR(50) NULL
)

GO

CREATE UNIQUE INDEX [IX_Tags_Name] ON [dbo].[Tags] ([TagType], [Name])
