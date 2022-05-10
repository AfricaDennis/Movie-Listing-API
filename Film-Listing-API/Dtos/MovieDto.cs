using System;
using System.Collections.Generic;

namespace Film_Listing_API.Dtos
{
    public class MovieDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public long Duration { get; set; }
        public string Synopsis { get; set; }
        public string Image { get; set; }
        public ActorDto Actor { get; set; }
        public IList<Actor> Actors { get; set; }
        public IList<Producer> Producers { get; set; }
    }
}
