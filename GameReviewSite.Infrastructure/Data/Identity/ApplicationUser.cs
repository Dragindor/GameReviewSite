using GameReviewSite.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace GameReviewSite.Infrastructure.Data.Identity
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(50)]
        public string? FirstName { get; set; }

        [StringLength(50)]
        public string? LastName { get; set; }
        
        public byte[] ProfilePicture { get; set; }

        public ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
    }
}
