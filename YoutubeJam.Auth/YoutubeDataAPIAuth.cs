using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;

namespace YoutubeJam.Auth
{
    public static class YoutubeDataAPIAuth
    {
        private static readonly string _applicationName = "YoutubeJAM";

        public static string YoutubeDataAPIKey { get; set; }

        public static YouTubeService GetYoutubeService()
        {
            // Return a new Youtube Service with the API key and the application name
            return new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = YoutubeDataAPIKey,
                ApplicationName = _applicationName
            });
        }

        public static CommentThreadListResponse GetCommentThreadListResponse(string videoId, int maxComments)
        {
            // Initialize the youtube service with the API key
            YouTubeService youtubeService = GetYoutubeService();

            // Construct the request
            CommentThreadsResource.ListRequest commentThreadsListRequest = youtubeService.CommentThreads.List("snippet");
            commentThreadsListRequest.MaxResults = maxComments;
            commentThreadsListRequest.TextFormat = CommentThreadsResource.ListRequest.TextFormatEnum.PlainText;
            commentThreadsListRequest.VideoId = videoId;

            // Retrieve the response
            CommentThreadListResponse commentThreadsListResponse = commentThreadsListRequest.Execute();

            // Return the response
            return commentThreadsListResponse;
        }
    }
}
