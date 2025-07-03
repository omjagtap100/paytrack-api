SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Lists](
	[ListId] [bigint] IDENTITY(1,1) NOT NULL,
	[CreatorUserId] [bigint] NOT NULL,
	[Title] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Icon] [nvarchar](max) NULL,
	[IsDeleted] [bit] NOT NULL DEFAULT ((0)),
	[UpdatedDate] [datetime] NOT NULL,
	[UpdatedBy] [varchar](255) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Lists] ADD PRIMARY KEY CLUSTERED 
(
	[ListId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Lists] ADD  DEFAULT (getdate()) FOR [UpdatedDate]
GO
