using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics;
using Microsoft.Azure.CognitiveServices.Language.TextAnalytics.Models;
using Microsoft.Rest;
using System.Net.Http;
using System.Web;
using YoutubeJam.Auth;

namespace YoutubeJam.BusinessLogic
{
    
    static class SentimentAnalysis
    {


        public static void AuthenticateAnalysis()
        {
            var credentials = new AzureTextAnalyticsAuth();
            var credentialsX = new ApiKeyServiceClientCredentials(subscriptionKey);


            string apiKey = credentials.GetAPIKey();
            string endpoint = credentials.GetEndPoint();

            TextAnalyticsClient client = new TextAnalyticsClient()
            {
                Endpoint = endpoint
            };

            Console.OutputEncoding = System.Text.Encoding.UTF8;
            SentimentAnalysisExample(client);
            // languageDetectionExample(client);
            // entityRecognitionExample(client);
            // KeyPhraseExtractionExample(client);
        }


        static void SentimentAnalysisExample(TextAnalyticsClient client)
        {
            var result = client.Sentiment("I had the best day of my life.", "en");
            Console.WriteLine($"Sentiment Score: {result.Score:0.00}");
        }

    }
   
}	
