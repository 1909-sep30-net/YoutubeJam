using System;
using System.Collections.Generic;
using System.Text;

namespace YoutubeJam.Auth
{
    public class AzureTextAnalyticsAuth
    {
        private readonly string apiKey = "be6e6df747a54718939279d9a26640ef";
        private readonly string endPoint = "https://youtubejam.cognitiveservices.azure.com/";
        private object subscriptionKey;

        //public AzureTextAnalyticsAuth(string apiKey, string endPoint)
        //{
        //    this.apiKey = apiKey;
        //    this.endPoint = endPoint;
        //}

        public string GetAPIKey()
        {
            return apiKey;
        }
        public string GetEndPoint()
        {
            return endPoint;
        }

    }
}
