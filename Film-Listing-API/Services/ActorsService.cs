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
    public interface IActorsService
    {
        Task<IOperationResult> GetAll();
        Task<IOperationResult> Get(long id);
        Task<IOperationResult> Update(long id, UpdateActorDto updateActorDto);
        Task<IOperationResult> Create(CreateActorDto newActor);
        Task<IOperationResult> Delete(long id);
    }

    public class ActorsService : IActorsService
    {
        private readonly FilmlistingContext _context;
        private readonly IMapper _mapper;
        private readonly IModelFactory _modelFactory;


        public ActorsService(FilmlistingContext context, IMapper mapper, IModelFactory modelFactory)
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
                var actor = await _context.Actors.AsQueryable()
                    .ToListAsync(CancellationToken.None);

                var actorDtos = _mapper.Map<List<ActorDto>>(actor);

                return OperationResult.Success(actorDtos);
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
                var actorDto = await _context.Actors
                    .Where(a => a.Id == id)
                    .Select(a => _mapper.Map<ActorDto>(a))
                    .FirstOrDefaultAsync(CancellationToken.None);

                if (actorDto == null) return OperationResult.NotFound();
                
                return OperationResult.Success(actorDto);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IOperationResult> Update(long id, UpdateActorDto updateActorDto)
        {
            try
            {
                var dbActors = await _context.Actors
                    .Where(a => a.Id == id)
                    .FirstOrDefaultAsync();

                if (dbActors == null) return OperationResult.NotFound();
                _modelFactory.UpdateActorFactory(dbActors, updateActorDto);
                _context.Actors.Update(dbActors);
                _context.SaveChanges();

                return OperationResult.Success();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IOperationResult> Create(CreateActorDto newActor)
        {
            try
            {
                var Actor = _modelFactory.CreateActorFactory(newActor);
                _context.Actors.Add(Actor);
                _context.SaveChanges();

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
                var Actor = await _context.Actors
                    .Where(a => a.Id == id)
                    .FirstOrDefaultAsync(CancellationToken.None);

                if ( Actor == null ) return OperationResult.NotFound();

                _context.Actors.Remove(Actor);
                _context.SaveChanges();

                return OperationResult.Success();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}
