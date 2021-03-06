using System.ComponentModel.DataAnnotations;

namespace GameReviewSite.Infrastructure.Data
{
    public class Game
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Url]
        [Required]
        public string Image { get; set; }

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
        public string Publisher { get; set; }

        [Required]
        [StringLength(10)]
        public string ReleaseDate { get; set; }       


        public ICollection<Tag> Tags { get; set; } = new HashSet<Tag>();

        public ICollection<Review> Reviews { get; set; } = new HashSet<Review>();

    }
}
