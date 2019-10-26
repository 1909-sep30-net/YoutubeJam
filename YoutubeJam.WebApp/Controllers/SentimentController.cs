using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using YoutubeJam.Auth;
using YoutubeJam.BusinessLogic;

namespace YoutubeJam.WebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SentimentController : ControllerBase
    {
        private readonly ILogger<SentimentController> _logger;

        public SentimentController(ILogger<SentimentController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public Sentiment Get(string videoId)
        {
            try
            {
                // Return a new sentiment with the summary
                return new Sentiment
                {
                    Summary = SummarizeCommentThreadList(ParseCommentThreadList(GetCommentThreadList(videoId)))
                };
            }
            catch (AggregateException ex)
            {
                foreach (var e in ex.InnerExceptions)
                {
                    _logger.LogError("Error: " + e.Message);
                }
                return null;
            }
        }

        private CommentThreadListResponse GetCommentThreadList(string videoId)
        {
            // Initialize the youtube service with the API key
            YouTubeService youtubeService = YoutubeDataAPIAuth.GetYoutubeService();

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

        private IList<string> ParseCommentThreadList(CommentThreadListResponse commentThreadListResponse)
        {
            // Construct the empty parsed comment threads list
            IList<string> parsedCommentThreadsList = new List<string>();

            // Iterate over the comment thread list response and add each comment text
            foreach (CommentThread commentThread in commentThreadListResponse.Items)
            {
                parsedCommentThreadsList.Add(commentThread.Snippet.TopLevelComment.Snippet.TextDisplay);
            }

            // Return the parsed comment threads list
            return parsedCommentThreadsList;
        }

        private string SummarizeCommentThreadList(IList<string> commentThreads)
        {
            // Construct a new string builder
            StringBuilder stringBuilder = new StringBuilder();

            // For each string in the comment threads append the string and a new line
            foreach (string str in commentThreads)
            {
                stringBuilder.Append($"{str}\n");
            }

            // Return the string
            return stringBuilder.ToString();
        }
    }
}
