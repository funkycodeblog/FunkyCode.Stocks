using System;
using System.IO;
using System.Net;

namespace FunkyCode.Stocks.DataUploadService
{
    public class GpwHistoricalDataDownloadService : IGpwHistoricalDataDownloadService
    {
        public string Download(DateTime dateTime, string targetDirectory)
        {
            var date = $"{dateTime.Day:00}-{dateTime.Month:00}-{dateTime.Year}";

            // https://www.gpw.pl/archiwum-notowan?fetch=1&type=10&instrument=&date={01-12-2020}

            var filePath = Path.Combine(targetDirectory, $"gpw{date}.xls");

            var url = $@"https://www.gpw.pl/archiwum-notowan?fetch=1&type=10&instrument=&date={date}";

            using var client = new WebClient();
            
            client.DownloadFile(url, filePath);

            return filePath;

        }
    }
}