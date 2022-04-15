using System.ComponentModel.DataAnnotations;

namespace GameReviewSite.Core.Models
{
    public class UserEditViewModel
    {
        public string UserId { get; set; }

        [Required]
        [Display(Name = "UserName")]
        public string? UserName { get; set; }      
    }
}
