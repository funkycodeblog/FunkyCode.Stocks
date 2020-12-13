using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataObj
{
    public class CompanyInfo
    {
        public string Name { get; set; }
        public string StockExchangeName { get; set; }
        public string Ticket { get; set; }
        public string ISIN { get; set; }

        public override string ToString()
        {
            return Name + '(' + Ticket + ';' + ISIN + ')';
        }
    }

}
