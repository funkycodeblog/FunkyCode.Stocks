using FunkyCode.Stocks.DataUploadService;
using NUnit.Framework;
using System;
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

            var url = $@"https://www.gpw.pl/archiwum-notowan?fetch=1&type=10&instrument=&date={dt}";

            using (var client = new WebClient())
            {
                client.DownloadFile(url, $"{dt}.csv");
            }
        }
    }
}