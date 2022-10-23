CREATE TABLE [dbo].[Layout]
(
	[Id] int identity primary key,
	[VenueId] int NOT NULL,
	[Description] nvarchar(1024) NOT NULL,
)
