using TvShowTracker.Models;

namespace DataAccess.Data
{
    public interface IShowData
    {
        Task DeleteShow(int id);
        Task<List<Show>> GetAllShows();
        Task<Show> GetShowById(int id);
        Task<List<Show>> GetShows(string userId);
        Task InsertShow(Show show);
        Task UpdateImdbRating(Show show);
        Task UpdateShow(Show show);
    }
}