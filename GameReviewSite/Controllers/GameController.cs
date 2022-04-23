using GameReviewSite.Core.Constants;
using GameReviewSite.Core.Contracts;
using GameReviewSite.Core.Models;
using GameReviewSite.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GameReviewSite.Controllers
{
    public class GameController : BaseController
    {
        private readonly IGameService gameService;
        private readonly ITagService tagService;
        // GET: GameController

        public GameController(IGameService gameService, ITagService tagService)
        {
            this.gameService = gameService;
            this.tagService = tagService;
        }

        public IActionResult Index()
        {
            return View();
        }

        // GET: GameController/Details/5

        // GET: GameController/Create
        //[Authorize(Roles = RoleConstants.Roles.Both)]
        [HttpGet]
        public async Task<IActionResult> AddGame()
        {
            ViewBag.Tags = tagService.GetTags().Result.ToList();
            return View();
        }

        // POST: GameController/Create
        [HttpPost]
        //[Authorize(Roles = RoleConstants.Roles.Both)]
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
            //try
            //{
            //    return RedirectToAction(nameof(Index));
            //}
            //catch
            //{
            //    return View();
            //}
        }

        // GET: GameController/Edit/5
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

        // GET: GameController/Delete/5        
        public async Task<IActionResult> ManageGames()
        {
            var games = await gameService.GetGames();

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
            return RedirectToAction("Index");
        }
    }
}
