using AutoMapper;
using Film_Listing_API.Dtos;
using Film_Listing_API.Mapper;
using Film_Listing_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Film_Listing_API.Services
{
    public interface IMoviesService
    {
        Task<IOperationResult> GetAll();
        Task<IOperationResult> Get(long id);
        Task<IOperationResult> Update(long id, UpdateMovieDto updateMovieDto);
        Task<IOperationResult> Create(CreateMovieDto newMovie);
        Task<IOperationResult> Delete(long id);
    }

    public class MoviesService : IMoviesService
    {
        private readonly FilmlistingContext _context;
        private readonly IMapper _mapper;
        private readonly IModelFactory _modelFactory;


        public MoviesService(FilmlistingContext context, IMapper mapper, IModelFactory modelFactory)
        {
            _context = context;
            _mapper = mapper;
            _modelFactory = modelFactory;
        }

        public async Task<IOperationResult> GetAll()
        {
            try
            {
                // TODO: LOG THE CALL WITH A LOGGER
                var movies = await _context.Movies.AsQueryable()
                    //.Include(m => m.Actor)
                    .ToListAsync(CancellationToken.None);

                var movieDtos = _mapper.Map<List<MovieDto>>(movies);
                foreach (var movieDto in movieDtos)
                {
                    await ApplyAdditionalData(movieDto);
                }
                return OperationResult.Success(movieDtos);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IOperationResult> Get(long id)
        {
            try
            {
                // TODO: LOG THE CALL WITH A LOGGER
                var movieDto = await _context.Movies
                    .Where(m => m.Id == id)
                    //.Include(m => m.Actor)
                    .Select(m => _mapper.Map<MovieDto>(m))
                    .FirstOrDefaultAsync(CancellationToken.None);
                if (movieDto == null) return OperationResult.NotFound();
                await ApplyAdditionalData(movieDto);
                return OperationResult.Success(movieDto);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IOperationResult> Update(long id, UpdateMovieDto updateMovieDto)
        {
            try
            {
                var dbMovie = await _context.Movies
                    .Where(m => m.Id == id)
                    .FirstOrDefaultAsync();

                if(dbMovie == null) return OperationResult.NotFound();
                _modelFactory.UpdateMovieFactory(dbMovie, updateMovieDto);
                _context.Movies.Update(dbMovie);
                await UpdateMovieData(id, updateMovieDto);
                _context.SaveChanges();
                return OperationResult.Success();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        private async Task UpdateMovieData(long id, UpdateMovieDto updateMovieDto)
        {
            var dbActorMovie = await _context.MovieActors.Where(ma => ma.MovieId == id).ToListAsync(CancellationToken.None);
            dbActorMovie.ForEach(ma => _context.MovieActors.Remove(ma));
            foreach(var actorId in updateMovieDto.ActorIds)
            {
                _context.MovieActors.Add(new MovieActor
                {
                    ActorId = actorId,
                    MovieId = id
                });
            }
            _context.SaveChanges();


            var dbMovieProducers = await _context.MovieProducers.Where(mp => mp.MovieId == id).ToListAsync(CancellationToken.None);
            dbMovieProducers.ForEach(mp => _context.MovieProducers.Remove(mp));
            foreach( var producerId in updateMovieDto.ProducerIds)
            {
                _context.MovieProducers.Add(new MovieProducer
                {
                    ProducerId = producerId,
                    MovieId = id
                });
            }
            _context.SaveChanges();
        }

        public async Task<IOperationResult> Create(CreateMovieDto newMovie)
        {
            try
            {
                var movie = _modelFactory.CreateMovieFactory(newMovie);
                _context.Movies.Add(movie);
                _context.SaveChanges();
                if ( newMovie.ProducerIds != null )
                {

                    foreach (var producerId in newMovie.ProducerIds)
                    {
                        _context.MovieProducers.Add(new MovieProducer
                        {
                            ProducerId = producerId,
                            MovieId = movie.Id
                        });
                    }
                }
                if (newMovie.ActorIds != null )
                {
                    foreach (var actorId in newMovie.ActorIds)
                    {
                        _context.MovieActors.Add(new MovieActor
                        {
                            ActorId = actorId,
                            MovieId = movie.Id
                        });
                    }
                }
                return OperationResult.Success();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }



        public async Task<IOperationResult> Delete(long id)
        {
            try
            {
                var movie = await _context.Movies
                    .Where(m => m.Id == id)
                    .FirstOrDefaultAsync(CancellationToken.None);

                if (movie == null) return OperationResult.NotFound();

                _context.Movies.Remove(movie);
                _context.SaveChanges();

                return OperationResult.Success();
            }

            catch (System.Exception ex)
            {
                throw ex;
            }
        }
        

        private async Task ApplyAdditionalData(MovieDto movieDto)
        { 
            movieDto.Producers = await _context.Producers
                .Where(p => _context.MovieProducers
                    .Where(mp => mp.MovieId == movieDto.Id)
                    .Select(mp => mp.ProducerId)
                    .ToList()
                    .Contains(p.Id))
                .ToListAsync(CancellationToken.None);
            movieDto.Actors = await _context.Actors
                .Where(a => _context.MovieActors
                    .Where(ma => ma.MovieId == movieDto.Id)
                    .Select(ma => ma.ActorId)
                    .ToList()
                    .Contains(a.Id))
                .ToListAsync(CancellationToken.None);
        }
    }
}
