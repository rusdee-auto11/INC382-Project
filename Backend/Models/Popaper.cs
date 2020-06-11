using System;
using System.Collections.Generic;

namespace Backend.Models
{
    public partial class Popaper
    {
        public Popaper()
        {
            BayData1 = new HashSet<BayData1>();
            ExitGateData1 = new HashSet<ExitGateData1>();
            InboundWbdata1 = new HashSet<InboundWbdata1>();
            OutboundWbdata1 = new HashSet<OutboundWbdata1>();
            SaleOfficeData1 = new HashSet<SaleOfficeData1>();
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
        public virtual ICollection<BayData1> BayData1 { get; set; }
        public virtual ICollection<ExitGateData1> ExitGateData1 { get; set; }
        public virtual ICollection<InboundWbdata1> InboundWbdata1 { get; set; }
        public virtual ICollection<OutboundWbdata1> OutboundWbdata1 { get; set; }
        public virtual ICollection<SaleOfficeData1> SaleOfficeData1 { get; set; }
    }
}
