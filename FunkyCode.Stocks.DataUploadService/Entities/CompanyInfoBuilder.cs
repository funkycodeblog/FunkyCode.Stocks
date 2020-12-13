using DataObj;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace GPW
{
    public class CompanyInfoBuilder
    {

        #region <singleton>
        CompanyInfoBuilder() { }

        static CompanyInfoBuilder _instance;

        public static CompanyInfoBuilder Instance
        {
            get
            {
                if (null == _instance) _instance = new CompanyInfoBuilder();
                return _instance;
            }
        }
        #endregion

        #region <pub>

        public List<CompanyInfo> GetCompanyInfos()
        {

            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string fileName = path + @"\Data\Lista_Spolek_GPW.csv";
            string[] lines = File.ReadAllLines(fileName);

            List<CompanyInfo> collection = new List<CompanyInfo>();
            foreach (string line in lines)
            {
                string[] values = line.Split(';');
                CompanyInfo iInfo = new CompanyInfo()
                {
                    Name = getTrimmed(values[0]),
                    StockExchangeName = getTrimmed(values[1]),
                    Ticket = getTrimmed(values[2]),
                    ISIN = getTrimmed(values[3])
                };

                collection.Add(iInfo);
            }

            return collection;


        }

        #endregion

        #region <prv>

        string getTrimmed(string value)
        {
            if (string.IsNullOrEmpty(value)) return value;
            else return value.Trim();

        }
        #endregion


    }
}
