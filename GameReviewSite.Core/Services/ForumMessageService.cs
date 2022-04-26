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
                ForumMessage forum = new ForumMessage()
                {
                    UserId = userId,
                    Date = DateTime.Now.ToString(),
                    Description = model.Description,
                };

                await data.ForumMessages.AddAsync(forum);
                await data.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task<IEnumerable<ForumMessageViewModel>> GetMessages()
        {
            var reviews = await data.ForumMessages
                .Include(x => x.User)
                .Select(x => new ForumMessageViewModel
                {
                    Id = x.Id,
                    Date = x.Date,
                    UserName = x.User.UserName,
                    Description = x.Description
                })
                .ToListAsync();



            return reviews;
        }
    }
}
