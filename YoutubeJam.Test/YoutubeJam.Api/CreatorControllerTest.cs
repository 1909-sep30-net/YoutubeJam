using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using Xunit;
using YoutubeJam.Api.Controllers;
using YoutubeJam.BusinessLogic;

namespace YoutubeJam.Test.YoutubeJam.Api
{
    public class CreatorControllerTest
    {
        [Fact]
        public void GetShouldGetCreators()
        {
            //arrange
            var mockRepo = new Mock<IRepository>();
            mockRepo.Setup(x => x.GetCreators())
                .Returns(new List<Creator>() {
                    new Creator(){
                        FirstName = "Marielle",
                        LastName = "Nolasco",
                        Password = "Password",
                        PhoneNumber = "5102898893",
                        Username = "mtnolasco"
                    }
                });
            var controller = new CreatorController(mockRepo.Object);

            //act
            var result = controller.Get();

            //assert
            var creators = Assert.IsAssignableFrom<List<Creator>>(result);
            Assert.Equal("5102898893", creators[0].PhoneNumber);
        }

        [Fact]
        public void PostShouldAddCreator()
        {
            //arrange
            var mockRepo = new Mock<IRepository>();
            var inputCreator = new Creator()
            {
                FirstName = "Marielle",
                LastName = "Nolasco",
                Password = "Password",
                PhoneNumber = "5102898893",
                Username = "mtnolasco"
            };
            var controller = new CreatorController(mockRepo.Object);

            //act
            var result = controller.Post(inputCreator);

            //assert
            Assert.IsAssignableFrom<OkResult>(result);
        }
    }
}