CREATE PROCEDURE [dbo].[DeleteEvent]
	@id INT
AS BEGIN
	DELETE FROM [dbo].[Event]
	WHERE [dbo].[Event].[Id] = @id;

	RETURN 0
END