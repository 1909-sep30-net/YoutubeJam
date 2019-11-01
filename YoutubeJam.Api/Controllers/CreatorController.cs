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
        public IEnumerable<Creator> Get()
        {
            return _repository.GetCreators();
        }

        // GET: api/Creator/5
        [HttpGet("{id}", Name = "Get")]
        public Creator Get(int id)
        {
            List<Creator> tempCreators = new List<Creator>();
            tempCreators = _repository.GetCreators();

            return tempCreators[id - 1];
        }

        // POST: api/Creator
        [HttpPost]
        public void Post([FromBody] Creator inputCreator)
        {
            Creator creator = new Creator()
            {
                FirstName = inputCreator.FirstName,
                LastName = inputCreator.LastName,
                Password = inputCreator.Password,
                PhoneNumber = inputCreator.PhoneNumber,
                Username = inputCreator.Username
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