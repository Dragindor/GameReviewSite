using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameReviewSite.Infrastructure.Data
{
    public class GameTag
    {
        [ForeignKey(nameof(Game))]
        public string GameId { get; set; }
        public Game Game { get; set; }

        [ForeignKey(nameof(Tag))]
        public string TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
