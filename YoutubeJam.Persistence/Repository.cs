using System.Collections.Generic;
using System.Linq;
using YoutubeJam.Persistence.Entities;
using BL = YoutubeJam.BusinessLogic;

namespace YoutubeJam.Persistence
{
    public class Repository : BL.IRepository
    {
        private readonly YouTubeJamContext _context;
        private readonly IMapper _map;

        /// <summary>
        /// Set up the dependencies of Repository
        /// </summary>
        public Repository(YouTubeJamContext context, IMapper map)
        {
            _context = context;
            _map = map;
        }

        /// <summary>
        /// Method for adding analysis to the db
        /// </summary>
        /// <param name="sentimentAverage"></param>
        /// <param name="c"></param>
        public void AddAnalysis(BL.AverageSentiment sentimentAverage, BL.Creator c)
        {
            _context.Analysis1.Add(_map.ParseAnalysis(sentimentAverage, c));
            _context.SaveChanges();
        }

        /// <summary>
        /// Adding a channel to the DB, must have a creator in the DB first
        /// </summary>
        /// <param name="c"></param>
        /// <param name="channelName"></param>
        public void AddChannel(BL.Creator c, string channelName)
        {
            _context.Channel.Add(_map.ParseChannel(c, channelName));
            _context.SaveChanges();
        }

        /// <summary>
        /// Method for adding creator to table
        /// </summary>
        /// <param name="c"></param>
        public void AddCreator(BL.Creator c)
        {
            _context.Creator.Add(_map.ParseCreator(c));
            _context.SaveChanges();
        }

        /// <summary>
        /// Method for adding video to DB, note that every video should have a channel
        /// </summary>
        /// <param name="videourl"></param>
        /// <param name="channelName"></param>

        public void AddVideo(string videourl, string channelName)
        {
            _context.Video.Add(_map.ParseVideo(videourl, channelName));
            _context.SaveChanges();
        }

        /// <summary>
        /// Method that gets the Analysis History of a certain video by a creator
        /// </summary>
        /// <param name="videourl"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public List<BL.AverageSentiment> GetAnalysisHistory(string videourl, BL.Creator c)
        {
            List<BL.AverageSentiment> analHist = new List<BL.AverageSentiment>();
            List<Analysis1> analHistfromDB = _context.Analysis1.Where(a => a.Vid.URL == videourl && a.Creatr.Email == c.Email).ToList();
            foreach (Analysis1 item in analHistfromDB)
            {
                analHist.Add(_map.ParseAnalysis(item));
            }
            return analHist;
        }

        /// <summary>
        /// Repo Method that returns a channel name
        /// </summary>
        /// <param name="creator"></param>
        /// <returns></returns>
        public BL.Creator GetUser(string creatorEmail)
        {
            return _map.ParseCreator(_context.Creator.FirstOrDefault(c => c.Email == creatorEmail));
        }

        /// <summary>
        /// Method that returns all creators from the db
        /// </summary>
        /// <returns></returns>
        public List<BL.Creator> GetCreators()
        {
            List<Creator> allCreators = _context.Creator.Select(c => c).ToList();
            List<BL.Creator> allCreatorsfromDB = new List<BL.Creator>();
            foreach (Entities.Creator item in allCreators)
            {
                allCreatorsfromDB.Add(_map.ParseCreator(item));
            }
            return allCreatorsfromDB;
        }

        /// <summary>
        /// Method that updates channel name of a creator
        /// </summary>
        /// <param name="newChannelName"></param>
        /// <param name="channelAuth"></param>
        public void UpdateChannelName(string newChannelName, BL.Creator channelAuth)
        {
            Channel toUpdate = _context.Channel.FirstOrDefault(c => c.ChannelAuthor.Email == channelAuth.Email);
            if (CheckIfChannelNameExists(newChannelName)) throw new ChannelNameTakenException("Channel Name Already Taken. Input a unique one");
            else
            {
                toUpdate.ChannelName = newChannelName;
                _context.SaveChanges();
            }
        }

        /// <summary>
        /// Method that takes in log in credentials of user and returns a creator object if it exists in db
        /// Might be used for cookies upon logging in
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="passsword"></param>
        /// <returns></returns>

        public BL.Creator LogIn(string email)
        {
            if (!_context.Creator.Any(c => c.Email == email)) throw new CreatorDoesNotExistException("Creator is not in database");
            return _map.ParseCreator(_context.Creator.FirstOrDefault(c => c.Email == email));
        }

        public bool CheckIfChannelNameExists(string channelName)
        {
            if (_context.Channel.FirstOrDefault(c => c.ChannelName == channelName) != null) return true;
            return false;
        }

        public void AddCreatorandChannel(BL.Creator c, string channelName)
        {
            if (CheckIfChannelNameExists(channelName)) throw new ChannelNameTakenException("Channel Name Already Taken");
            else
            {
                AddCreator(c);
                AddChannel(c, channelName);
            }
        }
    }
}