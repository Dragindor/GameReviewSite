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

        public async Task<bool> AddCommentToReview(Comment model)
        {
            var review = await data.Reviews.Where(x => x.Id == model.ReviewId)
                .FirstOrDefaultAsync();

            if (review == null)
            {
                return false;
            }
            try
            {
                Comment comment = new Comment()
                {
                    ReviewId=model.ReviewId,
                    Description = model.Description,
                    Date = DateTime.Now.ToString(),
                    UserId = model.UserId
                };
                review.Comments.Add(comment);

                await data.Comments.AddAsync(comment);
                await data.SaveChangesAsync();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<List<Comment>> GetCommentsByReview(string id)
        {
            var comments = await data.Comments
                .Include(x => x.User)
                .ToListAsync();

            return comments;
        }

        public async Task<IEnumerable<Comment>> GetComments()
        {
            var comments = await data.Comments
                .Include(x=>x.User)
                .Include(x=>x.Review)
                .ToListAsync();

            return comments;
        }
    }
}
