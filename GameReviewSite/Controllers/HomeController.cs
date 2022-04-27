using GameReviewSite.Core.Contracts;
using GameReviewSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Diagnostics;
using System.Dynamic;

namespace GameReviewSite.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> logger;

        private readonly IDistributedCache cache;
        private readonly IGameService gameService;
        private readonly ITagService tagService;
        private readonly IReviewService reviewService;
        public HomeController(ILogger<HomeController> _logger, IDistributedCache _cache, IGameService gameService,
             ITagService tagService, IReviewService reviewService)
        {
            this.reviewService = reviewService;
            this.gameService = gameService;
            this.tagService = tagService;
            logger = _logger;
            cache = _cache;
        }

        public async Task<IActionResult> Index()
        {
            dynamic mymodel = new ExpandoObject();
            mymodel.Game = await gameService.GetRecentGames();
            mymodel.Review = await reviewService.GetRecentReviews();

            return View(mymodel);
        }      

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

    }
}