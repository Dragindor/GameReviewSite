using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Infrastructure.Data.Identity;

namespace GameReviewSite.Infrastructure.Data
{
    public class Review
    {
        public Review()
        {
            this.Comments= new HashSet<Comment>();
        }
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
 
        //[Required]
        //public string GameId { get; set; }
        //
        //[ForeignKey(nameof(GameId))]
        //public Game Game { get; set; }
 
        
        [Required]
        public string Date { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        [Range(0.00, 10.00)]
        public double Rating { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}
