using System;
using System.Collections.Generic;

#nullable disable

namespace Film_Listing_API
{
    public partial class Movie
    {
               public Movie()
        {
            MovieActor = new HashSet<MovieActor>();
            MovieProducer = new HashSet<MovieProducer>();
        }
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public long Duration { get; set; }
        public string Synopsis { get; set; }
        public string Image { get; set; }

        public ICollection<MovieActor> MovieActor { get; set; }
        public ICollection<MovieProducer> MovieProducer { get; set; }
    }
}
