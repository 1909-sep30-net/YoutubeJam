using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        public async Task<IActionResult> PostAsync([FromBody] VideoHistory inputVideo)
        {
            AverageSentiment inputAnalysis = new AverageSentiment()
            {
                VideoURL = inputVideo.VideoUrl,
                AverageSentimentScore = inputVideo.AverageSentimentScore
            };
            Creator inputCreator = new Creator()
            {
                Email = inputVideo.Email
            };
            await _repository.AddAnalysisAsync(inputAnalysis, inputCreator);
            return CreatedAtAction("Post", inputVideo);
        }

        // GET: api/UserSentimentHistory
        [HttpGet]
        public async Task<IEnumerable<AverageSentiment>> GetAsync(string email)
        {
            return await _repository.GetUserSearchHistoryAsync(email);
        }

    }
}
