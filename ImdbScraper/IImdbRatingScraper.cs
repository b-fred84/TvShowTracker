namespace ImdbScraper
{
    public interface IImdbRatingScraper
    {
        decimal GetShowRatings(string showName);
    }
}