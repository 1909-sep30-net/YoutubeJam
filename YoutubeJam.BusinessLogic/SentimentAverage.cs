using System;
using System.Collections.Generic;

namespace YoutubeJam.BusinessLogic
{
    public class AverageSentiment
    {
        public double AverageSentimentScore { get; set; }

        public string VideoURL { get; set; }

        public DateTime AnalysisDate { get; set; }
        public IList<YoutubeComment> CommentList { get; set; } = new List<YoutubeComment>();
    }
}
