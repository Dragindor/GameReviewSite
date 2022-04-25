using GameReviewSite.Core.Models;
using GameReviewSite.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameReviewSite.Core.Contracts
{
    public interface IForumMessageService
    {
        Task<bool> AddForumMessage(ForumMessage model, string userId);

        Task<IEnumerable<ForumMessageViewModel>> GetReviews();
    }
}
