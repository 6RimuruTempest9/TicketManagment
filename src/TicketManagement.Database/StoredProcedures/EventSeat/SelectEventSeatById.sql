CREATE PROCEDURE [dbo].[SelectEventSeatById]
	@id INT
AS
BEGIN
	SELECT
		[dbo].[EventSeat].[Id],
		[dbo].[EventSeat].[EventAreaId],
		[dbo].[EventSeat].[Row],
		[dbo].[EventSeat].[Number],
		[dbo].[EventSeat].[State]
	FROM [dbo].[EventSeat]
	WHERE [dbo].[EventSeat].[Id] = @id;
END