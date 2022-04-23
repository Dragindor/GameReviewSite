using GameReviewSite.Infrastructure.Data;
using System.ComponentModel.DataAnnotations;

namespace GameReviewSite.Core.Models
{
    public class AddGameViewModel
    {
            public string Id { get; set; }

            [Required(ErrorMessage = "{0} is required")]
            [StringLength(50, ErrorMessage = "{0} must be between {2} and {1} characters", MinimumLength = 2)]
            public string Name { get; set; }

            [Required(ErrorMessage = "{0} is required")]
            [Display(Name = "Game Picture")]
            public string Image { get; set; }

            [Required(ErrorMessage = "{0} is required")]
            [StringLength(1000)]
            public string Description { get; set; }

            [Required(ErrorMessage = "{0} is required")]
            [Range(0.00, 999.99)]
            public double Price { get; set; }

            [Required(ErrorMessage = "{0} is required")]
            [StringLength(100)]
            public string Developer { get; set; }

            [Required(ErrorMessage = "{0} is required")]
            [StringLength(100)]
            public string Publisher { get; set; }

            [Required(ErrorMessage = "{0} is required")]
            [StringLength(10)]
            public string ReleaseDate { get; set; }           
        
            public ICollection<Tag> Tags { get; set; }
        
    }
}
