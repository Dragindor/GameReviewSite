using GameReviewSite.Core.Constants;
using GameReviewSite.Core.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace GameReviewSite.Controllers
{
    public class TagController : BaseController
    {
        private readonly IGameService gameService;
        private readonly ITagService tagService;
        // GET: GameController
        public TagController(IGameService _gameService, ITagService _tagService)
        {
            gameService = _gameService;
            tagService = _tagService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ManageTags()
        {
            var tags = await tagService.GetTags();
            return View(tags);
        }

        [HttpGet]
        public async Task<IActionResult> AddTag()
        {
            return View();
        }

        // POST: GameController/Create
        [HttpPost]
        //[Authorize(Roles = RoleConstants.Roles.Both)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddTag(string name)
        {

            if (await tagService.CreateTag(name))
            {
                ViewData[MessageConstants.SuccessMessage] = "Успешен запис!";
                return RedirectToAction(nameof(ManageTags));
            }
            else
            {
                ViewData[MessageConstants.ErrorMessage] = "Възникна грешка!";
            }

            return View();
            //try
            //{
            //    return RedirectToAction(nameof(Index));
            //}
            //catch
            //{
            //    return View();
            //}
        }
    }
}
