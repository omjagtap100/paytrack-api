SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [bigint] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Name] [nvarchar](255) NULL,
	[Email] [nvarchar](255) NULL,
	[Avatar] [nvarchar](MAX) NULL,
	[PhoneNumber] [nvarchar](50) NULL,
	[TOTPSecret] [nvarchar](255) NOT NULL,
	[FailedOTPAttempts] [int] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL DEFAULT GETDATE(),
	[UpdatedBy] [varchar](255) NOT NULL
) ON [PRIMARY]
GO
