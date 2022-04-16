using GameReviewSite.Core.Contracts;
using GameReviewSite.Core.Services;
using GameReviewSite.Infrastructure.Data;
using GameReviewSite.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;


namespace Microsoft.Extensions.DependencyInjection
{
   public static class ServiceCollectionExtension
   {
       public static IServiceCollection AddApplicationServices(this IServiceCollection services)
       {
           services.AddScoped<IApplicationDbRepository, ApplicationDbRepository>();
           services.AddScoped<IUserService, UserService>();
           services.AddScoped<IGameService, GameService>();
   
           return services;
       }
   
       public static IServiceCollection AddApplicationDbContexts(this IServiceCollection services, IConfiguration config)
       {
           var connectionString = config.GetConnectionString("DefaultConnection");
           services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(connectionString));
           services.AddDatabaseDeveloperPageExceptionFilter();
   
           return services;
       }
   }
}
