﻿using System.Collections.Generic;

namespace YoutubeJam.BusinessLogic
{
    /// <summary>
    /// Repository interface
    /// </summary>
    public interface IRepository
    {
        
        public void AddCreator(Creator c);
        public void AddChannel(Creator c, string channelName);
        public void AddVideo(string videourl, string channelName);
        public void AddAnalysis(AverageSentiment sentimentAverage, Creator c);

        public List<Creator> GetCreators();
        public List<AverageSentiment> GetAnalysisHistory(string videourl, Creator c);
        public string GetChannelName(Creator creator);
        
        public void UpdateChannelName(string newChannelName, Creator channelAuth);

        public Creator LogIn(string email);
    }
}
