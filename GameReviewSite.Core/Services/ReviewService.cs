using GameReviewSite.Core.Contracts;
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

        public async Task<bool> AddReviewToGame(Review model,string gameId)
        {
            var game = await data.Games.Where(x => x.Id == gameId)
                .Include(x => x.Reviews)
                .FirstOrDefaultAsync();

            if (game == null)
            {
                return false;
            }
            try
            {
                game.Reviews.Add(model);

                double score = 0;

                foreach (var review in game.Reviews)
                {
                    score += review.Rating;
                }

                score = score / game.Reviews.Count();

                game.Rating = score;

                await data.Reviews.AddAsync(model);
                await data.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        public async Task<IEnumerable<Review>> GetReviews()
        {
            var reviews = await data.Reviews
                .Include(x => x.Comments)
                .ToListAsync();

            return reviews;
        }
    }
}
