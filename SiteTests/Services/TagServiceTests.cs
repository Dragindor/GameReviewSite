using GameReviewSite.Core.Contracts;
using GameReviewSite.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using SiteTests.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SiteTests.Services
{
    public class TagServiceTests : IClassFixture<WebAppMock>
    {
        private IGameService gameService;
        private ITagService tagService;
        private readonly DependencyScope scope;

        public TagServiceTests(WebAppMock appMock)
        {
            scope = appMock.InitDb();
            gameService = scope.ResolveService<IGameService>();
            tagService = scope.ResolveService<ITagService>();
        }

        [Fact]
        public async Task CreateTag_Should_Add_To_DB()
        {
            //Arrange
            await tagService.CreateTag("Test");

            //Act
            var tagCountAfterCreation = await scope.DB.Tags.CountAsync();
            var tag=await scope.DB.Tags.SingleOrDefaultAsync();

            //Assert
            Assert.Equal(1, tagCountAfterCreation);
            Assert.Equal("Test", tag.Name);
        }

        [Fact]
        public async Task TagAlreadyExist_Returns_True_If_Tag_Exits()
        {
            //Arrange
            await tagService.CreateTag("Test");

            //Act

            //Assert
            Assert.True(await tagService.TagAlreadyExist("Test"));
        }

        [Fact]
        public async Task TagAlreadyExist_Returns_False_If_Game_Doesnt_Exits()
        {
            //Arrange

            //Act

            //Assert
            Assert.False(await tagService.TagAlreadyExist("Test"));
        }

        [Fact]
        public async Task GetTagForEdit_Returns_ArgumentNullExeption()
        {
            //Arrange

            //Act

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => tagService.GetTagForEdit("asd"));
        }

        [Fact]
        public async Task GetTagForEdit_Returns_Game()
        {
            //Arrange
            await tagService.CreateTag("Test");

            var tag = await scope.DB.Tags.FirstOrDefaultAsync();

            //Act
            var tagExits = await tagService.GetTagForEdit(tag.Id);

            //Assert
            Assert.Equal(tag.Id, tagExits.Id);
        }

        [Fact]
        public async Task UpdateGame_Returns_False()
        {
            //Arrange
            Tag tag = new Tag();
            //Act

            var shouldBeFalse = await tagService.UpdateTag(tag);

            //Assert
            Assert.False(shouldBeFalse);
        }

        [Fact]
        public async Task UpdateGame_Returns_UpdatesGame()
        {
            //Arrange
            await tagService.CreateTag("Test");
            var tagCreated = await scope.DB.Tags.FirstOrDefaultAsync();

            Tag tag = new Tag()
            {
                Name = "new Tag"
            };

            tag.Id = tagCreated.Id;

            //Act
            var shouldBeTrue = await tagService.UpdateTag(tag);
            var updatedTag = await tagService.GetTagForEdit(tag.Id);

            //Assert
            Assert.True(shouldBeTrue);
            Assert.Equal(tag.Name, updatedTag.Name);
        }

        [Fact]
        public async Task GetTag_Returns_List_Of_Tags()
        {
            //Arrange
            await tagService.CreateTag("Test");
            await tagService.CreateTag("Test 2");

            //Act
            var tags = await tagService.GetTags();

            //Assert
            Assert.IsType<List<Tag>>(tags);
            Assert.Equal(2,tags.Count());

        }
    }
}
