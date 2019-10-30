using YoutubeJam.BusinessLogic;
using YoutubeJam.Persistence.Entities;

namespace YoutubeJam.Persistence
{
    public interface IMapper
    {
        public BusinessLogic.Creator ParseCreator(Entities.Creator creator);

        public Entities.Creator ParseCreator(BusinessLogic.Creator creator);
        Video ParseVideo(string videourl);
        Analysis1 ParseAnalysis(AverageSentiment sentimentAverage, BusinessLogic.Creator c);
    }
}
