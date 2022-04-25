using GameReviewSite.Core.Contracts;

namespace GameReviewSite.Controllers
{
    public class ForumMessageController
    {
        private readonly IUserService userService;
        private readonly IForumMessageService forumMessageService;

        public ForumMessageController(IUserService userService, IForumMessageService forumMessageService)
        {
            this.userService = userService;
            this.forumMessageService = forumMessageService;
        }


    }
}
