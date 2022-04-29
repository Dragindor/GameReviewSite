using GameReviewSite.Core.Contracts;
using GameReviewSite.Core.Models;
using GameReviewSite.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using SiteTests.Mock;
using SiteTests.TestConstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SiteTests.Services
{
    public class ForumPostServiceTests : IClassFixture<WebAppMock>
    {
        private IForumMessageService forumMessageService;
        private readonly DependencyScope scope;

        public ForumPostServiceTests(WebAppMock appMock)
        {
            scope = appMock.InitDb();
            forumMessageService = scope.ResolveService<IForumMessageService>();
        }

        [Fact]
        public async Task CreateForumPost_Should_Add_ForumPost_To_DB()
        {
            //Arrange
            ForumMessage forumMessage = TestDataConstants.ForumMessageCreate;
            await forumMessageService.AddForumMessage(forumMessage, WebAppMock.UserId);

            //Act
            var ForumCountAfterCreation = await scope.DB.ForumMessages.CountAsync();

            //Assert
            Assert.Equal(2, ForumCountAfterCreation);
        }

        [Fact]
        public async Task GetForumPosts_Returns_ForumPosts()
        {
            //Arrange
            ForumMessage forumMessage = TestDataConstants.ForumMessageCreate;
            await forumMessageService.AddForumMessage(forumMessage, WebAppMock.UserId);

            //Act
            var animes = await forumMessageService.GetMessages();

            //Assert
            Assert.IsType<List<ForumMessageViewModel>>(animes);
        }
    }
}
