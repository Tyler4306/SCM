CREATE TABLE [dbo].[PendingReasons]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Reason] NVARCHAR(75) NOT NULL
)

GO

CREATE UNIQUE INDEX [IX_PendingReasons_Reason] ON [dbo].[PendingReasons] ([Reason])
