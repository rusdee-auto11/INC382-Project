using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class Gas
    {
        public Gas()
        {
            BayData1 = new HashSet<BayData1>();
            Popaper = new HashSet<Popaper>();
        }

        public string GasId { get; set; }
        public string GasType { get; set; }

        public virtual ICollection<BayData1> BayData1 { get; set; }
        public virtual ICollection<Popaper> Popaper { get; set; }
    }
}
