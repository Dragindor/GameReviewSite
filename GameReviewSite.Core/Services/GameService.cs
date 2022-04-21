using GameReviewSite.Core.Contracts;
using GameReviewSite.Core.Models;
using GameReviewSite.Infrastructure.Data;
using GameReviewSite.Infrastructure.Data.Common;
using GameReviewSite.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GameReviewSite.Core.Services
{
    public class GameService : IGameService
    {
        public readonly ApplicationDbContext data;
        public readonly IApplicationDbRepository repo;
        public GameService(ApplicationDbContext _data, IApplicationDbRepository _repo)
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

        public async Task<bool> CreateGame(AddGameViewModel model)
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
        public async Task<bool> DeleteGame(string gameid)
        {
            var game = data.Games.FirstOrDefault(x=>x.Id==gameid);
            if (game==null)
            {
                return false;
            }
            data.Games.Remove(game);
            await data.SaveChangesAsync();
            return true;
        }

        public Task<bool> AddReviewToGame(string gameId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<AllGamesViewModel>> GetGames()
        {
            return await repo.All<Game>()
                .Select(x => new AllGamesViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Image = x.Image,
                    Rating = x.Rating,
                    Tags = x.Tags
                })
                .ToListAsync();
        }       
    }    
}

