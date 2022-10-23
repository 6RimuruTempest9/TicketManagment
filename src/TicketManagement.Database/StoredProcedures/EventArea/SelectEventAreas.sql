CREATE PROCEDURE [dbo].[SelectEventAreas] AS
BEGIN
	SELECT
		[dbo].[EventArea].[Id],
		[dbo].[EventArea].[EventId],
		[dbo].[EventArea].[Description],
		[dbo].[EventArea].[CoordX],
		[dbo].[EventArea].[CoordY],
		[dbo].[EventArea].[Price]
	FROM [dbo].[EventArea];
END