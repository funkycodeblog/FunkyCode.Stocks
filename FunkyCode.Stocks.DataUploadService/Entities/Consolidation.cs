using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObj
{
    public class Consolidation : AnalysisElement
    {

        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public double Max { get; set; }
        public double Min { get; set; }

        public double Size 
        {   
            get
            {
                return Math.Abs(Max - Min);

            }
        }


        public override List<DateTime> GetDates()
        {
            throw new NotImplementedException();
        }
    }
}
