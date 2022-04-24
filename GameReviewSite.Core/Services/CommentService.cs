using GameReviewSite.Core.Contracts;
using GameReviewSite.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GameReviewSite.Core.Services
{

    public class CommentService : ICommentService
    {
        private readonly ApplicationDbContext data;
        public CommentService(ApplicationDbContext _data)
        {
            data = _data;
        }

        public async Task<bool> AddReviewToGame(Comment model, string reviewId)
        {
            var review = await data.Reviews.Where(x => x.Id == reviewId)
                .FirstOrDefaultAsync();

            if (review == null)
            {
                return false;
            }
            try
            {
                review.Comments.Add(model);

                await data.Comments.AddAsync(model);
                await data.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Comment>> GetReviews()
        {
            var reviews = await data.Comments
                .ToListAsync();

            return reviews;
        }
    }
}
