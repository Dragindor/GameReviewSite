using GameReviewSite.Core.Constants;
using GameReviewSite.Core.Contracts;
using GameReviewSite.Core.Models;
using GameReviewSite.Infrastructure.Data;
using GameReviewSite.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using System.Security.Claims;

namespace GameReviewSite.Controllers
{
    public class GameController : BaseController
    {
        private readonly IGameService gameService;
        private readonly ITagService tagService;
        private readonly IReviewService reviewService;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly UserManager<ApplicationUser> userManager;

        public GameController(IGameService gameService, ITagService tagService, 
            IReviewService reviewService, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            this.gameService = gameService;
            this.tagService = tagService;
            this.reviewService = reviewService;
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
        }

        public async Task<IActionResult> AllGames()
        {
            var games = await gameService.GetGames();
            return View(games);
        }

        [HttpGet]
        public async Task<IActionResult> gameDetails(string id)
        {
            dynamic mymodel = new ExpandoObject();
            mymodel.Game = await gameService.GetGameById(id);
            mymodel.Review = await reviewService.GetReviewsByGame(id);

            return View(mymodel);
        }

        [HttpPost]
        public async Task<IActionResult> gameDetails(Review model)
        {
            ClaimsPrincipal? userContext = httpContextAccessor.HttpContext?.User;
            ApplicationUser? user = await userManager.GetUserAsync(userContext);

            model.UserId = user.Id;

            ViewBag.Description = model.Description;
            ViewBag.Rating = model.Rating;

            if (await reviewService.AddReviewToGame(model))
            {
                ViewData[MessageConstants.SuccessMessage] = "Успешен запис!";
                return RedirectToAction(nameof(gameDetails), new { model.GameId });
            }
            else
            {
                ViewData[MessageConstants.ErrorMessage] = "Възникна грешка!";
            }

            return RedirectToAction(nameof(gameDetails), new { model.GameId });
        }


        [HttpGet]
        public async Task<IActionResult> AddGame()
        {
            ViewBag.Tags = tagService.GetTags().Result.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddGame(AddGameViewModel model)
        {
            
            if (await gameService.CreateGame(model))
            {
                ViewData[MessageConstants.SuccessMessage] = "Успешен запис!";
                return RedirectToAction(nameof(ManageGames));

            }
            else
            {
                ViewData[MessageConstants.ErrorMessage] = "Възникна грешка!";
            }

            return View(model);   
        }

        public async Task<IActionResult> Edit(string id)
        {
            var model = await gameService.GetGameForEdit(id);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AddGameViewModel model)
        {

            if (await gameService.UpdateGame(model))
            {
                ViewData[MessageConstants.SuccessMessage] = "Успешен запис!";
                return RedirectToAction(nameof(ManageGames));
            }
            else
            {
                ViewData[MessageConstants.ErrorMessage] = "Възникна грешка!";
            }

            return View(model);
        }    
        public async Task<IActionResult> ManageGames()
        {
            var games = await gameService.GetGamesToManage();

            return View(games);
        }
        [HttpGet]
        public async Task<IActionResult> EditGameTags(string id)
        {
            ViewBag.gameId = id;
            var game = await gameService.GetGameForEdit(id);
            if (game == null)
            {
                ViewBag.ErrorMessage = $"Game with Id = {id} cannot be found";
                return View("NotFound");
            }
            ViewBag.Name = game.Name;
            var model = new List<AddTagToGame>();
            List<Tag> tags = tagService.GetTags().Result.ToList();

            foreach (var tag in tags)
            {
                var addTagToGame = new AddTagToGame
                {
                    Id = tag.Id,
                    Name = tag.Name
                };
                if (await gameService.HasTag(game, tag.Name))
                {
                    addTagToGame.Selected = true;
                }
                else
                {
                    addTagToGame.Selected = false;
                }
                model.Add(addTagToGame);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditGameTags(List<AddTagToGame> model, string id)
        {
            var game = await gameService.GetGameForEdit(id);
            if (game == null)
            {
                return View();
            }
            List<Tag> tags = tagService.GetTags().Result.ToList();
            var result = await tagService.RemoveFromTagsAsync(game, tags);
            if (!result)
            {
                ModelState.AddModelError("", "Cannot remove user existing roles");
                return View(model);
            }
            result = await tagService.AddToTagsAsync(game, model);
            if (!result)
            {
                ModelState.AddModelError("", "Cannot add selected roles to user");
                return View(model);
            }
            
            return RedirectToAction(nameof(ManageGames));
        }
    }
}
