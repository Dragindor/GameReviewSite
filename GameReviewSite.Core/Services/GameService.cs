using GameReviewSite.Core.Contracts;
using GameReviewSite.Core.Models;
using GameReviewSite.Infrastructure.Data;
using GameReviewSite.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace GameReviewSite.Core.Services
{
    public class GameService : IGameService
    {
        private readonly ApplicationDbContext data;
        private readonly IApplicationDbRepository repo;
        public GameService(ApplicationDbContext _data, IApplicationDbRepository _repo)
        {
            data = _data;
            repo = _repo;
        }

        public async Task<bool> GameAlreadyExist(string name)
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
            var tags = this.data.Tags.ToList();
            if (await GameAlreadyExist(model.Name))
            {
                return false;
            }           
            var game = new Game
            {
                Name = model.Name,
                Image = model.Image,
                Rating = 0,
                Price=model.Price,
                Description=model.Description,
                Developer=model.Developer,
                Publisher=model.Publisher,
                ReleaseDate=model.ReleaseDate,
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
                    Price = x.Price,
                    Developer = x.Developer,
                    Publisher=x.Publisher,
                    ReleaseDate=x.ReleaseDate,
                    Tag=x.Tags.Select(x=>x.Name).FirstOrDefault(),
                    ReviewsCount=x.Reviews.Count()
                })
                .OrderByDescending(x=>x.Rating)
                .ToListAsync();
        }
        public async Task<IEnumerable<ManageGamesViewModel>> GetGamesToManage()
        {
            return await repo.All<Game>()
                .Select(x => new ManageGamesViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Image = x.Image,
                    Price = x.Price,
                    Developer = x.Developer,
                    Publisher = x.Publisher,
                    ReleaseDate = x.ReleaseDate
                })
                .ToListAsync();
        }

        public async Task<Game> GetGameForEdit(string id)
        {
            var game = data.Games.Where(x => x.Id == id)
                .Include(x => x.Tags)
                .FirstOrDefault();
            if (game == null)
            {
                throw new ArgumentNullException(nameof(game));
            }

            var foundGame = new Game()
            {
                Id = game.Id,
                Name = game.Name,
                Image = game.Image,
                Price = game.Price,
                Description = game.Description,
                Developer = game.Developer,
                Publisher = game.Publisher,
                ReleaseDate = game.ReleaseDate,
                Tags=game.Tags
            };

            return foundGame;
        }
        public async Task<bool> UpdateGame(AddGameViewModel model)
        {
            bool result = false;

            var game = await repo.GetByIdAsync<Game>(model.Id);

            if (game != null)
            {
                game.Name = model.Name;
                game.Image = model.Image;
                game.ReleaseDate = model.ReleaseDate;
                game.Price = model.Price;
                game.Developer = model.Developer;
                game.Publisher = model.Publisher;
                game.Description = model.Description;
                
                await repo.SaveChangesAsync();
                result = true;
            }
            return result;
        }

        public async Task<bool> HasTag(Game game, string TagName)
        {
            var tag = data.Tags.Where(x=>x.Name==TagName).FirstOrDefault();

            if (game.Tags==null)
            {
                return false;
            }
            if (game.Tags.Contains(tag))
            {
                return true;
            }
            return false;
        }

        public async Task<GameDetailsViewModel> GetGameById(string id)
        {
                var game = await data.Games.Where(x => x.Id == id)
                    .Include(x => x.Reviews)
                    .Include(x => x.Tags)
                    .FirstOrDefaultAsync();

                if (game == null)
                {
                    throw new ArgumentNullException(nameof(game));
                }

                StringBuilder sb = new StringBuilder();

                foreach (var tag in game.Tags)
                {
                    sb.Append($"{tag.Name}, ");
                }

                string Tags = sb.ToString().TrimEnd();
                var gameView = new GameDetailsViewModel()
                {
                    Id = game.Id,
                    Name = game.Name,
                    Image = game.Image,
                    ReleaseDate = game.ReleaseDate,
                    Description = game.Description,
                    Rating = game.Rating,
                    Developer = game.Developer,
                    Publisher = game.Publisher,
                    Tags = Tags,
                    Price = game.Price,
                    ReviewsCount = game.Reviews.Count,
                };

                return gameView;
            

        }

        public async Task<IEnumerable<AllGamesViewModel>> GetRecentGames()
        {
            var recentGames=data.Games
                .Include(x=>x.Tags)
                .Include(x=>x.Reviews)
                .Select(x => new AllGamesViewModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Image = x.Image,
                    Rating = x.Rating,
                    Price = x.Price,
                    Developer = x.Developer,
                    Publisher = x.Publisher,
                    ReleaseDate = x.ReleaseDate,
                    Tag = x.Tags.Select(x => x.Name).FirstOrDefault(),
                    ReviewsCount = x.Reviews.Count()
                }).ToList()
                .TakeLast(3)
                .ToList();
            return recentGames;           
        }
    }    
}

