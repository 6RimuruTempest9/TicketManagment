CREATE PROCEDURE [dbo].[SelectEventAreaById]
	@id INT
AS
BEGIN
	SELECT
		[dbo].[EventArea].[Id],
		[dbo].[EventArea].[EventId],
		[dbo].[EventArea].[Description],
		[dbo].[EventArea].[CoordX],
		[dbo].[EventArea].[CoordY],
		[dbo].[EventArea].[Price]
	FROM [dbo].[EventArea]
	WHERE [dbo].[EventArea].[Id] = @id;
END