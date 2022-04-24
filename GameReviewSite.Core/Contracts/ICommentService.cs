using GameReviewSite.Infrastructure.Data;

namespace GameReviewSite.Core.Contracts
{
    public interface ICommentService
    {
        Task<bool> AddReviewToGame(Comment model, string gameId);

        Task<IEnumerable<Comment>> GetReviews();
    }
}
