using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YoutubeJam.BusinessLogic;
using YoutubeJam.Persistence;
using YoutubeJam.Persistence.Entities;

namespace YoutubeJam.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreatorController : ControllerBase
    {
        // GET: api/Creator
        [HttpGet]
        public IEnumerable<BusinessLogic.Creator> Get(IEnumerable<BusinessLogic.Creator> creator)
        {
            return creator;
        }

        // GET: api/Creator/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Creator
        [HttpPost]
        public void Post([FromBody] BusinessLogic.Creator inputCreator)
        {
            var options = new DbContextOptionsBuilder<YouTubeJamContext>()
                .UseInMemoryDatabase("AddAnalysisShouldAdd")
                .Options;

            using var context = new YouTubeJamContext(options);
            var mapper = new DBMapper(context);
            var repo = new Repository(context, mapper);

            BusinessLogic.Creator creator = new BusinessLogic.Creator()
            {
                FirstName = inputCreator.FirstName,
                LastName = inputCreator.LastName,
                Password = inputCreator.Password,
                PhoneNumber = inputCreator.PhoneNumber
            };

            repo.AddCreator(creator);

        }

        // PUT: api/Creator/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
