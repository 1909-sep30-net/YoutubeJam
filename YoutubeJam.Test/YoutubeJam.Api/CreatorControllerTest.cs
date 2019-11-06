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
        /// <summary>
        /// Tests get method in controller
        /// </summary>
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
                        Email = "mtnolasco@up.edu.ph"
                    }
                });
            var controller = new CreatorController(mockRepo.Object);

            //act
            var result = controller.Get();

            //assert
            var creators = Assert.IsAssignableFrom<List<Creator>>(result);
            Assert.Equal("mtnolasco@up.edu.ph", creators[0].Email);
        }

        /// <summary>
        /// tests post method in controller
        /// </summary>
        [Fact]
        public void PostShouldAddCreator()
        {
            //arrange
            var mockRepo = new Mock<IRepository>();
            mockRepo.Setup(x => x.LogIn(It.IsAny<string>())).Throws(new Persistence.CreatorDoesNotExistException());
            var inputCreator = new Creator()
            {
                FirstName = "Marielle",
                LastName = "Nolasco",
                Email = "mtnolasco@up.edu.ph"
            };
            var controller = new CreatorController(mockRepo.Object);

            //act
            var result = controller.Post(inputCreator);

            //assert
            Assert.IsAssignableFrom<CreatedAtActionResult>(result);
        }
    }
}