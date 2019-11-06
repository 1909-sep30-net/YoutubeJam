using System;
using System.Linq;
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
        public Analysis1 ParseAnalysis(BL.AverageSentiment sentimentAverage, BL.Creator c)
        {
            return new Analysis1()
            {
                Creatr = GetCreatorByEmail(c.Email),
                Vid = GetVideoByURL(sentimentAverage.VideoURL),
                AnalDate = DateTime.Now,
                SentAve = (decimal)sentimentAverage.AverageSentimentScore
            };
        }
        /// <summary>
        /// Method that converts Business Logic Analysis Objects for API to Entity Analysis Objects for DB and vice versa
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public BL.AverageSentiment ParseAnalysis(Analysis1 item)
        {
            return new BL.AverageSentiment()
            {
                AverageSentimentScore = (double)item.SentAve,
                AnalysisDate = item.AnalDate
            };
        }
        /// <summary>
        /// Method that converts Business Logic Creator Objects for API to Entity Creator Objects for DB and vice versa
        /// </summary>
        /// <param name="creator"></param>
        /// <returns></returns>
        public Creator ParseCreator(BL.Creator creator)
        {
            return new Creator()
            {
                FirstName = creator.FirstName,
                LastName = creator.LastName,
                Email = creator.Email
            };
        }
        /// <summary>
        /// Method that converts Business Logic Creator Objects for API to Entity Creator Objects for DB and vice versa
        /// </summary>
        /// <param name="creator"></param>
        /// <returns></returns>
        public BL.Creator ParseCreator(Creator creator)
        {
            return new BL.Creator()
            {
                FirstName = creator.FirstName,
                LastName = creator.LastName,
                Email = creator.Email
            };
        }
        /// <summary>
        ///  Method that converts video url and channel name from API to Entity Video Objects for DB 
        /// </summary>
        /// <param name="videourl"></param>
        /// <param name="channelName"></param>
        /// <returns></returns>

        public Video ParseVideo(string videourl, string channelName)
        {
            return new Video()
            {
                URL = videourl,
                VideoChannel = GetChannelByName(channelName)
            };
        }
        /// <summary>
        /// Method that takes in a Channel Author and Channel Name from API and creates a Channel Object for the DB
        /// </summary>
        /// <param name="c"></param>
        /// <param name="channelName"></param>
        /// <returns></returns>
        public Channel ParseChannel(BL.Creator c, string channelName)
        {
            return new Channel()
            {
                ChannelName = channelName,
                ChannelAuthor = GetCreatorByEmail(c.Email)
            };
        }
        /// <summary>
        /// Method that gets creator from DB by their email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        private Creator GetCreatorByEmail(string email)
        {
            return _context.Creator.Single(c => c.Email == email);
        }
        /// <summary>
        /// Method that gets video from DB by its URL
        /// </summary>
        /// <param name="videoURL"></param>
        /// <returns></returns>
        private Video GetVideoByURL(string videoURL)
        {
            return _context.Video.Single(v => v.URL == videoURL);
        }
        /// <summary>
        /// Method that gets the channel from DB by its channel name
        /// </summary>
        /// <param name="channelName"></param>
        /// <returns></returns>
        private Channel GetChannelByName(string channelName)
        {
            return _context.Channel.SingleOrDefault(c => c.ChannelName == channelName);
        }
    }
}