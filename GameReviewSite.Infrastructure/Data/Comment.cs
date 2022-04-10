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
    public class Comment
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }       
        
        public ApplicationUser User { get; set; }

       //[Required]       
       //public string ReviewId { get; set; }
       //
       //[ForeignKey(nameof(ReviewId))]
       //public Review Review { get; set; }

        [Required]
        public string Date { get; set; }

        [Required]
        [StringLength(100)]
        public string Description { get; set; }
    }
}
