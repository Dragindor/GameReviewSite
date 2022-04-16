using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameReviewSite.Infrastructure.Data
{
    public class Game
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public byte[] Image { get; set; }

        [Required]
        [Range(0.00, 10.00)]
        public double Rating { get; set; }

        [Required]
        [Range(0.00,999.99)]
        public double Price { get; set; }

        [Required]
        [StringLength(1000)]
        public string Description { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Developer { get; set; }

        [Required]
        [StringLength(100)]
        public string publisher { get; set; }

        [Required]
        [StringLength(10)]
        public string ReleaseDate { get; set; }       

        [Required]
        [StringLength(300)]
        public string SystemRequirements { get; set; }

        public ICollection<Tag> Tags { get; set; } = new HashSet<Tag>();

        public ICollection<Review> Reviews { get; set; } = new HashSet<Review>();

    }
}
