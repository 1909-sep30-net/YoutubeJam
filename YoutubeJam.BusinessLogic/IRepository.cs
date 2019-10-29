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
        public void AddVideo(string videourl);

        public void AddAnalysis(AverageSentiment sentimentAverage, Creator c);
        
    }
}
