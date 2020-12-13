using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObj.Analysis
{
    public class PriceGapBuilder
    {

        #region <singleton>
        PriceGapBuilder() { }

        static PriceGapBuilder _instance;

        public static PriceGapBuilder Instance
        {
            get
            {
                if (null == _instance) _instance = new PriceGapBuilder();
                return _instance;
            }
        }
        #endregion

        #region <pub>

        public List<PriceGap> GetPriceGaps(List<Quotation> quotations)
        {
            List<PriceGap> gaps = new List<PriceGap>();
            List<Quotation> ordered = quotations.OrderBy(q => q.DateTime).ToList();

            for (int i = 0; i < quotations.Count -1; i++)
            {
                Quotation q = quotations[i];
                Quotation qNext = quotations[i + 1];

                if (PriceGap.IsPriceGap(q, qNext))
                    gaps.Add(new PriceGap(q, qNext));
            }

            return gaps;
        }

        #endregion


    
    }
}
