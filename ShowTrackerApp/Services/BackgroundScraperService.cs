using DataAccess.Data;
using ImdbScraper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;

//decide if keeping imdb rating, if not then delete this file  

namespace ShowTrackerApp.Services
{
    public class BackgroundScraperService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<BackgroundScraperService> _logger;
        

        public BackgroundScraperService(IServiceScopeFactory scopeFactory, ILogger<BackgroundScraperService> logger)
        {

            _scopeFactory = scopeFactory;
            _logger = logger;
            
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var showData = scope.ServiceProvider.GetRequiredService<ShowData>();
                    var ratingScraper = scope.ServiceProvider.GetRequiredService<IImdbRatingScraper>();

                    try
                    {
                        await ScrapeAndUpdateRatingsAsync(showData, ratingScraper);
                        
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "An error occurred while updating ratings.");
                    }
                }
                
                await Task.Delay(new TimeSpan(8, 0, 00));
            }
        }

        private async Task ScrapeAndUpdateRatingsAsync(ShowData showData, IImdbRatingScraper ratingScraper)
        {
            var shows = await showData.GetAllShows(); 

            foreach (var show in shows)
            {
                var imdbRating = ratingScraper.GetShowRatings(show.Name);
                show.ImdbRating = imdbRating;
                await showData.UpdateImdbRating(show);
            }
        }
    }
}
