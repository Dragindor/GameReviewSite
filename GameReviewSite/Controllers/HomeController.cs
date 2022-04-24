using GameReviewSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Diagnostics;

namespace GameReviewSite.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> logger;

        private readonly IDistributedCache cache;
        public HomeController(ILogger<HomeController> _logger, IDistributedCache _cache)
        {
            logger = _logger;
            cache = _cache;
        }

        //public async Task<IActionResult> Index()
        //{
        //    DateTime dateTime = DateTime.Now;
        //    var cachedData = await cache.GetStringAsync("cachedTime");
        //
        //    if (cachedData == null)
        //    {
        //        cachedData = dateTime.ToString();
        //        DistributedCacheEntryOptions distributedCacheOptions = new DistributedCacheEntryOptions()
        //        {
        //            SlidingExpiration = TimeSpan.FromSeconds(45),
        //            AbsoluteExpiration = DateTime.Now.AddSeconds(90)
        //        };
        //
        //        await cache.SetStringAsync("cachedTime", cachedData, distributedCacheOptions);
        //    }
        //
        //    return View(nameof(Index), cachedData);
        //}

        public IActionResult Index()
        {
            return View();
        }      

        public IActionResult Reviews()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}