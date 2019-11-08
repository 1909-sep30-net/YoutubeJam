using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using YoutubeJam.Api.Controllers;
using YoutubeJam.BusinessLogic;

namespace YoutubeJam.Test.YoutubeJam.Api
{
    public class UserSentimentHistoryControllerTest
    {
        [Fact]
        public async Task PostShouldAddAnalysisAsync()
        {
            //arrange
            var mockRepo = new Mock<IRepository>();
            var inputVideoHistory = new VideoHistory()
            {
                Email = "mtnolasco@up.edu.ph",
                VideoUrl = "ABC",
                AverageSentimentScore = 0.5,
                ChannelName = "Mathemars"

            };
            var controller = new UserSentimentHistoryController(mockRepo.Object);

            //act
            var result = await controller.PostAsync(inputVideoHistory);

            //assert
            Assert.IsAssignableFrom<CreatedAtActionResult>(result);
        }
        [Fact]
        public async Task PostShouldHandleRequestsAsync()
        {
            //arrange
            var mockRepo = new Mock<IRepository>();
            mockRepo.Setup(x => x.AddAnalysisAsync(It.IsAny<AverageSentiment>(), It.IsAny<Creator>())).Throws(new Persistence.CreatorDoesNotExistException());
            var inputVideoHistory = new VideoHistory()
            {
                Email = "mtnolasco@up.edu.ph",
                VideoUrl = "ABC",
                AverageSentimentScore = 0.5,
                ChannelName = "Mathemars"

            };
            var controller = new UserSentimentHistoryController(mockRepo.Object);

            //act
            var result = await controller.PostAsync(inputVideoHistory);

            //assert
            Assert.IsAssignableFrom<BadRequestResult>(result);
        }
        [Fact]

        public async Task GetShouldGetSomethingAsync()
        {
            //arrange
            var mockRepo = new Mock<IRepository>();
            mockRepo.Setup(x => x.GetUserSearchHistoryAsync(It.IsAny<string>())).Returns(Task.FromResult(new List<AverageSentiment>()
            {
                new AverageSentiment()
                {
                    VideoURL ="Abc",
                    AverageSentimentScore = 0.5
                }
            }));
            var controller = new UserSentimentHistoryController(mockRepo.Object);

            //act
            var result = await controller.GetAsync("mtnolasco@up.edu.ph");

            //assert
            Assert.NotNull(result);
        }
    }
}