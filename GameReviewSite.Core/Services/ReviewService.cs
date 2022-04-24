using GameReviewSite.Infrastructure.Data;

namespace GameReviewSite.Core.Services
{
    public class ReviewService
    {

        public readonly ApplicationDbContext data;
        public ReviewService(ApplicationDbContext _data)
        {
            data = _data;
        }
    }
}
