using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public async Task AddAnalysisAsync(BL.AverageSentiment sentimentAverage, BL.Creator c)
        {
            if( await _context.Video.FirstOrDefaultAsync(v => v.URL == sentimentAverage.VideoURL) == null)
            {
                await AddVideoAsync(sentimentAverage.VideoURL, c.ChannelName);
            }
            await _context.Analysis1.AddAsync(await _map.ParseAnalysisAsync(sentimentAverage, c));
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Adding a channel to the DB, must have a creator in the DB first
        /// </summary>
        /// <param name="c"></param>
        /// <param name="channelName"></param>
        public async Task AddChannelAsync(BL.Creator c, string channelName)
        {
            await _context.Channel.AddAsync(await _map.ParseChannelAsync(c, channelName));
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Method for adding creator to table
        /// </summary>
        /// <param name="c"></param>
        public async Task AddCreatorAsync(BL.Creator c)
        {
            await _context.Creator.AddAsync(await _map.ParseCreatorAsync(c));
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Method for adding video to DB, note that every video should have a channel
        /// </summary>
        /// <param name="videourl"></param>
        /// <param name="channelName"></param>

        public async Task AddVideoAsync(string videourl, string channelName)
        {
            await _context.Video.AddAsync(await _map.ParseVideoAsync(videourl, channelName));
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Method that gets the Analysis History of a certain video by a creator
        /// </summary>
        /// <param name="videourl"></param>
        /// <param name="c"></param>
        /// <returns></returns>
        public async Task<List<BL.AverageSentiment>> GetAnalysisHistoryAsync(string videourl, BL.Creator c)
        {
            List<BL.AverageSentiment> analHist = new List<BL.AverageSentiment>();
            List<Analysis1> analHistfromDB = await _context.Analysis1.Where(a => a.Vid.URL == videourl && a.Creatr.Email == c.Email).Include(a => a.Vid).ToListAsync();
            foreach (Analysis1 item in analHistfromDB)
            {
                analHist.Add(await _map.ParseAnalysisAsync(item));
            }
            return analHist;
        }

        /// <summary>
        /// Repo Method that returns a channel name
        /// </summary>
        /// <param name="creator"></param>
        /// <returns></returns>
        public async Task<BL.Creator> GetUserAsync(string creatorEmail)
        {
            return  await _map.ParseCreatorAsync(await _context.Creator.FirstOrDefaultAsync(c => c.Email == creatorEmail));
        }

        /// <summary>
        /// Method that returns all creators from the db
        /// </summary>
        /// <returns></returns>
        public async Task<List<BL.Creator>> GetCreatorsAsync()
        {
            List<Creator> allCreators = await _context.Creator.Select(c => c).ToListAsync();
            List<BL.Creator> allCreatorsfromDB = new List<BL.Creator>();
            foreach (Entities.Creator item in allCreators)
            {
                allCreatorsfromDB.Add(await _map.ParseCreatorAsync(item));
            }
            return allCreatorsfromDB;
        }

        /// <summary>
        /// Method that updates channel name of a creator
        /// </summary>
        /// <param name="newChannelName"></param>
        /// <param name="channelAuth"></param>
        public async Task UpdateChannelNameAsync(string newChannelName, BL.Creator channelAuth)
        {
            Channel toUpdate = await _context.Channel.FirstOrDefaultAsync(c => c.ChannelAuthor.Email == channelAuth.Email);
            if (await CheckIfChannelNameExistsAsync(newChannelName)) throw new ChannelNameTakenException("Channel Name Already Taken. Input a unique one");
            else
            {
                toUpdate.ChannelName = newChannelName;
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Method that takes in log in credentials of user and returns a creator object if it exists in db
        /// Might be used for cookies upon logging in
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <param name="passsword"></param>
        /// <returns></returns>

        public async Task<BL.Creator> LogInAsync(string email)
        {
            if (!await _context.Creator.AnyAsync(c => c.Email == email)) throw new CreatorDoesNotExistException("Creator is not in database");
            return await _map.ParseCreatorAsync(await _context.Creator.FirstOrDefaultAsync(c => c.Email == email));
        }

        public async Task<bool> CheckIfChannelNameExistsAsync(string channelName)
        {
            if (await _context.Channel.FirstOrDefaultAsync(c => c.ChannelName == channelName) != null) return true;
            return false;
        }

        public async Task AddCreatorandChannelAsync(BL.Creator c, string channelName)
        {
            if (await CheckIfChannelNameExistsAsync(channelName)) throw new ChannelNameTakenException("Channel Name Already Taken");
            else
            {
                await AddCreatorAsync(c);
                await AddChannelAsync(c, channelName);
            }
        }

        public async Task<List<BL.AverageSentiment>> GetUserSearchHistoryAsync(string creatorEmail)
        {
            List<Analysis1> analysesfromDB = await  _context.Analysis1.Where(a => a.Creatr.Email == creatorEmail).Include(a => a.Vid).ToListAsync();
            List<BL.AverageSentiment> userHistory = new List<BL.AverageSentiment>();
            foreach (var item in analysesfromDB)
            {
                userHistory.Add(await _map.ParseAnalysisAsync(item));

            }
            return userHistory;
        }
    }
}