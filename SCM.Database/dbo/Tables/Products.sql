CREATE TABLE [dbo].[Products]
(
	[Id] NVARCHAR(3) NOT NULL PRIMARY KEY, 
    [DepartmentId] INT NOT NULL DEFAULT 1, 
    [Name] NVARCHAR(50) NOT NULL, 
    [IsActive] BIT NOT NULL DEFAULT 1, 
    CONSTRAINT [FK_Products_To_Departments] FOREIGN KEY (DepartmentId) REFERENCES Departments(Id) ON DELETE SET DEFAULT
)

GO

CREATE UNIQUE INDEX [IX_Products_Name] ON [dbo].[Products] ([DepartmentId], [Name])
