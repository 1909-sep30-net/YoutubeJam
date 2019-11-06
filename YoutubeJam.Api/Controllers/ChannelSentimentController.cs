using Microsoft.AspNetCore.Mvc;
using YoutubeJam.BusinessLogic;

namespace YoutubeJam.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChannelSentimentController : ControllerBase
    {
        /// <summary>
        /// Action that returns videos and their sentiments from creator
        /// </summary>
        /// <returns></returns>
        // GET: api/Videos
        [HttpGet]
        public ChannelSentimentAverage Get(string channel)
        {
            return RetrieveVideos.RetrieveChannelAverage(channel);
        }
    }
}