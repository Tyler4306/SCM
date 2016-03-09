
EXEC dbo.q_log_call N'3/ 8/16', N'10:35AM', NULL, 
	NULL, N'< DISA  INCOMING >', N'0113330408', NULL;

EXEC dbo.q_log_call N'3/ 8/16', N'10:35AM', NULL, 
	NULL, N'< XXXDISA  INCOMING >', NULL, NULL;


EXEC dbo.q_log_call N'3/ 8/16', N'10:35AM', NULL, 
	NULL, N'< DISA  INCOMING >', N'0993585738', NULL;

EXEC dbo.q_log_call N'3/ 8/16', N'10:35AM', N'112', 
	NULL, N'< XXXDISA  INCOMING >', NULL, NULL;

EXEC dbo.q_take_call N'Home', N'112';
EXEC dbo.q_take_call N'Mobile', N'111';

EXEC dbo.q_log_call N'3/ 8/16', N'11:10AM', N'112', 
	N'03', N'< DISA  INCOMING >', N'0113330408', N'0:35'':0"';

EXEC dbo.q_log_call N'3/ 8/16', N'11:12AM', NULL, 
	NULL, N'< DISA  INCOMING >', N'0113332967', NULL;

EXEC dbo.q_take_call N'Home', N'112';


EXEC dbo.q_log_call N'3/ 8/16', N'10:35AM', N'111', 
	N'04', N'< DISA  INCOMING >', N'0993585738', N'0:40'':0"';

EXEC dbo.q_log_call N'3/ 8/16', N'10:35AM', N'112', 
	NULL, N'< XXXDISA  INCOMING >', NULL, NULL;

EXEC dbo.q_log_call N'3/ 8/16', N'11:20AM', N'112', 
	N'03', N'< DISA  INCOMING >', N'0113332967', N'0:8'':0"';