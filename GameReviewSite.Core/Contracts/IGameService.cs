using GameReviewSite.Core.Models;
using GameReviewSite.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GameReviewSite.Core.Models.AddGameViewModel;

namespace GameReviewSite.Core.Contracts
{
    public interface IGameService
    {     
        Task<bool> CreateGame(AddGameViewModel model);
        Task<bool> CreateTag(string name);
        Task<bool> DeleteGame(string gameid);
        Task<bool> AddReviewToGame(string gameId);

        Task<IEnumerable<AllGamesViewModel>> GetGames();

        //Task<AnimeEditViewModel> GetAnimeForEdit(string id);
    }
}
