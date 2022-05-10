using System;
using System.Collections.Generic;

#nullable disable

namespace Film_Listing_API
{
    public partial class MovieActor
    {
        public long MovieId { get; set; }
        public long ActorId { get; set; }

        public virtual Actor Actor { get; set; }
        public virtual Movie Movie { get; set; }
    }
}
