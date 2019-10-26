using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using YoutubeJam.BusinessLogic;

namespace YoutubeJam.WebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SentimentController : ControllerBase
    {
        private static readonly string[] Sentiments = new[]
        {
            "Optimistic", "Depressing", "Angry", "Happy", "Sad"
        };

        private readonly ILogger<SentimentController> _logger;

        public SentimentController(ILogger<SentimentController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public Sentiment Get()
        {
            var rng = new Random();
            return new Sentiment
            {
                Summary = Sentiments[rng.Next(Sentiments.Length)]
            };
        }
    }
}
