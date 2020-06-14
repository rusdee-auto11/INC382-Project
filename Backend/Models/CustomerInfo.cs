using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class CustomerInfo
    {
        public string CustomerId { get; set; }
        public string Name { get; set; }
        public string PhoneNo { get; set; }
        public string TaxPayerId { get; set; }
        public string Address { get; set; }
    }
}
