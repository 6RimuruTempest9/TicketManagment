CREATE PROCEDURE [dbo].[DeleteEventSeat]
	@id INT
AS
	DELETE FROM [dbo].[EventSeat]
	WHERE [dbo].[EventSeat].[Id] = @id;
RETURN 0
