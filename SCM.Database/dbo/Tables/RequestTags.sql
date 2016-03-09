CREATE TABLE [dbo].[RequestTags]
(
	[TagId] INT NOT NULL , 
    [ServiceRequestId] INT NOT NULL, 
    CONSTRAINT [PK_RequestTags] PRIMARY KEY ([TagId], [ServiceRequestId]), 
    CONSTRAINT [FK_RequestTags_To_Tags] FOREIGN KEY (TagId) REFERENCES Tags(Id), 
    CONSTRAINT [FK_RequestTags_To_ServiceRequests] FOREIGN KEY (ServiceRequestId) REFERENCES ServiceRequests(Id)
)
