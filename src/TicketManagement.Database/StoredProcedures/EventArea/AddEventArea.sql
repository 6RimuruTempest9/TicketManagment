CREATE PROCEDURE [dbo].[AddEventArea]
	@eventId INT,
	@description NVARCHAR(200),
	@coordX INT,
	@coordY INT,
	@price DECIMAL
AS
	INSERT INTO [dbo].[EventArea]
		([dbo].[EventArea].[EventId],
		 [dbo].[EventArea].[Description],
		 [dbo].[EventArea].[CoordX],
		 [dbo].[EventArea].[CoordY],
		 [dbo].[EventArea].[Price])
	VALUES (@eventId, @description, @coordX, @coordY, @price);
RETURN @@IDENTITY
