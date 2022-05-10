using System;

namespace Film_Listing_API.Dtos
{
    public class UpdateActorDto
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Image { get; set; }
    }
}
