using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObj
{
    public class ConsolidationBuilder
    {

        #region <singleton>
        ConsolidationBuilder() { }

        static ConsolidationBuilder _instance;

        public static ConsolidationBuilder Instance
        {
            get
            {
                if (null == _instance) _instance = new ConsolidationBuilder();
                return _instance;
            }
        }
        #endregion

        #region <pub>
        
        public Consolidation Build(List<Quotation> quotations)
        {
            Consolidation consolidation = new Consolidation();
            consolidation.Start = quotations.OrderBy(q => q.DateTime).First().DateTime;
            consolidation.End = quotations.OrderBy(q => q.DateTime).Last().DateTime;
            consolidation.Min = quotations.Min(q => Math.Min(q.Open, q.Close));
            consolidation.Max = quotations.Max(q => Math.Max(q.Open, q.Close));
            return consolidation;
        }

        
        #endregion

        #region <prv>
        #endregion			

    
    }
}
