﻿using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;
using System;
using YoutubeJam.Auth;

namespace YoutubeJam.BusinessLogic
{
    public static class SentimentAnalysis
    {
        private const string key_var = "TEXT_ANALYTICS_SUBSCRIPTION_KEY";
        private static readonly string subscriptionKey = Environment.GetEnvironmentVariable(key_var);
        private const string endpoint_var = "TEXT_ANALYTICS_ENDPOINT";
        private static readonly string endpoint = Environment.GetEnvironmentVariable(endpoint_var);

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
