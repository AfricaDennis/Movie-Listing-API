using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Film_Listing_API.Dtos;
using Film_Listing_API.Extensions;
using Film_Listing_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Film_Listing_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private readonly IActorsService _actorService;

        public ActorsController(IActorsService actorService)
        {
            _actorService = actorService;
        }

        // GET: api/Actors
        [HttpGet]
        public async Task<IActionResult> GetActors()
        {
            return (await _actorService.GetAll()).ContentOrError();
        }

        // GET: api/Actors/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetActor(long id)
        {
            return (await _actorService.Get(id)).ContentOrError();
        }

        // PUT: api/Actors/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActor([FromRoute] long id, UpdateActorDto updateActorDto)
        {
            return (await _actorService.Update(id, updateActorDto)).ContentOrError();
        }

        // POST: api/Actors
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<IActionResult> PostActor(CreateActorDto createActorDto)
        {
            return (await _actorService.Create(createActorDto)).ContentOrError();
        }

        // DELETE: api/Actors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActor(long id)
        {
            return (await _actorService.Delete(id)).ContentOrError();
        }

        //private bool ActorExists(int id)
        //{
        //    return _context.Actors.Any(e => e.Id == id);
        //}
    }
}
