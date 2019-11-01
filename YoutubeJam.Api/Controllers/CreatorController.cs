using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using YoutubeJam.BusinessLogic;

namespace YoutubeJam.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreatorController : ControllerBase
    {
        private IRepository _repository;

        public CreatorController(IRepository repository)
        {
            _repository = repository;
        }

        // GET: api/Creator
        [HttpGet]
        public IEnumerable<Creator> Get(IEnumerable<Creator> creator)
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
        public void Post([FromBody] Creator inputCreator)
        {
            BusinessLogic.Creator creator = new BusinessLogic.Creator()
            {
                FirstName = inputCreator.FirstName,
                LastName = inputCreator.LastName,
                Password = inputCreator.Password,
                PhoneNumber = inputCreator.PhoneNumber
            };

            _repository.AddCreator(creator);
        }

        // PUT: api/Creator/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            //Might implement later
        }
    }
}