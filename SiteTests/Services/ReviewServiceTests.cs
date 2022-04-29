using GameReviewSite.Core.Contracts;
using GameReviewSite.Core.Models;
using GameReviewSite.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using SiteTests.Mock;
using SiteTests.TestConstants;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace SiteTests.Services
{
    public class ReviewServiceTests : IClassFixture<WebAppMock>
    {
        private IReviewService reviewService;
        private IGameService gameService;
        private readonly DependencyScope scope;

        public ReviewServiceTests(WebAppMock appMock)
        {
            scope = appMock.InitDb();
            reviewService = scope.ResolveService<IReviewService>();
            gameService = scope.ResolveService<IGameService>();
            
        }


        [Fact]
        public async Task CreateReview_Should_Add_Review_To_DB_And_Game()
        {
            //Arrange
            AddGameViewModel gameView = TestDataConstants.GameCreate;
            await gameService.CreateGame(gameView);

            var createdGame= await scope.DB.Games.FirstOrDefaultAsync();

            Review review = TestDataConstants.Review;
            review.GameId=createdGame.Id;
            await reviewService.AddReviewToGame(review);


            //Act
            var reviewCountAfterCreation = await scope.DB.Reviews.CountAsync();
            var updatedGame = await gameService.GetGameById(createdGame.Id);

            //Assert
            Assert.Equal(2, reviewCountAfterCreation);
            Assert.Equal(2, updatedGame.ReviewsCount);
            Assert.Equal(10, updatedGame.Rating);
        }


        [Fact]
        public async Task CreateReview_Should_Return_False_With_No_Game()
        {
            //Arrange
            Review review = TestDataConstants.Review;

            //Act
            var shouldBeFalse=await reviewService.AddReviewToGame(review);

            //Assert
            Assert.False(shouldBeFalse);
        }

        [Fact]
        public async Task GetReviewForById_Returns_Review()
        {
            //Arrange
            AddGameViewModel gameView = TestDataConstants.GameCreate;
            await gameService.CreateGame(gameView);
        
            var createdGame = await scope.DB.Games.FirstOrDefaultAsync();
        
            Review review = TestDataConstants.Review;
            review.GameId = createdGame.Id;
            await reviewService.AddReviewToGame(review);
        
        
            //Act
            Review reviewCreated = await scope.DB.Reviews.FirstOrDefaultAsync();
        
            var reviewExits =  reviewService.GetReviewById(reviewCreated.Id).Result.Id;
        
            //Assert
            Assert.Equal(reviewExits, reviewCreated.Id);
        }

        [Fact]
        public async Task GetReviews_Should_Return_List_Of_Reviews()
        {
            //Arrange
            AddGameViewModel gameView = TestDataConstants.GameCreate;
            await gameService.CreateGame(gameView);

            var createdGame = await scope.DB.Games.FirstOrDefaultAsync();

            Review review = TestDataConstants.Review;
            review.GameId = createdGame.Id;
            await reviewService.AddReviewToGame(review);
            await reviewService.AddReviewToGame(review);

            //Act
            var reviews = await reviewService.GetReviews();

            //Assert
            Assert.IsType<List<Review>>(reviews);
        }

        [Fact]
        public async Task GetReviewsByGame_Should_Return_Reviews()
        {
            //Arrange
            AddGameViewModel gameView = TestDataConstants.GameCreate;
            await gameService.CreateGame(gameView);

            var createdGame = await scope.DB.Games.FirstOrDefaultAsync();

            Review review = TestDataConstants.Review;
            review.GameId = createdGame.Id;
            await reviewService.AddReviewToGame(review);
            await reviewService.AddReviewToGame(review);

            //Act
            var reviews = await reviewService.GetReviewsByGame(createdGame.Id);

            //Assert
            Assert.IsType<List<AllGameReviewsViewModel>>(reviews);
            Assert.Equal(3,reviews.Count);
        }
    }
}
