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
        [Key]
        public string Id { get; set; } 

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public string Image { get; set; }

        [Required]
        [Range(0.00, 10.00)]
        public double Rating { get; set; }
        public ICollection<Tag> Tags { get; set; }
    }
}
