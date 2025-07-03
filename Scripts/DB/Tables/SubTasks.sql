SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubTasks](
	[SubTaskID] [bigint] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[ParentItemID] [bigint] NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[IsCompleted] [bit] NOT NULL DEFAULT ((0)),
	[CompletedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL DEFAULT (getdate()),
	[UpdatedBy] [varchar](255) NOT NULL
)
GO