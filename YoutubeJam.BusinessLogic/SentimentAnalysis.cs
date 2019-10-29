﻿using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;
using System;
using System.Collections.Generic;
using YoutubeJam.Auth;

namespace YoutubeJam.BusinessLogic
{
    public class SentimentAnalysis
    {
        private const string key_var = "TEXT_ANALYTICS_SUBSCRIPTION_KEY";

        //private static readonly string subscriptionKey = Environment.GetEnvironmentVariable(key_var);
        private static readonly string subscriptionKey = "be6e6df747a54718939279d9a26640ef";

        private const string endpoint_var = "TEXT_ANALYTICS_ENDPOINT";

        //private static readonly string endpoint = Environment.GetEnvironmentVariable(endpoint_var);
        private static readonly string endpoint = "https://youtubejam.cognitiveservices.azure.com/";

        public static List<double> generalScore = new List<double>();

        static SentimentAnalysis()
        {
            if (null == subscriptionKey)
            {
                throw new Exception("Please set/export the environment variable: " + key_var);
            }
            if (null == endpoint)
            {
                throw new Exception("Please set/export the environment variable: " + endpoint_var);
            }
        }

        public static double SelectComments(string inputComment)
        {
            ApiKeyServiceClientCredentials credentials = new ApiKeyServiceClientCredentials(subscriptionKey);
            TextAnalyticsClient client = new TextAnalyticsClient(credentials)
            {
                Endpoint = endpoint
            };

            var result = client.Sentiment(inputComment, "en");

            return (double)result.Score;
        }
    }
}
