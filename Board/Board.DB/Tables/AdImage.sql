﻿CREATE TABLE [dbo].[AdImages](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[AdId] [bigint] NOT NULL,
	[Image] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_AdImages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
