using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        public async Task<IEnumerable<Creator>> GetAsync()
        {
            return await _repository.GetCreatorsAsync();
        }
        /// <summary>
        /// Action that returns latest creator account made
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{email}", Name = "Get")]
        public async Task<Creator> GetAsync(string email)
        {
            Creator creator = await _repository.GetUserAsync(email);
            return creator;
        }
        /// <summary>
        /// Action that creates a creator user
        /// </summary>
        /// <param name="inputCreator"></param>
        // POST: api/Creator
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Creator inputCreator)
        {

            try
            {
                await _repository.LogInAsync(inputCreator.Email);
                await _repository.UpdateChannelNameAsync(inputCreator.ChannelName, inputCreator);
                return Ok();
            }
            catch (CreatorDoesNotExistException)
            {
                Creator creator = new Creator()
                {
                    FirstName = inputCreator.FirstName,
                    LastName = inputCreator.LastName,
                    Email = inputCreator.Email
                };
                try
                {
                    await _repository.AddCreatorandChannelAsync(creator, inputCreator.ChannelName);
                    return CreatedAtAction("POST", inputCreator);
                }
                catch (ChannelNameTakenException)
                {
                    return BadRequest();
                }

                
            }
            catch (ChannelNameTakenException)
            {
                //change channel name and try again
                return BadRequest();
            }

        }
       

    }
}