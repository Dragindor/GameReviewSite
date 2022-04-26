using GameReviewSite.Core.Contracts;
using GameReviewSite.Core.Models;
using GameReviewSite.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GameReviewSite.Core.Services
{
    public class ReviewService : IReviewService
    {

        private readonly ApplicationDbContext data;
        public ReviewService(ApplicationDbContext _data)
        {
            data = _data;
        }

        public async Task<bool> AddReviewToGame(Review model)
        {
            var game = await data.Games.Where(x => x.Id == model.GameId)
                .Include(x => x.Reviews)
                .FirstOrDefaultAsync();

            if (game == null)
            {
                return false;
            }
            try
            {
                Review review = new Review()
                {
                    GameId = game.Id,
                    Description = model.Description,
                    Date = DateTime.Now.ToString(),
                    Rating=model.Rating,
                    UserId=model.UserId     
            };
                game.Reviews.Add(review);

                double score = 0;

                foreach (var rating in game.Reviews)
                {
                    score += rating.Rating;
                }

                score = score / game.Reviews.Count();

                game.Rating = score;

                await data.Reviews.AddAsync(review);
                await data.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        public async Task<Review> GetReviewById(string id)
        {
            var reviews = await data.Reviews
                .Include(x => x.Comments)
                .Include(x=>x.User)
                .Where(x => x.Id==id)
                .FirstOrDefaultAsync();

            return reviews;
        }

        public async Task<IEnumerable<Review>> GetReviews()
        {
            var reviews = await data.Reviews
                .Include(x => x.Comments)
                .Include(x=>x.Game)
                .Include(x=>x.User)
                .ToListAsync();

            return reviews;
        }

        public async Task<List<AllGameReviewsViewModel>> GetReviewsByGame(string id)
        {
            var reviews = await data.Reviews
                .Include(x=>x.User)
                .Where(x=>x.GameId==id)
                .Select(x=>new AllGameReviewsViewModel
                {
                    id=x.Id,
                    UserName=x.User.UserName,
                    Date=x.Date,
                    Description=x.Description,
                    Rating=x.Rating,
                    commentsCount=x.Comments.Count()
                })
                .ToListAsync();

            return reviews;
        }
    }
}
