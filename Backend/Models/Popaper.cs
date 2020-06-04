using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class Popaper
    {
        public Popaper()
        {
            BayData = new HashSet<BayData>();
            ExitGateData = new HashSet<ExitGateData>();
            InboundWbdata = new HashSet<InboundWbdata>();
            OutboundWbdata = new HashSet<OutboundWbdata>();
            SaleOfficeData = new HashSet<SaleOfficeData>();
        }

        public string PoNo { get; set; }
        public string PaymentNo { get; set; }
        public DateTime? Date { get; set; }
        public TimeSpan Time { get; set; }
        public int CustomerId { get; set; }
        public string Item { get; set; }
        public string UnitPriceId { get; set; }
        public double Quantity { get; set; }
        public double? Amount { get; set; }
        public string TruckId { get; set; }

        public virtual Gas ItemNavigation { get; set; }
        public virtual CostPriceGas UnitPrice { get; set; }
        public virtual ICollection<BayData> BayData { get; set; }
        public virtual ICollection<ExitGateData> ExitGateData { get; set; }
        public virtual ICollection<InboundWbdata> InboundWbdata { get; set; }
        public virtual ICollection<OutboundWbdata> OutboundWbdata { get; set; }
        public virtual ICollection<SaleOfficeData> SaleOfficeData { get; set; }
    }
}
