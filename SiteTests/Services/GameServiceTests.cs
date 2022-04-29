using GameReviewSite.Core.Contracts;
using GameReviewSite.Core.Models;
using Microsoft.EntityFrameworkCore;
using SiteTests.Mock;
using SiteTests.TestConstants;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace SiteTests.Services
{
    public class GameServiceTests : IClassFixture<WebAppMock>
    {
        private IGameService gameService;
        private readonly DependencyScope scope;

        public GameServiceTests(WebAppMock appMock)
        {
            scope = appMock.InitDb();
            gameService = scope.ResolveService<IGameService>();
        }

        [Fact]
        public async Task CreateGame_Should_Add_Game_To_DB()
        {
            //Arrange
            AddGameViewModel gameView = TestDataConstants.GameCreate;
            await gameService.CreateGame(gameView);

            //Act
            var gameCountAfterCreation = await scope.DB.Games.CountAsync();

            //Assert
            Assert.Equal(1, gameCountAfterCreation);
        }

        [Fact]
        public async Task GameAlreadyExist_Returns_True_If_Game_Exits()
        {
            //Arrange
            AddGameViewModel gameView = TestDataConstants.GameCreate;
            await gameService.CreateGame(gameView);

            //Act
            var gameExits = await gameService.GameAlreadyExist(gameView.Name);

            //Assert
            Assert.True(gameExits);
        }

        [Fact]
        public async Task GameAlreadyExist_Returns_False_If_Game_Doesnt_Exits()
        {
            //Arrange

            //Act
            var gameExits = await gameService.GameAlreadyExist("Random");

            //Assert
            Assert.False(gameExits);
        }

        [Fact]
        public async Task GetGameForEdit_Returns_ArgumentNullExeption()
        {
            //Arrange

            //Act

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(()=> gameService.GetGameForEdit("asd"));
        }

        [Fact]
        public async Task GetGameForEdit_Returns_Game()
        {
            //Arrange
            AddGameViewModel gameView = TestDataConstants.GameCreate;
            await gameService.CreateGame(gameView);
            
            var game= await scope.DB.Games.FirstOrDefaultAsync();           

            //Act
            var gameExits = await gameService.GetGameForEdit(game.Id);

            //Assert
            Assert.Equal(game.Id, gameExits.Id);
        }

        [Fact]
        public async Task UpdateGame_Returns_False()
        {
            //Arrange
            AddGameViewModel emptyModel = new AddGameViewModel();
            //Act

            var shouldBeFalse = await gameService.UpdateGame(emptyModel);

            //Assert
            Assert.False(shouldBeFalse);
        }

        [Fact]
        public async Task UpdateGame_Returns_UpdatesGame()
        {
            //Arrange
            AddGameViewModel gameView = TestDataConstants.GameCreate;
            await gameService.CreateGame(gameView);

            var game = await scope.DB.Games.FirstOrDefaultAsync();
            AddGameViewModel changedGame=new AddGameViewModel()
            {
                Id=game.Id,
                Name="Elden Ring2",
                Image = "https://www.gamespot.com/a/uploads/scale_landscape/43/434805/3944896-untitled-1.jpg",
                ReleaseDate = "22.10.2025",
                Price = 20,
                Description = "Everything IS WRONG",
                Developer = "whatever",
                Publisher = "whatever",
            };
            //Act
            var shouldBeTrue = await gameService.UpdateGame(changedGame);
            var updatedGame = await gameService.GetGameForEdit(game.Id);

            //Assert
            Assert.True(shouldBeTrue);
            Assert.Equal(changedGame.Name,          updatedGame.Name);
            Assert.Equal(changedGame.ReleaseDate,   updatedGame.ReleaseDate);
            Assert.Equal(changedGame.Price,         updatedGame.Price);
            Assert.Equal(changedGame.Description,   updatedGame.Description);
            Assert.Equal(changedGame.Developer,     updatedGame.Developer);
            Assert.Equal(changedGame.Publisher,     updatedGame.Publisher);
        }

        [Fact]
        public async Task GetGameById_Returns_ArgumentNullExeption()
        {
            //Arrange

            //Act

            //Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => gameService.GetGameById("asd"));
        }

        [Fact]
        public async Task GetGameForById_Returns_Game()
        {
            //Arrange
            AddGameViewModel gameView = TestDataConstants.GameCreate;
            await gameService.CreateGame(gameView);

            var game = await scope.DB.Games.FirstOrDefaultAsync();

            //Act
            var gameExits = await gameService.GetGameById(game.Id);

            //Assert
            Assert.Equal(game.Id, gameExits.Id);
        }

        [Fact]
        public async Task GetGames_Returns_List_Of_Games()
        {
            //Arrange
            AddGameViewModel gameView = TestDataConstants.GameCreate;
            await gameService.CreateGame(gameView);

            //Act
            var games = await gameService.GetGames();

            //Assert
            Assert.IsType<List<AllGamesViewModel>>(games);

        }
        [Fact]
        public async Task GetGamesToManage_Returns_List_Of_Games()
        {
            //Arrange
            AddGameViewModel gameView = TestDataConstants.GameCreate;
            await gameService.CreateGame(gameView);

            //Act
            var games = await gameService.GetGamesToManage();

            //Assert
            Assert.IsType<List<ManageGamesViewModel>>(games);

        }
    }
}
