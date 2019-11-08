using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        public async Task GetShouldGetCreatorsAsync()
        {
            //arrange
            var mockRepo = new Mock<IRepository>();
            mockRepo.Setup(x => x.GetCreatorsAsync())
                .Returns(Task.FromResult( new List<Creator>() {
                new Creator()
                {
                    FirstName = "Marielle",
                    LastName = "Nolasco",
                    Email = "mtnolasco@up.edu.ph"
                }
                }));
            var controller = new CreatorController(mockRepo.Object);

            //act
            var result = await controller.GetAsync();

            //assert
            var creators = Assert.IsAssignableFrom<List<Creator>>(result);
            Assert.Equal("mtnolasco@up.edu.ph", creators[0].Email);
        }
        [Fact]
        public async Task GetShouldReturnNewestCreatorAsync()
        {
            //arrange
            var mockRepo = new Mock<IRepository>();
            mockRepo.Setup(x => x.GetUserAsync(It.IsAny<string>()))
                .Returns(Task.FromResult(new Creator()
                {
                    FirstName = "Marielle",
                    LastName = "Nolasco",
                    Email = "mtnolasco@up.edu.ph"

                })
                    );
            var controller = new CreatorController(mockRepo.Object);

            //act
            var result = await controller.GetAsync("mtnolasco@up.edu.ph");

            //assert
            Assert.IsAssignableFrom<Creator>(result);
            
        }

        /// <summary>
        /// tests post method in controller
        /// </summary>
        [Fact]
        public async Task PostShouldAddCreatorIfLoginFailsAsync()
        {
            //arrange
            var mockRepo = new Mock<IRepository>();
            mockRepo.Setup(x => x.LogInAsync(It.IsAny<string>())).Throws(new Persistence.CreatorDoesNotExistException());
            var inputCreator = new Creator()
            {
                FirstName = "Marielle",
                LastName = "Nolasco",
                Email = "mtnolasco@up.edu.ph"
            };
            var controller = new CreatorController(mockRepo.Object);

            //act
            var result = await controller.PostAsync(inputCreator);

            //assert
            Assert.IsAssignableFrom<CreatedAtActionResult>(result);
        }

        [Fact]
        public async Task PostShouldUpdateChannelNameIfLoginPassesAsync()
        {
            //arrange
            var mockRepo = new Mock<IRepository>();
            var inputCreator = new Creator()
            {
                FirstName = "Marielle",
                LastName = "Nolasco",
                Email = "mtnolasco@up.edu.ph"
            };
            var controller = new CreatorController(mockRepo.Object);

            //act
            var result = await controller.PostAsync(inputCreator);

            //assert
            Assert.IsAssignableFrom<OkResult>(result);
        }
        [Fact]
        public async Task PostShouldHandleChannelNameTakenExceptionAsync()
        {
            //arrange
            var mockRepo = new Mock<IRepository>();
            mockRepo.Setup(x => x.UpdateChannelNameAsync(It.IsAny<string>(), It.IsAny<Creator>())).Throws(new Persistence.ChannelNameTakenException());
            var inputCreator = new Creator()
            {
                FirstName = "Marielle",
                LastName = "Nolasco",
                Email = "mtnolasco@up.edu.ph"
            };
            var controller = new CreatorController(mockRepo.Object);

            //act
            var result = await controller.PostAsync(inputCreator);

            //assert
            Assert.IsAssignableFrom<BadRequestResult>(result);

        }
        [Fact]
        public async Task PostShouldHandleChannelNameTakenExceptionUponCreationAsync()
        {
            //arrange
            var mockRepo = new Mock<IRepository>();
            mockRepo.Setup(x => x.LogInAsync(It.IsAny<string>())).Throws(new Persistence.CreatorDoesNotExistException());
            mockRepo.Setup(x => x.AddCreatorandChannelAsync(It.IsAny<Creator>(), It.IsAny<string>())).Throws(new Persistence.ChannelNameTakenException());
            var inputCreator = new Creator()
            {
                FirstName = "Marielle",
                LastName = "Nolasco",
                Email = "mtnolasco@up.edu.ph"
            };
            var controller = new CreatorController(mockRepo.Object);

            //act
            var result = await controller.PostAsync(inputCreator);

            //assert
            Assert.IsAssignableFrom<BadRequestResult>(result);

        }
    }
}