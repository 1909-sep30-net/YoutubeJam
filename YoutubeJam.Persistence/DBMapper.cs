using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using YoutubeJam.Persistence.Entities;
using BL = YoutubeJam.BusinessLogic;

namespace YoutubeJam.Persistence
{
    /// <summary>
    /// Class that maps business logic items to db objects
    /// </summary>
    public class DBMapper : IMapper
    {
        private readonly YouTubeJamContext _context;

        public DBMapper(YouTubeJamContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Method that converts Business Logic Analysis Objects for API to Entity Analysis Objects for DB and vice versa
        /// </summary>
        /// <param name="sentimentAverage"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public async Task<Analysis1> ParseAnalysisAsync(BL.AverageSentiment sentimentAverage, BL.Creator c)
        {
            return await Task.FromResult(new Analysis1()
            {
                Creatr = await GetCreatorByEmailAsync(c.Email),
                Vid = await GetVideoByURLAsync(sentimentAverage.VideoURL),
                AnalDate = DateTime.Now,
                SentAve = (decimal)sentimentAverage.AverageSentimentScore
            });
        }

        /// <summary>
        /// Method that converts Business Logic Analysis Objects for API to Entity Analysis Objects for DB and vice versa
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<BL.AverageSentiment> ParseAnalysisAsync(Analysis1 item)
        {
            return await Task.FromResult(new BL.AverageSentiment()
            {
                AverageSentimentScore = (double)item.SentAve,
                AnalysisDate = item.AnalDate,
                VideoURL = item.Vid.URL
            });
        }

        /// <summary>
        /// Method that converts Business Logic Creator Objects for API to Entity Creator Objects for DB and vice versa
        /// </summary>
        /// <param name="creator"></param>
        /// <returns></returns>
        public async Task<Creator> ParseCreatorAsync(BL.Creator creator)
        {
            return await Task.FromResult(new Creator()
            {
                FirstName = creator.FirstName,
                LastName = creator.LastName,
                Email = creator.Email
            });
        }

        /// <summary>
        /// Method that converts Business Logic Creator Objects for API to Entity Creator Objects for DB and vice versa
        /// </summary>
        /// <param name="creator"></param>
        /// <returns></returns>
        public async Task<BL.Creator> ParseCreatorAsync(Creator creator)
        {
            var Channel = await _context.Channel.FirstOrDefaultAsync(c => c.ChannelAuthor.Email == creator.Email);
            return new BL.Creator()
            {
                FirstName = creator.FirstName,
                LastName = creator.LastName,
                Email = creator.Email,
                ChannelName = Channel.ChannelName
            };
        }

        /// <summary>
        ///  Method that converts video url and channel name from API to Entity Video Objects for DB
        /// </summary>
        /// <param name="videourl"></param>
        /// <param name="channelName"></param>
        /// <returns></returns>

        public async Task<Video> ParseVideoAsync(string videourl, string channelName)
        {
            return new Video()
            {
                URL = videourl,
                VideoChannel = await GetChannelByNameAsync(channelName)
            };
        }

        /// <summary>
        /// Method that takes in a Channel Author and Channel Name from API and creates a Channel Object for the DB
        /// </summary>
        /// <param name="c"></param>
        /// <param name="channelName"></param>
        /// <returns></returns>
        public async Task<Channel> ParseChannelAsync(BL.Creator c, string channelName)
        {
            return new Channel()
            {
                ChannelName = channelName,
                ChannelAuthor = await GetCreatorByEmailAsync(c.Email)
            };
        }

        /// <summary>
        /// Method that gets creator from DB by their email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        private async Task<Creator> GetCreatorByEmailAsync(string email)
        {
            return await _context.Creator.SingleAsync(c => c.Email == email);
        }

        /// <summary>
        /// Method that gets video from DB by its URL
        /// </summary>
        /// <param name="videoURL"></param>
        /// <returns></returns>
        private async Task<Video> GetVideoByURLAsync(string videoURL)
        {
            return await _context.Video.SingleAsync(v => v.URL == videoURL);
        }

        /// <summary>
        /// Method that gets the channel from DB by its channel name
        /// </summary>
        /// <param name="channelName"></param>
        /// <returns></returns>
        private async Task<Channel> GetChannelByNameAsync(string channelName)
        {
            return await _context.Channel.SingleOrDefaultAsync(c => c.ChannelName == channelName);
        }
    }
}