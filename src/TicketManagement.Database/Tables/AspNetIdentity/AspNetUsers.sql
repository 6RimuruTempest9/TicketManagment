CREATE TABLE [dbo].[AspNetUsers]
(
	[Id] NVARCHAR(450) NOT NULL,
	[AccessFailedCount] INT NOT NULL,
	[ConcurrencyStamp] NVARCHAR(max) NULL,
	[Email] NVARCHAR(256) NULL,
	[EmailConfirmed] BIT NOT NULL,
	[LockoutEnabled] BIT NOT NULL,
	[LockoutEnd] DATETIMEOFFSET(7) NULL,
	[NormalizedEmail] NVARCHAR(256) NULL,
	[NormalizedUserName] NVARCHAR(256) NULL,
	[PasswordHash] NVARCHAR(max) NULL,
	[PhoneNumber] NVARCHAR(max) NULL,
	[PhoneNumberConfirmed] BIT NOT NULL,
	[SecurityStamp] NVARCHAR(max) NULL,
	[TwoFactorEnabled] BIT NOT NULL,
	[UserName] NVARCHAR(256) NULL,
	[FirstName] NVARCHAR(32) NULL,
	[LastName] NVARCHAR(32) NULL,
	[Language] NVARCHAR(32) NULL,
	[TimeZone] NVARCHAR(64) NULL,
	[Balance] DECIMAL CHECK([Balance] >= 0) NOT NULL,
	CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]