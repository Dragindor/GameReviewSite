using GameReviewSite.Core.Contracts;

namespace GameReviewSite.Controllers
{
    public class ReviewController : BaseController
    {
        private readonly IGameService gameService;
        private readonly IReviewService reviewService;

        public ReviewController(IGameService gameService, IReviewService reviewService)
        {
            this.gameService = gameService;
            this.reviewService = reviewService;
        }


    }
}
