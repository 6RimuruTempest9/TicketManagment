CREATE PROCEDURE [dbo].[DeleteEventArea]
	@id INT
AS
	DELETE FROM [dbo].[EventArea]
	WHERE [dbo].[EventArea].[Id] = @id;
RETURN 0
