using GameReviewSite.Core.Models;
using GameReviewSite.Infrastructure.Data;

namespace GameReviewSite.Core.Contracts
{
    public interface IGameService
    {     
        Task<bool> CreateGame(AddGameViewModel model);       
        Task<bool> DeleteGame(string gameid);
        Task<bool> AddReviewToGame(string gameId);
        Task<bool> HasTag(AddGameViewModel game, string TagName);
        Task<IEnumerable<AllGamesViewModel>> GetGames();
        Task<bool> UpdateGame(AddGameViewModel model);        
        Task<AddGameViewModel> GetGameForEdit(string id);
    }
}
