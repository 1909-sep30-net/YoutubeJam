//using Microsoft.Extensions.Logging;
//using Moq;
//using Xunit;
//using YoutubeJam.BusinessLogic;
//using YoutubeJam.WebApp.Controllers;

//namespace YoutubeJam.Test.YoutubeJam.Api
//{
//    public class SentimentControllerTest
//    {
//        [Fact]
//        public void GetShouldGetAverageSentiment()
//        {
//            //arrange
//            var mocklogger = new Mock<ILogger<VideoSentimentController>>();
//            var controller = new VideoSentimentController(mocklogger.Object);
//            //act
//            var result = controller.Get("iXwfBJYCTc4", 5);

//            //assert

//            Assert.IsAssignableFrom<AverageSentiment>(result);
//        }
//    }
//}