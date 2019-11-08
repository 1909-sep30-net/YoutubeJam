using System.Threading.Tasks;
using YoutubeJam.BusinessLogic;
using YoutubeJam.Persistence.Entities;

namespace YoutubeJam.Persistence
{
    public interface IMapper
    {
        public Task<BusinessLogic.Creator> ParseCreatorAsync(Entities.Creator creator);
        public Task<Entities.Creator> ParseCreatorAsync(BusinessLogic.Creator creator);
        public Task<Analysis1> ParseAnalysisAsync(AverageSentiment sentimentAverage, BusinessLogic.Creator c);
        public Task<AverageSentiment> ParseAnalysisAsync(Analysis1 item);
        public Task<Video> ParseVideoAsync(string videourl, string channelName);
        public Task<Channel> ParseChannelAsync(BusinessLogic.Creator c, string channelName);
    }
}
