CREATE PROCEDURE [dbo].[q_log_call]
	@tdate nvarchar(15) = NULL,
	@ttime nvarchar(15) = NULL,
	@textcode nvarchar(10) = NULL,
	@tlineno nvarchar(10) = NULL,
	@theader nvarchar(50) = NULL,
	@tno nvarchar(20) = NULL,
	@tdur nvarchar(20) = NULL
AS
	
	DECLARE @id INT;

	INSERT INTO dbo.QIN
		(TDate, TTime, TExtCode, TLineNo, THeader,
			TNo, TDur)
		VALUES
		(@tdate, @ttime, @textcode, @tlineno, @theader,
			@tno, @tdur); 
	
	SET @id = @@IDENTITY;

	-- CLOSE THE CALL BY UPDATING THE LINKED CALL RECORD
	IF(Len(IsNull(@textcode,N'')) > 0 AND Len(IsNull(@TNO,N'')) > 0)
		BEGIN
			DECLARE @linkid INT;
			DECLARE @linkuser NVARCHAR(50);
			DECLARE @linkext NVARCHAR(10);
			DECLARE @linkdate DATETIME;

			-- SELECT FIRST ACTIVE CALL QUEUED / FIFO
			SELECT TOP 1 @linkid = Id, 
				@linkuser = TakenBy,
				@linkext = TakenByExt,
				@linkdate = TakenOn
				FROM dbo.QIN
				WHERE
					(TNo = @tno)
					AND (LinkedTo IS NULL)
				ORDER BY Id

			-- UPDATE THE THE OPENNING RECORD
			UPDATE dbo.QIN
				SET LinkedTo = @id,
					TExtCode = @textcode
				WHERE Id = @linkid;

			-- UPDATE THIS RECORD WITH INFO FROM OPENNING
			UPDATE dbo.QIN
				SET IsTaken = 1,
					TakenBy = @linkuser,
					TakenByExt = @linkext,
					TakenOn = @linkdate,
					LinkedTo = @linkid
				WHERE Id = @id

			-- ARCHIVE THE TWO CALL RECORDS
			INSERT INTO dbo.CallArchives
				SELECT * FROM dbo.QIN
					WHERE Id IN ( @linkid, @id );

			-- DELETE THE TWO RECORDS
			DELETE FROM dbo.QIN
				WHERE Id IN ( @linkid, @id );

		END

RETURN 0
