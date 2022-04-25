using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameReviewSite.Core.Models
{
    public class AllGameReviewsViewModel
    {
        public string id { get; set; }
        public string UserName { get; set; }

        public string Date { get; set; }

        public string Description { get; set; }

        public double Rating { get; set; }

        public int commentsCount { get; set; }
    }
}
