CREATE TABLE [dbo].CallArchives
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [TDate] NVARCHAR(15) NULL, 
    [TTime] NVARCHAR(15) NULL, 
    [TExtCode] NVARCHAR(10) NULL, 
    [TLineNo] NVARCHAR(10) NULL, 
    [THeader] NVARCHAR(50) NULL, 
    [TNo] NVARCHAR(20) NULL, 
    [TDur] NVARCHAR(20) NULL, 
    [IsTaken] BIT NOT NULL DEFAULT 0, 
    [TakenByExt] NVARCHAR(10) NULL, 
    [TakenBy] NVARCHAR(50) NULL, 
    [TakenOn] DATETIME NULL, 
    [LogTime] DATETIME NOT NULL DEFAULT getdate(), 
    [LinkedTo] INT NULL
)
