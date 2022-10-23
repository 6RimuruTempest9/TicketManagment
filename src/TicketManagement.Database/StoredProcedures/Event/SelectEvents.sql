CREATE PROCEDURE [dbo].[SelectEvents] AS
BEGIN
	SELECT
		[dbo].[Event].[Id],
		[dbo].[Event].[Name],
		[dbo].[Event].[Description],
		[dbo].[Event].[LayoutId],
		[dbo].[Event].[StartDate],
		[dbo].[Event].[EndDate],
		[dbo].[Event].[ImageUrl]
	FROM [dbo].[Event];
END