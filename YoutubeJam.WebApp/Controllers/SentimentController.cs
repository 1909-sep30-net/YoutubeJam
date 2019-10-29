using Google.Apis.YouTube.v3.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
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
        public AverageSentiment Get(string videoId)
        {
            try
            {
                // Return a new sentiment with the summary
                return ParseCommentThreadListResponse(YoutubeDataAPIAuth.GetCommentThreadListResponse(videoId));
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

        private AverageSentiment ParseCommentThreadListResponse(CommentThreadListResponse commentThreadListResponse)
        {
            // Construct the empty parsed comment threads list
            AverageSentiment averageScore = new AverageSentiment();

            // Iterate over the comment thread list response and add each comment text
            foreach (CommentThread commentThread in commentThreadListResponse.Items)
            {
                var comment = new YoutubeComment();
                comment.AuthorName = commentThread.Snippet.TopLevelComment.Snippet.AuthorDisplayName;
                comment.Content = commentThread.Snippet.TopLevelComment.Snippet.TextDisplay;
                comment.SentimentScore = SentimentAnalysis.SelectComments(comment.Content);

                averageScore.CommentList.Add(comment);
            }

            averageScore.AverageSentimentScore = averageScore.CommentList.Average(c => c.SentimentScore);

            // Return the parsed comment threads list
            return averageScore;
        }
    }
}
