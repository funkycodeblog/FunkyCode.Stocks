using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataObj
{
    public class IndicatorPoint
    {
        public DateTime Date { get; set; }
        public double Value { get; set; }


        public override string ToString()
        {
           return  string.Format("{0} : {1}", Date, Value);
        }
    }
}
