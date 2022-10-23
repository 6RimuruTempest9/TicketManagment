CREATE PROCEDURE [dbo].[UpdateEventArea]
	@id INT,
	@eventId INT,
	@description NVARCHAR(200),
	@coordX INT,
	@coordY INT,
	@price DECIMAL
AS
	UPDATE [dbo].[EventArea]
	SET [dbo].[EventArea].[EventId] = @eventId,
		[dbo].[EventArea].[Description] = @description,
		[dbo].[EventArea].[CoordX] = @coordX,
		[dbo].[EventArea].[CoordY] = @coordY,
		[dbo].[EventArea].[Price] = @price
	WHERE [dbo].[EventArea].[Id] = @id;
RETURN 0
