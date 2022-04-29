using GameReviewSite.Core.Models;
using GameReviewSite.Infrastructure.Data;
using GameReviewSite.Infrastructure.Data.Identity;
using SiteTests.Mock;
using System;
using System.Collections.Generic;

namespace SiteTests.TestConstants
{
    public class TestDataConstants
    {
        public static readonly string GameId = Guid.NewGuid().ToString();
        public static readonly string ReviewId = Guid.NewGuid().ToString();
        public static readonly string UserId = Guid.NewGuid().ToString();
        public static readonly string ForumMessageId = Guid.NewGuid().ToString();

        public static AddGameViewModel GameCreate => new AddGameViewModel()
        {
            Id = GameId,
            Name = "Elden Ring",
            Image = "https://www.gamespot.com/a/uploads/scale_landscape/43/434805/3944896-untitled-1.jpg",
            ReleaseDate = "22.10.2022",
            Price = 59.99,
            Description = "In the 87 hours that it took me to beat Elden Ring, I was put through an absolute wringer of emotion: Anger as I was beaten down by its toughest challenges.",
            Developer = "FromSoftware Inc.",
            Publisher = "FromSoftware Inc., BANDAI NAMCO Entertainment",
        };

        public static ForumMessage ForumMessageCreate => new ForumMessage()
        {
            Id = ForumMessageId,
            Description = "In the 24 hours there are only 24 hours.",
            Date=DateTime.Now.ToString()
        };

        public static Review Review => new Review()
        {
            Id = ReviewId,
            Description = "Random Review",
            GameId = GameId,
            UserId=WebAppMock.UserId,
            Date=DateTime.Now.ToString(),
            Rating=10,
            Comments=new List<Comment>()
        };


    }
}
