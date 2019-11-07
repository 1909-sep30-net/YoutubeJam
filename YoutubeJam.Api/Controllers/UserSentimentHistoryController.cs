using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using YoutubeJam.BusinessLogic;

namespace YoutubeJam.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSentimentHistoryController : ControllerBase
    {
        private readonly IRepository _repository;

        public UserSentimentHistoryController(IRepository repository)
        {
            _repository = repository;
        }

        // POST: api/VideoSentimentHistory
        [HttpPost]
        public IActionResult Post([FromBody] VideoHistory inputVideo)
        {
            _repository.AddVideo(inputVideo.VideoUrl, inputVideo.ChannelName);
            return Ok();
        }

        // GET: api/UserSentimentHistory
        [HttpGet]
        public IEnumerable<AverageSentiment> Get(string email)
        {
            return _repository.GetUserSearchHistory(email);
        }

    }
}
