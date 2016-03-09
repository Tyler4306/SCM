CREATE PROCEDURE [dbo].[q_take_call]
	@user nvarchar(50),
	@userext nvarchar(10)
AS
	DECLARE @id int;

	SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

	BEGIN TRANSACTION;

	SELECT TOP 1 @id = Id 
		FROM dbo.QIN
		WHERE 
			(IsTaken = 0)
			AND (TExtCode IS NULL)
			AND (TNo IS NOT NULL)
			AND (LinkedTo IS NULL)
		ORDER BY Id

	UPDATE dbo.QIN
		SET IsTaken = 1,
			TakenBy = @user,
			TakenByExt = @userext,
			TakenOn = GETDATE()
		WHERE Id = @id;

	COMMIT TRANSACTION;

	SELECT * FROM QIN
		WHERE Id = @id;

RETURN @id;
