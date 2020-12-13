using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataObj
{
    public abstract class AnalysisElement
    {
        public abstract List<DateTime> GetDates();
        public string Description { get; set; }


    }
}
