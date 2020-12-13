
using DataObj;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GPW
{
    
    
    public class ReturnRateCalculator
    {

        #region <nested>

        public class Data
        {
            public CompanyInfo Company { get; set; }
            public List<Quotation> Quotations { get; set; }
            public double YearCoeff { get; set; }
            public double MonthCoeff { get; set; }
            public double WeekCoeff { get; set; }
        }
        
        
        public class Result
        {

            public string Ticket { get; set; }
            public string Name { get; set; }

            public double Week { get; set; }
            public double Month { get; set; }
            public double Year { get; set; }
            public double Average { get; set; }
            
            
        }
        #endregion


        #region <singleton>
        ReturnRateCalculator() { }

        static ReturnRateCalculator _instance;

        public static ReturnRateCalculator Instance
        {
            get
            {
                if (null == _instance) _instance = new ReturnRateCalculator();
                return _instance;
            }
        }
        #endregion

        #region <pub>

        public Result CalculateReturnRates(Data data)
        {

            try
            {


                List<Quotation> ordered = data.Quotations.OrderBy(q => q.Tick).ToList();

                if (ordered.Count < 250) return null;


                Quotation last = ordered.Last();
                DateTime lastDate = MyUtils.GetDateByTick(last.Tick);

                List<Quotation> weeklyQuotations = data.Quotations.GetRangeLast(6);
                List<Quotation> monthlyQuotations = data.Quotations.GetRangeLast(21);
                List<Quotation> yearlyQuotation = data.Quotations.GetRangeLast(250);

                double weeklyRate = calculateRate(weeklyQuotations.Last().Close, weeklyQuotations.First().Close);
                double monthlyRate = calculateRate(monthlyQuotations.Last().Close, monthlyQuotations.First().Close);
                double yearRate = calculateRate(yearlyQuotation.Last().Close, yearlyQuotation.First().Close);

                double averageRate = weeklyRate * data.WeekCoeff + monthlyRate * data.MonthCoeff + yearRate * data.YearCoeff;

                Result result = new Result
                {
                    Week = weeklyRate,
                    Month = monthlyRate,
                    Year = yearRate,
                    Average = averageRate,
                    Ticket = data.Company.Ticket,
                    Name = data.Company.Name
                    
                };

                return result;
            }
            catch (Exception exc)
            {

            }

            return null;
       

        }

        double calculateRate(double last, double first)
        {
            return (last - first) / first;
        }

        #endregion

        #region <prv>
        #endregion			

    }
}
