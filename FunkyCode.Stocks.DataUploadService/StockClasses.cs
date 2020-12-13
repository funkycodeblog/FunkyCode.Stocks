using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json.Serialization;
using System.Xml.Linq;
using DataObj;
using GPW;
using Newtonsoft.Json;

namespace FunkyCode.Stocks.DataUploadService
{
    public class RepositoryGPW : IQuotationProvider
    {

        #region <singleton
        RepositoryGPW() { }

        static RepositoryGPW _instance;

        public static RepositoryGPW Instance
        {
            get
            {
                if (null == _instance) _instance = new RepositoryGPW();
                return _instance;
            }
        }
        #endregion



        #region <nested>
        class data : ICsvSerializable
        {

            public int t { get; set; }
            public double o { get; set; }
            public double c { get; set; }
            public double h { get; set; }
            public double l { get; set; }
            public int v { get; set; }

            #region <ICsvSerializable>
            public string CsvGet()
            {
                string line = string.Join(";", t, o, c, h, l, v);
                return line;
            }

            public void CsvFetch(string value)
            {
                //value = value.Replace('.', ',');
                string[] values = value.Split(';');
                t = Convert.ToInt32(values[0]);
                o = Convert.ToDouble(values[1]);
                c = Convert.ToDouble(values[2]);
                h = Convert.ToDouble(values[3]);
                l = Convert.ToDouble(values[4]);
                v = Convert.ToInt32(values[5]);
            }
            #endregion

        }




        class ResponseData
        {

            public string mode { get; set; }
            public string isin { get; set; }
            public string from { get; set; }
            public string to { get; set; }

            public List<data> data { get; set; }


        }
        #endregion

        #region <const>
        string PATH = @"e:\MaciekSoft\Projects\ViewerNew\Data ";
        string[] _wig20 =
   {
    "ACP",
    "ALR",
    "BZW",
    "EUR",
    "JSW",
    "KER",
    "KGH",
    "LPP",
    "LTS",
    "LWB",
    "MBK",
    "OPL",
    "PEO",
    "PGE",
    "PGN",
    "PKN",
    "PKO",
    "PZU",
    "SNS",
    "TPE"};

        string[] _mWig40 = {
    "ALC",
    "AMC",
    "APT",
    "ATT",
    "BDX",
    "BHW",
    "BRS",
    "CAR",
    "CCC",
    "CDR",
    "CIE",
    "CPS",
    "EAT",
    "ECH",
    "EMP",
    "ENA",
    "ENG",
    "FTE",
    "GCH",
    "GNB",
    "GPW",
    "GTC",
    "GTN",
    "HWE",
    "ING",
    "ITG",
    "KRU",
    "KTY",
    "MDS",
    "MIL",
    "NET",
    "NEU",
    "NWG",
    "ORB",
    "PKP",
    "SNK",
    "TRK",
    "TVN",
    "WWL",
    "ZEP"};

        string[] _swig80 = {
    "ABC",
    "ABE",
    "ABS",
    "ACE",
    "ACG",
    "ACT",
    "AGO",
    "AGT",
    "ALI",
    "AML",
    "ASB",
    "AST",
    "ATC",
    "ATM",
    "BFT",
    "BIO",
    "BNP",
    "BOS",
    "BPH",
    "CIG",
    "CMP",
    "CMR",
    "COL",
    "CRM",
    "DBC",
    "DOM",
    "DUD",
    "DUO",
    "EEX",
    "ELB",
    "EMT",
    "FCL",
    "FMF",
    "FRO",
    "GCN",
    "GRI",
    "GRJ",
    "IPX",
    "JWC",
    "KFL",
    "KGN",
    "KPX",
    "KRI",
    "KRS",
    "KSW",
    "LBW",
    "LCC",
    "LTX",
    "MAB",
    "MAG",
    "MCI",
    "MDG",
    "MNC",
    "MON",
    "MSX",
    "MSZ",
    "OPF",
    "PCE",
    "PCM",
    "PEL",
    "PEP",
    "PHN",
    "PND",
    "PUE",
    "QRS",
    "RBW",
    "RDL",
    "RFK",
    "ROB",
    "SGN",
    "SKA",
    "SKT",
    "SMT",
    "STP",
    "STX",
    "VST",
    "WLT",
    "WSE",
    "ZKA",
    "ZMT"
};




#endregion
#region <IQuotationProvider>
        public List<Quotation> GetQuotations(string companyTicket, TickPeriodType period, DateTime start, DateTime end)
{

            List<CompanyInfo> companies = CompanyInfoBuilder.Instance.GetCompanyInfos();
            CompanyInfo company = companies.FirstOrDefault(c => c.Ticket == companyTicket);
            if (null == company) return null;

            ResponseData responseData = getResponseData(company.ISIN, start, end);

            List<Quotation> quotations = new List<Quotation>();
            foreach (data d in responseData.data)
            {
                Quotation q = convertDataToQuotation(d);
                if (q.Open > 0)
                    quotations.Add(q);
            }

            return quotations;


        }
        #endregion

        #region <pub>

        public List<GpwDailyQuotation> GetArchiveQuotations(DateTime dt)
        {

            double COEFF_DAILY_TURNOVER = 1000;

            string PATTERN = "http://www.gpw.pl/notowania_archiwalne_full?type=10&date={DATE}";
            string date = string.Format("{0}-{1:00}-{2:00}", dt.Year, dt.Month, dt.Day);
            string request = PATTERN.Replace("{DATE}", date);
            string response = getResponseAsString(request);

            int tablePos = response.IndexOf("<table class=\"tab03\">");
            int endTablePos = response.IndexOf("</table>");

            if (tablePos == -1 && endTablePos == -1) return null;

            string table = response.Substring(tablePos, endTablePos - tablePos + "</table>".Length);
            table = table.Replace("&nbsp;", "");

            XElement xTable = XElement.Parse(table);
            IEnumerable<XElement> xRows = xTable.Elements("tr");
DateTime limitDateTime = new DateTime(2011, 1, 1);
            double TURNOVER_COEFF_2 = 1.0;
            if (dt < limitDateTime) TURNOVER_COEFF_2 = 0.5;


            List<CompanyInfo> companies = CompanyInfoBuilder.Instance.GetCompanyInfos();
            List<GpwDailyQuotation> items = new List<GpwDailyQuotation>();
            bool isFirst = true;
            foreach (XElement el in xRows)
            {
                if (isFirst)
                {
                    isFirst = false;
                    continue;
                }

                List<XElement> xColums = el.Elements("td").ToList();

                GpwDailyQuotation q = new GpwDailyQuotation();
                q.ShortName = xColums[0].Value;
                q.ISIN = xColums[1].Value;
                q.Open = xColums[3].Value.ToDouble();
                q.High = xColums[4].Value.ToDouble();
                q.Low = xColums[5].Value.ToDouble();
                q.Close = xColums[6].Value.ToDouble();
                q.DailyChangeInPercent = xColums[7].Value.ToDouble();
                q.Volume = xColums[8].Value.ToInt();
                q.NumberOfTransactions = xColums[9].Value.ToInt();
                q.DailyTurnover = xColums[10].Value.ToDouble() * COEFF_DAILY_TURNOVER * TURNOVER_COEFF_2;
q.DateTime = dt;
q.TypePeriod = TickPeriodType.Day;
                q.Tick = MyUtils.GetTickFromDate(q.DateTime);

                CompanyInfo iCompany = companies.FirstOrDefault(c => c.ISIN == q.ISIN);
                if (null != iCompany) q.Symbol = iCompany.Ticket;

                items.Add(q);

            }

            return items;


        }


        public List<string> GetTickets(GpwIndexType typeOfIndex)
        {
            if (typeOfIndex == GpwIndexType.WIG20) return _wig20.ToList();
            else if (typeOfIndex == GpwIndexType.mWIG40) return _mWig40.ToList();
            else if (typeOfIndex == GpwIndexType.sWIG80) return _swig80.ToList();
            return new List<string>();
        }


        public List<CompanyInfo> GetCompanyInfos()
        {
            return CompanyInfoBuilder.Instance.GetCompanyInfos();
        }

        public List<Quotation> GetQuotations(string ticket)
        {
            string fileName = ticket + ".csv";

            string filePath = Path.Combine(PATH, fileName);

            string[] lines = File.ReadAllLines(filePath);

            List<Quotation> collection = new List<Quotation>();
            foreach (string line in lines)
            {

                data iData = new data();
                iData.CsvFetch(line);

                Quotation iQuotation = convertDataToQuotation(iData);
                collection.Add(iQuotation);
}
return collection;
}

        public void CreateDatabase()
        {

            List<CompanyInfo> companies = CompanyInfoBuilder.Instance.GetCompanyInfos();


            DateTime start = new DateTime(1990, 1, 1);
            DateTime end = new DateTime(2016, 1, 1);

            foreach (CompanyInfo cInfo in companies)
            {
                List<Quotation> quotations = GetQuotations(cInfo.Ticket, TickPeriodType.Day, start, end);
                // Database.AddQuotations(cInfo.ISIN, quotations);
            }

        }

        public void CreateDatabaseFromDailyQuotations()
        {

            //DateTime current = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day + 1);
            DateTime current = new DateTime(2014, 6, 11);
            DateTime firstDay = new DateTime(1991, 4, 16);
            //DateTime firstDay = current.AddMonths(-1);

            for (; ; )
            {
                current = current.AddDays(-1);

                if (current.DayOfWeek == DayOfWeek.Saturday || current.DayOfWeek == DayOfWeek.Sunday) continue;

                string dateAsString = current.ToShortDateString();
                string comment = "OK";

                List<GpwDailyQuotation> archiveQuotes = GetArchiveQuotations(current);
                if (null == archiveQuotes || archiveQuotes.Count == 0)
                {
                    comment = "...";
                }
                else
                {
                    // Database.AddQuotations(archiveQuotes);
                }

                string message = string.Format("{0}: {1}", dateAsString, comment);
                Console.WriteLine(message);



                if (current < firstDay) break;

            }



        }


        #endregion

        #region <prv>



        string getResponseAsString(string request)
        {

            WebRequest webRequest = System.Net.WebRequest.Create(request);

            WebResponse response = webRequest.GetResponse();

            Stream responseStream = response.GetResponseStream();

            StreamReader reader = new StreamReader(responseStream);

            string txt = reader.ReadToEnd();

            return txt;



        }


        ResponseData getResponseData(string isin, DateTime from, DateTime to)
        {

            string req = getRequest(isin, from, to);

            string txt = getResponseAsString(req);
string subTxt = txt.Substring(1, txt.Length - 2);

            ResponseData respoData = JsonConvert.DeserializeObject<ResponseData>(subTxt);

            return respoData;
        }

        string getRequest(string isin, DateTime from, DateTime to)
        {
            //string requestPattern = @"http://www.gpw.pl/chart.php?req=O:8:%22stdClass%22:1:{s:1:%220%22;O:8:%22stdClass%22:4:{s:4:%22isin%22;s:12:%22PLKGHM000017%22;s:4:%22mode%22;s:1:%22D%22;s:4:%22from%22;i:385679;s:2:%22to%22;i:394767;}}&timestamp=1421161572208");

            string REQUEST_PATTERN = @"http://www.gpw.pl/chart.php?req=O:8:%22stdClass%22:1:{s:1:%220%22;O:8:%22stdClass%22:4:{s:4:%22isin%22;s:12:%22{ISIN}%22;s:4:%22mode%22;s:1:%22D%22;s:4:%22from%22;i:{FROM};s:2:%22to%22;i:{TO};}}&timestamp=1421161572208";

            int fromInTicks = MyUtils.GetTickFromDate(from);
            int toInTicks = MyUtils.GetTickFromDate(to);

            string request = REQUEST_PATTERN
.Replace("{ISIN}", isin)
                .Replace("{FROM}", fromInTicks.ToString())
                .Replace("{TO}", toInTicks.ToString());

            return request;

        }




        void setDataToFile(CompanyInfo cInfo, ResponseData responseData)
        {

            try
            {

                if (!Directory.Exists(PATH)) Directory.CreateDirectory(PATH);

                string fileName = cInfo.Ticket + ".csv";
                string filePath = Path.Combine(PATH, fileName);

                string[] lines = responseData.data.Select(d => d.CsvGet()).ToArray();
                File.WriteAllLines(filePath, lines);
            }
            catch (Exception exc)
            {

            }

        }

        Quotation convertDataToQuotation(data data)
        {
            DateTime datetime = MyUtils.GetDateByTick(data.t);
            Quotation q = new Quotation
            {
                Open = data.o,
                Close = data.c,
                High = data.h,
                Low = data.l,
                Tick = data.t,
                Volume = data.v,
                TypePeriod = TickPeriodType.Day,
                DateTime = datetime
            };

            return q;

        }
        #endregion








    }
}
