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
    public interface IProducersService
    {
        Task<IOperationResult> GetAll();
        Task<IOperationResult> Get(long id);
        Task<IOperationResult> Update(long id, UpdateProducerDto updateProducerDto);
        Task<IOperationResult> Create(CreateProducerDto newProducerDto);
        Task<IOperationResult> Delete(long id);
    }

    public class ProducersService : IProducersService
    {
        private readonly FilmlistingContext _context;
        private readonly IMapper _mapper;
        private readonly IModelFactory _modelFactory;

        public ProducersService(FilmlistingContext context, IMapper mapper, IModelFactory modelFactory)
        {
            _context = context;
            _mapper = mapper;
            _modelFactory = modelFactory;
        }

        public async Task<IOperationResult> GetAll()
        {
            try
            {
                var producer = await _context.Producers.AsQueryable()
                    .ToListAsync(CancellationToken.None);

                var producerDtos = _mapper.Map<List<ProducerDto>>(producer);

                return OperationResult.Success(producerDtos);
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
                var producerDto = await _context.Producers
                    .Where(p => p.Id == id)
                    .Select(p => _mapper.Map<ProducerDto>(p))
                    .FirstOrDefaultAsync(CancellationToken.None);

                if (producerDto == null) return OperationResult.NotFound();

                return OperationResult.Success(producerDto);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IOperationResult> Update(long id, UpdateProducerDto updateProducerDto)
        {
            try
            {
                var dbProducer = await _context.Producers
                    .Where(p => p.Id == id)
                    .FirstOrDefaultAsync();

                if (dbProducer == null) return OperationResult.NotFound();

                _modelFactory.UpdateProducerFactory(dbProducer, updateProducerDto);
                _context.Producers.Update(dbProducer);
                _context.SaveChanges();

                return OperationResult.Success();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IOperationResult> Create(CreateProducerDto newProducer)
        {
            try
            {
                var Producer = _modelFactory.CreateProducerFactory(newProducer);
                _context.Producers.Add(Producer);
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
                var Producer = await _context.Producers
                    .Where(p => p.Id == id)
                    .FirstOrDefaultAsync(CancellationToken.None);

                if (Producer == null) return OperationResult.NotFound();

                _context.Producers.Remove(Producer);
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
