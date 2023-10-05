CREATE PROCEDURE [dbo].[ShowUpdate]
	@Id int,
	@Name NVARCHAR(50),
	@Genre NVARCHAR(50),
	@Series int,
	@Episode int,
	@CurrentlyWatching BIT
AS
BEGIN
   UPDATE ShowTracker
   SET
      [Name] = @Name,
	  Genre = @Genre,
      Series = @Series,
	  Episode = @Episode,
	  CurrentlyWatching = @CurrentlyWatching
   WHERE Id = @Id

  
END;