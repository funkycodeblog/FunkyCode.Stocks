using System.Collections.Generic;
using System.Text;
using DataObj;

namespace FunkyCode.Stocks.DataUploadService.Entities
{
    public class QuotationSet
    {
        public string Ticket { get; set; }
        public TickPeriodType PeriodType { get; set; }
        public List<QuotationSet> Quotations { get; set; } = new List<QuotationSet>();
    }
}
