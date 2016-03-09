CREATE TABLE [dbo].[Products]
(
	[Id] NVARCHAR(3) NOT NULL PRIMARY KEY, 
    [DepartmentId] INT NOT NULL, 
    [Name] NVARCHAR(50) NOT NULL, 
    CONSTRAINT [FK_Products_To_Departments] FOREIGN KEY (DepartmentId) REFERENCES Departments(Id)
)

GO

CREATE UNIQUE INDEX [IX_Products_Name] ON [dbo].[Products] ([DepartmentId], [Name])
