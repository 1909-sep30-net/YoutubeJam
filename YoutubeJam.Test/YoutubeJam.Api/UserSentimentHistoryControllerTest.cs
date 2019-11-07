using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using Xunit;
using YoutubeJam.Api.Controllers;
using YoutubeJam.BusinessLogic;

namespace YoutubeJam.Test.YoutubeJam.Api
{
    public class UserSentimentHistoryControllerTest
    {
        [Fact]
        public void PostShouldAddAnalysis()
        {
            //arrange
            var mockRepo = new Mock<IRepository>();
            var inputVideoHistory = new VideoHistory()
            {
                Email = "mtnolasco@up.edu.ph",
                VideoUrl = "ABC",
                AverageSentimentScore = 0.5

            };
            var controller = new UserSentimentHistoryController(mockRepo.Object);

            //act
            var result = controller.Post(inputVideoHistory);

            //assert
            Assert.IsAssignableFrom<CreatedAtActionResult>(result);
        }
        [Fact]

        public void GetShouldGetSomething()
        {
            //arrange
            var mockRepo = new Mock<IRepository>();
            mockRepo.Setup(x => x.GetUserSearchHistory(It.IsAny<string>())).Returns(new List<AverageSentiment>()
            {
                new AverageSentiment()
                {
                    VideoURL ="Abc",
                    AverageSentimentScore = 0.5
                }
            });
            var controller = new UserSentimentHistoryController(mockRepo.Object);

            //act
            var result = controller.Get("mtnolasco@up.edu.ph");

            //assert
            Assert.NotNull(result);
        }
    }
}