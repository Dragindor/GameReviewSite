using GameReviewSite.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

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

        public async Task<IActionResult> ManageReviews()
        {
            var reviews = await reviewService.GetReviews();

            return View(reviews);
        }
    }
}
