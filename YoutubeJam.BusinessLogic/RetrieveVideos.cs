using Google.Apis.YouTube.v3.Data;
using System.Collections.Generic;
using System.Linq;
using YoutubeJam.Auth;

namespace YoutubeJam.BusinessLogic
{
    public class RetrieveVideos
    {

        public static List<UserVideos> RetrieveVideosList(string channelId)
        {
            return GetVideos(channelId);
        }

        private static List<UserVideos> GetVideos(string channelId)
        {
            List<UserVideos> userVideos = new List<UserVideos>();

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
                    UserVideos tempVideo = new UserVideos();
                    tempVideo.VideoURL = playlistItem.Snippet.ResourceId.VideoId;
                    tempVideo.VideoTitle = playlistItem.Snippet.Title;
                    //Video Sentiment
                    tempVideo.SentimentScore = ParseCommentThreadListResponse(YoutubeDataAPIAuth.GetCommentThreadListResponse(tempVideo.VideoURL, 1));
                    userVideos.Add(tempVideo);
                }
                nextPageToken = playlistItemsListResponse.NextPageToken;

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
