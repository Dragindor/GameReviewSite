using GameReviewSite.Core.Contracts;
using GameReviewSite.Core.Models;
using GameReviewSite.Infrastructure.Data;
using GameReviewSite.Infrastructure.Data.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GameReviewSite.Core.Models.AddGameViewModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace GameReviewSite.Core.Services
{
    public class GameService : IGameService
    {
        public readonly ApplicationDbContext data;
        public readonly IRepository repo;
        public GameService(ApplicationDbContext _data,IRepository _repo)
        {
            data = _data;
            repo = _repo;
        }

        private async Task<bool> GameAlreadyExist(string name)
        {
            var game= await data.Games.FirstOrDefaultAsync(x=>x.Name==name);

            if (game==null)
            {
                return false;
            }
            return true;
        }

        public string AddReviewToGame(string userId, string cardId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> CreateGame(AddGameViewModel model, string userId)
        {
            if (await GameAlreadyExist(model.Name))
            {
                return false;
            }           

            var game = new Game
            {
                Name = model.Name,
                Image = model.GamePicture,
                Rating = 0,
                Price=model.Price,
                Description=model.Description,
                Developer=model.Developer,
                publisher=model.publisher,
                ReleaseDate=model.ReleaseDate,
                SystemRequirements=model.SystemRequirements,
                Tags=model.Tags
            };

            await data.Games.AddAsync(game);

            await data.SaveChangesAsync();

            return true;
        }
        public void DeleteGame(string gameid)
        {
            throw new NotImplementedException();
        }

        public List<AllGamesViewModel> GetAllGames()
        {
            throw new NotImplementedException();
        }

        public List<GameReviewsViewModel> GetReviews(string userId)
        {
            throw new NotImplementedException();
        }

        Task<List<AllGamesViewModel>> IGameService.GetAllGames()
        {
            throw new NotImplementedException();
        }

        Task<List<GameReviewsViewModel>> IGameService.GetReviews(string userId)
        {
            throw new NotImplementedException();
        }
    }

    
}

