using DataObj;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataObj
{
    public class GpwDailyQuotation : Quotation
    {
        public bool IsSensible { get; set; }
        public bool IsSelected { get; set; }

        public string ShortName { get; set; }
        public string ISIN { get; set; }
        public double DailyChangeInPercent { get; set; }
        public int NumberOfTransactions { get; set; }
        public double DailyTurnover { get; set; }
    }
}
