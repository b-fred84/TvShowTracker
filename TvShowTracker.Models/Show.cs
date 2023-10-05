using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TvShowTracker.Models
{
    public class Show
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
        public decimal ImdbRating { get; set; }
        public int Series { get; set; }
        public int Episode { get; set; }
        public bool CurrentlyWatching { get; set; }
        public string CurrentlyWatchingText { get; set; }
        public string UserId { get; set; }
    }
}
