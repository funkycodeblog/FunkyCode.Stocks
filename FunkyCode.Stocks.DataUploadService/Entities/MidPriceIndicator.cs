using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataObj.Indicators
{
    public class PriceIndicator : Indicator
    {



        public override List<DateTime> GetDates()
        {
            return Points.Select(p => p.Date).ToList();
        }
    }
}
