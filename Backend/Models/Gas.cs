using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class Gas
    {
        public Gas()
        {
            BayData = new HashSet<BayData>();
            Popaper = new HashSet<Popaper>();
        }

        public string GasId { get; set; }
        public string GasType { get; set; }

        public virtual ICollection<BayData> BayData { get; set; }
        public virtual ICollection<Popaper> Popaper { get; set; }
    }
}
