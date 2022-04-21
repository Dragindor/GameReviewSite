using GameReviewSite.Core.Models;
namespace GameReviewSite.Core.Contracts
{
    public interface IGameService
    {     
        Task<bool> CreateGame(AddGameViewModel model);       
        Task<bool> DeleteGame(string gameid);
        Task<bool> AddReviewToGame(string gameId);
        Task<IEnumerable<AllGamesViewModel>> GetGames();

        //Task<AnimeEditViewModel> GetAnimeForEdit(string id);
    }
}
