﻿CREATE TABLE [dbo].[AspNetUserClaims]
(
	[Id] INT IDENTITY(1,1) NOT NULL,
	[ClaimType] NVARCHAR(max) NULL,
	[ClaimValue] NVARCHAR(max) NULL,
	[UserId] NVARCHAR(450) NOT NULL,
	CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]