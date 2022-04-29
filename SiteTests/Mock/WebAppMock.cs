using GameReviewSite.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace SiteTests.Mock
{
    public record DependencyScope(AsyncServiceScope Scope, ApplicationDbContext DB)
    {
        public T ResolveService<T>()
            where T : notnull
        {
            return Scope.ServiceProvider.GetRequiredService<T>();
        }
    };

    public class WebAppMock : WebApplicationFactory<Program>
    {
        protected ApplicationDbContext dbContext;
        public static readonly string UserId = Guid.NewGuid().ToString();
        public static readonly string GameId = Guid.NewGuid().ToString();
        public static readonly string ReviewId = Guid.NewGuid().ToString();
        public static readonly string CommentId = Guid.NewGuid().ToString();
        public static readonly string ForumMessageId = Guid.NewGuid().ToString();

        public DependencyScope InitDb()
        {
            var scope = Services.CreateAsyncScope();
            dbContext = ResolveService<ApplicationDbContext>();
            Services.AddTestData().GetAwaiter().GetResult();
            return new(scope, dbContext);
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);
            builder
            .ConfigureTestServices(async svc =>
            {
                svc.RemoveService<DbContextOptions<ApplicationDbContext>>();

                svc.AddAuthentication("Test")
                   .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>("Test", x => { });

                Guid id = Guid.NewGuid();

                svc.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseInMemoryDatabase("MemoryDataBase" + id);
                });

                svc.AddTestHttpContextAccessor();

                var sp = svc.BuildServiceProvider();

                //await sp.AddTestData();

            });
        }

        private T ResolveService<T>()
         where T : notnull
        {
            return this.Services.CreateAsyncScope().ServiceProvider.GetRequiredService<T>();
        }
    }
}
