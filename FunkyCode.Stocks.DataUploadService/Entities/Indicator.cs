using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataObj
{
    public abstract class Indicator : AnalysisElement
    {
        public Indicator()
        {
            Points = new List<IndicatorPoint>();
        }

        public IndicatorType TypeOfIndicator { get; set; }
        public List<IndicatorPoint> Points { get; set; }
    
    }
}
