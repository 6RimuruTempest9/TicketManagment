CREATE PROCEDURE [dbo].[UpdateEventSeat]
	@id INT,
	@eventAreaId INT,
	@row INT,
	@number INT,
	@state INT
AS
	UPDATE [dbo].[EventSeat]
	SET [dbo].[EventSeat].[EventAreaId] = @eventAreaId,
		[dbo].[EventSeat].[Row] = @row,
		[dbo].[EventSeat].[Number] = @number,
		[dbo].[EventSeat].[State] = @state
	WHERE [dbo].[EventSeat].[Id] = @id;
RETURN 0
