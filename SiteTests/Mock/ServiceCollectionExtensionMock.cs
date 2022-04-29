using GameReviewSite.Infrastructure.Data;
using GameReviewSite.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SiteTests.Mock
{
    public static class ServiceCollectionExtensionMock
    {
        public static IServiceCollection RemoveService<TService>(this IServiceCollection svc)
        {
            var descriptor = svc.SingleOrDefault(
              d => d.ServiceType ==
                  typeof(TService)) ?? throw new ArgumentException(nameof(TService));

            svc.Remove(descriptor);

            return svc;
        }

        public static IServiceCollection AddTestHttpContextAccessor(this IServiceCollection svc)
        {
            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            var claimsPrincipal = new ClaimsPrincipal(
                new ClaimsIdentity(
                    new Claim[] { new Claim(ClaimTypes.NameIdentifier, WebAppMock.UserId.ToString()) }
                    )
                );

            mockHttpContextAccessor.Setup(x => x.HttpContext.User).Returns(claimsPrincipal);

            svc.RemoveService<IHttpContextAccessor>();

            svc.AddSingleton<IHttpContextAccessor>(mockHttpContextAccessor.Object);

            return svc;
        }

        public static async Task<IServiceProvider> AddTestData(this IServiceProvider serviceProvider)
        {

            using (var scope = serviceProvider.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<ApplicationDbContext>();
                var userManager = scopedServices.GetRequiredService<UserManager<ApplicationUser>>();

                //https://github.com/dotnet/efcore/issues/6282#issuecomment-509684621
                db.ChangeTracker.Clear();

                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                await userManager.CreateAsync(new ApplicationUser
                {
                    Id = WebAppMock.UserId,
                    UserName = "GueneaPig",
                    Email = "mail@mail.com",
                    NormalizedUserName = "GUENEAPIG".Normalize().ToUpperInvariant()
                });

                db.Games.Add(new Game
                {
                    Id = WebAppMock.GameId,
                    Name = "Elden Ring",
                    Image = "https://www.gamespot.com/a/uploads/scale_landscape/43/434805/3944896-untitled-1.jpg",
                    ReleaseDate = "22.10.2022",
                    Price = 59.99,
                    Description = "In the 87 hours that it took me to beat Elden Ring, I was put through an absolute wringer of emotion: Anger as I was beaten down by its toughest challenges.",
                    Developer = "FromSoftware Inc.",
                    Publisher = "FromSoftware Inc., BANDAI NAMCO Entertainment",
                });

                db.ForumMessages.Add(new ForumMessage
                {
                    Id = Guid.NewGuid().ToString(),
                    UserId = WebAppMock.UserId,
                    Description = "WHY DID THIS HAPPEN TO MEEEEEE!!!!",
                    Date = DateTime.Now.ToString(),
                });

                db.Reviews.Add(new Review()
                {
                    Id = WebAppMock.ReviewId,
                    Description = "Random Review",
                    GameId = WebAppMock.GameId,
                    UserId = WebAppMock.UserId,
                    Date = DateTime.Now.ToString(),
                    Rating = 10,
                    Comments = new List<Comment>()
                });

                db.Comments.Add(new Comment()
                {
                    Id = WebAppMock.CommentId,
                    UserId = WebAppMock.UserId,
                    ReviewId=WebAppMock.ReviewId,
                    Description = "Random Review",
                    Date = DateTime.Now.ToString(),
                });

                db.SaveChanges();

                return serviceProvider;
            }
        }
    }
}
