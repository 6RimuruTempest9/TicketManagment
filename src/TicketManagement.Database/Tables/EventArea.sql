CREATE TABLE [dbo].[EventArea]
(
	[Id] int identity primary key,
	[EventId] int NOT NULL,
	[Description] nvarchar(1024) NOT NULL,
	[CoordX] int NOT NULL,
	[CoordY] int NOT NULL,
	[Price] decimal NOT NULL
)
