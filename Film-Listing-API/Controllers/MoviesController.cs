using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Film_Listing_API;
using Film_Listing_API.Dtos;
using Film_Listing_API.Extensions;
using Film_Listing_API.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMoviesService _movieService;

        public MoviesController(IMoviesService movieService)
        {
            _movieService = movieService;
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<IActionResult> GetMovies()
        {
            return (await _movieService.GetAll()).ContentOrError();
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovie(long id)
        {
            return (await _movieService.Get(id)).ContentOrError();
        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie([FromRoute] long id, UpdateMovieDto updateMovieDto)
        {
            return (await _movieService.Update(id, updateMovieDto)).ContentOrError();
        }

        // POST: api/Movies
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<IActionResult> PostMovie(CreateMovieDto newMovieDto)
        {
            return (await _movieService.Create(newMovieDto)).ContentOrError();
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(long id)
        {
            return (await _movieService.Delete(id)).ContentOrError();
        }

    //    private bool MovieExists(int id)
    //    {
    //        return _context.Movies.Any(e => e.Id == id);
    //    }
    }
}