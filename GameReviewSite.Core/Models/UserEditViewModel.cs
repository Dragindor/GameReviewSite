using System.ComponentModel.DataAnnotations;

namespace GameReviewSite.Core.Models
{
    public class UserEditViewModel
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = "UserName")]
        public string? UserName { get; set; }      
    }
}
