CREATE TABLE [dbo].[ServiceRequests]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [CustomerId] INT NOT NULL, 
    [RequestDate] DATETIME NOT NULL DEFAULT getdate(), 
    [StatusId] INT NOT NULL DEFAULT 10, 
    [StatusDate] DATETIME NOT NULL DEFAULT getdate(), 
    [CenterId] INT NOT NULL DEFAULT 1, 
    [RQN] NVARCHAR(20) NULL, 
    [ReceiptNo] NVARCHAR(20) NULL, 
    [DepartmentId] INT NULL, 
    [ProductId] NVARCHAR(3) NULL, 
    [Model] NVARCHAR(50) NULL, 
    [SN] NVARCHAR(50) NULL, 
    [EngineerId] INT NULL, 
    [Description] NVARCHAR(MAX) NULL, 
    [Remarks] NVARCHAR(100) NULL, 
    [ClosingDate] DATETIME NULL, 
    [PendingReasonId] INT NULL, 
    [CancelReasonId] INT NULL, 
    [CreatedBy] NVARCHAR(50) NOT NULL DEFAULT N'SYSTEM', 
    [CreatedOn] DATETIME NOT NULL DEFAULT getdate(), 
    [UpdatedBy] NVARCHAR(50) NOT NULL DEFAULT N'SYSTEM', 
    [UpdatedOn] DATETIME NOT NULL DEFAULT getdate(), 
    [IsDeleted] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT [FK_ServiceRequest_To_Customers] FOREIGN KEY (CustomerId) REFERENCES Customers(Id), 
    CONSTRAINT [CK_ServiceRequest_StatusDate_Invalid] CHECK (StatusDate >= RequestDate), 
    CONSTRAINT [FK_ServiceRequests_To_Centers] FOREIGN KEY (CenterId) REFERENCES Centers(Id), 
    CONSTRAINT [FK_ServiceRequests_To_Departments] FOREIGN KEY (DepartmentId) REFERENCES Departments(Id), 
    CONSTRAINT [FK_ServiceRequests_To_Products] FOREIGN KEY (ProductId) REFERENCES Products(Id), 
    CONSTRAINT [FK_ServiceRequests_To_Engineers] FOREIGN KEY (EngineerId) REFERENCES Engineers(Id), 
    CONSTRAINT [FK_ServiceRequests_To_PendingReasons] FOREIGN KEY (PendingReasonId) REFERENCES PendingReasons(Id), 
    CONSTRAINT [FK_ServiceRequests_To_CancelReasons] FOREIGN KEY (CancelReasonId) REFERENCES CancelReasons(Id) 
)

GO

CREATE INDEX [IX_ServiceRequest_Customer] ON [dbo].[ServiceRequests] ([CustomerId])

GO


CREATE INDEX [IX_ServiceRequests_CustomerId] ON [dbo].[ServiceRequests] ([CustomerId])

GO

CREATE INDEX [IX_ServiceRequests_StatusId] ON [dbo].[ServiceRequests] ([StatusId], [StatusDate])

GO

CREATE INDEX [IX_ServiceRequests_RQN] ON [dbo].[ServiceRequests] ([RQN])

GO

CREATE INDEX [IX_ServiceRequests_ReceiptNo] ON [dbo].[ServiceRequests] ([ReceiptNo])

GO

CREATE INDEX [IX_ServiceRequests_DepartmentId] ON [dbo].[ServiceRequests] ([DepartmentId], [ProductId])

GO

CREATE INDEX [IX_ServiceRequests_Model] ON [dbo].[ServiceRequests] ([ProductId], [IsDeleted])

GO

CREATE INDEX [IX_ServiceRequests_SN] ON [dbo].[ServiceRequests] ([ProductId], [SN])

GO

CREATE INDEX [IX_ServiceRequests_EngineerId] ON [dbo].[ServiceRequests] ([EngineerId])

GO

CREATE INDEX [IX_ServiceRequests_Remarks] ON [dbo].[ServiceRequests] ([Remarks])

GO

CREATE INDEX [IX_ServiceRequests_PendingReasonId] ON [dbo].[ServiceRequests] ([PendingReasonId])

GO

CREATE INDEX [IX_ServiceRequests_CancelReasonId] ON [dbo].[ServiceRequests] ([CancelReasonId])

GO

CREATE INDEX [IX_ServiceRequests_RequestDate] ON [dbo].[ServiceRequests] (RequestDate)
