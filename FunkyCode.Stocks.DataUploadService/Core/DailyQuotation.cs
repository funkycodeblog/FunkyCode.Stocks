using System;

namespace FunkyCode.Stocks.DataUploadService.Entities
{
    public class DailyQuotation
    {
        public string Ticket { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
    }
}