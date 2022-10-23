CREATE PROCEDURE [dbo].[SelectEventById]
	@id INT
AS
BEGIN
	SELECT
		[dbo].[Event].[Id],
		[dbo].[Event].[Name],
		[dbo].[Event].[Description],
		[dbo].[Event].[LayoutId],
		[dbo].[Event].[StartDate],
		[dbo].[Event].[EndDate],
		[dbo].[Event].[ImageUrl]
	FROM [dbo].[Event]
	WHERE [dbo].[Event].[Id] = @id;
END