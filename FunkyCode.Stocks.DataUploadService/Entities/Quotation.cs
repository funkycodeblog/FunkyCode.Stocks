using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataObj
{
    public class Quotation : AnalysisElement, ICsvSerializable
    {

        public override string ToString()
        {
            return string.Format("{0}; o={1}; c={2}; h={3}; l={4}", DateTime, Open, Close, High, Low);
        }

        const string CONST_CSV_SEPARATOR = ";";

        public TickPeriodType TypePeriod { get; set; }
        public DateTime DateTime { get; set; }
        public string Symbol { get; set; }
        public int Tick { get; set; }
        public double Open { get; set; }
        public double Close { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public int Volume { get; set; }


        public double GetTop ()
        {
            return Math.Max(Open, Close);
        }

        public double GetBottom()
        {
            return Math.Min(Open, Close);
        }




        public double Size
        {
            get
            {
                return Math.Abs(Open - Close);

            }
        }

        public double MidPrice
        {
            get
            {
                return Math.Min(Open, Close) + 0.5 * Size;

            }

        }

        public MovementType TypeOfQuotation
        {

            get
            {
                if (Open < Close) return MovementType.Up;
                else if (Close < Open) return MovementType.Down;
                return MovementType.Unknown;
            }


        }



        #region <ICsvSerializable>
        public string CsvGet()
        {
            string[] values = {Symbol, Tick.ToString(), Open.ToString(), Close.ToString(), High.ToString(), Low.ToString(), Volume.ToString()};
            string line = string.Join<string>(CONST_CSV_SEPARATOR, values.ToArray());
            return line;
        }
      

       

        public void CsvFetch(string value)
        {
            char separator = CONST_CSV_SEPARATOR[0];
            string[] values = value.Split(separator);

            Symbol = values[0];
            Tick = int.Parse(values[1]);
            Open = double.Parse(values[2]);
            Close = double.Parse(values[3]);
            High = double.Parse(values[4]);
            Low = double.Parse(values[5]);
            Volume = int.Parse(values[6]);
        }
#endregion

        public override List<DateTime> GetDates()
        {
            throw new NotImplementedException();
        }
    }
}
