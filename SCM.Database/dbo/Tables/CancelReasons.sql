CREATE TABLE [dbo].[CancelReasons]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Reason] NVARCHAR(75) NOT NULL
)

GO

CREATE UNIQUE INDEX [IX_CancelReasons_Reason] ON [dbo].[CancelReasons] ([Reason])
