using GameReviewSite.Infrastructure.Data;

namespace GameReviewSite.Core.Contracts
{
    public interface ICommentService
    {
        Task<bool> AddCommentToReview(Comment model);

        Task<IEnumerable<Comment>> GetComments();

        Task<List<Comment>> GetCommentsByReview(string id);
    }
}
