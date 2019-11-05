using Google.Apis.YouTube.v3.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using YoutubeJam.Auth;
using YoutubeJam.BusinessLogic;

namespace YoutubeJam.WebApp.Controllers
{
    /// <summary>
    /// The controller for YoutubeJAM sentiment API
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class VideoSentimentController : ControllerBase
    {
        private readonly ILogger<VideoSentimentController> _logger;

        public VideoSentimentController(ILogger<VideoSentimentController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Returns the sentiments of a youtube video's comments and its average
        /// </summary>
        /// <param name="videoId">The ID of the video</param>
        /// <param name="maxComments">The maximum comments to retrieve</param>
        /// <returns>The average sentiment object</returns>
        [HttpGet]
        public AverageSentiment Get(string videoId, int maxComments)
        {
            try
            {
                // Return a new sentiment with the summary
                return ParseCommentThreadListResponse(YoutubeDataAPIAuth.GetCommentThreadListResponse(videoId, maxComments));
            }
            catch (AggregateException ex)
            {
                // Log every exception
                foreach (var e in ex.InnerExceptions)
                {
                    _logger.LogError("Error: " + e.Message);
                }

                // Return nothing
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

            // Set the average sentiment score
            averageScore.AverageSentimentScore = averageScore.CommentList.Average(c => c.SentimentScore);
            averageScore.VideoURL = commentThreadListResponse.Items[0].Snippet.VideoId;

            // Return the parsed comment threads list
            return averageScore;
        }
    }
}
