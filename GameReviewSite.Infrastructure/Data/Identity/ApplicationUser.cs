using GameReviewSite.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Warehouse.Infrastructure.Data.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            this.Reviews = new HashSet<Review>();
            this.Comments = new HashSet<Comment>();
        }

        [StringLength(50)]
        public string? FirstName { get; set; }

        [StringLength(50)]
        public string? LastName { get; set; }

        public ICollection<Review> Reviews { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
