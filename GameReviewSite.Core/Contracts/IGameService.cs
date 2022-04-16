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
        //Task<IEnumerable<UserListViewModel>> GetUsers();
        //
        //Task<UserEditViewModel> GetUserForEdit(string id);
        //
        //Task<bool> UpdateUser(UserEditViewModel model);
        //
        //Task<ApplicationUser> GetUserById(string id);
        //Task<Game> AddGame(CreateGameViewModel model);
        Task<List<AllGamesViewModel>> GetAllGames();
        Task<List<GameReviewsViewModel>> GetReviews(string userId);
        Task<bool> CreateGame(AddGameViewModel model, string userId);
        void DeleteGame(string gameid);
        string AddReviewToGame(string userId, string cardId);
    }
}
