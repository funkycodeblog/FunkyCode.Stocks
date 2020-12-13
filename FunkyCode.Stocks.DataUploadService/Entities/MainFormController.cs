using DataObj;
using GPW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GPWTest.Forms
{
    public class MainFormController
    {

        public List<ReturnRateCalculator.Result> GetResults(List<CompanyInfo> companies)
        {

            List<ReturnRateCalculator.Result> collection = new List<ReturnRateCalculator.Result>();
            foreach (CompanyInfo company in companies)
            {

                List<Quotation> quotations = RepositoryGPW.Instance.GetQuotations(company.Ticket);

                ReturnRateCalculator.Data iData = new ReturnRateCalculator.Data
                {
                    Company = company,
                    Quotations = quotations,
                    WeekCoeff = 0.35,
                    MonthCoeff = 0.35,
                    YearCoeff = 0.3
                };

                ReturnRateCalculator.Result iResult = ReturnRateCalculator.Instance.CalculateReturnRates(iData);
                if (null != iResult)
                    collection.Add(iResult);
            }

            return collection;

        }
    
    
    }
}
