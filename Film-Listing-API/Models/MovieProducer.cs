using System;
using System.Collections.Generic;

#nullable disable

namespace Film_Listing_API
{
    public partial class MovieProducer
    {
        public long MovieId { get; set; }
        public long ProducerId { get; set; }

        public virtual Movie Movie { get; set; }
        public virtual Producer Producer { get; set; }
    }
}
