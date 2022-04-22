using GameReviewSite.Core.Constants;
using GameReviewSite.Core.Contracts;
using GameReviewSite.Core.Models;
using GameReviewSite.Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameReviewSite.Controllers
{
    public class GameController : BaseController
    {
        private readonly IGameService gameService;
        // GET: GameController
        public GameController(IGameService _gameService)
        {
            gameService = _gameService;   
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
        [Authorize(Roles = RoleConstants.Roles.Both)]
        public async Task<IActionResult> Edit(int id)
        {
            return View();
        }

        // POST: GameController/Edit/5
        [HttpPost]
        [Authorize(Roles = RoleConstants.Roles.Both)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: GameController/Delete/5
        [Authorize(Roles = RoleConstants.Roles.Both)]
        public async Task<IActionResult> Delete()
        {
            return View();
        }

        // POST: GameController/Delete/5
        [HttpPost]
        [Authorize(Roles = RoleConstants.Roles.Both)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            if (await gameService.DeleteGame(id))
            {
                ViewData[MessageConstants.SuccessMessage] = "Успешен запис!";
                return RedirectToAction(nameof(ManageGames));
            }
            else
            {
                ViewData[MessageConstants.ErrorMessage] = "Възникна грешка!";
            }
            return View();
        }

        public async Task<IActionResult> ManageGames()
        {
            var games = await gameService.GetGames();

            return View(games);
        }
    }
}
