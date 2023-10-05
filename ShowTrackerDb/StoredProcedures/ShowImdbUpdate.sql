CREATE PROCEDURE [dbo].[ShowImdbUpdate]
	@Name NVARCHAR(50),
	@ImdbRating DECIMAL(2,1)
AS
BEGIN
   UPDATE ShowTracker
   SET
      ImdbRating = @ImdbRating
   WHERE [Name] = @Name

  
END;