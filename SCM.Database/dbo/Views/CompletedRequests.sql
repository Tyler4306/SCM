CREATE VIEW [dbo].[CompletedRequests]
	AS SELECT * 
	FROM ServiceRequests
	WHERE StatusId >= 90

