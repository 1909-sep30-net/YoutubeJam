using System;
using System.Collections.Generic;
using System.Text;

namespace YoutubeJam.BusinessLogic
{
    public class VideoHistory
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ChannelName { get; set; }
        public double AverageSentimentScore { get; set; }
        public string VideoUrl { get; set; }
        public DateTime AnalysisDate { get; set; }
        public IList<YoutubeComment> CommentList { get; set; } = new List<YoutubeComment>();

    }
}
