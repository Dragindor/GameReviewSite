using GameReviewSite.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameReviewSite.Core.Contracts
{
    public interface IReviewService
    {
        Task<bool> AddReviewToGame(Review model, string gameId);

        Task<IEnumerable<Review>> GetReviews();
    }
}
