using YoutubeJam.BusinessLogic;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace YoutubeJam.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideosController : ControllerBase
    {
        
        /// <summary>
        /// Action that returns videos and their sentiments from creator 
        /// </summary>
        /// <returns></returns>
        // GET: api/Videos
        [HttpGet]
        public List<UserVideos> Get(string channel)
        {
            return RetrieveVideos.RetrieveVideosList(channel);
        }

    }
}
