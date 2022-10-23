CREATE PROCEDURE [dbo].[SelectEventSeats] AS
BEGIN
	SELECT
		[dbo].[EventSeat].[Id],
		[dbo].[EventSeat].[EventAreaId],
		[dbo].[EventSeat].[Row],
		[dbo].[EventSeat].[Number],
		[dbo].[EventSeat].[State]
	FROM [dbo].[EventSeat];
END