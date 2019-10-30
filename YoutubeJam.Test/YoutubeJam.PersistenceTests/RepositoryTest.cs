﻿using Microsoft.EntityFrameworkCore;
using System.Linq;
using Xunit;
using YoutubeJam.BusinessLogic;
using YoutubeJam.Persistence;
using YoutubeJam.Persistence.Entities;

namespace YoutubeJam.Test
{
    public class RepositoryTest
    {
        [Fact]
        public void AddVideoShouldAddVideo()
        {
            //arrange
            var options = new DbContextOptionsBuilder<YouTubeJamContext>()
                .UseInMemoryDatabase("AddVideoShouldAdd")
                .Options;
            var url = "abc";

            using var context = new YouTubeJamContext(options);
            var mapper = new DBMapper(context);
            var repo = new Repository(context, mapper);

            //act
            repo.AddVideo(url);


            //assert
            using var assertContext = new YouTubeJamContext(options);
            var rest = assertContext.Video.First(v => v.URL == url);

            Assert.NotNull(rest);
        }

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
                Password = "Password",
                PhoneNumber = "(510) 289 8893"
            };

            AverageSentiment avg = new AverageSentiment()
            {
                VideoURL = "Abc",
                AverageSentimentScore = 0.5
            };

            //act
            repo.AddVideo("Abc");
            repo.AddCreator(c);
            repo.AddAnalysis(avg, c);

            //assert
            using var assertContext = new YouTubeJamContext(options);
            var rest = assertContext.Analysis1.First();
            Assert.NotNull(rest);

        }
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
                Password = "Password",
                PhoneNumber = "(510) 289 8893"
            };

            AverageSentiment avg = new AverageSentiment()
            {
                VideoURL = "Abc",
                AverageSentimentScore = 0.5
            };

            //act
            repo.AddVideo("Abc");
            repo.AddCreator(c);
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
        [Fact]

        public void AddAnalysisShouldNotCreateNewVideos()
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
                Password = "Password",
                PhoneNumber = "(510) 289 8893"
            };

            AverageSentiment avg = new AverageSentiment()
            {
                VideoURL = "Abc",
                AverageSentimentScore = 0.5
            };

            //act
            repo.AddVideo("Abc");
            repo.AddCreator(c);
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
    }
}