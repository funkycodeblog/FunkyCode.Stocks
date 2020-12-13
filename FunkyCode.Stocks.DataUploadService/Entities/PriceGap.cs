using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataObj
{
    public class PriceGap : AnalysisElement
    {
        #region <static>
        public static bool IsPriceGap(Quotation first, Quotation second)
        {
            double firstTop = first.GetTop();
            double firstBottom = first.GetBottom();

            double secondTop = second.GetTop();
            double secondBottom = second.GetBottom();

            bool isPriceGap = (secondBottom > firstTop) || (secondTop < firstBottom);

            return isPriceGap;
        }
        #endregion

        #region <ctor>
        public PriceGap(Quotation first,  Quotation second)
        {

            First = first;
            Second = second;

            double firstTop = First.GetTop();
            double firstBottom = First.GetBottom();

            double secondTop = Second.GetTop();
            double secondBottom = Second.GetBottom();

            if (secondBottom > firstTop)
            {
                Start = firstTop;
                End = secondBottom;
            }
            else if (secondTop < firstBottom)
            {
                Start = firstBottom;
                End = secondBottom;
            }

        }
        #endregion

        #region <props>
        public Quotation First { get; private set; }
        public Quotation Second { get; private set; }
        public double Start { get; private set; }
        public double End { get; private set; }

        public double Height
        {
            get
            {
                return Math.Abs(Start - End);
            }
        }

        public MovementType TypeOfMovement
        {

            get
            {
                if (Start < End) return MovementType.Up;
                else if (End < Start) return MovementType.Down;
                return MovementType.Unknown;
            }


        }
        #endregion



        public override List<DateTime> GetDates()
        {
            throw new NotImplementedException();
        }
    }
}
