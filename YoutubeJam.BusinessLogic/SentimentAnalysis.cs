using System;
using System.Collections.Generic;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;
using SentimentAnalysis;

namespace YoutubeJam.BusinessLogic
{
    class SentimentAnalysis
    {

        private const string key_var = "TEXT_ANALYTICS_SUBSCRIPTION_KEY";
        private static readonly string subscriptionKey = Environment.GetEnvironmentVariable(key_var);

        private const string endpoint_var = "TEXT_ANALYTICS_ENDPOINT";
        private static readonly string endpoint = Environment.GetEnvironmentVariable(endpoint_var);

        static int noOfComments = 1;

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

        static void SelectComments()
        {
            ApiKeyServiceClientCredentials credentials = new ApiKeyServiceClientCredentials(subscriptionKey);
            TextAnalyticsClient client = new TextAnalyticsClient(credentials)
            {
                Endpoint = endpoint
            };

            string inputComment = ""; //Get A Single Comment From The YoutubeDataAPI
            
            List<double> generalScore = new List<double>();

            for (int i = 0; i < noOfComments; i++)
            {
                generalScore.Add(AnalyzeComment(client, inputComment));
            }

        }

        static double AnalyzeComment(TextAnalyticsClient client, string inputComment)
        {
            var result = client.Sentiment(inputComment, "en");

            return (double) result.Score;
        }

    }
}
