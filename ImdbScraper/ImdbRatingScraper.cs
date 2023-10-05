using HtmlAgilityPack;
using System.Text;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace ImdbScraper;

public class ImdbRatingScraper : IImdbRatingScraper
{
 
    public decimal GetShowRatings(string showName)
    {

        string baseUrl = "https://www.imdb.com/";
        HtmlWeb web = new HtmlWeb();

        string modName = showName.Replace(" ", "%20");

        string searchUrl = baseUrl + "find/?q=" + modName + "&ref_=nv_sr_sm";
        //Console.WriteLine(searchUrl);

        HtmlDocument searchPage = web.Load(searchUrl);

        HtmlNode aTag = searchPage.DocumentNode.SelectSingleNode("//a[@class='ipc-metadata-list-summary-item__t']");

        string link = "";

        if (aTag != null)
        {
            link = aTag.GetAttributeValue("href", "");
            //Console.WriteLine("Link: " + link);
        }
        else
        {
            Console.WriteLine("Link not found.");
        }

        searchUrl = baseUrl + link;
        //Console.WriteLine("for page: " + searchUrl);

        HtmlDocument showPage = web.Load(searchUrl);

        HtmlNode imdbRating = showPage.DocumentNode.SelectSingleNode("//div[@data-testid='hero-rating-bar__aggregate-rating__score']//span[@class='sc-bde20123-1 iZlgcd']");
        decimal rating = 0.0m;

        if (imdbRating != null && decimal.TryParse(imdbRating.InnerText, out decimal parsedRating))
        {
            rating = parsedRating;
        }
        else
        {
            rating = 0.0m;
        }

        //Console.WriteLine(rating);

        return rating;
    }
}

