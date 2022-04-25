using GameReviewSite.Core.Contracts;
using GameReviewSite.Core.Models;
using GameReviewSite.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GameReviewSite.Core.Services
{
    public class ForumMessageService : IForumMessageService
    {
        private readonly ApplicationDbContext data;
        public ForumMessageService(ApplicationDbContext _data)
        {
            data = _data;
        }

        public async Task<bool> AddForumMessage(ForumMessage model, string userId)
        {
            try
            {
                await data.ForumMessages.AddAsync(model);
                await data.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<ForumMessageViewModel>> GetReviews()
        {
            var reviews = await data.Reviews
                .Include(x => x.User)
                .Select(x=>new ForumMessageViewModel
                {
                    Id=x.Id,
                    Date=x.Date,
                    UserName=x.User.UserName,
                    Description=x.Description
                })
                .ToListAsync();



            return reviews;
        }
    }
}
