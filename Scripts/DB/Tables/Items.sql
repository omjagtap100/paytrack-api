SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Items](
	[ItemId] [bigint] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[ListId] [bigint] NOT NULL,
	[Title] [nvarchar](1000) NOT NULL,
	[Notes] [nvarchar](max) NULL,
	[AssignedToUserId] [bigint] NULL,
	[DueDate] [datetime] NULL,
	[IsCompleted] [bit] NOT NULL DEFAULT(0),
	[CompletedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
	[Icon] [nvarchar](max) NULL,
	[Priority] [int] NOT NULL DEFAULT(1),
	[UpdatedDate] [datetime] NOT NULL DEFAULT GETDATE(),
	[UpdatedBy] [varchar](255) NOT NULL
);
