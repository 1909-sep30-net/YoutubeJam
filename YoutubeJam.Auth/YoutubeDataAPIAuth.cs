using Google.Apis.Services;
using Google.Apis.YouTube.v3;

namespace YoutubeJam.Auth
{
    public static class YoutubeDataAPIAuth
    {
        private static string _youtubeDataApiKey = "AIzaSyDJB8DOsIR_cmSFZlok1XE-p-jmflLcrFs";
        private static string _applicationName = "YoutubeJAM";

        public static string GetYoutubeDataAPIKey()
        {
            return _youtubeDataApiKey;
        }

        public static string GetApplicationName()
        {
            return _applicationName;
        }

        public static YouTubeService GetYoutubeService()
        {
            return new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = GetYoutubeDataAPIKey(),
                ApplicationName = GetApplicationName()
            });
        }
    }
}
