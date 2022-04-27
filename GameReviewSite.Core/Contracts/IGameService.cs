using GameReviewSite.Core.Models;
using GameReviewSite.Infrastructure.Data;

namespace GameReviewSite.Core.Contracts
{
    public interface IGameService
    {     
        Task<bool> CreateGame(AddGameViewModel model);       
        Task<bool> DeleteGame(string gameid);
        Task<bool> AddReviewToGame(string gameId);
        Task<bool> HasTag(Game game, string TagName);
        Task<IEnumerable<AllGamesViewModel>> GetGames();
        Task<IEnumerable<AllGamesViewModel>> GetRecentGames();
        Task<IEnumerable<ManageGamesViewModel>> GetGamesToManage();
        Task<bool> UpdateGame(AddGameViewModel model);        
        Task<Game> GetGameForEdit(string id);
        Task<GameDetailsViewModel> GetGameById(string id);
    }
}
