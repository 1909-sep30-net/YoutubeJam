using Google.Apis.YouTube.v3.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using YoutubeJam.Auth;

namespace YoutubeJam.BusinessLogic
{
    public static class RetrieveVideos
    {
        public static ChannelSentimentAverage RetrieveChannelAverage(string channelId)
        {
            ChannelSentimentAverage channelSentiment = new ChannelSentimentAverage();
            channelSentiment.UserVideos = GetVideos(channelId);
            channelSentiment.AnalysisDate = DateTime.Now;
            channelSentiment.AverageSentiment = channelSentiment.UserVideos.Average(c => c.SentimentScore);
            return channelSentiment;
        }

        public static List<UserVideo> RetrieveVideosList(string channelId)
        {
            return GetVideos(channelId);
        }

        private static List<UserVideo> GetVideos(string channelId)
        {
            List<UserVideo> userVideos = new List<UserVideo>();

            var youtubeService = YoutubeDataAPIAuth.GetYoutubeService();

            var channelsListRequest = youtubeService.Channels.List("contentDetails");
            channelsListRequest.ForUsername = channelId;

            var channelsListResponse = channelsListRequest.Execute();

            foreach (var channel in channelsListResponse.Items)
            {
                var uploadsListId = channel.ContentDetails.RelatedPlaylists.Uploads;
                var nextPageToken = "";

                var playlistItemsListRequest = youtubeService.PlaylistItems.List("snippet");
                playlistItemsListRequest.PlaylistId = uploadsListId;
                playlistItemsListRequest.MaxResults = 5;
                playlistItemsListRequest.PageToken = nextPageToken;

                var playlistItemsListResponse = playlistItemsListRequest.Execute();
                foreach (var playlistItem in playlistItemsListResponse.Items)
                {
                    UserVideo tempVideo = new UserVideo();
                    tempVideo.VideoURL = playlistItem.Snippet.ResourceId.VideoId;
                    tempVideo.VideoTitle = playlistItem.Snippet.Title;
                    //Video Sentiment
                    tempVideo.SentimentScore = ParseCommentThreadListResponse(YoutubeDataAPIAuth.GetCommentThreadListResponse(tempVideo.VideoURL, 1));
                    userVideos.Add(tempVideo);
                }
                
            }
            return userVideos;
        }

        private static double ParseCommentThreadListResponse(CommentThreadListResponse commentThreadListResponse)
        {
            AverageSentiment averageScore = new AverageSentiment();
            foreach (CommentThread commentThread in commentThreadListResponse.Items)
            {
                var comment = new YoutubeComment();
                comment.AuthorName = commentThread.Snippet.TopLevelComment.Snippet.AuthorDisplayName;
                comment.Content = commentThread.Snippet.TopLevelComment.Snippet.TextDisplay;
                comment.SentimentScore = SentimentAnalysis.SelectComments(comment.Content);

                averageScore.CommentList.Add(comment);
            }
            averageScore.AverageSentimentScore = averageScore.CommentList.Average(c => c.SentimentScore);
            averageScore.VideoURL = commentThreadListResponse.Items[0].Snippet.VideoId;

            return averageScore.AverageSentimentScore;
        }
    }
}