using GameReviewSite.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            public byte[] GamePicture { get; set; }

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
            public string publisher { get; set; }

            [Required(ErrorMessage = "{0} is required")]
            [StringLength(10)]
            public string ReleaseDate { get; set; }           

            [Required(ErrorMessage = "{0} is required")]
            [StringLength(300)]
            public string SystemRequirements { get; set; }

            [Required(ErrorMessage = "{0} is required")]
            public ICollection<Tag> Tags { get; set; }
        
    }
}
