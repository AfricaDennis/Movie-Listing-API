using AutoMapper;
using Film_Listing_API.Dtos;

namespace Film_Listing_API.Mapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile() 
        {
            CreateMap<Movie, MovieDto>()
                .ForMember(m => m.Actors, opt => opt.Ignore())
                .ForMember(m => m.Producers, opt => opt.Ignore());

            CreateMap<Actor, ActorDto>();

            CreateMap<Producer, ProducerDto>();
        }
    }
}
