using GameReviewSite.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameReviewSite.Core.Models
{
    public class GameDetailsViewModel
    {
        public string Id { get; set; } 

        public string Name { get; set; }

        public string Image { get; set; }

        public double Rating { get; set; }

        public double Price { get; set; }

        public string Description { get; set; }

        public string Developer { get; set; }

        public string Publisher { get; set; }

        public string ReleaseDate { get; set; }


        public string Tags { get; set; } 

        public int ReviewsCount { get; set; } 
    }
}
