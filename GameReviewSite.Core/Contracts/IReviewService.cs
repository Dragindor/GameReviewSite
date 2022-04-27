using GameReviewSite.Core.Models;
using GameReviewSite.Infrastructure.Data;

namespace GameReviewSite.Core.Contracts
{
    public interface IReviewService
    {
        Task<bool> AddReviewToGame(Review model);

        Task<IEnumerable<Review>> GetReviews();
        Task<IEnumerable<RecentReviewsViewModel>> GetRecentReviews();

        Task<List<AllGameReviewsViewModel>> GetReviewsByGame(string id);
        Task<Review> GetReviewById(string id);
    }
}
