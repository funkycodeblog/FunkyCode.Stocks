using FunkyCode.Stocks.DataUploadService;
using NUnit.Framework;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace FunkyCode.Stocks.UnitTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var dt = @"01-12-2020";

            // https://www.gpw.pl/archiwum-notowan?fetch=1&type=10&instrument=&date={01-12-2020}

            var url = $@"https://www.gpw.pl/archiwum-notowan?fetch=1&type=10&instrument=&date={dt}";

            using (var client = new WebClient())
            {
                client.DownloadFile(url, $"{dt}.csv");
            }
        }

        [Test]
        public void ReaderTest()
        {

            var dateTime = new DateTime(2020,12,1);

            var targetDirectory = @"c:\Data\Projects.Pets\FunkyCode.Stocks\_resx\excel";

            var reader = new XlsDataProvider();
            var downloadService = new GpwHistoricalDataDownloadService();

            var downloadedFilePath = downloadService.Download(dateTime, targetDirectory);


            var table = reader.GetSheetAsTable(downloadedFilePath, "");


        }
    }
}