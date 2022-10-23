CREATE PROCEDURE [dbo].[AddEventSeat]
	@eventAreaId INT,
	@row INT,
	@number INT,
	@state INT
AS
	INSERT INTO [dbo].[EventSeat]
		([dbo].[EventSeat].[EventAreaId],
		 [dbo].[EventSeat].[Row],
		 [dbo].[EventSeat].[Number],
		 [dbo].[EventSeat].[State])
	VALUES (@eventAreaId, @row, @number, @state);
RETURN @@IDENTITY
