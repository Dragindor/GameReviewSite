using GameReviewSite.Core.Contracts;
using GameReviewSite.Core.Models;
using GameReviewSite.Infrastructure.Data.Identity;
using GameReviewSite.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GameReviewSite.Core.Servicec
{
    public class UserService : IUserService
    {
        private readonly IApplicationDbRepository repo;

        public UserService(IApplicationDbRepository _repo)
        {
            repo = _repo;
        }

        public async Task<ApplicationUser> GetUserById(string id)
        {
            return await repo.GetByIdAsync<ApplicationUser>(id);
        }

        public async Task<UserEditViewModel> GetUserForEdit(string id)
        {
            var user = await repo.GetByIdAsync<ApplicationUser>(id);

            return new UserEditViewModel()
            {
                UserId = user.Id,
                UserName = user.UserName
            };
        }

        public async Task<IEnumerable<UserListViewModel>> GetUsers()
        {
            return await repo.All<ApplicationUser>()
                .Select(user => new UserListViewModel()
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email
                })
                .ToListAsync();
        }

        public async Task<bool> UpdateUser(UserEditViewModel model)
        {
            bool result = false;
            var user = await repo.GetByIdAsync<ApplicationUser>(model.UserId);

            if (user != null)
            {
                user.UserName = model.UserName;

                await repo.SaveChangesAsync();
                result = true;
            }

            return result;
        }
    }
}
