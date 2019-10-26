using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;

namespace YoutubeJam.Auth
{
    public static class YoutubeDataAPIAuth
    {
        private static string _youtubeDataApiKey = "AIzaSyDJB8DOsIR_cmSFZlok1XE-p-jmflLcrFs";
        private static string _applicationName = "YoutubeJAM";

        private static string GetYoutubeDataAPIKey()
        {
            return _youtubeDataApiKey;
        }

        private static string GetApplicationName()
        {
            return _applicationName;
        }

        private static YouTubeService GetYoutubeService()
        {
            // Return a new Youtube Service with the API key and the application name
            return new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = GetYoutubeDataAPIKey(),
                ApplicationName = GetApplicationName()
            });
        }

        public static CommentThreadListResponse GetCommentThreadListResponse(string videoId)
        {
            // Initialize the youtube service with the API key
            YouTubeService youtubeService = GetYoutubeService();

            // Construct the request
            CommentThreadsResource.ListRequest commentThreadsListRequest = youtubeService.CommentThreads.List("snippet");
            commentThreadsListRequest.MaxResults = 100;
            commentThreadsListRequest.TextFormat = CommentThreadsResource.ListRequest.TextFormatEnum.PlainText;
            commentThreadsListRequest.VideoId = videoId;

            // Retrieve the response
            CommentThreadListResponse commentThreadsListResponse = commentThreadsListRequest.Execute();

            // Return the response
            return commentThreadsListResponse;
        }
    }
}
