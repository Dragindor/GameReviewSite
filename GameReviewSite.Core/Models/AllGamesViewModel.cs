using GameReviewSite.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameReviewSite.Core.Models
{
    public class AllGamesViewModel
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

        public string SystemRequirements { get; set; }
    }
}
