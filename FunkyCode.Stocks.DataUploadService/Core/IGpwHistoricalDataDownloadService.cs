using System;
using System.Collections.Generic;
using System.Text;
using FunkyCode.Stocks.DataUploadService.Entities;

namespace FunkyCode.Stocks.DataUploadService
{
    public interface IGpwHistoricalDataDownloadService
    {
        string Download(DateTime dateTime, string targetDirectory);
    }

    public interface IGwpQuotationProvider
    {
        List<DailyQuotation> GetQuotations(DateTime date);
    }


    public interface IConfig
    {
        string TargetDirectory { get; set; }
    }


    public class GwpQuotationProvider : IGwpQuotationProvider
    {
        private readonly IGpwHistoricalDataDownloadService _downloadService;
        private readonly IXlsDataProvider _xlsDataProvider;
        private readonly IConfig _config;

        public GwpQuotationProvider(
            IGpwHistoricalDataDownloadService downloadService, 
            IXlsDataProvider xlsDataProvider,
            IConfig config
            
            )
        {
            _downloadService = downloadService;
            _xlsDataProvider = xlsDataProvider;
            _config = config;
        }

        public List<DailyQuotation> GetQuotations(DateTime date)
        {
            var downloadedFilePath = _downloadService.Download(date, _config.TargetDirectory);
            
            var table = _xlsDataProvider.GetSheetAsTable(downloadedFilePath, "");

            var rows = table.GetLength(0);

            var quotations = new List<DailyQuotation>();

            for (var r = 1; r < rows; r++)
            {
                var ticket = (string)table[r, 1];
                var amount = (decimal)table[r, 7];

                var quotation = new DailyQuotation
                {
                    Ticket = ticket,
                    Date = date,
                    Amount = amount,
                };

                quotations.Add(quotation);
            }

            return quotations;

        }
    }

}
