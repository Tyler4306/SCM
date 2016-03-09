CREATE TABLE [dbo].[CustomerTags]
(
	[TagId] INT NOT NULL , 
    [CustomerId] INT NOT NULL, 
    CONSTRAINT [PK_CustomerTags] PRIMARY KEY ([TagId], [CustomerId]), 
    CONSTRAINT [FK_CustomerTags_To_Customers] FOREIGN KEY (CustomerId) REFERENCES Customers(Id), 
    CONSTRAINT [FK_CustomerTags_To_Tags] FOREIGN KEY (TagId) REFERENCES Tags(Id)
)
