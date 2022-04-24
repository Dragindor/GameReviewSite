using GameReviewSite.Infrastructure.Data;

namespace GameReviewSite.Core.Contracts
{
    public interface IReviewService
    {
        Task<bool> AddReviewToGame(Review model, string gameId);

        Task<IEnumerable<Review>> GetReviews();
    }
}
