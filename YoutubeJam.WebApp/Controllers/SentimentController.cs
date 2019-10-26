using Google.Apis.YouTube.v3.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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
        public IList<YoutubeComment> Get(string videoId)
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

        private IList<YoutubeComment> ParseCommentThreadListResponse(CommentThreadListResponse commentThreadListResponse)
        {
            // Construct the empty parsed comment threads list
            IList<YoutubeComment> parsedCommentThreadsList = new List<YoutubeComment>();

            // Iterate over the comment thread list response and add each comment text
            foreach (CommentThread commentThread in commentThreadListResponse.Items)
            {
                parsedCommentThreadsList.Add(new YoutubeComment
                {
                    AuthorName = commentThread.Snippet.TopLevelComment.Snippet.AuthorDisplayName,
                    Content = commentThread.Snippet.TopLevelComment.Snippet.TextDisplay
                });
            }

            // Return the parsed comment threads list
            return parsedCommentThreadsList;
        }
    }
}
