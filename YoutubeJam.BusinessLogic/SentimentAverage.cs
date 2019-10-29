using System.Collections.Generic;

namespace YoutubeJam.BusinessLogic
{
    public class AverageSentiment
    {
        public double AverageSentimentScore { get; set; }

        public IList<YoutubeComment> CommentList { get; set; } = new List<YoutubeComment>();
    }
}
