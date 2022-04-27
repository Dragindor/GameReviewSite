using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameReviewSite.Core.Models
{
    public class RecentReviewsViewModel
    {
        public string Id { get; set; }
        public string GameId { get; set; }
        public string GameName { get; set; }
        public string Image { get; set; }

        public string Date { get; set; }

        public string Description { get; set; }

        public double Rating { get; set; }

        public int commentsCount { get; set; }
    }
}
