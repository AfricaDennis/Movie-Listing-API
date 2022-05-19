using System;
using System.Collections.Generic;

#nullable disable

namespace Film_Listing_API
{

    public partial class Actor
    {
        public Actor()
        {
            MovieActor = new HashSet<MovieActor>();
        }
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string Image { get; set; }
        public ICollection<MovieActor> MovieActor { get; set; }

    }
}
