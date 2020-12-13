using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DataObj
{
    public class QuotationBuilder
    {
        #region <singleton>
        QuotationBuilder() { }

        static QuotationBuilder _instance;

        public static QuotationBuilder Instance
        {
            get
            {
                if (null == _instance) _instance = new QuotationBuilder();
                return _instance;
            }
        }
        #endregion

        #region <pub>

        // 2014.12.10,06:15,1.23919,1.23934,1.23888,1.23913,570
        public List<Quotation> GetQuotationFromMT4File(string filePath)
        {
            if (null == filePath) return new List<Quotation>();

            List<Quotation> collection = new List<Quotation>();
            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                if (string.IsNullOrEmpty(line)) continue;

                string[] items = line.Split(',');

              
                
                DateTime dateTime = getDateTimeFromItems(items[0], items[1]);
                //items = items.Select(i => i.Replace('.', ',')).ToArray();

                double open = Convert.ToDouble(items[2]);
                double high = Convert.ToDouble(items[3]);
                double low = Convert.ToDouble(items[4]);
                double close = Convert.ToDouble(items[5]);
               
                

                int volume = Convert.ToInt32(items[6]);

                Quotation iQuotation = new Quotation
                {
                    DateTime = dateTime,
                    Open = open,
                    Close = close,
                    High = high,
                    Low = low,
                    Volume = volume

                };

                collection.Add(iQuotation);
            }


            TickPeriodType tickPeriod = getTickTypeFromQuotations(collection[0], collection[1]);
            foreach (Quotation q in collection)
                q.TypePeriod = tickPeriod;


            return collection;




        }


        public List<Quotation> GetQuotationFromCSVFile(string path)
        {


            string[] lines = File.ReadAllLines(path);
            List<Quotation> collection = new List<Quotation>();
            foreach (string line in lines)
            {
                Quotation quotation = new Quotation();
                quotation.CsvFetch(line);
                collection.Add(quotation);
            }

            return collection;

        }
        
        #endregion

        #region <prv>
        DateTime getDateTimeFromItems(string date, string time)
        {
            string[] dateItems = date.Split('.');
            string[] timeItems = time.Split(':');

            int year = Convert.ToInt32(dateItems[0]);
            int month = Convert.ToInt32(dateItems[1]);
            int day = Convert.ToInt32(dateItems[2]);

            int hour = Convert.ToInt32(timeItems[0]);
            int minute = Convert.ToInt32(timeItems[1]);

            DateTime dt = new DateTime(year, month, day, hour, minute, 0);
            return dt;

        }

        TickPeriodType getTickTypeFromQuotations(Quotation q1, Quotation q2)
        {

            TimeSpan ts = q2.DateTime - q1.DateTime;
            int inMinutes = ts.Minutes;

            if (inMinutes == 1) return TickPeriodType.Minute_1;
            else if (inMinutes == 5) return TickPeriodType.Minute_5;
            else if (inMinutes == 15) return TickPeriodType.Minute_15;
            else if (inMinutes == 60) return TickPeriodType.Hour_1;
            else if (inMinutes == 240) return TickPeriodType.Hour_4;
            else if (inMinutes == 1440) return TickPeriodType.Day;
            else if (inMinutes == 10080) return TickPeriodType.Week;

            return TickPeriodType.Unknown;

        }
        #endregion			

    
    }
}
