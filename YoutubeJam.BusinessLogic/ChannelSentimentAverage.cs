using System;
using System.Collections.Generic;
using System.Text;

namespace YoutubeJam.BusinessLogic
{
    public class ChannelSentimentAverage
    {

        public double AverageSentiment { get; set; }

        public DateTime AnalysisDate { get; set; }

        public IList<UserVideo> UserVideos { get; set; } = new List<UserVideo>();

    }
}
