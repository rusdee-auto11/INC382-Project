using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class CostPriceGas
    {
        public CostPriceGas()
        {
            Popaper = new HashSet<Popaper>();
        }

        public string GasPriceId { get; set; }
        public DateTime Date { get; set; }
        public string GasId { get; set; }
        public double Cost { get; set; }
        public double Price { get; set; }

        public virtual ICollection<Popaper> Popaper { get; set; }
    }
}
