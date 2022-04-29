using GameReviewSite.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace SiteTests.Mocking
{
    public class ApplicationFactory
    {
        public record DependencyScope(AsyncServiceScope Scope, ApplicationDbContext Db)
        {
            public T ResolveService<T>()
                where T : notnull
            {
                return Scope.ServiceProvider.GetRequiredService<T>();
            }
        };

        public class CustomWebApplicationFactory : WebApplicationFactory<Program>
        {

            //public static readonly Guid UserId = Guid.NewGuid();
            //public static readonly Guid GameId = Guid.NewGuid();
            //public static readonly Guid RatingId = Guid.NewGuid();
            private readonly AsyncServiceScope scope;
            protected ApplicationDbContext db;

            protected override void ConfigureClient(HttpClient client)
            {
                base.ConfigureClient(client);
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Test");
            }

            public DependencyScope InitDb()
            {
                var scope = Services.CreateAsyncScope();
                db = ResolveService<ApplicationDbContext>();
                //Services.AddTestData().GetAwaiter().GetResult();
                return new(scope, db);
            }

            private T ResolveService<T>()
             where T : notnull
            {
                return this.Services.CreateAsyncScope().ServiceProvider.GetRequiredService<T>();
            }
        }
    }
}
