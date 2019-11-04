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

        public Analysis1 ParseAnalysis(BL.AverageSentiment sentimentAverage, BL.Creator c)
        {
            return new Analysis1()
            {
                Creatr = GetCreatorByPhoneNumber(c.PhoneNumber),
                Vid = GetVideoByURL(sentimentAverage.VideoURL),
                AnalDate = DateTime.Now,
                SentAve = (decimal)sentimentAverage.AverageSentimentScore
            };
        }

        public BL.AverageSentiment ParseAnalysis(Analysis1 item)
        {
            return new BL.AverageSentiment()
            {
                AverageSentimentScore = (double)item.SentAve,
                AnalysisDate = item.AnalDate
            };
        }

        private Creator GetCreatorByPhoneNumber(string phoneNumber)
        {
            return _context.Creator.Single(c => c.PhoneNumber == phoneNumber);
        }

        private Video GetVideoByURL(string videoURL)
        {
            return _context.Video.Single(v => v.URL == videoURL);
        }

        public Creator ParseCreator(BL.Creator creator)
        {
            return new Creator()
            {
                FirstName = creator.FirstName,
                LastName = creator.LastName,
                PhoneNumber = creator.PhoneNumber,
                Password = creator.Password,
                UserName = creator.Username,
            };
        }

        public BL.Creator ParseCreator(Creator creator)
        {
            return new BL.Creator()
            {
                FirstName = creator.FirstName,
                LastName = creator.LastName,
                PhoneNumber = creator.PhoneNumber,
                Password = creator.Password,
                Username = creator.UserName
            };
        }

        public Video ParseVideo(string videourl, string channelName)
        {
            return new Video()
            {
                URL = videourl,
                VideoChannel = GetChannelByName(channelName)
            };
        }

        private Channel GetChannelByName(string channelName)
        {
            return _context.Channel.SingleOrDefault(c => c.ChannelName == channelName);
        }

        public Channel ParseChannel(BL.Creator c, string channelName)
        {
            return new Channel()
            {
                ChannelName = channelName,
                ChannelAuthor = GetCreatorByPhoneNumber(c.PhoneNumber)
            };
        }
    }
}