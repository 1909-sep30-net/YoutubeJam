using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;

namespace YoutubeJam.Auth
{
    public static class YoutubeDataAPIAuth
    {
        public static string YoutubeDataAPIKey;
        private static string _applicationName = "YoutubeJAM";

        public static string GetYoutubeDataAPIKey()
        {
            return YoutubeDataAPIKey;
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
            commentThreadsListRequest.MaxResults = 10;
            commentThreadsListRequest.TextFormat = CommentThreadsResource.ListRequest.TextFormatEnum.PlainText;
            commentThreadsListRequest.VideoId = videoId;

            // Retrieve the response
            CommentThreadListResponse commentThreadsListResponse = commentThreadsListRequest.Execute();

            // Return the response
            return commentThreadsListResponse;
        }
    }
}
