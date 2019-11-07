using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace YoutubeJam.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoSentimentHistoryController : ControllerBase
    {
        // GET: api/VideoSentimentHistory
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/VideoSentimentHistory/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/VideoSentimentHistory
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

    }
}
