CREATE VIEW [dbo].[ActiveRequests]
	AS SELECT * 
	FROM ServiceRequests
	WHERE (StatusId < 90)
