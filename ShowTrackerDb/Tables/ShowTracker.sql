CREATE TABLE [dbo].[ShowTracker]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Genre] NVARCHAR(100) NULL, 
    [ImdbRating] DECIMAL(2, 1) NULL, 
    [Series] INT NULL, 
    [Episode] INT NULL, 
    [CurrentlyWatching] BIT NULL, 
    [UserId] NVARCHAR(50) NULL
)
