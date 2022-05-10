using AutoMapper;
using Film_Listing_API.Dtos;

namespace Film_Listing_API.Mapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile() 
        {
            CreateMap<Movie, MovieDto>();
        }
    }
}
