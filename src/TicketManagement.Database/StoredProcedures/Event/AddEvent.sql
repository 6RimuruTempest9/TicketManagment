CREATE PROCEDURE [dbo].[AddEvent]
	@name NVARCHAR(120),
	@description NVARCHAR(MAX),
	@startDate DATETIME,
	@endDate DATETIME,
	@imageUrl NVARCHAR(512),
	@layoutId INT
AS BEGIN
	INSERT INTO [dbo].[Event]
		([dbo].[Event].[Name],
		 [dbo].[Event].[Description],
		 [dbo].[Event].[LayoutId],
		 [dbo].[Event].[StartDate],
		 [dbo].[Event].[EndDate],
		 [dbo].[Event].[ImageUrl])
	VALUES (@name, @description, @layoutId, @startDate, @endDate, @imageUrl);

	DECLARE @eventId INT
	SELECT @eventId = IDENT_CURRENT('[dbo].[Event]')

	DECLARE @defaultPrice DECIMAL
	SET @defaultPrice = 1000

	INSERT INTO [dbo].[EventArea]
		([dbo].[EventArea].[EventId],
		 [dbo].[EventArea].[Description],
		 [dbo].[EventArea].[CoordX],
		 [dbo].[EventArea].[CoordY],
		 [dbo].[EventArea].[Price])
	SELECT @eventId,
		   [dbo].[Area].[Description],
		   [dbo].[Area].[CoordX],
		   [dbo].[Area].[CoordY],
		   @defaultPrice
	FROM [dbo].[Area]
	WHERE [dbo].[Area].[LayoutId] = @layoutId

	DECLARE @freePlaceState INT
	SET @freePlaceState = 0

	INSERT INTO [dbo].[EventSeat] ([dbo].[EventSeat].[EventAreaId], [dbo].[EventSeat].[Number], [dbo].[EventSeat].[Row], [dbo].[EventSeat].[State])
	SELECT [EventAreaSubTable].[Id],
		   [SeatSubTable].[Number],
		   [SeatSubTable].[Row],
		   @freePlaceState
	FROM
	(
		SELECT [dbo].[EventArea].[Id],
				[dbo].[EventArea].[Description]
		FROM [dbo].[EventArea]
		INNER JOIN [dbo].[Event] ON [dbo].[Event].[Id] = [dbo].[EventArea].[EventId]
		WHERE [dbo].[Event].[Id] = @eventId
	) [EventAreaSubTable]
	INNER JOIN
	(
		SELECT [dbo].[Seat].[Number],
			   [dbo].[Seat].[Row],
			   [dbo].[Area].[Description]
		FROM [dbo].[Seat]
		INNER JOIN [dbo].[Area] ON [dbo].[Area].[Id] = [dbo].[Seat].[AreaId]
		WHERE [dbo].[Area].[LayoutId] = @layoutId
	) [SeatSubTable]
	ON [EventAreaSubTable].[Description] = [SeatSubTable].[Description]
END