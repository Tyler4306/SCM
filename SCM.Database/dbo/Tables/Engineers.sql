CREATE TABLE [dbo].[Engineers]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [DepartmentId] INT NOT NULL DEFAULT 1, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Phone] NVARCHAR(20) NULL, 
    [IsActive] BIT NOT NULL DEFAULT 1, 
    CONSTRAINT [FK_Engineers_To_Departments] FOREIGN KEY (DepartmentId) REFERENCES Departments(Id) ON DELETE SET DEFAULT
)

GO

CREATE INDEX [IX_Engineers_Name] ON [dbo].[Engineers] ([DepartmentId], [Name])
