SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reminders](
	[ReminderId] [bigint] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[ItemId] [bigint] NOT NULL,
	[ReminderTime] [datetime] NOT NULL,
	[IsSent] [bit] NOT NULL DEFAULT ((0)),
	[IsDeleted] [bit] NOT NULL DEFAULT ((0)),
	[Message] [nvarchar](500) NULL,
	[UpdatedDate] [datetime] NOT NULL DEFAULT (getdate()),
	[UpdatedBy] [varchar](255) NOT NULL
) ON [PRIMARY]
GO