CREATE PROCEDURE [dbo].[ShowDelete]
	@Id int
AS
BEGIN
    DELETE
	FROM [dbo].[ShowTracker]
	WHERE Id = @Id;
END