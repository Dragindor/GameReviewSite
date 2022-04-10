using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Warehouse.Infrastructure.Data.Identity;

namespace GameReviewSite.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           //modelBuilder.Entity<Comment>()
           //    .HasOne<ApplicationUser>(x => x.User)
           //.WithMany(g => g.Comments)
           //.HasForeignKey(s => s.UserId)
           //.OnDelete(DeleteBehavior.NoAction);
           //
           //modelBuilder.Entity<Comment>()
           //    .HasOne<Review>(x => x.Review)
           //.WithMany(g => g.Comments)
           //.HasForeignKey(s => s.ReviewId);
           //
           //modelBuilder.Entity<ApplicationUser>()
           //    .HasMany<Comment>(x => x.Comments)
           //    .WithOne()
           //    .HasForeignKey(x => x.ReviewId)
           //    .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);            
            modelBuilder.Entity<IdentityUser>(entity =>
            {
                entity.ToTable(name: "User");
            });
            modelBuilder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "Role");
            });
            modelBuilder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles");
            });
            modelBuilder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims");
            });
            modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins");
            });
            modelBuilder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaims");
            });
            modelBuilder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens");
            });
        }

        public DbSet<Game> Games { get; set; }

        public DbSet<Comment> Comments { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Tag> Tags { get; set; }

    }
}