﻿CREATE TABLE [dbo].[Purchase]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1),
	[UserId] NVARCHAR(450) NOT NULL,
	[EventSeatId] INT NOT NULL,
	[Time] DATETIME NOT NULL,
	[Price] DECIMAL NOT NULL
)