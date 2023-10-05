using TvShowTracker.Models;

using DataAccess.DataAccess;

using Dapper;
using DataAccess.Data;

namespace DataAccess.Data
{
    public class ShowData : IShowData
    {
        private readonly ISqlDataAccess _db;

        public ShowData(ISqlDataAccess db)
        {
            _db = db;
        }


        public async Task<List<Show>> GetShows(string userId)
        {
            var results = await _db.LoadData<Show, dynamic>("ShowGetInfo", new { UserId = userId });

            foreach (var show in results)
            {
                show.CurrentlyWatchingText = show.CurrentlyWatching ? "Yes" : "No";
            }

            return results.ToList();
        }

        public async Task<List<Show>> GetAllShows()
        {
            var results = await _db.LoadData<Show, dynamic>("ShowGetInfo", null);


            return results.ToList();
        }

        public async Task<Show> GetShowById(int id)
        {
            var results = await _db.LoadData<Show, dynamic>("ShowGetInfo", new { Id = id });
            return results.FirstOrDefault();
        }
        public Task InsertShow(Show show)
        {

            return _db.SaveData("ShowInsert", new { show.Name, show.Genre, show.ImdbRating, show.UserId });
        }

        public Task UpdateShow(Show show)
        {
            return _db.SaveData("ShowUpdate", new
            {
                show.Id,
                show.Name,
                show.Genre,
                show.Series,
                show.Episode,
                CurrentlyWatching = show.CurrentlyWatching ? 1 : 0
            });
        }

        public async Task UpdateImdbRating(Show show)
        {
            await _db.SaveData("ShowImdbUpdate", new { show.Name, show.ImdbRating });
        }

        public Task DeleteShow(int id)
        {
            return _db.SaveData("ShowDelete", new { Id = id });
        }
    }
}
