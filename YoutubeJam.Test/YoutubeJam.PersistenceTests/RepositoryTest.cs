using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task AddCreatorsShouldAddCreatorsAsync()
        {
            //arrange
            var options = new DbContextOptionsBuilder<YouTubeJamContext>()
                .UseInMemoryDatabase("AddCreatorsShouldAdd")
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
            await repo.AddCreatorAsync(c);

            //assert
            using var assertContext = new YouTubeJamContext(options);
            var rest = assertContext.Creator.Select(c => c);
            Assert.NotNull(rest);
        }

        [Fact]
        public async Task AddChannelShouldAddChannelAsync()
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
            await repo.AddCreatorAsync(c);
            await repo.AddChannelAsync(c, channelName);

            //assert
            using var assertContext = new YouTubeJamContext(options);
            var result = await assertContext.Channel.FirstOrDefaultAsync(c => c.ChannelName == channelName);
            Assert.NotNull(result);
        }

        /// <summary>
        /// Tests the adding a video to the database
        /// </summary>
        [Fact]
        public async Task AddVideoShouldAddVideoAsync()
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
            await repo.AddCreatorAsync(c);
            await repo.AddChannelAsync(c, channelName);
            await repo.AddVideoAsync(url, channelName);

            //assert
            using var assertContext = new YouTubeJamContext(options);
            var rest = await assertContext.Video.FirstAsync(v => v.URL == url);

            Assert.NotNull(rest);
        }

        /// <summary>
        /// Tests if analysis can be stored in db
        /// </summary>
        [Fact]
        public async Task AddAnalysisShouldAddAsync()
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
            await repo.AddCreatorAsync(c);
            await repo.AddChannelAsync(c, "MatheMartian");
            await repo.AddVideoAsync("Abc", "MatheMartian");
            await repo.AddAnalysisAsync(avg, c);

            //assert
            using var assertContext = new YouTubeJamContext(options);
            var rest = await assertContext.Analysis1.FirstAsync();
            Assert.NotNull(rest);
        }

        /// <summary>
        /// Tests if analysis is stored properly (i.e. now new instances of creators are created)
        /// </summary>
        [Fact]
        public async Task AddAnalysisShouldNotCreateNewCreatorsAsync()
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
            await repo.AddCreatorAsync(c);
            await repo.AddChannelAsync(c, "MatheMartian");
            await repo.AddVideoAsync("Abc", "MatheMartian");
            await repo.AddAnalysisAsync(avg, c);

            //assert
            using var assertContext = new YouTubeJamContext(options);
            mapper = new DBMapper(assertContext);
            repo = new Repository(assertContext, mapper);
            var result = await repo.GetCreatorsAsync();

            if (result.Count > 1) Assert.True(false);
            else
            {
                Assert.True(true);
            }
        }

        /// <summary>
        /// Tests if adding analysis works properly (i.e. new videos aren't created)
        /// </summary>
        [Fact]
        public async Task AddAnalysisShouldNotCreateNewVideosAsync()
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
            await repo.AddCreatorAsync(c);
            await repo.AddChannelAsync(c, "MatheMartian");
            await repo.AddVideoAsync("Abc","MatheMartian");
            await repo.AddAnalysisAsync(avg, c);

            //assert
            using var assertContext = new YouTubeJamContext(options);
            var result = await assertContext.Video.Select(v => v).ToListAsync();
            if (result.Count() > 1) Assert.True(false);
            else
            {
                Assert.True(true);
            }
        }

        [Fact]
        public async Task AddAnalysisShouldAddVidAsync()
        {
            //arrange
            var options = new DbContextOptionsBuilder<YouTubeJamContext>()
                .UseInMemoryDatabase("AddAnalysisShouldAddVid")
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
            await repo.AddCreatorAsync(c);
            await repo.AddChannelAsync(c, "MatheMartian");
            await repo.AddAnalysisAsync(avg, c);

            //assert
            using var assertContext = new YouTubeJamContext(options);
            var rest = await assertContext.Analysis1.FirstAsync();
            Assert.NotNull(rest);
        }
        /// <summary>
        /// Testing if the Analysis History can be retrieved
        /// </summary>
        [Fact]
        public async Task GetAnalHistoryShouldGetSomethingAsync()
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
            await repo.AddCreatorAsync(c);
            await repo.AddChannelAsync(c, "MatheMartian");
            await repo.AddVideoAsync("Abc", "MatheMartian");
            await repo.AddAnalysisAsync(avg, c);

            //assert
            using var assertContext = new YouTubeJamContext(options);
            mapper = new DBMapper(assertContext);
            repo = new Repository(assertContext, mapper);
            var result = await repo.GetAnalysisHistoryAsync("Abc", c);
            Assert.True(result.Count() > 0);
        }

        /// <summary>
        /// Testing the logging in functionality
        /// </summary>
        [Fact]
        public async Task LogInShouldLogInAsync()
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
            await repo.AddCreatorAsync(c);
            await repo.AddChannelAsync(c, "Amrs");

            //assert
            using var assertContext = new YouTubeJamContext(options);
            mapper = new DBMapper(assertContext);
            repo = new Repository(assertContext, mapper);
            var result = await repo.LogInAsync(email);
            Assert.NotNull(result);
        }
        /// <summary>
        /// Testing the logging in functionality
        /// </summary>
        [Fact]
        public async Task LogInShouldHandleExceptionsAsync()
        {
            //assert
            var options = new DbContextOptionsBuilder<YouTubeJamContext>()
                .UseInMemoryDatabase("LogInShouldHandleExceptions")
                .Options;


            string email = "mtnolasco@up.edu.ph";
            
            
            using var assertContext = new YouTubeJamContext(options);
            var mapper = new DBMapper(assertContext);
            var repo = new Repository(assertContext, mapper);
            //act & assert
            try
            {
                var result = await repo.LogInAsync(email);
                Assert.True(false);
            }
            catch(Persistence.CreatorDoesNotExistException)
            {
                Assert.True(true);
            }
            
        }
        /// <summary>
        /// Tests if Get channel name functionality works
        /// </summary>
        [Fact]
        public async Task GetChannelNameShouldGetSomethingAsync()
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
            await repo.AddCreatorAsync(c);
            await repo.AddChannelAsync(c, "MatheMartian");

            //assert
            using var assertContext = new YouTubeJamContext(options);
            mapper = new DBMapper(assertContext);
            repo = new Repository(assertContext, mapper);
            var result = await repo.GetUserAsync("mtnolasco@up.edu.ph");
            Assert.True(result.ChannelName == "MatheMartian");

        }

        [Fact]
        public async Task GetUserHistoryShouldGetSomethingAsync()
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
            await repo.AddCreatorAsync(c);
            await repo.AddChannelAsync(c, "MatheMartian");
            await repo.AddVideoAsync("Abc", "MatheMartian");
            await repo.AddAnalysisAsync(avg, c);

            //assert
            using var assertContext = new YouTubeJamContext(options);
            mapper = new DBMapper(assertContext);
            repo = new Repository(assertContext, mapper);
            var result = await repo.GetUserSearchHistoryAsync("mtnolasco@up.edu.ph");
            Assert.NotNull(result[0].VideoURL);
        }
        /// <summary>
        /// Method that tests the update channe name functionality of the repo
        /// </summary>
        [Fact]
        public async Task UpdateChannelNameShouldUpdateAsync()
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
            await repo.AddCreatorAsync(c);
            await repo.AddChannelAsync(c, "MatheMartian");
            await repo.UpdateChannelNameAsync("MathMars", c);

            //assert
            using var assertContext = new YouTubeJamContext(options);
            mapper = new DBMapper(assertContext);
            repo = new Repository(assertContext, mapper);
            var result = await repo.GetUserAsync("mtnolasco@up.edu.ph");
            Assert.True(result.ChannelName == "MathMars");
        }

       
        [Fact]
        public async Task ChannelNameShouldBeUniqueTest2Async()
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
            await repo.AddCreatorAsync(c);

            try
            {
                await repo.AddChannelAsync(c, "MatheMartian");
                await repo.UpdateChannelNameAsync("MatheMartian",c);

                //assert
                Assert.True(false);
            }
            catch
            {
                Assert.True(true);
            }


        }

        [Fact]
        public async Task AddCreatorandChannelShouldAddAsync()
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
            await repo.AddCreatorandChannelAsync(c, channelName);
            //assert
            using var assertContext = new YouTubeJamContext(options);
            var result = assertContext.Channel.FirstOrDefaultAsync(c => c.ChannelName == channelName);
            Assert.NotNull(result);
        }
        [Fact]
        public async Task AddCreatorandChannelShouldBeUniqueAsync()
        {
            //arrange
            var options = new DbContextOptionsBuilder<YouTubeJamContext>()
                .UseInMemoryDatabase("AddCreatorandChannelShouldBeUnique")
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
            try
            {
                await repo.AddCreatorandChannelAsync(c, channelName);
                await repo.AddCreatorandChannelAsync(c, channelName);
                Assert.True(false);
            }
            catch (ChannelNameTakenException)
            {
                Assert.True(true);
            }
            
        }
        [Fact]
        public async Task AddAnalysisAsyncWithLimitedInfoShouldAddAsync()
        {
            //arrange
            var options = new DbContextOptionsBuilder<YouTubeJamContext>()
                .UseInMemoryDatabase("AddAnalysisAsyncWithLimitedInfoShouldAddAsync")
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
            BusinessLogic.Creator b = new BusinessLogic.Creator()
            {
                Email = "mtnolasco@up.edu.ph",
                ChannelName = "Mathemartian"
            };
            AverageSentiment avg = new AverageSentiment()
            {
                VideoURL = "Abc",
                AverageSentimentScore = 0.5
            };

            //act
            await repo.AddCreatorAsync(c);
            await repo.AddChannelAsync(c, "MatheMartian");
            await repo.AddAnalysisAsync(avg, b);

            //assert
            using var assertContext = new YouTubeJamContext(options);
            var rest = await assertContext.Analysis1.FirstAsync();
            Assert.NotNull(rest);
        }
    }
}