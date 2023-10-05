CREATE PROCEDURE [dbo].[ShowInsert]

	   @Name NVARCHAR(50),
       @Genre NVARCHAR(50),
       @ImdbRating DECIMAL(2,1) = null,
       @UserId NVARCHAR(50)

AS
BEGIN
    INSERT INTO ShowTracker ([Name], Genre, ImdbRating, Series, Episode, CurrentlyWatching, UserId)
    VALUES (@Name, @Genre, @ImdbRating, 1, 1, 0, @UserId);

END
