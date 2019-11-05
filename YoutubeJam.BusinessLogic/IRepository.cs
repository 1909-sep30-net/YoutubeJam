using System.Collections.Generic;

namespace YoutubeJam.BusinessLogic
{
    /// <summary>
    /// Repository interface
    /// </summary>
    public interface IRepository
    {
        public List<Creator> GetCreators();
        public void AddCreator(Creator c);
        public void AddVideo(string videourl, string channelName);
        public void AddChannel(Creator c, string channelName);

        public void AddAnalysis(AverageSentiment sentimentAverage, Creator c);
        public List<AverageSentiment> GetAnalysisHistory(string videourl, Creator c);

        public Creator LogIn(string email);
    }
}
