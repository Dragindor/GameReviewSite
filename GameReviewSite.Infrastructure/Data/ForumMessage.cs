using GameReviewSite.Infrastructure.Data.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameReviewSite.Infrastructure.Data
{
    public class ForumMessage
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        [Required]
        public string Date { get; set; }

        [Required]
        [StringLength(100)]
        public string Description { get; set; }
    }
}
