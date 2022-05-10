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
    public class ProducersController : ControllerBase
    {
        private readonly IProducersService _ProducersService;

        public ProducersController(IProducersService ProducersService)
        {
            _ProducersService = ProducersService;
        }

        // GET: api/Producers
        [HttpGet]
        public async Task<IActionResult> GetProducers()
        {
            return (await _ProducersService.GetAll()).ContentOrError();
        }

        // GET: api/Producers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProducer(long id)
        {
            return (await _ProducersService.Get(id)).ContentOrError();
        }

        // PUT: api/Producers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProducer([FromRoute]long id, UpdateProducerDto updateProducerDto)
        {
            return (await _ProducersService.Update(id, updateProducerDto)).ContentOrError();
        }

        // POST: api/Producers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<IActionResult> PostProducer(CreateProducerDto newProducerDto)
        {
            return (await _ProducersService.Create(newProducerDto)).ContentOrError();
        }

        // DELETE: api/Producers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducer(long id)
        {
            return (await _ProducersService.Delete(id)).ContentOrError();
        }
    }
}
