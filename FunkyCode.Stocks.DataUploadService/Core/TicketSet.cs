using System.Collections.Generic;

namespace FunkyCode.Stocks.DataUploadService.Entities
{
    public class TicketSet
    {
        public Dictionary<string, QuotationSet> Set { get; set; } = new Dictionary<string, QuotationSet>();
    }
}