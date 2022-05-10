using System;

namespace Film_Listing_API.Dtos
{
    public class ProducerDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime FundationDate { get; set; }
        public string Image { get; set; }
    }
}
