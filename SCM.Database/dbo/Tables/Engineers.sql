CREATE TABLE [dbo].[Engineers]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [DepartmentId] INT NOT NULL, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Phone] NVARCHAR(20) NULL, 
    CONSTRAINT [FK_Engineers_To_Departments] FOREIGN KEY (DepartmentId) REFERENCES Departments(Id)
)

GO

CREATE INDEX [IX_Engineers_Name] ON [dbo].[Engineers] ([DepartmentId], [Name])
