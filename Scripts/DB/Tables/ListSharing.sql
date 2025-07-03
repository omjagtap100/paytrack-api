SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ListSharing](
	[SharingId] [bigint] IDENTITY(1,1) NOT NULL,
	[ListId] [bigint] NOT NULL,
	[SharedWithUserId] [bigint] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
	[UpdatedBy] [varchar](255) NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ListSharing] ADD PRIMARY KEY CLUSTERED 
(
	[SharingId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ListSharing] ADD  DEFAULT (getdate()) FOR [UpdatedDate]
GO
