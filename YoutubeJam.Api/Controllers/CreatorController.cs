using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using YoutubeJam.BusinessLogic;
using YoutubeJam.Persistence;

namespace YoutubeJam.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreatorController : ControllerBase
    {
        /// <summary>
        /// Controller for our creator users
        /// </summary>
        private readonly IRepository _repository;

        public CreatorController(IRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Action that returns all creator records from db
        /// </summary>
        /// <returns></returns>
        // GET: api/Creator
        [HttpGet]
        public IEnumerable<Creator> Get()
        {
            return _repository.GetCreators();
        }
        /// <summary>
        /// Action that returns latest creator account made
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Creator/5
        [HttpGet("{id}", Name = "Get")]
        public Creator Get(int id)
        {
            List<Creator> tempCreators = _repository.GetCreators();
            return tempCreators[id - 1];
        }
        /// <summary>
        /// Action that creates a creator user
        /// </summary>
        /// <param name="inputCreator"></param>
        // POST: api/Creator
        [HttpPost]
        public IActionResult Post([FromBody] Creator inputCreator)
        {
            
            try
            {
                _repository.LogIn(inputCreator.Email);
                return Ok();
            }
            catch (CreatorDoesNotExistException ex)
            {
                Creator creator = new Creator()
                {
                    FirstName = inputCreator.FirstName,
                    LastName = inputCreator.LastName,
                    Email = inputCreator.Email
                };

                _repository.AddCreatorandChannel(creator, inputCreator.ChannelName);
                return CreatedAtAction("POST", inputCreator);
            }
            catch (ChannelNameTakenException ex)
            {
                //change channel name and try again
                return BadRequest();
            }
            
        }
        /// <summary>
        /// Action for modifying creator records
        /// </summary>
        /// <param name="id"></param>
        /// <param name="value"></param>
        // PUT: api/Creator/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            //Might implement later
        }

    }
}