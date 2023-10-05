CREATE PROCEDURE [dbo].[ShowGetInfo]
	@Id int = null,
    @UserId NVARCHAR(50) = null
AS
BEGIN
     SELECT
        Id,
        [Name],
        Genre,
        ImdbRating,
        Series,
        Episode,
        CurrentlyWatching
       
    FROM ShowTracker
    WHERE  (@UserId is not null and UserId = @UserId)
    or (@Id is not null and Id = @Id)
    or (@Id IS NULL AND @UserId IS NULL)
    ORDER BY CurrentlyWatching DESC, [Name];
END