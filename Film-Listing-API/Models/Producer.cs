﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Film_Listing_API
{
    public partial class Producer
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime FundationDate { get; set; }
        public string Image { get; set; }
    }
}
