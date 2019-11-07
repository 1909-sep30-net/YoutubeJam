using Microsoft.EntityFrameworkCore;
using System.Linq;
using Xunit;
using YoutubeJam.BusinessLogic;
using YoutubeJam.Persistence;
using YoutubeJam.Persistence.Entities;

namespace YoutubeJam.Test
{
    /// <summary>
    /// Test class to test the repo functions and all other functions that deal with the database
    /// </summary>
    public class RepositoryTest
    {
        /// <summary>
        /// Testing if creators are added to database
        /// </summary>
        [Fact]
        public void AddCreatorsShouldAddCreators()
        {
            //arrange
            var options = new DbContextOptionsBuilder<YouTubeJamContext>()
                .UseInMemoryDatabase("AddAnalysisShouldAdd")
                .Options;

            using var context = new YouTubeJamContext(options);
            var mapper = new DBMapper(context);
            var repo = new Repository(context, mapper);

            BusinessLogic.Creator c = new BusinessLogic.Creator()
            {
                FirstName = "Marielle",
                LastName = "Nolasco",
                Email = "mtnolasco@up.edu.ph"
            };

            //act
            repo.AddCreator(c);

            //assert
            using var assertContext = new YouTubeJamContext(options);
            var rest = assertContext.Creator.Select(c => c);
            Assert.NotNull(rest);
        }

        [Fact]
        public void AddChannelShouldAddChannel()
        {
            //arrange
            var options = new DbContextOptionsBuilder<YouTubeJamContext>()
                .UseInMemoryDatabase("AddChannelShouldAdd")
                .Options;

            using var context = new YouTubeJamContext(options);
            var mapper = new DBMapper(context);
            var repo = new Repository(context, mapper);

            BusinessLogic.Creator c = new BusinessLogic.Creator()
            {
                FirstName = "Marielle",
                LastName = "Nolasco",
                Email = "mtnolasco@up.edu.ph"
            };
            string channelName = "MatheMartian";
            //act
            repo.AddCreator(c);
            repo.AddChannel(c, channelName);

            //assert
            using var assertContext = new YouTubeJamContext(options);
            var result = assertContext.Channel.FirstOrDefault(c => c.ChannelName == channelName);
            Assert.NotNull(result);
        }

        /// <summary>
        /// Tests the adding a video to the database
        /// </summary>
        [Fact]
        public void AddVideoShouldAddVideo()
        {
            //arrange
            var options = new DbContextOptionsBuilder<YouTubeJamContext>()
                .UseInMemoryDatabase("AddVideoShouldAdd")
                .Options;

            using var context = new YouTubeJamContext(options);
            var mapper = new DBMapper(context);
            var repo = new Repository(context, mapper);

            BusinessLogic.Creator c = new BusinessLogic.Creator()
            {
                FirstName = "Marielle",
                LastName = "Nolasco",
                Email = "mtnolasco@up.edu.ph"
            };
            var url = "abc";
            string channelName = "MatheMartian";

            //act
            repo.AddCreator(c);
            repo.AddChannel(c, channelName);
            repo.AddVideo(url, channelName);

            //assert
            using var assertContext = new YouTubeJamContext(options);
            var rest = assertContext.Video.First(v => v.URL == url);

            Assert.NotNull(rest);
        }

        /// <summary>
        /// Tests if analysis can be stored in db
        /// </summary>
        [Fact]
        public void AddAnalysisShouldAdd()
        {
            //arrange
            var options = new DbContextOptionsBuilder<YouTubeJamContext>()
                .UseInMemoryDatabase("AddAnalysisShouldAdd")
                .Options;

            using var context = new YouTubeJamContext(options);
            var mapper = new DBMapper(context);
            var repo = new Repository(context, mapper);

            BusinessLogic.Creator c = new BusinessLogic.Creator()
            {
                FirstName = "Marielle",
                LastName = "Nolasco",
                Email = "mtnolasco@up.edu.ph"
            };

            AverageSentiment avg = new AverageSentiment()
            {
                VideoURL = "Abc",
                AverageSentimentScore = 0.5
            };

            //act
            repo.AddCreator(c);
            repo.AddChannel(c, "MatheMartian");
            repo.AddVideo("Abc", "MatheMartian");
            repo.AddAnalysis(avg, c);

            //assert
            using var assertContext = new YouTubeJamContext(options);
            var rest = assertContext.Analysis1.First();
            Assert.NotNull(rest);
        }

        /// <summary>
        /// Tests if analysis is stored properly (i.e. now new instances of creators are created)
        /// </summary>
        [Fact]
        public void AddAnalysisShouldNotCreateNewCreators()
        {
            //arrange
            var options = new DbContextOptionsBuilder<YouTubeJamContext>()
                .UseInMemoryDatabase("AddAnalysisShouldNotCreateNewCreators")
                .Options;

            using var context = new YouTubeJamContext(options);
            var mapper = new DBMapper(context);
            var repo = new Repository(context, mapper);

            BusinessLogic.Creator c = new BusinessLogic.Creator()
            {
                FirstName = "Marielle",
                LastName = "Nolasco",
                Email = "mtnolasco@up.edu.ph"
            };

            AverageSentiment avg = new AverageSentiment()
            {
                VideoURL = "Abc",
                AverageSentimentScore = 0.5
            };

            //act
            repo.AddCreator(c);
            repo.AddChannel(c, "MatheMartian");
            repo.AddVideo("Abc", "MatheMartian");
            repo.AddAnalysis(avg, c);

            //assert
            using var assertContext = new YouTubeJamContext(options);
            mapper = new DBMapper(assertContext);
            repo = new Repository(assertContext, mapper);

            if (repo.GetCreators().Count > 1) Assert.True(false);
            else
            {
                Assert.True(true);
            }
        }

        /// <summary>
        /// Tests if adding analysis works properly (i.e. new videos aren't created)
        /// </summary>
        [Fact]
        public void AddAnalysisShouldNotCreateNewVideos()
        {
            //arrange
            var options = new DbContextOptionsBuilder<YouTubeJamContext>()
                .UseInMemoryDatabase("AddAnalysisShouldNotCreateNewVideos")
                .Options;

            using var context = new YouTubeJamContext(options);
            var mapper = new DBMapper(context);
            var repo = new Repository(context, mapper);

            BusinessLogic.Creator c = new BusinessLogic.Creator()
            {
                FirstName = "Marielle",
                LastName = "Nolasco",
                Email = "mtnolasco@up.edu.ph"
            };

            AverageSentiment avg = new AverageSentiment()
            {
                VideoURL = "Abc",
                AverageSentimentScore = 0.5
            };

            //act
            repo.AddCreator(c);
            repo.AddChannel(c, "MatheMartian");
            repo.AddVideo("Abc","MatheMartian");
            repo.AddAnalysis(avg, c);

            //assert
            using var assertContext = new YouTubeJamContext(options);
            var result = assertContext.Video.Select(v => v).ToList();
            if (result.Count() > 1) Assert.True(false);
            else
            {
                Assert.True(true);
            }
        }

        [Fact]
        public void AddAnalysisShouldAddVid()
        {
            //arrange
            var options = new DbContextOptionsBuilder<YouTubeJamContext>()
                .UseInMemoryDatabase("AddAnalysisShouldAdd")
                .Options;

            using var context = new YouTubeJamContext(options);
            var mapper = new DBMapper(context);
            var repo = new Repository(context, mapper);

            BusinessLogic.Creator c = new BusinessLogic.Creator()
            {
                FirstName = "Marielle",
                LastName = "Nolasco",
                Email = "mtnolasco@up.edu.ph"
            };

            AverageSentiment avg = new AverageSentiment()
            {
                VideoURL = "Abc",
                AverageSentimentScore = 0.5
            };

            //act
            repo.AddCreator(c);
            repo.AddChannel(c, "MatheMartian");
            repo.AddAnalysis(avg, c);

            //assert
            using var assertContext = new YouTubeJamContext(options);
            var rest = assertContext.Analysis1.First();
            Assert.NotNull(rest);
        }
        /// <summary>
        /// Testing if the Analysis History can be retrieved
        /// </summary>
        [Fact]
        public void GetAnalHistoryShouldGetSomething()
        {
            //arrange
            var options = new DbContextOptionsBuilder<YouTubeJamContext>()
                .UseInMemoryDatabase("GetAnalHistoryShouldReturnSomething")
                .Options;

            using var context = new YouTubeJamContext(options);
            var mapper = new DBMapper(context);
            var repo = new Repository(context, mapper);

            BusinessLogic.Creator c = new BusinessLogic.Creator()
            {
                FirstName = "Marielle",
                LastName = "Nolasco",
                Email = "mtnolasco@up.edu.ph"
            };

            AverageSentiment avg = new AverageSentiment()
            {
                VideoURL = "Abc",
                AverageSentimentScore = 0.5
            };

            //act
            repo.AddCreator(c);
            repo.AddChannel(c, "MatheMartian");
            repo.AddVideo("Abc", "MatheMartian");
            repo.AddAnalysis(avg, c);

            //assert
            using var assertContext = new YouTubeJamContext(options);
            mapper = new DBMapper(assertContext);
            repo = new Repository(assertContext, mapper);
            var result = repo.GetAnalysisHistory("Abc", c).ToList();
            Assert.True(result.Count() > 0);
        }

        /// <summary>
        /// Testing the logging in functionality
        /// </summary>
        [Fact]
        public void LogInShouldLogIn()
        {
            //assert
            var options = new DbContextOptionsBuilder<YouTubeJamContext>()
                .UseInMemoryDatabase("LogInShouldLogIn")
                .Options;

            using var context = new YouTubeJamContext(options);
            var mapper = new DBMapper(context);
            var repo = new Repository(context, mapper);

            string email = "mtnolasco@up.edu.ph";
            BusinessLogic.Creator c = new BusinessLogic.Creator()
            {
                FirstName = "Marielle",
                LastName = "Nolasco",
                Email = email
            };

            //act

            repo.AddCreator(c);
            repo.AddChannel(c, "Mars");
            //assert
            using var assertContext = new YouTubeJamContext(options);
            mapper = new DBMapper(assertContext);
            repo = new Repository(assertContext, mapper);
            var result = repo.LogIn(email);
            Assert.NotNull(result);
        }
        /// <summary>
        /// Tests if Get channel name functionality works
        /// </summary>
        [Fact]
        public void GetChannelNameShouldGetSomething()
        {
            //arrange
            var options = new DbContextOptionsBuilder<YouTubeJamContext>()
                .UseInMemoryDatabase("GetChannelNameShouldGetSomething")
                .Options;

            using var context = new YouTubeJamContext(options);
            var mapper = new DBMapper(context);
            var repo = new Repository(context, mapper);

            BusinessLogic.Creator c = new BusinessLogic.Creator()
            {
                FirstName = "Marielle",
                LastName = "Nolasco",
                Email = "mtnolasco@up.edu.ph"
            };

            

            //act
            repo.AddCreator(c);
            repo.AddChannel(c, "MatheMartian");

            //assert
            using var assertContext = new YouTubeJamContext(options);
            mapper = new DBMapper(assertContext);
            repo = new Repository(assertContext, mapper);
            var result = repo.GetUser("mtnolasco@up.edu.ph");
            Assert.True(result.ChannelName == "MatheMartian");

        }

        [Fact]
        public void GetUserHistoryShouldGetSomething()
        {
            //arrange
            var options = new DbContextOptionsBuilder<YouTubeJamContext>()
                .UseInMemoryDatabase("GetUserHistoryShouldGetSomething")
                .Options;

            using var context = new YouTubeJamContext(options);
            var mapper = new DBMapper(context);
            var repo = new Repository(context, mapper);

            BusinessLogic.Creator c = new BusinessLogic.Creator()
            {
                FirstName = "Marielle",
                LastName = "Nolasco",
                Email = "mtnolasco@up.edu.ph"
            };

            AverageSentiment avg = new AverageSentiment()
            {
                VideoURL = "Abc",
                AverageSentimentScore = 0.5
            };

            //act
            repo.AddCreator(c);
            repo.AddChannel(c, "MatheMartian");
            repo.AddVideo("Abc", "MatheMartian");
            repo.AddAnalysis(avg, c);

            //assert
            using var assertContext = new YouTubeJamContext(options);
            mapper = new DBMapper(assertContext);
            repo = new Repository(assertContext, mapper);
            var result = repo.GetUserSearchHistory("mtnolasco@up.edu.ph");
            Assert.NotNull(result[0].VideoURL);
        }
        /// <summary>
        /// Method that tests the update channe name functionality of the repo
        /// </summary>
        [Fact]
        public void UpdateChannelNameShouldUpdate()
        {
            //arrange
            var options = new DbContextOptionsBuilder<YouTubeJamContext>()
                .UseInMemoryDatabase("UpdateChannelNameShouldUpdate")
                .Options;

            using var context = new YouTubeJamContext(options);
            var mapper = new DBMapper(context);
            var repo = new Repository(context, mapper);

            BusinessLogic.Creator c = new BusinessLogic.Creator()
            {
                FirstName = "Marielle",
                LastName = "Nolasco",
                Email = "mtnolasco@up.edu.ph"
            };

            //act
            repo.AddCreator(c);
            repo.AddChannel(c, "MatheMartian");
            repo.UpdateChannelName("MathMars", c);

            //assert
            using var assertContext = new YouTubeJamContext(options);
            mapper = new DBMapper(assertContext);
            repo = new Repository(assertContext, mapper);
            var result = repo.GetUser("mtnolasco@up.edu.ph");
            Assert.True(result.ChannelName == "MathMars");
        }

       
        [Fact]
        public void ChannelNameShouldBeUniqueTest2()
        {
            //arrange
            var options = new DbContextOptionsBuilder<YouTubeJamContext>()
                .UseInMemoryDatabase("ChannelNameShouldBeUniqueTest2")
                .Options;

            using var context = new YouTubeJamContext(options);
            var mapper = new DBMapper(context);
            var repo = new Repository(context, mapper);

            BusinessLogic.Creator c = new BusinessLogic.Creator()
            {
                FirstName = "Marielle",
                LastName = "Nolasco",
                Email = "mtnolasco@up.edu.ph"
            };

            //act
            repo.AddCreator(c);

            try
            {
                repo.AddChannel(c, "MatheMartian");
                repo.UpdateChannelName("MatheMartian",c);

                //assert
                Assert.True(false);
            }
            catch
            {
                Assert.True(true);
            }


        }

        [Fact]
        public void AddCreatorandChannelShouldAdd()
        {
            //arrange
            var options = new DbContextOptionsBuilder<YouTubeJamContext>()
                .UseInMemoryDatabase("AddCreatorandChannelShouldAdd")
                .Options;

            using var context = new YouTubeJamContext(options);
            var mapper = new DBMapper(context);
            var repo = new Repository(context, mapper);

            BusinessLogic.Creator c = new BusinessLogic.Creator()
            {
                FirstName = "Marielle",
                LastName = "Nolasco",
                Email = "mtnolasco@up.edu.ph"
            };
            string channelName = "MatheMartian";
            //act
            repo.AddCreatorandChannel(c, channelName);
            //assert
            using var assertContext = new YouTubeJamContext(options);
            var result = assertContext.Channel.FirstOrDefault(c => c.ChannelName == channelName);
            Assert.NotNull(result);
        }
    }
}