CREATE PROCEDURE [dbo].[UpdateEvent]
	@id INT,
	@name NVARCHAR(120),
	@description NVARCHAR(MAX),
	@layoutId INT,
	@startDate DATETIME,
	@endDate DATETIME,
	@imageUrl NVARCHAR(512)
AS BEGIN
	DELETE FROM [dbo].[Event]
	WHERE [dbo].[Event].[Id] = @id;

	EXECUTE AddEvent @name, @description,  @startDate, @endDate, @imageUrl, @layoutId
	
	RETURN 0
END