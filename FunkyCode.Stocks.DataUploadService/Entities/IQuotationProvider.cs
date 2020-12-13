using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataObj
{
    public interface IQuotationProvider
    {
        List<Quotation> GetQuotations(string companyTicket, TickPeriodType period, DateTime start, DateTime end);
    }


}
