using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameReviewSite.Infrastructure.Data
{
    public class Tag
    {
        public Tag()
        {
            this.Games = new HashSet<Game>();
        }
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public ICollection<Game> Games { get; set; }
    }
}
