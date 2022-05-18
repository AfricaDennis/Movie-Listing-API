using AutoMapper;
using Film_Listing_API.Dtos;
using System.Linq;

namespace Film_Listing_API.Mapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile() 
        {
            CreateMap<Movie, MovieDto>()
                .ForMember(m => m.ActorIds, opt => opt.MapFrom(dest => dest.MovieActor
                .Select(m => m.ActorId)))
                .ForMember(m => m.ProducerIds, opt => opt.MapFrom(dest => dest.MovieProducer
                .Select(m => m.ProducerId)));

            CreateMap<Actor, ActorDto>();

            CreateMap<Producer, ProducerDto>();
        }
    }
}
