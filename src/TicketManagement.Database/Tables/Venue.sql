CREATE TABLE [dbo].[Venue]
(
	[Id] int identity primary key,
	[Description] nvarchar(1024) NOT NULL,
	[Address] nvarchar(512) NOT NULL,
	[Phone] nvarchar(32),
)
