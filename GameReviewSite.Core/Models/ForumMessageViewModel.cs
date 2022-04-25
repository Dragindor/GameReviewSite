using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameReviewSite.Core.Models
{
    public class ForumMessageViewModel
    {
        public string Id { get; set; } 

        public string UserName{ get; set; }

        public string Date { get; set; }

        public string Description { get; set; }
    }
}
