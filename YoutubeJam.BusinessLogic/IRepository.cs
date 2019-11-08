using System.Collections.Generic;
using System.Threading.Tasks;

namespace YoutubeJam.BusinessLogic
{
    /// <summary>
    /// Repository interface
    /// </summary>
    public interface IRepository
    {
        
        public  Task AddCreatorAsync(Creator c);
        public Task AddChannelAsync(Creator c, string channelName);
        public Task AddVideoAsync(string videourl, string channelName);
        public Task AddAnalysisAsync(AverageSentiment sentimentAverage, Creator c);

        public Task<List<Creator>> GetCreatorsAsync();
        public Task<List<AverageSentiment>> GetAnalysisHistoryAsync(string videourl, Creator c);
        public Task<Creator> GetUserAsync(string creatorEmail);
        public Task<List<AverageSentiment>> GetUserSearchHistoryAsync(string creatorEmail);
        
        public Task UpdateChannelNameAsync(string newChannelName, Creator channelAuth);

        public Task<Creator> LogInAsync(string email);

        public Task AddCreatorandChannelAsync(Creator c, string channelName);
    }
}
