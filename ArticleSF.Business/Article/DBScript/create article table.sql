/****** Object:  Table [dbo].[Article]    Script Date: 05-11-2019 17:23:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Article](
	[ArticleId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryId] [int] NULL,
	[Title] [varchar](2500) NOT NULL,
	[Body] [nvarchar](max) NULL,
	[PublishDate] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


