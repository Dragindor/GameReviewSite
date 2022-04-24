using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameReviewSite.Core.Models
{
    public class ManageGamesViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }

        public double Price { get; set; }

        public string Developer { get; set; }

        public string Publisher { get; set; }

        public string ReleaseDate { get; set; }

    }
}
